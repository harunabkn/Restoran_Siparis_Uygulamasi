using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran_Siparis_Uygulamasi.Model
{
    public partial class frmPOS : Form
    {
        public frmPOS()
        {
            InitializeComponent();
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();

            urunPaneli.Controls.Clear();
            LoadProducts();
        }
        private void AddCategory()
        {
            string qry = "Select * from Kategori";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            kategoriPaneli.Controls.Clear();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.FillColor = Color.FromArgb(50, 55, 89);
                    b.Size = new Size(134, 45);
                    b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    b.Text = row["kategoryAdi"].ToString();

                    b.Click += new EventHandler(b_Click); // Kategoriye tıklandığında ürünleri filtreler
                    kategoriPaneli.Controls.Add(b);
                }
            }
        }


        private void b_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
            string kategoriAdi = b.Text;

            string qry = $"Select kategoriID From Kategori WHERE kategoryAdi = '{kategoriAdi}'";
            if (MainClass.con.State == ConnectionState.Closed)
            {
                MainClass.con.Open(); // Bağlantıyı aç
            }

            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            int kategoriID = Convert.ToInt32(cmd.ExecuteScalar());
            MainClass.con.Close(); // Bağlantıyı kapat

            // Kategoriye göre ürünleri yükle
            LoadProductsByCategory(kategoriID);
        }


        /*private void b_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
            foreach (var item in urunPaneli.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PCategory == b.Text; // Eğer kategoriAdi eşleşecekse
            }
        }
*/

        private void AddItems(string id, string name, string kategoriID, string uFiyat, Image uResim)
        {
            var w = new ucProduct()
            {
                PName = name,
                PPrice = uFiyat,
                PCategory = kategoriID,
                PImage = uResim,
                id = Convert.ToInt32(id)
            };

            urunPaneli.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;

                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    // Eğer ürün zaten ekliyse, miktarı artır ve toplamı güncelle
                    if (Convert.ToInt32(item.Cells["dgvid"].Value) == wdg.id)
                    {
                        // Miktarı artır
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;

                        // Toplam fiyatı güncelle
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                                                         double.Parse(item.Cells["dgvPrice"].Value.ToString());

                        // Toplam tutarı hemen güncelle
                        GetTotal();
                        return;
                    }
                }

                // Eğer ürün daha önce eklenmemişse yeni ürün olarak ekle
                guna2DataGridView1.Rows.Add(new object[] { 0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice });

                // Yeni ürün eklendikten sonra toplam tutarı hesapla
                GetTotal();
            };

        }


        private void LoadProducts()
        {
            string qry = "Select * From Urunler u inner join Kategori k on k.kategoriID = u.kategoriID";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            urunPaneli.Controls.Clear(); // Önceki ürünleri temizle

            foreach (DataRow item in dt.Rows)
            {
                Byte[] imagearray = (byte[])item["uResim"];
                AddItems(item["UrunID"].ToString(), item["UrunAdi"].ToString(), item["kategoriID"].ToString(), item["uFiyat"].ToString(), Image.FromStream(new MemoryStream(imagearray)));
            }
        }
        private void LoadProductsByCategory(int kategoriID)
        {
            string qry = $"Select * From Urunler WHERE kategoriID = {kategoriID}";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            urunPaneli.Controls.Clear(); // Önceki ürünleri temizle

            foreach (DataRow item in dt.Rows)
            {
                Byte[] imagearray = (byte[])item["uResim"];
                AddItems(item["UrunID"].ToString(), item["UrunAdi"].ToString(), item["kategoriID"].ToString(), item["uFiyat"].ToString(), Image.FromStream(new MemoryStream(imagearray)));
            }
        }






        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in urunPaneli.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtAra.Text.Trim().ToLower());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnYeni_Click(object sender, EventArgs e)
        {

        }

        private void lblMasa_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int sayac = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                sayac++;
                row.Cells[0].Value = sayac;
            }
        }

        private void GetTotal()
        {
            double tot = 0;
            lblToplam.Text = "";
            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                tot += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            }
            lblToplam.Text = tot.ToString("N2");
        }



    }
}
