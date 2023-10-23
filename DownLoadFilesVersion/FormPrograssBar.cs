using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownLoadFilesVersion
{
    public partial class FormPrograssBar : Form
    {
        public FormPrograssBar()
        {
            InitializeComponent();
        }

        private void FormPrograssBar_Load(object sender, EventArgs e)
        {

        }

        public void OnWorkerProgressChanged(object sender, ProgressChangedArgs e)
        {
            // Cross thread - so you don't get the cross-threading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    OnWorkerProgressChanged(sender, e);
                });
                return;
            }

            // Change control
            this.label1.Text = "Progress Changed i: " + e.Progress;
            this.progressBar1.Value = Convert.ToInt32(e.Progress);
            
            this.label2.Text = e.Message.ToString();

            listBox1.Items.Add(e.Message.ToString());
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            if (e.Progress == "100")
            {
                Close();
            }
        }
    }

    
}
