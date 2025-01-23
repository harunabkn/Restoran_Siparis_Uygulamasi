using Guna.UI2.WinForms;
using Restoran_Siparis_Uygulamasi.Model;
using Restoran_Siparis_Uygulamasi;
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
    public partial class frmSiparis : Form
    {
        public frmSiparis()
        {
            InitializeComponent();
        }
        private readonly string connectionString = "Data Source=HUAWEI\\SQLEXPRESS; Initial Catalog=DbRestoranSiparis; Integrated Security=True; TrustServerCertificate=True;";

        public int AnaID = 0;
        public string SiparisTuru = "";
        public int kuryeID = 0;
        public string musteriAdi = "";
        public string musteriTelefon = "";

        private void frmPOS_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            KategoriEkleButon();

            urunPaneli.Controls.Clear();
            TumUrunler();
        }
        private void KategoriEkleButon()
        {
            string qry = "SELECT * FROM Kategori";
            SqlCommand cmd = new SqlCommand(qry, AnaSinif.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            kategoriPaneli.Controls.Clear();

            // "Tüm Kategoriler" butonunu ekle
            Guna.UI2.WinForms.Guna2Button allButton = new Guna.UI2.WinForms.Guna2Button();
            allButton.FillColor = Color.FromArgb(50, 55, 89);
            allButton.Size = new Size(134, 45);
            allButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            allButton.Text = "Tüm Kategoriler";
            allButton.Click += new EventHandler(b_Click);
            kategoriPaneli.Controls.Add(allButton);

            // Veritabanından gelen kategoriler için butonları ekle
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.FillColor = Color.FromArgb(50, 55, 89);
                    b.Size = new Size(134, 45);
                    b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    b.Text = row["kategoryAdi"].ToString();
                    b.Click += new EventHandler(b_Click);
                    kategoriPaneli.Controls.Add(b);
                }
            }
        }



        private void b_Click(object sender, EventArgs e)//butonlara tıklanınca yapılacak işlemler
        {
            // Tıklanan kategori butonunu al
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;

            // Eğer "Tüm Kategoriler" seçildiyse tüm ürünleri göster
            if (b.Text == "Tüm Kategoriler")
            {
                TumUrunler(); // Tüm ürünleri yükleme
                return;
            }

            // Kategori ID'sini veya Adını al ve ürünleri yükle
            string kategoriAdi = b.Text;
            Kategori_Urun_Olusturma(kategoriAdi); 
        }

        private void TumUrunler()
        {
            string qry = "Select * From Urunler u inner join Kategori k on k.kategoriID = u.kategoriID";
            SqlCommand cmd = new SqlCommand(qry, AnaSinif.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            urunPaneli.Controls.Clear(); // Önceki ürünleri temizle

            foreach (DataRow item in dt.Rows)
            {
                Byte[] imagearray = (byte[])item["uResim"];
                EkleOgeleri("0", item["UrunID"].ToString(), item["UrunAdi"].ToString(), item["kategoriID"].ToString(), item["uFiyat"].ToString(), Image.FromStream(new MemoryStream(imagearray)));
            }
        }

        private void Kategori_Urun_Olusturma(string kategoriAdi)
        {
            // Kategori adına göre ürünleri filtrele
            string qry = @"
        SELECT u.UrunID, u.UrunAdi, u.kategoriID, u.uFiyat, u.uResim 
        FROM Urunler u
        INNER JOIN Kategori k ON k.kategoriID = u.kategoriID
        WHERE k.kategoryAdi = @kategoriAdi";

            SqlCommand cmd = new SqlCommand(qry, AnaSinif.con);
            cmd.Parameters.AddWithValue("@kategoriAdi", kategoriAdi);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            // Ürün panelini temizle ve yeni ürünleri yükle
            urunPaneli.Controls.Clear();

            foreach (DataRow item in dt.Rows)
            {
                Byte[] imageArray = (byte[])item["uResim"];
                EkleOgeleri("0",
                    item["UrunID"].ToString(),
                    item["UrunAdi"].ToString(),
                    item["kategoriID"].ToString(),
                    item["uFiyat"].ToString(),
                    Image.FromStream(new MemoryStream(imageArray))
                );
            }
        }


        private void EkleOgeleri(string id, string proID, string name, string kategoriID, string uFiyat, Image uResim)
        {
            var w = new ucUrunSablon()
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
                var wdg = (ucUrunSablon)ss;

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


        private void LoadEntries()
        {
            string qry = @"
        SELECT a.masaAdi, a.garsonAdi, a.siparisTuru, d.detayID, u.UrunAdi, d.proID, d.adet, d.fiyat, d.tumtoplam
        FROM masaDetay d
        INNER JOIN Anamasa a ON d.AnaID = a.AnaID
        INNER JOIN Urunler u ON d.proID = u.UrunID
        WHERE d.AnaID = @AnaID";

            SqlCommand cmd = new SqlCommand(qry, AnaSinif.con);
            cmd.Parameters.AddWithValue("@AnaID", AnaID); // Ana sipariş ID'sini filtreliyoruz
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                // Sipariş türünü ayarla
                SiparisTuru = dt.Rows[0]["siparisTuru"].ToString();

                // Sipariş türüne göre butonları işaretle
                if (SiparisTuru == "Masa")
                {
                    btnMasa.Checked = true;
                }
                else if (SiparisTuru == "Paket")
                {
                    btnPaket.Checked = true;
                }
                else if (SiparisTuru == "Teslimat")
                {
                    btnTeslimat.Checked = true;
                }

                // Ürünleri DataGridView'e yükle
                guna2DataGridView1.Rows.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    object[] obj = {
                guna2DataGridView1.Rows.Count + 1,
                row["detayID"],
                row["proID"],
                row["UrunAdi"],
                row["adet"],
                row["fiyat"],
                row["tumtoplam"]
            };
                    guna2DataGridView1.Rows.Add(obj);
                }
            }
            GetTotal();
        }






        private void btnCikis_Click(object sender, EventArgs e)//kapatma butonu
        {
            this.Close();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in urunPaneli.Controls)
            {
                var pro = (ucUrunSablon)item;
                pro.Visible = pro.PName.ToLower().Contains(txtAra.Text.Trim().ToLower());
            }
        }


        private void btnYeni_Click(object sender, EventArgs e)//form temizleme yeni butonu
        {
            // Masa ve garson bilgilerini temizle
            lblMasa.Text = "";
            lblGarson.Text = "";
            lblKuryeIsmı.Text = ""; // Kurye bilgisi varsa onu da sıfırla

            lblMasa.Visible = false;
            lblGarson.Visible = false;
            lblKuryeIsmı.Visible = false;

            // DataGridView (sipariş listesi) temizle
            guna2DataGridView1.Rows.Clear();

            // Toplam tutarı sıfırla
            lblToplam.Text = "0.00";

            // Ürün panelini temizle (isteğe bağlı)
            urunPaneli.Controls.Clear();

            // Sipariş türünü ve sipariş ID'sini sıfırla
            SiparisTuru = "";
            AnaID = 0;

            // Ürünleri yeniden yükle (isteğe bağlı)
            TumUrunler();
        }



        private void btnTeslimat_Click(object sender, EventArgs e)// teslimat butonu fonksiyonu 
        {
            lblMasa.Text = "";       // Masa bilgisi sıfırla
            lblGarson.Text = "";     // Garson bilgisi sıfırla
            lblMasa.Visible = false;
            lblGarson.Visible = false;

            SiparisTuru = "Teslimat";

            // Teslimat formunu aç
            frmTeslimatBilgi frm = new frmTeslimatBilgi
            {
                masaID = AnaID,
                siparisTuru = SiparisTuru
            };

            frm.ShowDialog(); // Formu modal olarak aç

            // Form kapandıktan sonra bilgileri kontrol et
            if (!string.IsNullOrEmpty(frm.txtIsım.Text) && !string.IsNullOrEmpty(frm.txtPhone.Text))
            {
                lblKuryeIsmı.Text = $"Müşteri Adı: {frm.txtIsım.Text} " +
                                    $"Telefon: {frm.txtPhone.Text} " +
                                    $"Kurye: {frm.cbKurye.Text}";
                lblKuryeIsmı.Visible = true; // Bilgileri görünür yap
                musteriAdi = frm.txtIsım.Text;
                musteriTelefon = frm.txtPhone.Text;
            }
        }


        private void btnPaket_Click(object sender, EventArgs e)//paket butonu ile ilgili fonksiyon 
        {
            lblMasa.Text = "";
            lblGarson.Text = "";
            lblMasa.Visible = false;
            lblGarson.Visible = false;
            SiparisTuru = "Paket";

            frmTeslimatBilgi frm = new frmTeslimatBilgi
            {
                masaID = AnaID,
                siparisTuru = SiparisTuru
            };

            frm.ShowDialog();
            
            if(!string.IsNullOrEmpty(frm.txtIsım.Text) && !string.IsNullOrEmpty(frm.txtPhone.Text))
            {
                kuryeID = frm.kuryeID;
                lblKuryeIsmı.Text = "Müşteri Adı: " + frm.txtIsım.Text + " Telefon: " + frm.txtPhone.Text;
                lblKuryeIsmı.Visible = true;
                musteriAdi = frm.txtIsım.Text;
                musteriTelefon = frm.txtPhone.Text;

            }
        }

        private void btnMasa_Click(object sender, EventArgs e) // masa seçimi ile ilgili fonksiyon
        {

            SiparisTuru = "Masa";
            lblKuryeIsmı.Visible = false;
            // Masa seçimi
            frmMasaSec frm = new frmMasaSec();
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
            frmGarsonSec frm2 = new frmGarsonSec();
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





        private void btnFis_Click(object sender, EventArgs e)// fiş ile ilgili fonksiyon
        {
            // Ürün ve sipariş türü kontrolü
            if (guna2DataGridView1.Rows.Count == 0)
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Show("Lütfen en az bir ürün ekleyin!");
                return;
            }

            if (string.IsNullOrEmpty(SiparisTuru))
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Show("Lütfen sipariş türünü seçin!");
                return;
            }

            string qry1 = "";
            string qry2 = "";
            bool yeniSiparis = AnaID == 0; // Yeni sipariş mi kontrolü

            // Ana sipariş ekleme veya güncelleme işlemi
            if (yeniSiparis) // Yeni sipariş oluşturuluyorsa
            {
                qry1 = @"Insert into Anamasa 
                 (mTarih, mZaman, masaAdi, garsonAdi, durum, siparisTuru, toplam, kabul, degistir, kuryeID, musAdi, musTelefon) 
                 Values 
                 (@mTarih, @mZaman, @masaAdi, @garsonAdi, @durum, @siparisTuru, @toplam, @kabul, @degistir, @kuryeID, @musAdi, @musTelefon);
                 Select SCOPE_IDENTITY()";
            }
            else // Mevcut sipariş güncelleniyorsa
            {
                qry1 = @"Update Anamasa 
                 Set durum = @durum, toplam = @toplam, kabul = @kabul, degistir = @degistir 
                 Where AnaID = @AnaID;";
            }

            SqlCommand cmd = new SqlCommand(qry1, AnaSinif.con);
            cmd.Parameters.AddWithValue("@AnaID", AnaID);
            cmd.Parameters.AddWithValue("@mTarih", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@mZaman", DateTime.Now.ToString("HH:mm:ss"));
            cmd.Parameters.AddWithValue("@masaAdi", lblMasa.Text);
            cmd.Parameters.AddWithValue("@garsonAdi", lblGarson.Text);
            cmd.Parameters.AddWithValue("@durum", "Hazırlanıyor");
            cmd.Parameters.AddWithValue("@siparisTuru", SiparisTuru);
            cmd.Parameters.AddWithValue("@toplam", Convert.ToDouble(lblToplam.Text));
            cmd.Parameters.AddWithValue("@kabul", 0.0);
            cmd.Parameters.AddWithValue("@degistir", 0.0);
            cmd.Parameters.AddWithValue("@kuryeID", kuryeID);
            cmd.Parameters.AddWithValue("@musAdi", musteriAdi);
            cmd.Parameters.AddWithValue("@musTelefon", musteriTelefon);

            if (AnaSinif.con.State == ConnectionState.Closed) AnaSinif.con.Open();

            if (yeniSiparis)
            {
                AnaID = Convert.ToInt32(cmd.ExecuteScalar());
            }
            else
            {
                cmd.ExecuteNonQuery();
            }

            if (AnaSinif.con.State == ConnectionState.Open) AnaSinif.con.Close();

            // Sipariş detaylarını işleme (ekleme veya güncelleme)
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                int detayID = row.Cells["dgvid"].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells["dgvid"].Value);

                if (detayID == 0) // Yeni ürün ekleniyorsa
                {
                    qry2 = @"Insert into masaDetay (AnaID, proID, adet, fiyat, tumtoplam) 
                     Values (@AnaID, @proID, @adet, @fiyat, @tumtoplam);";
                }
                else // Mevcut ürün güncelleniyorsa
                {
                    qry2 = @"Update masaDetay 
                     Set proID = @proID, adet = @adet, fiyat = @fiyat, tumtoplam = @tumtoplam 
                     Where detayID = @detayID;";
                }

                SqlCommand cmd2 = new SqlCommand(qry2, AnaSinif.con);
                cmd2.Parameters.AddWithValue("@detayID", detayID);
                cmd2.Parameters.AddWithValue("@AnaID", AnaID);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproID"].Value));
                cmd2.Parameters.AddWithValue("@adet", Convert.ToInt32(row.Cells["dgvQty"].Value));
                cmd2.Parameters.AddWithValue("@fiyat", Convert.ToDouble(row.Cells["dgvPrice"].Value));
                cmd2.Parameters.AddWithValue("@tumtoplam", Convert.ToDouble(row.Cells["dgvAmount"].Value));

                if (AnaSinif.con.State == ConnectionState.Closed) AnaSinif.con.Open();
                cmd2.ExecuteNonQuery();
                if (AnaSinif.con.State == ConnectionState.Open) AnaSinif.con.Close();
            }

            // Yeni sipariş mi yoksa güncelleme mi olduğuna göre mesaj göster
            if (yeniSiparis)
            {
                guna2MessageDialog1.Show("Sipariş başarıyla alındı!");
            }
            else
            {
                guna2MessageDialog1.Show("Sipariş başarıyla güncellendi!");
            }

            // Form alanlarını sıfırla
            guna2DataGridView1.Rows.Clear();
            lblMasa.Text = "";
            lblGarson.Text = "";
            lblToplam.Text = "00";
            lblKuryeIsmı.Text = "";
        }



        public int id = 0;

        private void btnFatura_Click(object sender, EventArgs e)//fatura fonksiyon
        {
            frmFaturaListe frm = new frmFaturaListe();
            frm.Show(); // ShowDialog yerine Show kullan
            frm.FormClosed += (s, ev) =>
            {
                if (frm.mainID > 0)
                {
                    id = frm.mainID;
                    AnaID = frm.mainID;
                    LoadEntries();
                }
            };
        }









        private void btnOdeme_Click_1(object sender, EventArgs e)//ödeme işlemi için yönlendirme
        {
            string durumQuery = $"SELECT durum FROM Anamasa WHERE AnaID = {AnaID}";
            string durum = "";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(durumQuery, con))
                    {
                        durum = cmd.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Durum kontrolü sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Durum kontrolü
            if (durum == "Hazırlanıyor") // Sadece "Hazırlanıyor" durumunda uyarı göster
            {
                var result = MessageBox.Show("Sipariş hazırlık aşamasında. Yine de ödeme yapmak istiyor musunuz?",
                                             "Uyarı",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return; // İşlemi durdur
                }
            }

            // Ödeme ekranına yönlendirme
            frmOdeme odemeFormu = new frmOdeme
            {
                MainID = AnaID,
                faturaTutari = Convert.ToDouble(lblToplam.Text) // Fatura tutarını ödeme ekranına aktar
            };
            odemeFormu.ShowDialog();
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

        private void lblMasa_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}



