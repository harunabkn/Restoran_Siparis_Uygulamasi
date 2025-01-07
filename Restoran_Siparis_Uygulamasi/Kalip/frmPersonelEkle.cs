using System;
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
    public partial class frmPersonelEkle : ekleModeli
    {
        public frmPersonelEkle()
        {
            InitializeComponent();
        }

        public int id = 0;

        private void frmStaffAdd_Load(object sender, EventArgs e)
        {
            
        }

        public override void btnKaydet_Click(object sender, EventArgs e)
        {
            string qry = "";
            if (id == 0)
            {
                qry = "insert into Personel Values(@Name ,@numara ,@rol)";
                // qry = "INSERT INTO Kategori (kategoryAdi) VALUES (@Name)";
            }
            else
            {
                qry = "UPDATE Personel SET pAdi = @Name,pNumara = @numara,pRol = @rol WHERE personelID = @id";
            }


            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtIsım.Text);
            ht.Add("@numara", txtTelefon.Text);
            ht.Add("@rol", cbPozisyon.Text);

            if (AnaSinif.SQL(qry, ht) > 0)
            {
                guna2MessageDialog1.Show("Başarıyla Kaydedildi...");
                id = 0;
                txtIsım.Text = "";
                txtTelefon.Text = "";
                cbPozisyon.SelectedIndex = -1;
                txtIsım.Focus();
            }

        }

    }
}
