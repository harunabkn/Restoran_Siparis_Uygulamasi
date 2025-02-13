﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran_Siparis_Uygulamasi.Model
{
    public partial class frmMasaEkle : ekleModeli
    {
        public frmMasaEkle()
        {
            InitializeComponent();
        }

        public int id = 0;

        public override void btnKaydet_Click(object sender, EventArgs e)
        {
            string qry = "";
            if (id == 0)
            {
                qry = "insert into Tablo Values(@Name)";
                // qry = "INSERT INTO Kategori (kategoryAdi) VALUES (@Name)";
            }
            else
            {
                qry = "UPDATE Tablo SET tabloAdi = @Name WHERE tabloID = @id";
            }


            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtIsım.Text);

            if (AnaSinif.SQL(qry, ht) > 0)
            {
                guna2MessageDialog1.Show("Başarıyla Kaydedildi...");
                id = 0;
                //txtIsım.Text = "";
                txtIsım.Focus();
            }

        }
    }
}
