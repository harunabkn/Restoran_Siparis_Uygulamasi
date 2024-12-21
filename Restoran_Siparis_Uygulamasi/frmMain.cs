using Restoran_Siparis_Uygulamasi.View;
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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public void AddControls(Form f)
        {
            ControlsPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            ControlsPanel.Controls.Add(f);
            f.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblKullanici.Text = MainClass.KULLANICI;
        }

        private void btnAnasayfa_Click(object sender, EventArgs e)
        {
            AddControls(new frmHome());
        }

        private void btnKategoriler_Click(object sender, EventArgs e)
        {
            AddControls(new frmCategoryView());
        }

        private void btnMasalar_Click(object sender, EventArgs e)
        {
            AddControls(new frmTableView());
        }
    }
}
