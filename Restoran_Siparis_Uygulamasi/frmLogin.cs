﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran_Siparis_Uygulamasi
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            //main class içerisine veritabanı bağlantısı ve kullanıcı doğrulama fonksiyonu (IsValidUser) yazılması gerekli

            if(MainClass.IsValidUser(txtKullanici.Text, txtSifre.Text) == false )
            {
                hataliGiris.Show("Geçersiz kullanıcı adı veya şifre");
                return;
            }
            else
            {
                this.Hide();
                frmMain frm= new frmMain();
                frm.Show();
            }
            // veritabanına örnek bir kullanıcı eklenmesi gerekli test için
        }
    }
}
