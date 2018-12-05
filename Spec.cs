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
    public partial class Spec : MaterialForm
    {
        string idsp;
        string idgr;
        public Spec()
        {
            InitializeComponent();
        }


        public void get_sp()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            SqlCommand get_sp = new SqlCommand("select ID_SP as\"idd\", NAME_SP AS \"named\" from SPECIAL", connection);
            connection.Open();
            SqlDataReader dr = get_sp.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "named";
            comboBox1.ValueMember = "idd";
            connection.Close();
        }

            private void Spec_Load(object sender, EventArgs e)
        {
            get_sp();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            SqlCommand Sp = new SqlCommand("select ID_SP as 'КОД СПЕЦИАЛЬНОСТИ', NAME_SP as 'НАИМЕНОВАНИЕ СПЕЦИАЛЬНОСТИ' from SPECIAL", connection);
            connection.Open();
            SqlDataReader dataReader = Sp.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(dataReader);
            dataGridView1.DataSource = table1;
            dataGridView1.Columns[0].Visible = false;

            SqlCommand Gr = new SqlCommand("select ID_GR as 'КОД ГРУППЫ', NOMER_GR as 'НОМЕР ГРУППЫ', NAME_SP as 'НАИМЕНОВАНИЕ СПЕЦИАЛЬНОСТИ'  from [dbo].[GRUPP] inner join [dbo].[SPECIAL] on [dbo].[GRUPP].[ID_SP]=[dbo].[SPECIAL].[ID_SP]", connection);
            SqlDataReader dataRead = Gr.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dataRead);
            dataGridView2.DataSource = table;
            dataGridView2.Columns[0].Visible = false;
            connection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idsp = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e) //изменить
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand upd = new SqlCommand("update special set Name_sp = @Name_sp where id_sp = @id_sp", connection);
            upd.Parameters.AddWithValue("@ID_SP", idsp);
            upd.Parameters.AddWithValue("@NAME_SP", textBox1.Text);
            upd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Изменение произошло успешно");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand Sp = new SqlCommand("select ID_SP as 'КОД СПЕЦИАЛЬНОСТИ', NAME_SP as 'НАИМЕНОВАНИЕ СПЕЦИАЛБНОСТИ' from SPECIAL", connection);
            SqlDataReader dataReader = Sp.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(dataReader);
            dataGridView1.DataSource = table1;
            dataGridView1.Columns[0].Width = 100;
        }

        private void button1_Click(object sender, EventArgs e) //добавить специальность
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand adds = new SqlCommand("[dbo].[special_add]", connection);
            adds.CommandType = CommandType.StoredProcedure;
            adds.Parameters.AddWithValue("@NAME_SP", textBox1.Text);
            adds.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Добавление произошло успешно"); 
        }

        private void button4_Click(object sender, EventArgs e)//поиск
        {

            for (int i = 0; i < dataGridView1.RowCount; i++)

                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        break;
                    }

                    if (textBox1.Text == dataGridView1.Rows[i].Cells[j].Value.ToString())
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[j];
                        dataGridView1.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }

        }

        private void button2_Click(object sender, EventArgs e) //удалить специальность
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Program.connection;
                connection.Open();
                SqlCommand ddel = new SqlCommand("special_del", connection);
                ddel.CommandType = CommandType.StoredProcedure;
                ddel.Parameters.AddWithValue("@id_sp", Convert.ToInt32(idsp));
                ddel.ExecuteNonQuery();
                connection.Close();
                
            }
            catch
            {
                MessageBox.Show("Проблема с удалением!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idgr = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
        }

        private void button10_Click(object sender, EventArgs e)//добавить группы
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand addG = new SqlCommand("[dbo].[grupp_add]", connection);
            addG.CommandType = CommandType.StoredProcedure;
            addG.Parameters.AddWithValue("@NOMER_CR ", textBox2.Text);
            addG.Parameters.AddWithValue("@ID_SP", comboBox1.SelectedValue);
            addG.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Добавление произошло успешно");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Program.connection;
                connection.Open();
                SqlCommand ddel = new SqlCommand("grupp_del", connection);
                ddel.CommandType = CommandType.StoredProcedure;
                ddel.Parameters.AddWithValue("@id_gr", Convert.ToInt32(idgr));
                ddel.ExecuteNonQuery();
                connection.Close();

            }
            catch
            {
                MessageBox.Show("Проблема с удалением!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void button6_Click(object sender, EventArgs e) //обновить
        {
            get_sp();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand Gr = new SqlCommand("select ID_GR as 'КОД ГРУППЫ', NOMER_GR as 'НОМЕР ГРУППЫ', NAME_SP as 'НАИМЕНОВАНИЕ СПЕЦИАЛЬНОСТИ'  from [dbo].[GRUPP] inner join [dbo].[SPECIAL] on [dbo].[GRUPP].[ID_SP]=[dbo].[SPECIAL].[ID_SP]", connection);
            SqlDataReader dataRead = Gr.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dataRead);
            dataGridView2.DataSource = table;
            dataGridView2.Columns[0].Width = 100;
            connection.Close();
        }

        private void button8_Click(object sender, EventArgs e) //изменить группы
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand upd = new SqlCommand("update grupp set Nomer_gr = @NOMER_CR , id_sp=@id_sp where id_gr = @id_gr", connection);
            upd.Parameters.AddWithValue("@ID_GR", idgr);
            upd.Parameters.AddWithValue("@NOMER_CR ", textBox2.Text);
            upd.Parameters.AddWithValue("@ID_SP", comboBox1.SelectedValue);
            upd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Изменение произошло успешно");
        }

        private void button7_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < dataGridView2.RowCount; i++)

                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    if (dataGridView2.Rows[i].Cells[j].Value == null)
                    {
                        break;
                    }

                    if (textBox2.Text == dataGridView2.Rows[i].Cells[j].Value.ToString())
                    {
                        dataGridView2.CurrentCell = dataGridView2.Rows[i].Cells[j];
                        dataGridView2.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mMenu menu = new mMenu();
            this.Hide();
            menu.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
