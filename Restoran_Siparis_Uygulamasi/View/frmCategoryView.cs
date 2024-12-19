using Restoran_Siparis_Uygulamasi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restoran_Siparis_Uygulamasi.View
{
    public partial class frmCategoryView : sampleView
    {
        public frmCategoryView()
        {
            InitializeComponent();
        }
        public override void btnEkle_Click(object sender, EventArgs e)
        {

        }

        public override void txtAra_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name =="dgvedit")
            {
                frmCategoryAdd frm = new frmCategoryAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtIsım.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvName"].Value);
                frm.ShowDialog();
                //GetData();
            } //devamını sana bıraktım veritabanı kısmı
            
        }
    }
}
