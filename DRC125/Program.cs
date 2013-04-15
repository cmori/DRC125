using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DRC125
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(params string[] arg)
        {
            //string[] v = System.IO.Directory.GetFiles("C:\\Users\\chihiro\\Documents\\スキャン\\新しいフォルダー (2)", "*.bmp", System.IO.SearchOption.TopDirectoryOnly);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EditView(arg.ToList<string>()));
            //Application.Run(new EditView(v.ToList<string>()));
        }
    }
}
