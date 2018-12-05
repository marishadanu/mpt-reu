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
using Word = Microsoft.Office.Interop.Word;

namespace mpt
{
    public partial class Vedom : MaterialForm
    {
       
        private readonly string TemplateFileName = @"C:\Users\pascha\Desktop\МПТу\ПП 03.01\mpt\vedom.docx";


        public Vedom()
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

            SqlCommand get_gr = new SqlCommand("select ID_GR as\"idd1\", NOMER_GR AS \"named1\" from GRUPP", connection);
            SqlDataReader drr = get_gr.ExecuteReader();
            DataTable dtt = new DataTable();
            dtt.Load(drr);
            comboBox2.DataSource = dtt;
            comboBox2.DisplayMember = "named1";
            comboBox2.ValueMember = "idd1";

            SqlCommand get_ds = new SqlCommand("select ID_DISCIP as\"idd\", NAME_DISCIP AS \"named\" from DISCIP", connection);
            SqlDataReader dr2 = get_ds.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(dr2);
            comboBox3.DataSource = dt2;
            comboBox3.DisplayMember = "named";
            comboBox3.ValueMember = "idd";
            connection.Close();
        }
        
        private void Vedom_Load(object sender, EventArgs e)
        {
            get_sp();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        public DataTable getData(string querry)
        {
            SqlConnection con1 = new SqlConnection("Data Source = PASCHA-ПК\\FUGGIBD;Initial Catalog=mpt;Persist Security Info = True; User ID = sa; Password = Dk2GiV4zM!06 ");
            SqlCommand get1 = new SqlCommand(querry, con1);
            SqlDataAdapter adt = new SqlDataAdapter(get1);
            DataTable dataTable = new DataTable();
            adt.Fill(dataTable);
            return dataTable;
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            int val;
            Int32.TryParse(comboBox3.SelectedValue.ToString(), out val);
            string querry = "select id_elem as 'КОД ЭЛЕМЕНТА', name_elem as 'НАЗВАНИЕ ЭЛЕИЕНТА' from elem_mod where id_discip = " + val;
            dataGridView1.DataSource = getData(querry);
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            try
            {
                var special = comboBox1.Text.ToString();
                var modul = comboBox3.Text.ToString();
                var gruppa = comboBox2.Text.ToString();
                var element = dataGridView1.CurrentRow.Cells[1].Value.ToString();

                var wordAp = new Word.Application();
                wordAp.Visible = false;

                try
                {
                    var wordDocument = wordAp.Documents.Open(TemplateFileName);
                    ReplaceWordStub("{special}", special, wordDocument);
                    ReplaceWordStub("{modul}", modul, wordDocument);
                    ReplaceWordStub("{gruppa}", gruppa, wordDocument);
                    ReplaceWordStub("{element}", element, wordDocument);
                    wordDocument.SaveAs(@"C:\Users\pascha\Desktop\МПТу\ПП 03.01\mpt\vedom.docx");
                    wordAp.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Ошибка!");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var rang = wordDocument.Content;
            rang.Find.ClearFormatting();
            rang.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlavM gm = new GlavM();
            this.Hide();
            gm.Show();
        }
    }
}
