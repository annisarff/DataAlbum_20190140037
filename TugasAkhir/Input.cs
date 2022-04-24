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
    public partial class Input : Form
    {
        //membuat koneksi ke database
        public string koneksi = "data source = LAPTOP-07EGJ8M5\\SQLEXPRESS01;database=Album;User ID=sa;Password=annisa16";
        public SqlConnection conn;

        //menginisialisai koneksi
        public Input()
        {
            InitializeComponent();
            conn = new SqlConnection(koneksi);
            refreshform();
        }
        //memasukkan database ke datagridview
        public void dgview()
        {
            conn.Open();
            string str = "select * from dbo.Barang";
            SqlDataAdapter da = new SqlDataAdapter(str,conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            conn.Close();

        }

        //mengaktifkan button input
        public void btnSbmt_Click(object sender, EventArgs e)
        {
            conn.Open();
            //menggunakan query agar bisa memasukkan data ke dalam database
            string strs = "insert into dbo.Barang values(@kode, @album, @jenis, @harga, @grup)";
            SqlCommand cmd = new SqlCommand(strs, conn);
            cmd.Parameters.AddWithValue("@kode", (txtKode.Text));
            cmd.Parameters.AddWithValue("@album", (txtAlbum.Text));
            cmd.Parameters.AddWithValue("@jenis", (txtVers.Text));
            cmd.Parameters.AddWithValue("@harga", (txtHarga.Text));
            cmd.Parameters.AddWithValue("@grup",(textBox1.Text));
            cmd.ExecuteNonQuery();
            //menutup koneksi
            conn.Close();
            MessageBox.Show("Berhasil Menambahkan Data");
            //menapilkan data dengan datagridview
            dgview();



        }

        //mengaktifkan komponen pada tampilan
        public void btnAdd_Click(object sender, EventArgs e)
        {
            txtKode.Text = "";
            txtAlbum.Text = "";
            txtVers.Text = "";
            txtHarga.Text = "";
            txtKode.Enabled = true;
            txtAlbum.Enabled = true;
            textBox1.Enabled = true;
            txtVers.Enabled = true;
            txtHarga.Enabled = true;
            btnSbmt.Enabled = true;
            btnClr.Enabled = true;
            btnDlt.Enabled = true;
            btnAdd.Enabled = false;
        }

        //mengaktifkan button Buka agar menampilkan data pada datagridview
        public void btnOpn_Click(object sender, EventArgs e)
        {
            dgview();
            btnOpn.Enabled = false;
        }

        //membuat refreshform agar komponen menjadi disabled sebelum di klik button add
        public void refreshform()
        {
            txtKode.Enabled = false;
            txtAlbum.Enabled = false;
            textBox1.Enabled = false;
            txtVers.Enabled = false;
            txtHarga.Enabled = false;
            btnSbmt.Enabled = false;
            btnDlt.Enabled = true;
            btnClr.Enabled = true;
            btnUp.Enabled = true;
            btnAdd.Enabled = true;


        }

        //mengaktifkan button delete
        public void btnDlt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin ingin menghapus album: " + txtAlbum.Text + "?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string album = txtAlbum.Text;
                conn.Open();
                //menggunakan query agar bisa menghapus data dari database
                string str = "delete from dbo.Barang where nama_album = @album";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("album", txtAlbum.Text));
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Data Berhasil Dihapus", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //menapilkan data dengan datagridview
                dgview();
                refreshform();
                btnOpn.Enabled = true;
            }
        }
        //agar dapat mengklik baris dan menamilkan data pada textbox
        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtKode.Text = row.Cells["kode"].Value.ToString();
                txtAlbum.Text = row.Cells["nama_album"].Value.ToString();
                txtVers.Text = row.Cells["jenis"].Value.ToString();
                txtHarga.Text = row.Cells["harga"].Value.ToString();
                textBox1.Text = row.Cells["kode_grup"].Value.ToString();
                
            }
            catch (Exception X)
            {
                MessageBox.Show(X.ToString());
            }
        }

        //mengaktifkan button Update
        public void btnUp_Click(object sender, EventArgs e)
        {
            string kode = txtKode.Text;
            string album = txtAlbum.Text;
            string grup = textBox1.Text;
            string versi = txtVers.Text;
            string harga = txtHarga.Text;
           
            if (MessageBox.Show("Yakin ingin mengedit grup ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                conn.Open();
                //menggunakan query agar bisa memasukkan data ke dalam database
                string str = "UPDATE dbo.Barang set nama_album='" + album + "',kode_grup='" + grup + "',jenis='" + versi + "',harga='" + harga + "' where kode='" + kode + "'";
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("Data Berhasil Diubah", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //menapilkan data dengan datagridview
                dgview();
                refreshform();
                btnOpn.Enabled = true;
            }
        }

        //mengaktifkan button Clear
        public void btnClr_Click(object sender, EventArgs e)
        {
            refreshform();
        }

        //mengaktifkan menu agar ketika di klik menuju halaman yang dipil
        public void daftarGrupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grup grp = new Grup();
            grp.Show();
            this.Hide();
        }

        //mengaktifkan menu agar ketika di klik menuju halaman yang dipil
        public void albumToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            Input ip = new Input();
            ip.Show();
            this.Hide();
        }
    }
    
}
