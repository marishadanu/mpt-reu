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
    public partial class Sotr : MaterialForm
    {
        string ids;
        public Sotr()
        {
            InitializeComponent();
        }

        private void Sotr_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand Sot_s = new SqlCommand("select ID_S AS 'КОД СОТРУДНИКА',FAM_S AS 'ФАМИЛИЯ СОТРУДНИКА', IM_S AS 'ИМЯ СОТРУДНИКА', OTCH_S AS 'ОТЧЕСТВО СОТРУДНИКА',LOGIN_S AS 'ЛОГИН СОТРУДНИКА', PAS_S AS 'ПАРОЛЬ СОТРУДНИКА', DOLZHN AS 'НАЗВАНИЕ ДОЛЖНОСТИ', ACCESS AS 'ДОСТУП' FROM [dbo].[SOTR]", connection);
            SqlDataReader dataReader = Sot_s.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(dataReader);
            dataGridView1.DataSource = table1;
            dataGridView1.Columns[0].Visible = false;
            connection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ids = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e) //добавить
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand adds = new SqlCommand("[dbo].[sotr_add]", connection);
            adds.CommandType = CommandType.StoredProcedure;
            adds.Parameters.AddWithValue("@FAM_S", textBox1.Text);
            adds.Parameters.AddWithValue("@IM_S", textBox2.Text);
            adds.Parameters.AddWithValue("@OTCH_S", textBox3.Text);
            adds.Parameters.AddWithValue("@LOGIN_S", textBox5.Text);
            adds.Parameters.AddWithValue("@PAS_S", textBox6.Text);
            adds.Parameters.AddWithValue("@DOLZHN", textBox4.Text);
            adds.Parameters.AddWithValue("@ACCESS", textBox7.Text);
            adds.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Добавление произошло успешно");
        }

        private void button2_Click(object sender, EventArgs e) //удалить
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Program.connection;
                connection.Open();
                SqlCommand ddel = new SqlCommand("sotr_del", connection);
                ddel.CommandType = CommandType.StoredProcedure;
                ddel.Parameters.AddWithValue("@id_s", Convert.ToInt32(ids));
                ddel.ExecuteNonQuery();
                connection.Close();

            }
            catch
            {
                MessageBox.Show("Проблема с удалением!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void button3_Click(object sender, EventArgs e) //изменить
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand upd = new SqlCommand("update sotr set  fam_s = @fam_s, im_s = @im_s, otch_s = @otch_s, login_s = @login_s, pas_s = @pas_s, dolzhn = @dolzhn, access = @access  where id_s = @id_s", connection);
            upd.Parameters.AddWithValue("@id_s", ids);
            upd.Parameters.AddWithValue("@fam_s", textBox1.Text);
            upd.Parameters.AddWithValue("@im_s", textBox2.Text);
            upd.Parameters.AddWithValue("@otch_s", textBox3.Text);
            upd.Parameters.AddWithValue("@login_s", textBox5.Text);
            upd.Parameters.AddWithValue("@pas_s", textBox6.Text);
            upd.Parameters.AddWithValue("@dolzhn", textBox4.Text);
            upd.Parameters.AddWithValue("@access", textBox7.Text);
            upd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Изменение произошло успешно");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)

                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        break;
                    }

                    if (textBox8.Text == dataGridView1.Rows[i].Cells[j].Value.ToString())
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[j];
                        dataGridView1.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mMenu menu = new mMenu();
            this.Hide();
            menu.Show();
        }
    }
}
