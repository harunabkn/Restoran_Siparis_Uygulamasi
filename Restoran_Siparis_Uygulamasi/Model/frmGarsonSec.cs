using System;
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
    public partial class frmGarsonSec : Form
    {
        public frmGarsonSec()
        {
            InitializeComponent();
            this.KeyDown += frmWaiterSelect_KeyDown; // KeyDown olayını burada bağla
            this.KeyPreview = true;
        }

        private void frmWaiterSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); // Escape tuşuna basıldığında form kapanır
            }
        }

        public string garsonAdi;

        private void frmWaiterSelect_Load(object sender, EventArgs e)
        {
            string qry = "Select * from Personel where pRol = 'Garson'";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            DataTable veri = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(veri);

            foreach (DataRow row in veri.Rows)
            {
                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                b.Text = row["pAdi"].ToString();
                b.Width = 150;
                b.Height = 50;
                b.FillColor = Color.FromArgb(241, 85, 126);
                b.HoverState.FillColor = Color.FromArgb(50, 55, 89);

                b.Click += new EventHandler(b_Click);

                flowLayoutPanel1.Controls.Add(b);
            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            garsonAdi = (sender as Guna.UI2.WinForms.Guna2Button).Text.ToString();
            this.Close();
        }
    }
}
