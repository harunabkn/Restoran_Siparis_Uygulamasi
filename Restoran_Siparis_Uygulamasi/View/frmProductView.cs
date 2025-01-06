using Restoran_Siparis_Uygulamasi.Model;
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
    public partial class frmProductView : sampleView
    {
        public frmProductView()
        {
            InitializeComponent();
        }
        private readonly string connectionString = "Data Source=WIN-JF9UFIAIC1K\\SQLEXPRESS; Initial Catalog=DbRestoranSiparis; Integrated Security=True; TrustServerCertificate=True;";


        private void frmProductView_Load(object sender, EventArgs e)
        {
            GetData();
            UrunList();
        }
        public void GetData()
        {
            string qry = "select u.UrunID,u.UrunAdi,u.uFiyat,k.kategoriID,k.kategoryAdi from Urunler u inner join Kategori k on k.kategoriID = u.UrunID where UrunAdi like '+" + txtAra.Text + "%'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPrice);
            lb.Items.Add(dgvcatID);
            lb.Items.Add(dgvcat);
            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }

        private void UrunList()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Urunler";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                guna2DataGridView1.DataSource = dataTable;
            }
        }


        public override void btnEkle_Click(object sender, EventArgs e)
        {
            frmProductAdd frm = new frmProductAdd();
            frm.ShowDialog();
            GetData();
            UrunList();

        }

        public override void txtAra_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtAra.Text;
            string query = "SELECT * FROM Urunler WHERE UrunAdi LIKE @aranan";

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
                frmProductAdd frm = new frmProductAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.cID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvcatID"].Value);
                frm.ShowDialog();
                GetData();
                UrunList();
            }
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                if (guna2MessageDialog1.Show("Silmek istediğinizden emin misin?") == DialogResult.Yes)
                {

                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string qry = "Delete from Urunler where UrunID= " + id + "";
                    Hashtable ht = new Hashtable();
                    MainClass.SQL(qry, ht);


                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Başarıyla Silindi...");
                    UrunList();

                }
            }

        }

    }
}
