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
    public partial class frmBillList : sampleAdd
    {
        public frmBillList()
        {
            InitializeComponent();
        }
        public int mainID;
        private void frmBillList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            string qry = @"SELECT AnaID, masaAdi, garsonAdi, siparisTuru, durum, toplam 
                   FROM Anamasa";

            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvtable);
            lb.Items.Add(dgvWaiter);
            lb.Items.Add(dgvType);
            lb.Items.Add(dgvStatus);
            lb.Items.Add(dgvTotal);

            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }




        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int sayac = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                sayac++;
                row.Cells[0].Value = sayac;
            }
        }






        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                
                mainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                this.Close();

            }

            
        }
    }
}
