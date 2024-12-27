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
    internal class MainClass
    {
        public static readonly string conection = "Data Source=WIN-JF9UFIAIC1K\\SQLEXPRESS; Initial Catalog=DbRestoranSiparis; Integrated Security=True; TrustServerCertificate=True;";

        public static SqlConnection con = new SqlConnection(conection);


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
                KULLANICI = veri.Rows[0]["klnAdi"].ToString();
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
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if (con.State == ConnectionState.Closed) { con.Open(); }
                res = cmd.ExecuteNonQuery();
                if(con.State == ConnectionState.Open)
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

        public static void LoadData(string qry, DataGridView gridv, ListBox listb)
        {

            gridv.CellFormatting += gv_CellFormatting;
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter veriAdaptor = new SqlDataAdapter(cmd);
                DataTable veri = new DataTable();
                veriAdaptor.Fill(veri);
                for (int i = 0; i < listb.Items.Count; i++)
                {
                    string colNam1 = ((DataGridViewColumn)listb.Items[i]).Name;
                    gridv.Columns[colNam1].DataPropertyName = veri.Columns[i].ToString();
                }
                gridv.DataSource = veri;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
                con.Close();
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



    }
}
