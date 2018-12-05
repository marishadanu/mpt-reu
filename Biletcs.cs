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
    public partial class Biletcs : MaterialForm
    {
        string idbl;
        string usetext;
        public Biletcs()
        {
            InitializeComponent();
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
            comboBox1.DataSource = dt2;
            comboBox1.DisplayMember = "named";
            comboBox1.ValueMember = "idd";
            connection.Close();
        }
        private void Biletcs_Load(object sender, EventArgs e)
        {
            get_ds();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            SqlCommand EL = new SqlCommand("select Id_bilet as 'КОД БИЛЕТА', vopros AS 'ВОПРОСЫ', NAME_DISCIP AS 'ДИСЦИПЛИНА'" +
                " FROM BILET INNER JOIN DISCIP ON [DBO].[BILET].[ID_DISCIP] = [DBO].[DISCIP].[ID_DISCIP]", connection);
            connection.Open();
            SqlDataReader dataRead = EL.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dataRead);
            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].Width = 100;
            connection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            idbl = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (usetext == " ")
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Program.connection;
                connection.Open();
                SqlCommand addG = new SqlCommand("[dbo].[bilet_add]", connection);
                addG.CommandType = CommandType.StoredProcedure;
                addG.Parameters.AddWithValue("@vopros", richTextBox1.Text);
                addG.Parameters.AddWithValue("@ID_discip", comboBox1.SelectedValue);
                addG.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Добавление произошло успешно");
            }
            else
            {
                MessageBox.Show("Поля пустые!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand upd = new SqlCommand("update bilet set vopros = @vopros, id_discip = @id_discip where id_bilet = @id_bilet", connection);
            upd.Parameters.AddWithValue("@ID_bilet", idbl);
            upd.Parameters.AddWithValue("@vopros", richTextBox1.Text);
            upd.Parameters.AddWithValue("@ID_discip", comboBox1.SelectedValue);
            upd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Изменение произошло успешно");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Program.connection;
                connection.Open();
                SqlCommand ddel = new SqlCommand("bilet_del", connection);
                ddel.CommandType = CommandType.StoredProcedure;
                ddel.Parameters.AddWithValue("@id_bilet", Convert.ToInt32(idbl));
                ddel.ExecuteNonQuery();
                connection.Close();

            }
            catch
            {
                MessageBox.Show("Проблема с удалением!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GlavM gm = new GlavM();
            this.Hide();
            gm.Show();
        }
    }

  
}
