using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using DownLoadFilesVersion;

namespace DownLoadFilesVersion
{
    public partial class Form1 : Form
    {

        private ProgramVersion programaVersion = new ProgramVersion();
        private ChangeVersionInfoFiles changeVersionInfoFiles = null;
        private string rootFolder = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var buildVersions = programaVersion.GetBuildVersions();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(buildVersions.versions);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Class2.CreateObject(textBox2);
        }



        public class Class2
        {
            private const string URL = "http://localhost:8080/simple/ControllerMultipart/tets1234213/v12313424";
            //private const string DATA = @"{""object"":{""name"":""Name""}}";


            public static void CreateObject(TextBox textBox)
            {
                try
                {
                    var httpClient = new HttpClient();
                    var imageBytes = httpClient.GetByteArrayAsync(URL);
                    textBox.Text = imageBytes.Result.Length.ToString();
                    var writer = new BinaryWriter(File.OpenWrite("Руководство TERMOCLIP TOOLS.pdf"));
                    writer.Write(imageBytes.Result);
                    writer.Close();
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine("-----------------");
                    Console.Out.WriteLine(e.Message);
                }

            }

           

        }

        private void button3_Click(object sender, EventArgs e)
        {
            changeVersionInfoFiles = null;
            changeVersionInfoFiles = programaVersion.GetListNameUpdataFiles(programaVersion.GetIdCurrentVersion(rootFolder), comboBox1.SelectedItem.ToString());
            textBox1.Text = changeVersionInfoFiles.filesDelete.Length.ToString();
            textBox2.Text = changeVersionInfoFiles.filesAdd.Length.ToString();
            programaVersion.DownLoadNewVersion(changeVersionInfoFiles, rootFolder);
            programaVersion.SetIdCurrentVersion(comboBox1.SelectedItem.ToString(), rootFolder);
            textBox4.Text = programaVersion.GetIdCurrentVersion(rootFolder);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //label3.Text =  programaVersion.GetSetting();

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog2 = new FolderBrowserDialog();
            folderBrowserDialog2.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            DialogResult result = folderBrowserDialog2.ShowDialog();

            if (result == DialogResult.OK)
            {
                rootFolder = folderBrowserDialog2.SelectedPath;

                textBox3.Text = folderBrowserDialog2.SelectedPath;

                textBox4.Text = programaVersion.GetIdCurrentVersion(rootFolder);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            PromgramWork.UpdateFiles(programaVersion.GetIdCurrentVersion(rootFolder), comboBox1.SelectedItem.ToString(), rootFolder);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PromgramWork.AutoUpdateFiles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog2 = new FolderBrowserDialog();
            folderBrowserDialog2.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            DialogResult result = folderBrowserDialog2.ShowDialog();

            if (result == DialogResult.OK)
            {
                listBox1.Items.Add(folderBrowserDialog2.SelectedPath);

                
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (var str in listBox1.Items)
            {
                list.Add(str.ToString().Replace("\\", "/"));
            }

            CreateBuild.Create(list.ToArray());
        }
    }
}
