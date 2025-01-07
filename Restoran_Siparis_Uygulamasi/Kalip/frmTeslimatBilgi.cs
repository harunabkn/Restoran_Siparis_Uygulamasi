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
    public partial class frmTeslimatBilgi : Form
    {
        public frmTeslimatBilgi()
        {
            InitializeComponent();
        }

        private void cbKurye_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbKurye.SelectedValue != null && int.TryParse(cbKurye.SelectedValue.ToString(), out int selectedKuryeID))
            {
                kuryeID = selectedKuryeID;
            }
            else
            {
                Console.WriteLine("SelectedValue null veya dönüştürülemiyor.");
            }
        }


        public string siparisTuru = "";
        public int kuryeID = 0;
        public string musAdi = "";
        public int masaID = 0;

        private void frmAddCustomer_Load(object sender, EventArgs e)
        {
            if (siparisTuru == "Paket")
            {
                lblKurye.Visible = false;
                cbKurye.Visible = false;
            }

            string qry = "Select personelID 'id',pAdi 'Adı' from Personel where pRol = 'Kurye'";
            AnaSinif.CBFill(qry, cbKurye);

            if(masaID > 0)
            {
                cbKurye.SelectedValue = kuryeID;
            }
        }
    }
}
