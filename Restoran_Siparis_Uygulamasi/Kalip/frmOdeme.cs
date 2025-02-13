﻿using System;
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

        public double faturaTutari;
        public int MainID = 0;

        private void txtAlinanTutar_TextChanged(object sender, EventArgs e)
        {
            double faturaTutari = 0; // Fatura tutarı
            double alinanTutar = 0; // Alınan tutar
            double.TryParse(txtFaturaTutari.Text, out faturaTutari);
            //double paraUstu = 0; // Para üstü veya eksik tutar

            /*double.TryParse(txtFaturaTutari.Text, out faturaTutari);
            double.TryParse(txtAlinanTutar.Text, out alinanTutar);
*/
            if (string.IsNullOrWhiteSpace(txtAlinanTutar.Text) || !double.TryParse(txtAlinanTutar.Text, out alinanTutar))
            {
                txtParaUstu.Text = ""; // Boş bırak
                return; // Hesaplama yapmadan çık
            }

            // Eksik ödeme durumu
            if (alinanTutar < faturaTutari)
            {
                txtParaUstu.Text = $"Eksik: {Math.Abs(faturaTutari - alinanTutar):N2} TL";
            }
            else
            {
                // Para üstü hesaplama
                double paraUstu = alinanTutar - faturaTutari;
                txtParaUstu.Text = $"{paraUstu:N2} TL";
            }
        }


        private void frmCheckout_Load(object sender, EventArgs e)
        {
            txtFaturaTutari.Text = faturaTutari.ToString();
        }

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            double amt = 0; // Fatura tutarı
            double receipt = 0; // Alınan tutar

            double.TryParse(txtFaturaTutari.Text, out amt);
            double.TryParse(txtAlinanTutar.Text, out receipt);

            if (receipt < amt)
            {
                MessageBox.Show("Eksik ödeme yapıldı! Lütfen tam tutarı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // İşlemi durdur
            }

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

                    MessageBox.Show("Ödeme Tamamlandı", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

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




