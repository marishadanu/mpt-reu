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
    
    public partial class Uch : MaterialForm
    {
        string idgr;
        string idpl;
        public Uch()
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
            comboBox2.DataSource = dt;
            comboBox2.DisplayMember = "named";
            comboBox2.ValueMember = "idd";
            connection.Close();
        }

        public void get_sp1()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand get_sp = new SqlCommand("select ID_SP as\"idd\", NAME_SP AS \"named\" from SPECIAL", connection);
            SqlDataReader dr = get_sp.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            comboBox3.DataSource = dt;
            comboBox3.DisplayMember = "named";
            comboBox3.ValueMember = "idd";
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
            comboBox1.DataSource = dt2;
            comboBox1.DisplayMember = "named";
            comboBox1.ValueMember = "idd";
            connection.Close();
        }
        private void Uch_Load(object sender, EventArgs e)
        {
            get_sp();
            get_ds();
            get_sp1();
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            SqlCommand Sp = new SqlCommand("select ID_PLAN as 'КОД ПЛАНА', KOLVO_CHAS as 'КОЛИЧЕСТВО ЧАСОВ', NAME_DISCIP AS 'ДИСЦИПЛИНА', NAME_SP AS 'СПЕЦИАЛЬНОСТЬ' "+
            "from[UCH_PLAN] inner join[dbo].[DISCIP] on[dbo].[uch_plan].[id_discip] =[dbo].[discip].[id_discip] inner join special on[dbo].[uch_plan].[id_sp] = [dbo].[special].[id_sp]", connection);
            connection.Open();
            SqlDataReader dataReader = Sp.ExecuteReader();
            DataTable table1 = new DataTable();
            table1.Load(dataReader);
            dataGridView1.DataSource = table1;
            dataGridView1.Columns[0].Visible = false;

            SqlCommand Gr = new SqlCommand("select ID_Graf as 'КОД ГРАФИКА', KURS as 'КУРС', NEDEL as 'НЕДЕЛИ', CHAS_OB AS 'ОБЯЗАТЕЛЬННЫЕ ЧАСЫ',NAME_SP AS 'СПЕЦИАЛЬНОСТЬ'"+
               "from [dbo].[UCH_GRAF] inner join [dbo].[SPECIAL] on [dbo].[UCH_GRAF].[ID_SP]=[dbo].[SPECIAL].[ID_SP]", connection);
            SqlDataReader dataRead = Gr.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(dataRead);
            dataGridView2.DataSource = table;
            dataGridView2.Columns[0].Visible = false;
            connection.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //план добавить
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand addp = new SqlCommand("[dbo].[plan_add]", connection);
            addp.CommandType = CommandType.StoredProcedure;
            addp.Parameters.AddWithValue("@kolvo_chas ", textBox1.Text);
            addp.Parameters.AddWithValue("@ID_discip", comboBox1.SelectedValue);
            addp.Parameters.AddWithValue("@ID_sp", comboBox1.SelectedValue);
            addp.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Добавление произошло успешно");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idpl = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idgr = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            comboBox3.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)//график добавить
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand addG = new SqlCommand("[dbo].[graf_add]", connection);
            addG.CommandType = CommandType.StoredProcedure;
            addG.Parameters.AddWithValue("@kurs", textBox2.Text);
            addG.Parameters.AddWithValue("@nedel", textBox3.Text);
            addG.Parameters.AddWithValue("@chas_ob", textBox4.Text);
            addG.Parameters.AddWithValue("@ID_sp", comboBox3.SelectedValue);
            addG.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Добавление произошло успешно");
        }

        private void button5_Click(object sender, EventArgs e) //ИЗМЕНИТЬ ГРАФИК
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand upd = new SqlCommand("update uch_graf set kurs = @kurs, nedel = @nedel, chas_ob = @chas_ob, id_sp = @id_sp where id_graf = @id_graf", connection);
            upd.Parameters.AddWithValue("@ID_graf", idgr);
            upd.Parameters.AddWithValue("@kurs", textBox2.Text);
            upd.Parameters.AddWithValue("@nedel", textBox3.Text);
            upd.Parameters.AddWithValue("@chas_ob", textBox4.Text);
            upd.Parameters.AddWithValue("@ID_SP", comboBox3.SelectedValue);
            upd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Изменение произошло успешно");
        }

        private void button3_Click(object sender, EventArgs e)//ИЗМЕНИТЬ ПЛАН
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = Program.connection;
            connection.Open();
            SqlCommand upd = new SqlCommand("update uch_plan set kolvo_chas = @kolvo_chas, id_discip = @id_discip, id_sp = @id_sp where id_plan = @id_plan", connection);
            upd.Parameters.AddWithValue("@ID_plan", idpl);
            upd.Parameters.AddWithValue("@kolvo_chas", textBox1.Text);
            upd.Parameters.AddWithValue("@ID_discip", comboBox1.SelectedValue);
            upd.Parameters.AddWithValue("@ID_SP", comboBox2.SelectedValue);
            upd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Изменение произошло успешно");
        }

        private void button2_Click(object sender, EventArgs e) //УДАЛИТЬ ПЛАН
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Program.connection;
                connection.Open();
                SqlCommand ddel = new SqlCommand("plan_del", connection);
                ddel.CommandType = CommandType.StoredProcedure;
                ddel.Parameters.AddWithValue("@id_plan", Convert.ToInt32(idpl));
                ddel.ExecuteNonQuery();
                connection.Close();

            }
            catch
            {
                MessageBox.Show("Проблема с удалением!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e) //УДАЛИТЬ ГРАФМ=ИК
        {
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = Program.connection;
                connection.Open();
                SqlCommand ddel = new SqlCommand("graf_del", connection);
                ddel.CommandType = CommandType.StoredProcedure;
                ddel.Parameters.AddWithValue("@id_graf", Convert.ToInt32(idgr));
                ddel.ExecuteNonQuery();
                connection.Close();

            }
            catch
            {
                MessageBox.Show("Проблема с удалением!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mMenu menu = new mMenu();
            this.Hide();
            menu.Show();
        }
    }
}
