using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DownLoadFilesVersion;

namespace DownloadVersion2
{
    public partial class FormSelectNumberFolder : Form
    {
        public int NumberOfFolders { get; set; }
        public FormSelectNumberFolder()
        {
            InitializeComponent();
        }

        private void FormSelectNumberFolder_Load(object sender, EventArgs e)
        {
            
            comboBox1.Items.Clear();
            comboBox1.Items.Add("rootfolder1");
            comboBox1.Items.Add("rootfolder2");
            comboBox1.Items.Add("rootfolder3");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NumberOfFolders = comboBox1.SelectedIndex;
            Close(); }
    }
}
