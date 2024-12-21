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
    public partial class frmCategoryAdd : sampleAdd
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }

        public int id = 0;
        public override void btnKaydet_Click(object sender, EventArgs e)
        {
            string qry = "";
            if (id == 0)
            {
                qry = "insert into Kategori Values(@Name)";
               // qry = "INSERT INTO Kategori (kategoryAdi) VALUES (@Name)";
            }
            else
            {
                qry = "UPDATE Kategori SET kategoryAdi = @Name WHERE kategoriID = @id";
            } 


            Hashtable ht = new Hashtable();
            ht.Add("@id", id);
            ht.Add("@Name", txtIsım.Text);

            if (MainClass.SQL(qry, ht) > 0)
            {
                guna2MessageDialog1.Show("Başarıyla Kaydedildi...");
                id = 0;
                //txtIsım.Text = "";
                txtIsım.Focus();
            }
            
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
