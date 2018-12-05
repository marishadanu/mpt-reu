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
    public partial class GlavM : MaterialForm
    {
        public GlavM()
        {
            InitializeComponent();
        }

        private void GlavM_Load(object sender, EventArgs e)
        {

        }

        private void materialFlatButton2_Click(object sender, EventArgs e)
        {
            Vedom ved = new Vedom();
            this.Hide();
            ved.Show();
        }

        private void materialFlatButton3_Click(object sender, EventArgs e)
        {
            prot pr = new prot();
            this.Hide();
            pr.Show();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            Biletcs bl = new Biletcs();
            this.Hide();
            bl.Show();
        }

        private void GlavM_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
