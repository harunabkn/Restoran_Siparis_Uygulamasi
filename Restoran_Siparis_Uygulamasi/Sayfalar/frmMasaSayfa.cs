﻿ using Restoran_Siparis_Uygulamasi.Model;
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

namespace Restoran_Siparis_Uygulamasi.View
{
    public partial class frmMasaSayfa : sayfaModeli
    {
        private readonly string connectionString = "Data Source=HUAWEI\\SQLEXPRESS; Initial Catalog=DbRestoranSiparis; Integrated Security=True; TrustServerCertificate=True;";
        public frmMasaSayfa()
        {
            InitializeComponent();
        }



        public void GetData()
        {
            string qry = "select * from Tablo where tabloAdi like '+" + txtAra.Text + "%'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            AnaSinif.VeriYukle(qry, guna2DataGridView1, lb);
        }
        private void MasaList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tablo";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                guna2DataGridView1.DataSource = dataTable;
            }
        }

        public override void btnEkle_Click(object sender, EventArgs e)
        {
            frmMasaEkle frm = new frmMasaEkle();
            frm.ShowDialog();
            GetData();
            MasaList();

        }

        public override void txtAra_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtAra.Text;
            string query = "SELECT * FROM Tablo WHERE tabloAdi LIKE @aranan";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@aranan", aranan + "%");

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                guna2DataGridView1.DataSource = dataTable;
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmKategoriEkle frm = new frmKategoriEkle();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtIsım.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);
                frm.ShowDialog();
                GetData();
                MasaList();
            }
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                if (guna2MessageDialog1.Show("Silmek istediğinizden emin misin?") == DialogResult.Yes)
                {

                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string qry = "Delete from Tablo where tabloID= " + id + "";
                    Hashtable ht = new Hashtable();
                    AnaSinif.SQL(qry, ht);


                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Başarıyla Silindi...");
                    MasaList();

                }
            }

        }
        private void frmTableView_Load_1(object sender, EventArgs e)
        {
            GetData();
            MasaList();

        }
    }
}
