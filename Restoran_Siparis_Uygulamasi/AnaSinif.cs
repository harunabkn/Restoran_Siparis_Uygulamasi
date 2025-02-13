﻿using Restoran_Siparis_Uygulamasi.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran_Siparis_Uygulamasi
{
    internal class AnaSinif
    {
        //Hangi SQL Server'a bağlanacak  kullanacağı veritabanı
        public static readonly string conection = "Data Source=HUAWEI\\SQLEXPRESS; Initial Catalog=DbRestoranSiparis; Integrated Security=True; TrustServerCertificate=True;";

        //sql server ile bağlantı kurmak için bir nesne oluşturur connection kullanarak
        public static SqlConnection con = new SqlConnection(conection);



        //kullanıcı adı ve şifre kontrolü yapılır 
        public static bool KullaniciKontrol(string kullanici,string sifre)
        {
            bool kontrol = false;
            string qry = @"Select * from Kullanicilar where kullaniciAdi ='" + kullanici + "' and kullaniciSifre ='"+ sifre + "' "; 
            SqlCommand cmd = new SqlCommand(qry, con);
            DataTable veri = new DataTable();
            SqlDataAdapter veriAdaptor = new SqlDataAdapter(cmd);
            veriAdaptor.Fill(veri);

            if (veri.Rows.Count > 0)
            {
                kontrol = true;
                KULLANICI = veri.Rows[0]["klnRol"].ToString();
            }

            return kontrol;
        }



        public static string kullanici;
        public static string KULLANICI
        {
            get { return kullanici; }
            private set { kullanici = value; }
        }

        public static int SQL(string qry, Hashtable ht)
        {
            int res = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;

                foreach (DictionaryEntry item in ht)
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);//SQL enjeksiyon saldırılarına karşı koruma sağlar
                }
                if (con.State == ConnectionState.Closed) { con.Open(); }//bağlantının açık olup olmadığı kontrol edilir açık değilse açar
                res = cmd.ExecuteNonQuery();
                if(con.State == ConnectionState.Open)//bağlantı açıksa kapatır
                {
                    con.Close();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
                con.Close();
            }
            return res;
        }

        public static void VeriYukle(string qry, DataGridView gridv, ListBox listb)
        {
            // Null kontrolleri
            if (gridv == null)
                throw new ArgumentNullException(nameof(gridv), "DataGridView nesnesi Boş.");

            if (listb == null)
                throw new ArgumentNullException(nameof(listb), "ListBox nesnesi Boş.");

            if (listb.Items.Count == 0)
            {
                MessageBox.Show("ListBox içinde herhangi bir öğe bulunmuyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Veritabanı sorgusu
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter veriAdaptor = new SqlDataAdapter(cmd);
                DataTable veri = new DataTable();
                veriAdaptor.Fill(veri);

                // Sütun eşlemesi ve veri kaynağı
                for (int i = 0; i < listb.Items.Count; i++)
                {
                    string colNam1 = ((DataGridViewColumn)listb.Items[i]).Name;

                    if (!gridv.Columns.Contains(colNam1))
                    {
                        MessageBox.Show($"'{colNam1}' adında bir sütun bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue; // Döngünün bir sonraki iterasyonuna geç
                    }

                    gridv.Columns[colNam1].DataPropertyName = veri.Columns[i].ToString();
                }

                gridv.DataSource = veri;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close(); // Bağlantıyı kapat
                }
            }
        }


        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView gridv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int sayac = 0;
            foreach(DataGridViewRow row in gridv.Rows)
            {
                sayac++;
                row.Cells[0].Value = sayac;
            }
        }


        public static void CBFill(string qry, ComboBox cb)
        {
            using (SqlCommand cmd = new SqlCommand(qry, AnaSinif.con))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable veri = new DataTable();
                adapter.Fill(veri);

                cb.DataSource = veri;           // Veriyi ComboBox'a bağla
                cb.DisplayMember = "Adı";       // Görüntülenecek sütun adı (Alias: 'Adı')
                cb.ValueMember = "id";          // Seçilen değerin tutulacağı sütun (Alias: 'id')
                cb.SelectedIndex = -1;          // Varsayılan olarak hiçbir şey seçilmesin
            }
        }


        internal static void BlurBackground(Form frm)
        {
            // Overlay Form Oluşturma
            Form overlayForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                Opacity = 0.5,
                BackColor = Color.Black,
                Size = Screen.PrimaryScreen.Bounds.Size,
                StartPosition = FormStartPosition.Manual,
                Location = Screen.PrimaryScreen.Bounds.Location,
                TopMost = true,
                ShowInTaskbar = false
            };

            try
            {
                overlayForm.Show(); // Overlay'i göster
                Application.DoEvents(); // Ekranı güncellemek için

                frm.TopMost = true; // Ana formu overlay'in üstünde göstermek için
                frm.Show(); // Modal olmayan şekilde formu aç
                frm.FormClosed += (s, e) => overlayForm.Close(); // Form kapanınca overlay'i kapat
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                overlayForm.Dispose(); // Kaynakları temizle
            }
        }




    }
}

