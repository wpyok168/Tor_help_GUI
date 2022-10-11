using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Tor便携启动辅助器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Text = "Tor便携启动辅助器 " + Application.ProductVersion;
            this.Text = "Tor便携启动辅助器 V1.3";
            toolStripStatusLabel1.Text = "Tor版本: " + gettorvol();

            Process[] ps = Process.GetProcessesByName("tor");
            if (ps.Length > 0)
            {
                this.button1.Text = "停 止 Tor";
            }
            //foreach (Process item in ps)
            //{
            //    if (item.ProcessName.Equals("tor"))
            //    {
            //        this.button1.Text = "停 止 Tor";
            //        //item.Kill();
            //    }
            //}



        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //Process[] ps = Process.GetProcessesByName("tor");
            //if (ps.Length>0)
            //{
            //    MessageBox.Show("tor便携版已启动，无需启动");
            //}
            //else
            //{
            //    //Process.Start("tor.exe", "-f torrc");
            //    this.runtor();
            //}
            if (this.button1.Text.Equals("启  动  Tor"))
            {
                this.stoptor();
                this.runtor();
                this.button1.Text = "停  止  Tor";
            }
            else
            {
                
                this.stoptor();
                this.button1.Text = "启  动  Tor";
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.stoptor();
        }

        private void runtor()
        {
            Process.Start("tor.exe", "-f torrc");
            //Thread.Sleep(1000);
            toolStripStatusLabel1.Text = "Tor版本: " + gettorvol();
        }

        private void stoptor()
        {
            //runcmd("taskkill /IM tor.exe /F");
            //runcmd("taskkill /IM obfsproxy.exe /F");
            //runcmd("taskkill /IM obfs4proxy.exe /F");
            //runcmd("taskkill /IM fteproxy.exe /F");
            //runcmd("taskkill /IM terminateprocess-buffer.exe /F");
            //runcmd("taskkill /IM meek-client-torbrowser.exe /F");
            //runcmd("taskkill /IM meek-client.exe /F");
            killprocess("tor");
            killprocess("obfsproxy");
            killprocess("obfs4proxy");
            killprocess("fteproxy");
            killprocess("terminateprocess-buffer");
            killprocess("meek-client-torbrowser");
            killprocess("meek-client");
            //runcmd("cd.>Data\tor.log");
            Thread.Sleep(1000);
            string path = System.Environment.CurrentDirectory + "\\Data\\";
            FileStream stream = new FileStream(path + "tor.log", FileMode.Create);
            stream.Close();
            
        }

        private void killprocess(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);
            if (processes.Length > 0)
            {
                foreach (Process item in processes)
                {
                    if (item.ProcessName.Equals(name))
                    {
                        item.Kill();
                    }
                }
            }
        }
        private string runcmd(string cmdsql)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            p.StandardInput.WriteLine(cmdsql + "&exit");
            p.StandardInput.AutoFlush = true;
            string outstr = p.StandardOutput.ReadLine();
            p.WaitForExit();
            p.Close();
            return outstr;
        }

        private void 线路切换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.stoptor();
            string path = System.Environment.CurrentDirectory+ "\\Data\\";
            DirectoryInfo dirinfo = new DirectoryInfo(path);
            FileSystemInfo[] files = dirinfo.GetFileSystemInfos();
            if (files.Count()>0)
            {
                //MessageBox.Show(files.Count().ToString());
                foreach (FileSystemInfo item in files)
                {
                    if (item is DirectoryInfo)
                    {
                        //this.runcmd("rd /s /q " + item.FullName);
                        Directory.Delete(item.FullName);
                    }
                    else if (item.Name.Equals("geoip"))
                    {
                        
                    }
                    else if (item.Name.Equals("geoip6"))
                    {
                        
                    }
                    else
                    {
                        File.Delete(item.FullName);
                    }
                }
            }
            this.runtor();
            this.button1.Text = "停  止  Tor";
        }

        private void 全局http1270019050ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            RegistryKey reg = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            RegistryKey reg1 = reg.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings",true);
            string proxyenable = reg1.GetValue("ProxyEnable").ToString();
            //MessageBox.Show(proxyenable);
            if (proxyenable.Equals("0"))
            {
                reg1.SetValue("ProxyEnable", 1);
                reg1.SetValue("ProxyServer", "127.0.0.1:9150");
            }
            else
            {
                reg1.SetValue("ProxyServer", "127.0.0.1:9150");
            }
            reg1.Close();
            reg.Close();

        }

        private void 取消全局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings",true);
            reg.SetValue("ProxyEnable", 0);

        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Visible = true;
            this.Show();
        }

        private void 隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            this.Hide();
            this.notifyIcon1.Visible = true;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process[] ps = Process.GetProcessesByName("tor");
            if (ps.Length > 0)
            {
                this.stoptor();
            }
            this.formclose = true;
            this.Close();
        }

        private bool formclose = false;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formclose)
            {
                e.Cancel = false;  
                return;
            }
            e.Cancel = true;
            this.Hide();
            this.notifyIcon1.Visible = true;
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            Process.Start("https://black1ce.com/");
        }

        private string gettorvol()
        {
            Thread.Sleep(2000);
            string path = System.Environment.CurrentDirectory + "\\Data\\";
            string outstr = string.Empty;
            if (File.Exists(path+ "tor.log"))
            {
                
                File.Copy(path + "tor.log", path + "tor.bak",true);
                using (StreamReader sr = new StreamReader(path + "tor.bak"))
                {
                    string str = sr.ReadLine();
                    if (str!=null)
                    {
                        str = str.Substring(str.IndexOf("[notice] Tor ") + "[notice] Tor ".Length, str.IndexOf(" (git") + 1 - (str.IndexOf("[notice] Tor ") + "[notice] Tor ".Length));
                        outstr = str;
                        
                    }
                    //while (str != null)
                    //{
                    //    str += r.ReadLine();
                    //}
                    sr.Close();
                }
                File.Delete(path + "tor.bak");
                
                //toolStripStatusLabel2.Text = str;
                //if (sr.Read() > 0)
                //{

                //}
            }
            
            return outstr;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void toolStripStatusLabel4_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("设置");
            Form2 form2 = new Form2();
            form2.ShowDialog();
            
        }
    }
}
