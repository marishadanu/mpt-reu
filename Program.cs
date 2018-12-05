using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mpt
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static string connection = "Data Source=PASCHA-ПК\\FUGGIBD;Initial Catalog=mpt;Persist Security Info=True; User ID = sa;Password = Dk2GiV4zM!06";
        public static string login;
        public static int Admin_access;
    }
}
