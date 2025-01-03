using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Restoran_Siparis_Uygulamasi.Model
{
    public partial class frmAddCustomer : Form
    {
        public frmAddCustomer()
        {
            InitializeComponent();
        }

        private void cbKurye_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public string orderType = "";
        public int driverID = 0;
        public int mainID = 0;

        private void frmAddCustomer_Load(object sender, EventArgs e)
        {
            if (orderType == "Paket")
            {
                lblKurye.Visible = false;
                cbKurye.Visible = false;
            }
        }
    }
}
