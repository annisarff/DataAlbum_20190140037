using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TugasAkhir
{
    public partial class Awal : Form
    {
        public Awal()
        {
            InitializeComponent();
        }

       

           //mengaktifkan menu agar ketika di klik menuju halaman yang dipilih
        private void daftarGrupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grup grp = new Grup();
            grp.Show();
            this.Hide();
        }

        //mengaktifkan menu agar ketika di klik menuju halaman yang dipil
        private void albumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input ip = new Input();
            ip.Show();
            this.Hide();
        }
    }
}
