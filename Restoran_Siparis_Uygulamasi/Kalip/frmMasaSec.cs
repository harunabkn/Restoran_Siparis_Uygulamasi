using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran_Siparis_Uygulamasi.Model
{
    public partial class frmMasaSec : Form
    {
        public frmMasaSec()
        {
            InitializeComponent();
        }

        public string MasaAdi;

        private void frmTableSelect_Load(object sender, EventArgs e)
        {
            string qry = "Select * from Tablo";
            SqlCommand cmd = new SqlCommand(qry, AnaSinif.con);
            DataTable veri = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(veri);

            foreach (DataRow row in veri.Rows)
            {
                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                b.Text = row["tabloAdi"].ToString();
                b.Width = 150;
                b.Height = 50;
                b.FillColor = Color.FromArgb(241, 85, 126);
                b.HoverState.FillColor = Color.FromArgb(50, 55, 89);

                // Masa durumu kontrolü
                string durumSorgu = @"
            SELECT TOP 1 durum 
            FROM Anamasa 
            WHERE masaAdi = @masaAdi 
            ORDER BY AnaID DESC";

                SqlCommand durumCmd = new SqlCommand(durumSorgu, AnaSinif.con);
                durumCmd.Parameters.AddWithValue("@masaAdi", row["tabloAdi"].ToString());

                if (AnaSinif.con.State == ConnectionState.Closed) AnaSinif.con.Open();
                object result = durumCmd.ExecuteScalar();
                if (AnaSinif.con.State == ConnectionState.Open) AnaSinif.con.Close();

                // Durum kontrolü
                if (result != null)
                {
                    string durum = result.ToString();
                    Debug.WriteLine($"Masa: {row["tabloAdi"]}, Durum: {durum}");

                    if (durum == "Hazırlanıyor" || durum == "Sipariş Hazır")
                    {
                        b.Enabled = false;
                        b.FillColor = Color.Gray; // Devre dışı olduğu belli olsun
                    }
                }
                else
                {
                    Debug.WriteLine($"Masa: {row["tabloAdi"]}, Durum: NULL (Aktif)");
                }

                // Tıklama olayını ekle
                b.Click += new EventHandler(b_Click);
                flowLayoutPanel1.Controls.Add(b);
            }
        }


        private void b_Click(object sender, EventArgs e)
        {
           MasaAdi = (sender as Guna.UI2.WinForms.Guna2Button).Text.ToString();
            this.Close();
        }

        private void frmTableSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Escape tuşuyla formu kapatır
            }
        }



    }
}
