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
    public partial class frmRapor : sayfaModeli
    {
        public frmRapor()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            string qryPersonel = @"
    SELECT 
        garsonAdi AS Personel, 
        COUNT(*) AS SiparisSayisi, 
        SUM(toplam) AS ToplamKazanc 
    FROM 
        Anamasa 
    WHERE 
        garsonAdi IS NOT NULL AND garsonAdi != '' 
    GROUP BY 
        garsonAdi";
            string qryGelir = @"
    SELECT 
        CONVERT(DATE, mtarih) AS Tarih, 
        SUM(toplam) AS GunlukGelir 
    FROM 
        Anamasa 
    GROUP BY 
        CONVERT(DATE, mtarih) 
    ORDER BY 
        Tarih";


            // Personel raporu için listbox ve gridview sütunlarını eşleştir
            ListBox lbPersonel = new ListBox();
            lbPersonel.Items.Add(dgvPersonelName);
            lbPersonel.Items.Add(dgvPersonelSiparis);
            lbPersonel.Items.Add(dgvPersonelKazanc);

            AnaSinif.VeriYukle(qryPersonel, guna2DataGridView1, lbPersonel);

            // Gelir raporu için listbox ve gridview sütunlarını eşleştir
            ListBox lbGelir = new ListBox();
            lbGelir.Items.Add(dgvTarih);
            lbGelir.Items.Add(dgvGelir);

            AnaSinif.VeriYukle(qryGelir, guna2DataGridView2, lbGelir);
        }

        

       

        private void frmRapor_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
