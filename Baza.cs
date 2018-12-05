using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Win32;

namespace mpt
{
    class Baza
    {
        public static string DS;
        public static string IC;
        public static string UN;
        public static string UP;
        public event Action<bool> Status;


        public SqlConnection Connection = new SqlConnection("Data Source=Empty; Initial Catalog = Empty;Persist Security Info=True; User ID=Empty; password =\"Empty\"");
        public void Register_get()
        {
            try
            {
                RegistryKey mpt_Option = Registry.CurrentConfig;
                RegistryKey MPT = mpt_Option.CreateSubKey("MPT");
                DS = MPT.GetValue("DS").ToString();
                IC = MPT.GetValue("IC").ToString();
                UN = MPT.GetValue("UN").ToString();
                UP = MPT.GetValue("UP").ToString();
                Set_Connection();
            }
            catch
            {
                RegistryKey mpt_Option = Registry.CurrentConfig;
                RegistryKey MPT = mpt_Option.CreateSubKey("MPT");
                MPT.SetValue("DS", "Empty");
                MPT.SetValue("IC", "Empty");
                MPT.SetValue("UN", "Empty");
                MPT.SetValue("UP", "Empty");
            }
        }
        public void Register_set(string DSvalue, string ICvalue, string UNvalue, string UPvalue)
        {
            RegistryKey mpt_Option = Registry.CurrentConfig;
            RegistryKey MPT = mpt_Option.CreateSubKey("MPT");
            MPT.SetValue("DS", DSvalue);
            MPT.SetValue("IC", ICvalue);
            MPT.SetValue("UN", UNvalue);
            MPT.SetValue("UP", UPvalue);

            Register_get();
        }
        public void Set_Connection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Data Source=PASCHA-ПК\\FUGGIBD;Initial Catalog=mpt;Persist Security Info=True; User ID = sa;Password=Dk2GiV4zM!06";
        }
        public void Connection_State()
        {
            Register_get();
            try
            {
                Connection.Open();
                Status(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Status(false);
            }
        }
    }
}
