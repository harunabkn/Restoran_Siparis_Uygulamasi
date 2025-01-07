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
    public partial class ekleModeli : Form
    {
        public ekleModeli()
        {
            InitializeComponent();
        }

        public virtual void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public virtual void btnKaydet_Click(object sender, EventArgs e)
        {

        }
    }
}
