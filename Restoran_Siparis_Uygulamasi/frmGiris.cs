using System;
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
    public partial class frmGiris : Form
    {
        public frmGiris()
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

            if (AnaSinif.KullaniciKontrol(txtKullanici.Text, txtSifre.Text) == false )
            {
                hataliGiris.Show("Geçersiz kullanıcı adı veya şifre");
                return;
            }
            else
            {
                this.Hide();
                frmAnasayfa frm= new frmAnasayfa();
                frm.Show();
            }
            // veritabanına örnek bir kullanıcı eklenmesi gerekli test için
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)//enter tuşuna basılınca giriş butonunun tetiklenmesini sağğlamak için fonksiyon
        {
            if (keyData == Keys.Enter)
            {
                btnGiris.PerformClick(); // Giriş butonunun tıklama olayını tetikleme
                return true; // Tuşun işlendiğini bildirir
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
