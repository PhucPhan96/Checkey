using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;

namespace CheckKey
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            label1.Text = GetMain();
            label2.Text = GetCPU();
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.ShowDialog();
        }

        private void btnTrial_Click(object sender, EventArgs e)
        {
            frmTrial tr = new frmTrial();
            tr.ShowDialog();
        }

        string GetMain()
        {
            string main = "";
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
            foreach (ManagementObject getserial in MOS.Get())
            {
                main = getserial["SerialNumber"].ToString();
            }
            return main;
        }

        string GetCPU()
        {
            string main = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_processor");
            foreach (ManagementObject getserial in searcher.Get())
            {
                main = getserial["ProcessorID"].ToString();
            }
            return main;
        }
    }
}
