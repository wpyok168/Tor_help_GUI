using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tor便携启动辅助器
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        

        private void Form2_Load(object sender, EventArgs e)
        {
            //隐藏tabPage1
            //tabPage1.Parent=null;
            string path = System.Environment.CurrentDirectory + "\\";
            if (File.Exists(path+ "torrc"))
            {
                readtorrc();
            }
            else
            {
                FileStream fs = new FileStream(path + "torrc", FileMode.Create);
                fs.Close();
            }
            
            //groupBox1.Enabled = false;
            //groupBox2.Enabled = false;
            //groupBox3.Enabled = false;
            
        }

        
        private void readtorrc()
        {
            string path = System.Environment.CurrentDirectory + "\\";
            using (StreamReader sr = new StreamReader(path + "torrc"))
            {
                string str = sr.ReadLine();
                string[] socks5 = new string[3];
                string[] https = new string[3];
                string[] bridge = new string[2];
                List<string> bridges = new List<string>();

                while (str != null)
                {
                    if (str.IndexOf("Socks5Proxy ") >= 0 && !str.Contains("#"))
                    {
                        //radioButton4.Checked = true;
                        //return;
                        socks5[0] = str.Substring("Socks5Proxy ".Length);
                    }
                    if (str.IndexOf("Socks5ProxyUsername ") >= 0 && !str.Contains("#"))
                    {
                        socks5[1] = str.Substring("Socks5ProxyUsername ".Length);
                    }
                    if (str.IndexOf("Socks5ProxyPassword ") >= 0 && !str.Contains("#"))
                    {
                        socks5[2] = str.Substring("Socks5ProxyPassword ".Length);
                    }
                    if (str.IndexOf("Socks4Proxy ") >= 0 && !str.Contains("#"))
                    {
                        radioButton3.Checked = true;
                        textBox4.Text = str.Substring("Socks4Proxy ".Length);
                        //return;
                    }
                    if (str.IndexOf("HTTPSProxy ") >= 0 && !str.Contains("#"))
                    {
                        //radioButton2.Checked = true;
                        //return;
                        https[0] = str.Substring("HTTPSProxy ".Length); ;
                    }
                    if (str.IndexOf("HTTPSProxyAuthenticator ") >= 0 && !str.Contains("#"))
                    {
                        https[1] = str.Substring("HTTPSProxyAuthenticator ".Length, str.IndexOf(":") - "HTTPSProxyAuthenticator ".Length);
                        https[2] = str.Substring(str.IndexOf(":") + 1);
                    }
                    if (str.IndexOf("UseBridges ") >= 0 && !str.Contains("#") && str.Contains("1"))
                    {
                        bridge[0] = "true";
                    }
                    if (str.IndexOf("obfs4proxy") >= 0 && !str.Contains("#"))
                    {
                        //radioButton6.Checked = true;
                        bridge[1] = "obfs4proxy";

                    }
                    if (str.IndexOf("fteproxy") >= 0 && !str.Contains("#"))
                    {
                        //radioButton7.Checked = true;
                        bridge[1] = "fteproxy";
                    }
                    if (str.IndexOf("meek-client-torbrowser") >= 0 && !str.Contains("#"))
                    {
                        //radioButton8.Checked = true;
                        bridge[1] = "meek";
                    }
                    if (str.IndexOf("Bridge ") >= 0 && !str.Contains("#"))
                    {
                        bridges.Add(str.Substring("Bridge ".Length));
                    }
                    //else
                    //{
                    //    radioButton1.Checked = true;
                    //}
                    //MessageBox.Show(str);
                    str = sr.ReadLine();
                }
                sr.Close();
                //MessageBox.Show(socks5.ToString());
                if (socks5[0] != null)
                {
                    radioButton4.Checked = true;
                    textBox5.Text = socks5[0];
                    textBox6.Text = socks5[1];
                    textBox7.Text = socks5[2];
                }
                if (https[0] != null)
                {
                    radioButton2.Checked = true;
                    textBox1.Text = https[0];
                    textBox2.Text = https[1];
                    textBox3.Text = https[2];
                }
                if (radioButton3.Checked)
                {

                }
                if (bridge[0] != null)
                {
                    if (bridge[1].Equals("obfs4proxy"))
                    {
                        radioButton6.Checked = true;
                    }
                    if (bridge[1].Equals("fteproxy"))
                    {
                        radioButton7.Checked = true;
                    }
                    if (bridge[1].Equals("meek"))
                    {
                        radioButton8.Checked = true;
                    }
                    if (bridges.Count > 0)
                    {
                        foreach (string item in bridges)
                        {
                            addtb(item);
                        }
                    }
                }
                if (bridge[0] == null)
                {
                    radioButton5.Checked = true;
                }

                if (!radioButton2.Checked && !radioButton3.Checked && !radioButton4.Checked)
                {
                    radioButton1.Checked = true;
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = false;
                }

            }
            
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            addtb(string.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deltb();
        }

        private int x = 5;
        private int y = 5;
        private int tbname = 0;
        private void addtb(string tbtext)
        {
            TextBox tb = new TextBox();
            tb.Name = "tb" + Convert.ToString(++tbname);
            tb.Parent = panel1;
            tb.BringToFront();
            tb.Width = 423;
            tb.Location = new Point(x, y);
            if (!string.IsNullOrEmpty(tbtext))
            {
                tb.Text = tbtext;
            }
            tb.Show();
            y = y + 25;
        }

        private void deltb()
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is TextBox)
                {
                    //方法一
                    //MessageBox.Show(item.TabIndex.ToString());
                    panel1.Controls.RemoveAt(0);
                    y = y - 25;
                    tbname--;
                    return;
                    //方法二
                    //int a = Convert.ToInt32(item.Name.Substring("System.Windows.Forms.TextBox, Text: ".Length));
                    //if (a == tbname)
                    //{
                    //    panel1.Controls.Remove(item);
                    //    tbname--;
                    //    y = y - 25;
                    //    return;
                    //}
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //List<string> bridges = new List<string>();
            //foreach (Control item in panel1.Controls)
            //{
            //    if (item is TextBox)
            //    {
            //        MessageBox.Show(item.Name.ToString());
            //    }
            //}
            List<string> bridges = new List<string>();
            if (radioButton6.Checked || radioButton7.Checked || radioButton8.Checked)
            {
                //MessageBox.Show(panel1.Controls.Count.ToString());
                //if (panel1.Controls.Count == 1)
                //{
                //    MessageBox.Show("无网桥，无法保存，或者网桥方式选择选择none");
                //    return;
                //}
                
                foreach (Control item in panel1.Controls)
                {
                    if (item is TextBox)
                    {
                        //MessageBox.Show(item.Name.ToString());
                        if (!string.IsNullOrEmpty(item.Text.ToString()))
                        {
                            if (item.Text.ToString().Contains("obfs") || item.Text.ToString().Contains("scramblesuit") && radioButton6.Checked)
                            {
                                bridges.Add(item.Text.ToString());
                            }
                            else if (item.Text.ToString().Contains("fte") && radioButton7.Checked)
                            {
                                bridges.Add(item.Text.ToString());
                            }
                            else if (item.Text.ToString().Contains("meek") && radioButton8.Checked)
                            {
                                bridges.Add(item.Text.ToString());
                            }
                        }
                    }
                }
                if (bridges.Count <= 0)
                {
                    MessageBox.Show("无网桥，无法保存，或者网桥方式选择选择none");
                    return;
                }
            }
           
            if (radioButton1.Checked)
            {
                if (bridges.Count > 0)
                {
                    writetorrc(null, null, bridges);
                }
                else
                {
                    writetorrc(null, null, null);
                }
                
                
            }
            if (radioButton2.Checked)
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("代理服务器不能为空");
                    textBox1.Focus();
                }
                else
                {
                    List<string> list = new List<string>();
                    list.Add(textBox1.Text.ToString());
                    if (!string.IsNullOrEmpty(textBox2.Text))
                    {
                        list.Add(textBox2.Text.ToString());
                        if (!string.IsNullOrEmpty(textBox3.Text))
                        {
                            list.Add(textBox3.Text.ToString());

                        }
                    }
                    if (bridges.Count > 0)
                    {
                        
                        writetorrc("https", list, bridges);
                    }
                    else
                    {
                        writetorrc("https", list, null);
                    }
                    
                    
                }
            }
            if (radioButton3.Checked)
            {
                if (string.IsNullOrEmpty(textBox4.Text))
                {
                    MessageBox.Show("代理服务器不能为空");
                    textBox4.Focus();
                    
                }
                else
                {
                    List<string> list = new List<string>();
                    list.Add(textBox4.Text.ToString());

                    if (bridges.Count > 0)
                    {
                       writetorrc("socks4", list, bridges);
                    }
                    else
                    {
                        writetorrc("socks4", list, null);
                    }
                    
                }
            }
            if (radioButton4.Checked)
            {
                if (string.IsNullOrEmpty(textBox5.Text))
                {
                    MessageBox.Show("代理服务器不能为空");
                    textBox5.Focus();
                    
                }
                else
                {
                    List<string> list = new List<string>();
                    list.Add(textBox5.Text.ToString());
                    if (!string.IsNullOrEmpty(textBox6.Text))
                    {
                        list.Add(textBox6.Text.ToString());
                        if (!string.IsNullOrEmpty(textBox7.Text))
                        {
                            list.Add(textBox7.Text.ToString());

                        }
                    }
                    if (bridges.Count > 0)
                    {
                        
                        writetorrc("socks5", list, bridges);
                    }
                    else
                    {
                        writetorrc("socks5", list, null);
                    }
                    
                }
            }
        }

        private void writetorrc(string proxytype,List<string> list, List<string> bridges)
        {
            string path = System.Environment.CurrentDirectory + "\\";
            using (StreamWriter sw = new StreamWriter(path + "torrc"))
            {
                sw.WriteLine("# This file was generated by Tor; if you edit it, comments will not be preserved");
                sw.WriteLine("# The old torrc file was renamed to torrc.orig.1 or similar, and Tor will ignore it");
                sw.WriteLine(" ");
                if (bridges!=null)
                {
                    if (bridges.Count > 0)
                    {
                        foreach (string item in bridges)
                        {
                            sw.WriteLine("Bridge " + item);
                        }
                        if (radioButton6.Checked)
                        {
                            sw.WriteLine(@"ClientTransportPlugin obfs2,obfs3,obfs4,scramblesuit exec PluggableTransports\obfs4proxy.exe");
                        }
                        if (radioButton7.Checked)
                        {
                            sw.WriteLine(@"ClientTransportPlugin fte exec TPluggableTransports\fteproxy.exe --managed");
                        }
                        if (radioButton8.Checked)
                        {
                            sw.WriteLine(@"ClientTransportPlugin meek exec PluggableTransports\terminateprocess-buffer.exe PluggableTransports\meek-client-torbrowser.exe -- PluggableTransports\meek-client.exe");
                        }
                    }
                }
                
                sw.WriteLine("DataDirectory ./Data");
                sw.WriteLine("GeoIPFile ./Data/geoip");
                sw.WriteLine("GeoIPv6File ./Data/geoip6");
                sw.WriteLine("Log notice file ./Data/tor.log");
                if (!string.IsNullOrEmpty(proxytype))
                {
                    if (proxytype.Equals("https"))
                    {
                        sw.WriteLine("HTTPSProxy " + list[0].ToString());
                        if (list.Count > 1)
                        {
                            sw.WriteLine("HTTPSProxyAuthenticator " + list[1].ToString()+":"+ list[2].ToString());
                        }

                    }
                    if (proxytype.Equals("socks4"))
                    {
                        sw.WriteLine("Socks4Proxy " + list[0].ToString());
                    }
                    if (proxytype.Equals("socks5"))
                    {
                        sw.WriteLine("Socks5Proxy " + list[0].ToString());
                        if (list.Count > 1)
                        {
                            sw.WriteLine("Socks5ProxyUsername " + list[1].ToString());
                        }
                        if (list.Count > 2)
                        {
                            sw.WriteLine("Socks5ProxyPassword " + list[2].ToString());
                        }

                    }
                }

                if (bridges == null)
                {
                    sw.WriteLine("ReachableAddresses *:80,*:443");
                    sw.WriteLine("ReachableAddresses reject *:*");
                    sw.WriteLine("ReachableAddresses reject *:*");
                }
                
                if (bridges != null)
                {
                    sw.WriteLine("UseBridges 1");
                    
                }
                sw.WriteLine("HTTPTunnelPort 127.0.0.1:9150");
                sw.WriteLine("SocksPort 9050 IPv6Traffic PreferIPv6 KeepAliveIsolateSOCKSAuth");
                sw.WriteLine("ControlPort 9151");
                sw.WriteLine("CookieAuthentication 1");
                sw.Close();
                MessageBox.Show("请手动重启Tor，使配置生效。", "Tor配置保存成功");
                this.Close();
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
                groupBox3.Enabled = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = true;
            }
        }
    }
}
