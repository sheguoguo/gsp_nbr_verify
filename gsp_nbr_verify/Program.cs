using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace gsp_nbr_verify
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new compare_form());
            //Application.Run(new Form1());
        }
    }
}
