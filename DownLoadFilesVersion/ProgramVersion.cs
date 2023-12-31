﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.Specialized;
using System.Threading;


namespace DownLoadFilesVersion
{
    public class ProgramVersion
    {
        public event EventHandler<ProgressChangedArgs> ProgressChanged;
        //public string idCurrentVersion;
        //public string idNewVresion;
        //public string rootFolder;

        public ProgramVersion()
        {
            
        }

        protected void OnProgressChanged(ProgressChangedArgs e)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, e);
            }
        }

        public void UpDate()
        {
            string idNewVresion = ProgramSettings.GetSetting("NeedCurrentVersion");
            if (idNewVresion == null)
            {
                return;
            }
            else if (idNewVresion == "last")
            {
                var buildVersions = GetBuildVersions();
                idNewVresion = buildVersions.versions.Last();
            }
            

            var changeVersionInfoFiles = GetListNameUpdataFiles(GetIdCurrentVersion(), idNewVresion);
            DownLoadNewVersion(changeVersionInfoFiles);
            SetIdCurrentVersion(idNewVresion);
            
        }

        public BuildVersions GetBuildVersions()
        {
            string ip = ProgramSettings.GetSetting("ip");
            string URL = "http://" + ip + ":8080/simple/simple2/getAllNameVersion";

            string DATA = @"{""object"":{""name"":""Name""}}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = DATA.Length;
            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            requestWriter.Write(DATA);
            requestWriter.Close();


            BuildVersions buildVersions = null;
            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                buildVersions = Serialize.Deserialize(response);
                
                Console.Out.WriteLine(response);
                responseReader.Close();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }
            return buildVersions;
        }

        public string GetIdCurrentVersion()
        {

            string rootFolder = ProgramSettings.GetSetting("folderWithCurrentVersion");
            string IdCurrentVersion = "0";
            string path = "CurrentVersion.txt";
            try
            {
                using (StreamReader reader = new StreamReader(Path.Combine(rootFolder, path)))
                {
                    IdCurrentVersion = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                using (StreamWriter writer = new StreamWriter(Path.Combine(rootFolder, path), false))
                {
                    writer.Write(IdCurrentVersion);
                }
            }
            

            return IdCurrentVersion;
        }

        public void SetIdCurrentVersion(string idNew)
        {
            string rootFolder = ProgramSettings.GetSetting("folderWithCurrentVersion");
            string path = "CurrentVersion.txt";
            using (StreamWriter writer = new StreamWriter(Path.Combine(rootFolder, path), false))
            {
                writer.Write(idNew);
            }
        }

        public ChangeVersionInfoFiles GetListNameUpdataFiles(string idCurrentVersion, string idNewVresion)
        {
            string ip = ProgramSettings.GetSetting("ip");
            string URL = "http://" + ip + ":8080/simple/simple2/getListNameUpdataFiles";

            ChangeVersion changeVersion = new ChangeVersion(idCurrentVersion, idNewVresion);

            string DATA = Serialize.SerializeChangeVersion(changeVersion);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = DATA.Length;
            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            requestWriter.Write(DATA);
            requestWriter.Close();
            OnProgressChanged(new ProgressChangedArgs(0.ToString(), "Получение списка файлов на обглвлене"));

            ChangeVersionInfoFiles changeVersionInfoFiles = null;
            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                Console.Out.WriteLine(response);
                //System.Windows.Forms.MessageBox.Show(response);

                changeVersionInfoFiles = Serialize.DeserializeListInfoFile(response);
                

               
                responseReader.Close();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }
            return changeVersionInfoFiles;
        }

        public string GetRootFolder(string idRootForlder)
        {
            int i = -1;
            try
            {
                try
                {
                    i = Convert.ToInt32(idRootForlder);
                }
                catch(Exception e)
                {
                    //System.Windows.Forms.MessageBox.Show(" Проблемы с билдом " + i++);
                }

                try
                {
                    string[] array = ProgramSettings.GetSetting("rootFolders").Split(';');
                    return array[i];
                }
                catch (Exception)
                {
                    /*
                    System.Windows.Forms.MessageBox.Show(
                        "В файле настроек недостает целевых адресов в свойстве rootFolders. Минимум: " + i++);*/
                }
                
                
            }
            catch (Exception e)
            {

            }
            

             
            return null;
        }

        public void DownLoadNewVersion(ChangeVersionInfoFiles changeVersionInfoFiles)
        {
            
            foreach (var file in changeVersionInfoFiles.filesDelete)
            {
                string rootFolder = GetRootFolder(file.id_target_path);
                if (rootFolder == null) continue;


                string path = rootFolder + file.path;
                if (File.Exists(path))
                {
                    // If file found, delete it
                    File.Delete(path);
                    Console.WriteLine("File deleted." + file.path);
                }
            }

            int prosent = 0;
            int k = 0;
            foreach (var file in changeVersionInfoFiles.filesAdd)
            {
                
                prosent = Convert.ToInt32((k * 100) / changeVersionInfoFiles.filesAdd.Length) ;
                OnProgressChanged(new ProgressChangedArgs(prosent.ToString(), file.path));

                k++;
                var arrayByte = GetFileFromServer(file.idFile);
                Console.WriteLine("GetFileFromServer." + file.path);
                string path = file.path;
                string[] names = path.Split('/');
                string rootFolder = GetRootFolder(file.id_target_path);
                if (rootFolder == null) continue;
                string current = rootFolder;
                for (int i = 1; i < names.Length; i++)
                {
                    if (i == names.Length - 1)
                    {
                        current = Path.Combine(current, names[i]);
                        if (File.Exists(current))
                        {
                            // If file found, delete it
                            File.Delete(current);
                            Console.WriteLine("File deleted." + file.path);
                        }
                        var writer = new BinaryWriter(File.OpenWrite(current));
                        writer.Write(arrayByte);
                        writer.Close();
                    }
                    else
                    {
                        current = Path.Combine(current, names[i]);
                        Directory.CreateDirectory(current);
                    }
                    
                }
                prosent = Convert.ToInt32((k * 100) / changeVersionInfoFiles.filesAdd.Length);
                OnProgressChanged(new ProgressChangedArgs(prosent.ToString(), file.path));
            }
            

        }

        public byte[] GetFileFromServer(int id)
        {
            try
            {
                string ip = ProgramSettings.GetSetting("ip");
                string URL = "http://" + ip + ":8080/simple/ControllerMultipart2/" + id.ToString() + "/v12313424";
                var httpClient = new HttpClient();
                var arrayBytes = httpClient.GetByteArrayAsync(URL);
                return  arrayBytes.Result;
                
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }

            return null;
        }


    }
}
