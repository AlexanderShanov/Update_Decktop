using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DownLoadFilesVersion.Data;

namespace DownLoadFilesVersion
{
    public class CreateBuild
    {
        public static void Create(string[] folders)
        {
            RequestValue requestValue = new RequestValue();
            List<RequestValueFolder> list = new List<RequestValueFolder>();
            foreach (var folder in folders)
            {
                list.Add(new RequestValueFolder(folder, ""));
            }
            requestValue.myArray = list.ToArray();



            string ip = ProgramSettings.GetSetting("ip");
            string URL = "http://" + ip + ":8080/simple/simple2/CreateNewBuildFromDecktopRequest";

            //string DATA = @"{""object"":{""name"":""Name""}}";
            string DATA = Serialize.SerializeCreateRequest(requestValue);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/json;charset=utf-8";
            //byte[] array = Encoding.UTF8.GetBytes(DATA);
            //request.ContentLength = DATA.Length;
            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());//, System.Text.Encoding.UTF8
            requestWriter.Write(DATA);
            requestWriter.Close();


            BuildVersions buildVersions = null;
            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                string response = responseReader.ReadToEnd();
                //buildVersions = Serialize.Deserialize(response);

                Console.Out.WriteLine(response);
                responseReader.Close();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(e.Message);
            }
            return ;

        }
    }
}
