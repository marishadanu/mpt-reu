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

namespace mpt
{
    public partial class mMenu : MaterialForm
    {
        public mMenu()
        {
            InitializeComponent();
        }

        private void mMenu_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            Spec sp = new Spec();
            this.Hide();
            sp.Show();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            Sotr sotr = new Sotr();
            this.Hide();
            sotr.Show();
        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            Stud ssd = new Stud();
            this.Hide();
            ssd.Show();
        }

        private void materialFlatButton5_Click(object sender, EventArgs e)
        {
            Uch ch = new Uch();
            this.Hide();
            ch.Show();
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            Modul md = new Modul();
            this.Hide();
            md.Show();
        }

        private void mMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
