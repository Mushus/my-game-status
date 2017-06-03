using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGameStatus
{
    public partial class Form1 : Form
    {
        private Process hProcess = null;
        private string processName = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 自身のプロセスを取得する
            hProcess = Process.GetCurrentProcess();
            processName = hProcess.ProcessName;
            //Visible = false;
            // プロセス名を設定
            statusToolStripMenuItem.Text = processName;
            Text = processName;
            label.Text = processName;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void contextMenuStrip_Opened(object sender, EventArgs e)
        {
            // 一覧を作る
            string dir = System.Environment.CurrentDirectory;
            string[] files = Directory.GetFiles(dir);

            ToolStripDropDownMenu statuses = new ToolStripDropDownMenu();

            foreach (string file in files)
            {
                if (Path.GetExtension(file) != ".exe")
                {
                    continue;
                }

                string name = Path.GetFileNameWithoutExtension(file);
                bool isMyProcess = (processName == name);

                // items
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = name;
                item.Checked = isMyProcess;
                if (!isMyProcess)
                {
                    item.Click += new System.EventHandler(selectStatus);
                }

                statuses.Items.Add(item);
            }

            statusToolStripMenuItem.DropDown = statuses;
        }

        private void selectStatus(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            string fileName = item.Text;

            string dir = System.Environment.CurrentDirectory;
            string exePath = dir + @"\" + fileName + ".exe";

            Process p = Process.Start(exePath);

            this.Close();
        }

    }
}
