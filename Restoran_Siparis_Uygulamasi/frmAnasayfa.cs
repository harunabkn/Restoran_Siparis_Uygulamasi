using Guna.UI2.WinForms;
using Restoran_Siparis_Uygulamasi.Model;
using Restoran_Siparis_Uygulamasi.View;
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
    public partial class frmAnasayfa : Form
    {
        static frmAnasayfa _obj;

        public static object Instance { get; internal set; }

        public frmAnasayfa()
        {
            InitializeComponent();
        }




        public void AddControls(Form f)
        {
            ControlsPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            ControlsPanel.Controls.Add(f);
            f.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblKullanici.Text = AnaSinif.KULLANICI;
            
        }

        private void btnAnasayfa_Click(object sender, EventArgs e)
        {
            if (AnaSinif.KULLANICI == "Müdür") // Kullanıcının rolü Admin mi kontrol edilir
            {
                // Admin yetkisi varsa Rapor ekranını aç
                AddControls(new frmRapor());
            }
            else
            {
                // Guna2MessageDialog kullanarak uyarı mesajını göster
                Guna2MessageDialog messageDialog = new Guna2MessageDialog
                {
                    Buttons = MessageDialogButtons.OK,
                    Icon = MessageDialogIcon.Warning,
                    Text = "Bu bölüme erişim yetkiniz yok!",
                    Caption = "Yetki Hatası",
                    Style = MessageDialogStyle.Light // İsteğe bağlı: Light veya Dark tasarım
                };

                messageDialog.Show();
            }
            
        }

        private void btnKategoriler_Click(object sender, EventArgs e)
        {
            AddControls(new frmKategoriSayfa());
        }

        private void btnMasalar_Click(object sender, EventArgs e)
        {
            AddControls(new frmMasaSayfa());
        }

        private void btnOdeme_Click(object sender, EventArgs e)
        {
            frmSiparis frm = new frmSiparis();
            frm.Show();
        }

        private void btnPersonel_Click(object sender, EventArgs e)
        {

           
            if (AnaSinif.KULLANICI == "Müdür") // Kullanıcının rolü Admin mi kontrol edilir
            {
                // Admin yetkisi varsa Personel ekranını aç
                AddControls(new frmPersonelSayfa());
            }
            else
            {
                // Guna2MessageDialog kullanarak uyarı mesajını göster
                Guna2MessageDialog messageDialog = new Guna2MessageDialog
                {
                    Buttons = MessageDialogButtons.OK,
                    Icon = MessageDialogIcon.Warning,
                    Text = "Bu bölüme erişim yetkiniz yok!",
                    Caption = "Yetki Hatası",
                    Style = MessageDialogStyle.Light // İsteğe bağlı: Light veya Dark tasarım
                };

                messageDialog.Show();
            }

        }

        private void btnUrunler_Click(object sender, EventArgs e)
        {
            AddControls(new frmUrunSayfa());
        }

        private void btnMutfak_Click(object sender, EventArgs e)
        {
            AddControls(new frmMutfakSayfa());
        }
    }
}
