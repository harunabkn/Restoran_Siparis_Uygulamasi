using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
