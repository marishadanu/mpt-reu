using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data.Sql;
using System.Data.SqlClient;

namespace mpt
{
    public partial class Avt : MaterialForm

    {
        
        
        public string Admin_access = "select [dbo].[SOTR].[ACCESS] from [dbo].[SOTR] where [dbo].[SOTR].[ID_S]=";
        int Access;
        public Avt()
        {
            InitializeComponent();
        }
       

        private void Avt_Load(object sender, EventArgs e)
        {
            
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Program.connection;
                SqlCommand g_login = new SqlCommand("select ID_S from Sotr where LOGIN_S = '" + materialSingleLineTextField1.Text + "' and PAS_S = '" + materialSingleLineTextField2.Text + "'", connection);
                connection.Open();
                SqlDataReader read_login = g_login.ExecuteReader();
            if (read_login.HasRows == true)
            {
                read_login.Close();
                MessageBox.Show("Вы успешно вошли в систему", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.login = g_login.ExecuteScalar().ToString();
                SqlCommand AdminAccCmd = new SqlCommand(Admin_access + Program.login, connection);
                Program.Admin_access = Convert.ToInt32(AdminAccCmd.ExecuteScalar().ToString());
                Access = Convert.ToInt32(AdminAccCmd.ExecuteScalar().ToString());

                switch (Access)
                {
                    case (1):
                        mMenu main_form = new mMenu();
                        this.Close();
                        main_form.Show();
                        break;

                    case (0):
                        GlavM gv = new GlavM();
                        this.Close();
                        gv.Show();
                        break;
                }
            }

            else
            {
                MessageBox.Show("Пользователь с такими данными не найден в системе.\nПовторите попытку ввода.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
          
            /*SqlConnection connectio
             * n = new SqlConnection();
            connection.ConnectionString = Program.connection;
            SqlCommand g_login = new SqlCommand("select ID_S from Sotr where LOGIN_S = '" + materialSingleLineTextField1.Text + "' and PAS_S = '" + materialSingleLineTextField2.Text + "'", connection);
            connection.Open();
            SqlDataReader r_login = g_login.ExecuteReader();
            if (r_login.HasRows == true)
            {
                r_login.Close();
                MessageBox.Show("Вы вощли в систему", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.Close();
                GlavM mg = new GlavM();
                this.Hide();
                mg.Show();
            }
            else
            {
                MessageBox.Show("Пользователь с логином " + materialSingleLineTextField1.Text + " не найден в системе. Повторите попытку ввода.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
        }

      
    }
}
