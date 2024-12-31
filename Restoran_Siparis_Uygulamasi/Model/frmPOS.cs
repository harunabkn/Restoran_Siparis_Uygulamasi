using System;
using System.Collections;
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
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Restoran_Siparis_Uygulamasi.Model
{
    public partial class frmPOS : Form
    {
        public frmPOS()
        {
            InitializeComponent();
        }

        public int AnaID = 0;
        public string SiparisTuru;

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
            if (b.Text == "All Categories")
            {
                txtAra.Text = "1";
                txtAra.Text = "";
                return;
            }

            foreach (var item in urunPaneli.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PCategory.ToLower().Contains(b.Text.Trim().ToLower());
            }
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

        private void AddItems(string id, string proID, string name, string kategoriID, string uFiyat, Image uResim)
        {
            var w = new ucProduct()
            {
                PName = name,
                PPrice = uFiyat,
                PCategory = kategoriID,
                PImage = uResim,
                id = Convert.ToInt32(proID)
            };

            urunPaneli.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;

                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    if (Convert.ToInt32(item.Cells["dgvproID"].Value) == wdg.id)
                    {
                        // Mevcut miktarı artır
                        int currentQty = int.Parse(item.Cells["dgvQty"].Value.ToString());
                        currentQty++;
                        item.Cells["dgvQty"].Value = currentQty;

                        // Yeni toplam fiyatı hesapla
                        double price = double.Parse(item.Cells["dgvPrice"].Value.ToString());
                        item.Cells["dgvAmount"].Value = currentQty * price;

                        // Debug ile kontrol
                        Debug.WriteLine($"Ürün Güncellendi - ID: {wdg.id}, Qty: {currentQty}, Price: {price}, Amount: {item.Cells["dgvAmount"].Value}");
                        GetTotal();
                        return;
                    }
                }

                // Eğer ürün daha önce eklenmemişse yeni ürün olarak ekle
                guna2DataGridView1.Rows.Add(new object[] {0, 0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice });
                Debug.WriteLine($"Yeni Ürün Eklendi - ID: {wdg.id}, Name: {wdg.PName}, Price: {wdg.PPrice}");
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
                AddItems("0",item["UrunID"].ToString(), item["UrunAdi"].ToString(), item["kategoriID"].ToString(), item["uFiyat"].ToString(), Image.FromStream(new MemoryStream(imagearray)));
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
                AddItems("0", item["UrunID"].ToString(), item["UrunAdi"].ToString(), item["kategoriID"].ToString(), item["uFiyat"].ToString(), Image.FromStream(new MemoryStream(imagearray)));
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

            lblMasa.Text = "";
            lblGarson.Text = "";
            lblMasa.Visible = false;
            lblGarson.Visible = false;
            guna2DataGridView1.Rows.Clear();
            AnaID = 0;
            lblToplam.Text = "00";

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
                if (item.Cells["dgvAmount"].Value != null &&
                    !string.IsNullOrEmpty(item.Cells["dgvAmount"].Value.ToString()))
                {
                    double amount = double.Parse(item.Cells["dgvAmount"].Value.ToString());
                    tot += amount;
                }
            }

            lblToplam.Text = tot.ToString("N2");
            Debug.WriteLine($"Toplam Tutar: {tot}");
        }

        private void btnTeslimat_Click(object sender, EventArgs e)
        {

            lblMasa.Text = "";
            lblGarson.Text = "";
            lblMasa.Visible = false;
            lblGarson.Visible = false;
            SiparisTuru = "Teslimat";

        }

        private void btnPaket_Click(object sender, EventArgs e)
        {
            lblMasa.Text = "";
            lblGarson.Text = "";
            lblMasa.Visible = false;
            lblGarson.Visible = false;
            SiparisTuru = "Paket";
        }

        private void btnMasa_Click(object sender, EventArgs e)
        {

            SiparisTuru = "Din In";
            // Masa seçimi
            frmTableSelect frm = new frmTableSelect();
            frm.ShowDialog(); // Masa seçim formunu göster

            if (!string.IsNullOrEmpty(frm.MasaAdi))
            {
                lblMasa.Text = frm.MasaAdi; // Masa adı seçildiyse güncelle
                lblMasa.Visible = true;    // Masa label'ını görünür yap
            }
            else
            {
                lblMasa.Text = "Masa Seçilmedi"; // Masa seçilmediyse varsayılan metin
                lblMasa.Visible = true;
            }

            // Garson seçimi
            frmWaiterSelect frm2 = new frmWaiterSelect();
            frm2.ShowDialog(); // Garson seçim formunu göster

            if (!string.IsNullOrEmpty(frm2.garsonAdi))
            {
                lblGarson.Text = frm2.garsonAdi; // Garson adı seçildiyse güncelle
                lblGarson.Visible = true;       // Garson label'ını görünür yap
            }
            else
            {
                lblGarson.Text = "Garson Seçilmedi"; // Garson seçilmediyse varsayılan metin
                lblGarson.Visible = true;
            }
        }

        private void btnFis_Click(object sender, EventArgs e)
        {
            string qry1 = "";
            string qry2 = "";

            int detayID = 0;

            if (AnaID == 0) // Insert işlemi
            {
                qry1 = @"Insert into Anamasa (mTarih, mZaman, masaAdi, garsonAdi, durum, siparisTuru, toplam, kabul, degistir) 
                 Values (@mTarih, @mZaman, @masaAdi, @garsonAdi, @durum, @siparisTuru, @toplam, @kabul, @degistir);
                 Select SCOPE_IDENTITY();";
            }
            else // Update işlemi
            {
                qry1 = @"Update Anamasa 
                 Set durum = @durum, toplam = @toplam, kabul = @kabul, degistir = @degistir 
                 Where AnaID = @AnaID;";
            }

            SqlCommand cmd = new SqlCommand(qry1, MainClass.con);
            cmd.Parameters.AddWithValue("@AnaID", AnaID);
            cmd.Parameters.AddWithValue("@mTarih", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@mZaman", DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("@masaAdi", lblMasa.Text);
            cmd.Parameters.AddWithValue("@garsonAdi", lblGarson.Text);
            cmd.Parameters.AddWithValue("@durum", "Pending");
            cmd.Parameters.AddWithValue("@siparisTuru", SiparisTuru);
            cmd.Parameters.AddWithValue("@toplam", Convert.ToDouble(lblToplam.Text));
            cmd.Parameters.AddWithValue("@kabul", 0.0);
            cmd.Parameters.AddWithValue("@degistir", 0.0);

            if (MainClass.con.State == ConnectionState.Closed) MainClass.con.Open();

            if (AnaID == 0)
            {
                AnaID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            else
            {
                cmd.ExecuteNonQuery();
            }

            if (MainClass.con.State == ConnectionState.Open) MainClass.con.Close();

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                detayID = row.Cells["dgvid"].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells["dgvid"].Value);

                if (detayID == 0) // Insert işlemi
                {
                    qry2 = @"Insert into masaDetay (AnaID, proID, adet, fiyat, tumtoplam) 
                     Values (@AnaID, @proID, @adet, @fiyat, @tumtoplam);";
                }
                else // Update işlemi
                {
                    qry2 = @"Update masaDetay 
                     Set proID = @proID, adet = @adet, fiyat = @fiyat, tumtoplam = @tumtoplam 
                     Where detayID = @detayID;";
                }

                SqlCommand cmd2 = new SqlCommand(qry2, MainClass.con);
                cmd2.Parameters.AddWithValue("@detayID", detayID);
                cmd2.Parameters.AddWithValue("@AnaID", AnaID);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproID"].Value));
                cmd2.Parameters.AddWithValue("@adet", Convert.ToInt32(row.Cells["dgvQty"].Value));
                cmd2.Parameters.AddWithValue("@fiyat", Convert.ToDouble(row.Cells["dgvPrice"].Value));
                cmd2.Parameters.AddWithValue("@tumtoplam", Convert.ToDouble(row.Cells["dgvAmount"].Value));

                if (MainClass.con.State == ConnectionState.Closed) MainClass.con.Open();
                cmd2.ExecuteNonQuery();
                if (MainClass.con.State == ConnectionState.Open) MainClass.con.Close();
            }

            guna2MessageDialog1.Show("Başarıyla Kaydedildi");
            guna2DataGridView1.Rows.Clear();
            lblMasa.Text = "";
            lblGarson.Text = "";
            lblToplam.Text = "00";
        }

    }
}
