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
using DownLoadFilesVersion.Data;
using DownloadVersion2;

namespace DownLoadFilesVersion
{
    public partial class Form1 : Form
    {

        private ProgramVersion programaVersion = new ProgramVersion();
        private ChangeVersionInfoFiles changeVersionInfoFiles = null;
        private string rootFolder = "";
        private List<RequestValueFolder> requestFolders = new List<RequestValueFolder>();

        public Form1()
        {
            InitializeComponent();
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
                FormSelectNumberFolder formSelectNumberFolder = new FormSelectNumberFolder();
                formSelectNumberFolder.ShowDialog();
                if (formSelectNumberFolder.NumberOfFolders != -1)
                {
                    string pathValue = folderBrowserDialog2.SelectedPath.Replace("\\", "/");
                    requestFolders.Add(new RequestValueFolder(pathValue, formSelectNumberFolder.NumberOfFolders.ToString()));
                    listBox1.Items.Add("number target folder: " + formSelectNumberFolder.NumberOfFolders.ToString() 
                                                                + " value folder:  " + pathValue);
                }
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            CreateBuild.Create(requestFolders);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog2.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                FormSelectNumberFolder formSelectNumberFolder = new FormSelectNumberFolder();
                formSelectNumberFolder.ShowDialog();
                if (formSelectNumberFolder.NumberOfFolders != -1)
                {
                    string pathValue = openFileDialog2.SafeFileName.Replace("\\", "/");
                    requestFolders.Add(new RequestValueFolder(pathValue, formSelectNumberFolder.NumberOfFolders.ToString()));
                    listBox1.Items.Add("number target folder: " + formSelectNumberFolder.NumberOfFolders.ToString()
                                                                + " value file:  " + pathValue);
                }
            }


        }
    }
}
