using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mpt
{
    
    public partial class Form1 : Form
    {
        private Baza _BZ;
        public Form1()
        {
           
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _BZ = new Baza();
            _BZ.Register_set("PASCHA-ПК\\FUGGIBD", "mpt","sa","Dk2Giv4zM!06");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Maximum == progressBar1.Value)
            {
                timer1.Enabled = false;
                this.Hide();
                Avt avt = new Avt();
                avt.Show();
            }
            else
            {
                progressBar1.PerformStep();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
