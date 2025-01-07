using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Restoran_Siparis_Uygulamasi.View
{
    public partial class frmMutfakSayfa : Form
    {
        public frmMutfakSayfa()
        {
            InitializeComponent();
        }

        private void frmKitchenView_Load(object sender, EventArgs e)
        {
            siparisAl();
        }

        private void siparisAl()
        {
            flowLayoutPanel1.Controls.Clear();
            string qry1 = @"SELECT * FROM Anamasa WHERE durum = 'Hazırlanıyor'";
            SqlCommand cmd1 = new SqlCommand(qry1, AnaSinif.con);
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt1);

            FlowLayoutPanel p1;

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                p1 = new FlowLayoutPanel
                {
                    AutoSize = true,
                    Width = flowLayoutPanel1.Width - 20,
                    FlowDirection = FlowDirection.TopDown,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(10)
                };

                FlowLayoutPanel p2 = new FlowLayoutPanel
                {
                    BackColor = Color.FromArgb(50, 55, 89),
                    AutoSize = true,
                    FlowDirection = FlowDirection.TopDown,
                    Margin = new Padding(0),
                    Padding = new Padding(10)
                };

                Label lb1 = new Label
                {
                    Text = "Masa Adı: " + dt1.Rows[i]["masaAdi"].ToString(),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Margin = new Padding(0, 0, 0, 5),
                    AutoSize = true
                };
                Label lb2 = new Label
                {
                    Text = "Garson Adı: " + dt1.Rows[i]["garsonAdi"].ToString(),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9),
                    Margin = new Padding(0, 0, 0, 5),
                    AutoSize = true
                };
                Label lb3 = new Label
                {
                    Text = "Sipariş Zamanı: " + dt1.Rows[i]["mZaman"].ToString(),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9),
                    Margin = new Padding(0, 0, 0, 5),
                    AutoSize = true
                };
                Label lb4 = new Label
                {
                    Text = "Sipariş Türü: " + dt1.Rows[i]["siparisTuru"].ToString(),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9),
                    Margin = new Padding(0, 0, 0, 10),
                    AutoSize = true
                };

                p2.Controls.Add(lb1);
                p2.Controls.Add(lb2);
                p2.Controls.Add(lb3);
                p2.Controls.Add(lb4);

                p1.Controls.Add(p2);

                int anaID = Convert.ToInt32(dt1.Rows[i]["AnaID"].ToString());

                string qry2 = @"SELECT u.UrunAdi, d.adet FROM masaDetay d
                        INNER JOIN Urunler u ON u.UrunID = d.proID
                        WHERE d.AnaID = " + anaID;

                SqlCommand cmd2 = new SqlCommand(qry2, AnaSinif.con);
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);

                FlowLayoutPanel p3 = new FlowLayoutPanel
                {
                    AutoSize = true,
                    FlowDirection = FlowDirection.TopDown,
                    Margin = new Padding(10, 5, 10, 5)
                };

                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    Label lb5 = new Label
                    {
                        Text = $"{j + 1}. {dt2.Rows[j]["UrunAdi"]} - {dt2.Rows[j]["adet"]} adet",
                        ForeColor = Color.Black,
                        Font = new Font("Segoe UI", 9),
                        Margin = new Padding(5, 2, 3, 2),
                        AutoSize = true
                    };
                    p3.Controls.Add(lb5);
                }

                p1.Controls.Add(p3);

                // Buton ekleme
                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button
                {
                    AutoRoundedCorners = true,
                    Size = new Size(100, 35),
                    FillColor = Color.FromArgb(241, 85, 126),
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.White,
                    Margin = new Padding(10, 5, 10, 5),
                    Text = "Tamamla",
                    Tag = dt1.Rows[i]["AnaID"].ToString()
                };
                b.Click += new EventHandler(b_click);

                p1.Controls.Add(b);

                flowLayoutPanel1.Controls.Add(p1);
            }
        }




        private void b_click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((sender as Guna.UI2.WinForms.Guna2Button).Tag.ToString());

            guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
            guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;

            if (guna2MessageDialog1.Show("Bu siparişi tamamlamak istediğinizden emin misiniz?") == DialogResult.Yes)
            {
                string qry = @"UPDATE Anamasa SET durum = 'Sipariş Hazır' WHERE AnaID = @ID";
                Hashtable ht = new Hashtable { { "@ID", id } };

                if (AnaSinif.SQL(qry, ht) > 0)
                {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Sipariş başarıyla tamamlandı.");
                }
                siparisAl();
            }
        }
    }
}
