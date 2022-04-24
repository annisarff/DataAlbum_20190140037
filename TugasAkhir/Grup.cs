using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TugasAkhir
{
    public partial class Grup : Form
    {
        //membuat koneksi ke database
        private string kstr = "data source = LAPTOP-07EGJ8M5\\SQLEXPRESS01;database=Album;User ID=sa;Password=annisa16";
        private SqlConnection koneksi;

        //menginisialisai koneksi
        public Grup()
        {
            InitializeComponent();
            koneksi = new SqlConnection(kstr);
            refreshform();
        }
        //memasukkan database ke datagridview
        private void dgview()
        {
            koneksi.Open();
            string str = "select * from dbo.Grup";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();

        }

        //mengaktifkan button Buka agar menampilkan data pada datagridview
        private void btnBuka_Click(object sender, EventArgs e)
        {
            dgview();
            btnBuka.Enabled = false;
        }

        //membuat refreshform agar komponen menjadi disabled sebelum di klik button add
        private void refreshform()
        {
            tbId.Text = "";
            tbGrup.Text = "";
            tbId.Enabled = true;
            tbGrup.Enabled = true;
            btnTambah.Enabled = false;
            btnBersih.Enabled = false;

        }

        //mengaktifkan komponen pada tampilan
        private void btnAdd_Click(object sender, EventArgs e)
        {
            tbId.Enabled = true;
            tbGrup.Enabled = true;
            btnTambah.Enabled = true;
            btnBersih.Enabled = true;
        }

        //mengaktifkan button Clear
        private void btnBersih_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        //mengaktifkan button input
        private void btnTambah_Click(object sender, EventArgs e)
        {
            string id = tbId.Text;
            string namaGrup = tbGrup.Text;
            //jika nama dan id kosong
            if (namaGrup == "" || id == "")
            {
                MessageBox.Show("Masukkan Data Grup", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                koneksi.Open();
                //menggunakan query agar bisa memasukkan data ke dalam database
                string str = "insert into dbo.Grup values(@id, @namaGrup)";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("id", tbId.Text));
                cmd.Parameters.Add(new SqlParameter("namaGrup", tbGrup.Text));
                cmd.ExecuteNonQuery();

                //menutup koneksi
                koneksi.Close();
                MessageBox.Show("Data Berhasil Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //menapilkan data dengan datagridview
                dgview();
                refreshform();
            }
        }
        //mengaktifkan button delete
        private void btnHapus_Click(object sender, EventArgs e)
        {
            string id = tbId.Text;
            string namaGrup = tbGrup.Text;
            if (MessageBox.Show("Yakin ingin menghapus grup : "+tbGrup.Text+"?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
            {
                koneksi.Open();
                //menggunakan query agar bisa menghapus data dari database
                string str = "delete from dbo.Grup where nama_grup = @namaGrup";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("id", tbId.Text));
                cmd.Parameters.Add(new SqlParameter("namaGrup", tbGrup.Text));
                cmd.ExecuteNonQuery();
                //menutup koneksi
                koneksi.Close();
                MessageBox.Show("Data Berhasil Dihapus", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //menapilkan data dengan datagridview
                dgview();
                refreshform();
                btnBuka.Enabled = true;
            }
            

        }
        //agar dapat mengklik baris dan menamilkan data pada textbox
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                tbId.Text = row.Cells["kode_grup"].Value.ToString();
                tbGrup.Text = row.Cells["nama_grup"].Value.ToString();

            }
            catch(Exception X)
            {
                MessageBox.Show(X.ToString());
            }
        }
        //mengaktifkan button Update
        private void btnUp_Click(object sender, EventArgs e)
        {
            string id = tbId.Text;
            string namaGrup = tbGrup.Text;
            if (MessageBox.Show("Yakin ingin mengedit grup ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                koneksi.Open();
                //menggunakan query agar bisa memasukkan data ke dalam database
                string str = "UPDATE dbo.Grup set nama_grup='" + namaGrup + "' where kode_grup='" + id + "'";
                SqlCommand cmd = new SqlCommand(str, koneksi);
                cmd.ExecuteNonQuery();
                //menutup koneksi
                koneksi.Close();
                MessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //menapilkan data dengan datagridview
                dgview();
                refreshform();
                btnBuka.Enabled = true;
            }
        }



        //mengaktifkan menu agar ketika di klik menuju halaman yang dipil
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
