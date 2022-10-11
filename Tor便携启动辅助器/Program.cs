using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tor便携启动辅助器
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            bool createnew=false;
            using (Mutex mutex = new Mutex(true, Application.ProductName, out createnew))
            {
                if (createnew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
                else
                {
                    MessageBox.Show("程序已在后台运行，请从系统托盘重新打开");
                    Thread.Sleep(1000);
                    System.Environment.Exit(1);
                }
            }
        }
    }
}
