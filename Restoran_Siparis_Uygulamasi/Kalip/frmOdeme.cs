using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran_Siparis_Uygulamasi.Model
{
    public partial class frmOdeme : ekleModeli
    {
        public frmOdeme()
        {
            InitializeComponent();
        }
        private readonly string connectionString = "Data Source=HUAWEI\\SQLEXPRESS; Initial Catalog=DbRestoranSiparis; Integrated Security=True; TrustServerCertificate=True;";

        public double amt;
        public int MainID = 0;

        private void txtAlinanTutar_TextChanged(object sender, EventArgs e)
        {
            double amt = 0;
            double receipt = 0;
            double change = 0;

            double.TryParse(txtFaturaTutari.Text, out amt);
            double.TryParse(txtAlinanTutar.Text, out receipt);

            change = Math.Abs(amt - receipt);

            txtParaUstu.Text = change.ToString();
        }

        private void frmCheckout_Load(object sender, EventArgs e)
        {
            txtFaturaTutari.Text = amt.ToString();
        }

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            if (MainID > 0)
            {
                string updateQuery = $"UPDATE Anamasa SET durum = 'Ödendi' WHERE AnaID = {MainID}";

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Fatura Ödendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Fatura listesini yenile
                    frmFaturaListe billList = Application.OpenForms.OfType<frmFaturaListe>().FirstOrDefault();
                    if (billList != null)
                    {
                        billList.LoadData(); // Mevcut verileri tekrar yükle
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Geçerli bir fatura seçilmedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



    }
}





/* string qry = @" Update Anamasa set toplam = @total, kabul = @rec, degistir = @change, 
                   durum = 'Paid' where AnaID = @id";

           Hashtable ht = new Hashtable();
           ht.Add("@id", MainID);
           ht.Add("@total", txtFaturaTutari.Text);
           ht.Add("@rec", txtAlinanTutar.Text);
           ht.Add("@change", txtParaUstu.Text);

           if (MainClass.SQL(qry, ht) > 0)
           {
               guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
               guna2MessageDialog1.Show("Başarıyla Kaydedildi");
               this.Close();
           }*/