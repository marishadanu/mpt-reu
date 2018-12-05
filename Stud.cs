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
    public partial class Stud : MaterialForm
    {
        string idst;
        public Stud()
        {
            InitializeComponent();
        }

        public void get_st()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            SqlCommand get_sp = new SqlCommand("select ID_GR as\"idd\", NOMER_GR AS \"named\" from GRUPP", connection);
            connection.Open();
            SqlDataReader dr = get_sp.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "named";
            comboBox1.ValueMember = "idd";
            connection.Close();
        }
        private void Stud_Load(object sender, EventArgs e)
        {
            get_st();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand Sot_s = new SqlCommand("select ID_ST AS 'КОД СОТРУДНИКА',FAM_ST AS 'ФАМИЛИЯ СОТРУДНИКА', IM_ST AS 'ИМЯ СОТРУДНИКА', OTCH_ST AS 'ОТЧЕСТВО СОТРУДНИКА', NOMER_GR AS 'НОМЕР ГРУППЫ' FROM [dbo].[STUDENT] inner join [dbo].[GRUPP] on [dbo].[STUDENT].[ID_GR]=[dbo].[GRUPP].[ID_GR]", connection);
            SqlDataReader dataReader = Sot_s.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(dataReader);
            dataGridView1.DataSource = table1;
            dataGridView1.Columns[0].Visible = false;
            connection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idst = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
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

        private void button1_Click(object sender, EventArgs e) //добавить
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand adds = new SqlCommand("[dbo].[student_add]", connection);
            adds.CommandType = CommandType.StoredProcedure;
            adds.Parameters.AddWithValue("@FAM_ST", textBox1.Text);
            adds.Parameters.AddWithValue("@IM_ST", textBox2.Text);
            adds.Parameters.AddWithValue("@OTCH_ST", textBox3.Text);
            adds.Parameters.AddWithValue("@ID_GR", comboBox1.SelectedValue);
            adds.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Добавление произошло успешно");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mMenu menu = new mMenu();
            this.Hide();
            menu.Show();
        }

        private void button2_Click(object sender, EventArgs e) //УДАЛИТЬ
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Program.connection;
                connection.Open();
                SqlCommand ddel = new SqlCommand("student_del", connection);
                ddel.CommandType = CommandType.StoredProcedure;
                ddel.Parameters.AddWithValue("@id_st", Convert.ToInt32(idst));
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
            SqlCommand upd = new SqlCommand("update student set  fam_st = @fam_st, im_st = @im_st, otch_st = @otch_st, id_gr = @id_gr where id_st = @id_st", connection);
            upd.Parameters.AddWithValue("@id_st", idst);
            upd.Parameters.AddWithValue("@fam_st", textBox1.Text);
            upd.Parameters.AddWithValue("@im_st", textBox2.Text);
            upd.Parameters.AddWithValue("@otch_st", textBox3.Text);
            upd.Parameters.AddWithValue("@id_gr", comboBox1.SelectedValue);
            upd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Изменение произошло успешно");
        }
    }
}
