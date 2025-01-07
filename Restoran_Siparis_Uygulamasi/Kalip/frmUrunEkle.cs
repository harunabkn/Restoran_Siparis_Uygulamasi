using Guna.UI2.WinForms;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Restoran_Siparis_Uygulamasi.Model
{
    public partial class frmUrunEkle : ekleModeli
    {
        public frmUrunEkle()
        {
            InitializeComponent();
        }

        public int id = 0;
        public int cID = 0;
        private readonly string connectionString = "Data Source=HUAWEI\\SQLEXPRESS; Initial Catalog=DbRestoranSiparis; Integrated Security=True; TrustServerCertificate=True;";


        private void frmProductAdd_Load(object sender, EventArgs e)
        {
            string qry = "SELECT kategoriID, kategoryAdi FROM Kategori";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(qry, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Yeni bir satır ekle: Boş seçim için
                DataRow row = dt.NewRow();
                row["kategoriID"] = 0; // Varsayılan bir ID (kullanılmayan bir ID)
                row["kategoryAdi"] = "Seçiniz"; // Gösterilecek metin
                dt.Rows.InsertAt(row, 0); // İlk sıraya ekle

                // ComboBox'a veri bağlama
                cbKat.DataSource = dt;
                cbKat.DisplayMember = "kategoryAdi"; // Görüntülenecek değer
                cbKat.ValueMember = "kategoriID";    // Seçilen değer
            }

            // Güncelleme durumu için
            if (cID > 0)
            {
                cbKat.SelectedValue = cID; // Güncellenen kategori
            }
            else
            {
                cbKat.SelectedIndex = 0; // Varsayılan olarak "Seçiniz"
            }
        }


        string filePath;
        Byte[] imageByteArray;

        private void btnAra_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png)|* .png; *.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                txtImage.Image = new Bitmap(filePath);
                
            }
        }



        public override void btnKaydet_Click(object sender, EventArgs e)
        {
            string qry = "";
            if (id == 0)
            {
                qry = "insert into Urunler Values(@Name ,@fiyat ,@kat,@img)";
                // qry = "INSERT INTO Kategori (kategoryAdi) VALUES (@Name)";
            }
            else
            {
                qry = "UPDATE Urunler SET UrunAdi = @Name, uFiyat = @fiyat, kategoriID = @kat, uResim = @img WHERE UrunID = @id";
            }


            Image temp = new Bitmap(txtImage.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imageByteArray = ms.ToArray();



            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtIsım.Text);
            ht.Add("@fiyat", txtFiyat.Text);
            ht.Add("@kat", Convert.ToInt32(cbKat.SelectedValue));
            ht.Add("@img", imageByteArray);
            if (AnaSinif.SQL(qry, ht) > 0)
            {
                guna2MessageDialog1.Show("Başarıyla Kaydedildi...");
                id = 0;
                cID = 0;
                txtIsım.Text = "";
                txtFiyat.Text = "";
                cbKat.SelectedIndex = 0;
                cbKat.SelectedIndex = -1;
                txtImage.Image = Restoran_Siparis_Uygulamasi.Properties.Resources.icons8_shopping_mall_100;
                txtIsım.Focus();
            }

        }


        private void ForUpdateLoadData()
        {
            string qry = @"Select * from Urunler where UrunID = " + id + "";
            SqlCommand cmd = new SqlCommand(qry, AnaSinif.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtIsım.Text = dt.Rows[0]["UrunAdi"].ToString();
                txtFiyat.Text = dt.Rows[0]["uFiyat"].ToString();

                Byte[] imageArray = (byte[])(dt.Rows[0]["uresim"]);
                byte[] imageByteArray = imageArray;
                txtImage.Image = Image.FromStream(new MemoryStream(imageArray));
                
            }
        }


    }
}
