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
    public partial class Modul : MaterialForm
    {
        string idmd;
        string idel;
        public Modul()
        {
            InitializeComponent();
        }

        public void get_sp()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand get_sp = new SqlCommand("select ID_SP as\"idd\", NAME_SP AS \"named\" from SPECIAL", connection);
            SqlDataReader dr = get_sp.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "named";
            comboBox1.ValueMember = "idd";
            connection.Close();
        }

        public void get_ds()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand get_ds = new SqlCommand("select ID_DISCIP as\"idd\", NAME_DISCIP AS \"named\" from DISCIP", connection);
            SqlDataReader dr2 = get_ds.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(dr2);
            comboBox2.DataSource = dt2;
            comboBox2.DisplayMember = "named";
            comboBox2.ValueMember = "idd";
            connection.Close();
        }
        private void Modul_Load(object sender, EventArgs e)
        {
            get_sp();
            get_ds();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            SqlCommand ds = new SqlCommand("select ID_DISCIP as 'КОД МОДУЛЯ', NAME_DISCIP AS 'НАИМЕНОВАНИЕ МОДУЛЯ', NAME_SP AS 'СПЕЦИАЛЬНОСТЬ'" +
                " FROM DISCIP INNER JOIN SPECIAL ON [DBO].[DISCIP].[ID_SP] = [DBO].[SPECIAL].[ID_SP]", connection);
            connection.Open();
            SqlDataReader dataReader = ds.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(dataReader);
            dataGridView1.DataSource = table1;
            dataGridView1.Columns[0].Visible = false;

            SqlCommand el = new SqlCommand("select ID_ELEM as 'КОД ЭЛЕМЕНТА', NAME_ELEM AS 'НАИМЕНОВАНИЕ ЭЛЕМЕНТА МОДУЛЯ',  NAME_DISCIP AS 'НАИМЕНОВАНИЕ МОДУЛЯ'" +
               " FROM ELEM_MOD INNER JOIN DISCIP ON [DBO].[ELEM_MOD].[ID_DISCIP] = [DBO].[DISCIP].[ID_DISCIP]", connection);
            SqlDataReader dataRead = el.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dataRead);
            dataGridView2.DataSource = table;
            dataGridView2.Columns[0].Visible = false;
            connection.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mMenu menu = new mMenu();
            this.Hide();
            menu.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idel = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            comboBox2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idmd = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void button11_Click(object sender, EventArgs e) //добавить эл
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand addel = new SqlCommand("[dbo].[elem_add]", connection);
            addel.CommandType = CommandType.StoredProcedure;
            addel.Parameters.AddWithValue("@name_elem", textBox2.Text);
            addel.Parameters.AddWithValue("@ID_DISCIP", comboBox2.SelectedValue);
            addel.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Добавление произошло успешно");
        }

        private void button10_Click(object sender, EventArgs e) //удалить эл
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Program.connection;
                connection.Open();
                SqlCommand ddel = new SqlCommand("ELEM_del", connection);
                ddel.CommandType = CommandType.StoredProcedure;
                ddel.Parameters.AddWithValue("@id_elem", Convert.ToInt32(idel));
                ddel.ExecuteNonQuery();
                connection.Close();

            }
            catch
            {
                MessageBox.Show("Проблема с удалением!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e) //изменить эл
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand upd = new SqlCommand("update elem_mod set name_elem = @name_elem,id_discip = @id_discip where id_elem = @id_elem", connection);
            upd.Parameters.AddWithValue("@ID_elem", idel);
            upd.Parameters.AddWithValue("@name_elem", textBox2.Text);
            upd.Parameters.AddWithValue("@ID_discip", comboBox2.SelectedValue);
            upd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Изменение произошло успешно");
        }

        private void button8_Click(object sender, EventArgs e) //поиск эл
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

        private void button4_Click(object sender, EventArgs e) //поск пм
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

        private void button1_Click(object sender, EventArgs e) //добавить пм
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand addG = new SqlCommand("[dbo].[discip_add]", connection);
            addG.CommandType = CommandType.StoredProcedure;
            addG.Parameters.AddWithValue("@name_discip", textBox1.Text);
            addG.Parameters.AddWithValue("@ID_sp", comboBox1.SelectedValue);
            addG.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Добавление произошло успешно");
        }

        private void button2_Click(object sender, EventArgs e) //удалить пм
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Program.connection;
                connection.Open();
                SqlCommand ddel = new SqlCommand("discip_del", connection);
                ddel.CommandType = CommandType.StoredProcedure;
                ddel.Parameters.AddWithValue("@id_discip", Convert.ToInt32(idmd));
                ddel.ExecuteNonQuery();
                connection.Close();

            }
            catch
            {
                MessageBox.Show("Проблема с удалением!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e) //изменить пм
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand upd = new SqlCommand("update discip set name_discip = @name_discip, id_sp = @id_sp where id_discip = @id_discip", connection);
            upd.Parameters.AddWithValue("@ID_discip", idmd);
            upd.Parameters.AddWithValue("@name_discip", textBox1.Text);
            upd.Parameters.AddWithValue("@ID_SP", comboBox1.SelectedValue);
            upd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Изменение произошло успешно");
        }
    }
}
