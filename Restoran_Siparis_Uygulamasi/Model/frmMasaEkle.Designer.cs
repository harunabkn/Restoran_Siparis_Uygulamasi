namespace Restoran_Siparis_Uygulamasi.Model
{
    partial class frmMasaEkle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtIsım = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = global::Restoran_Siparis_Uygulamasi.Properties.Resources.icons8_table_52;
            this.guna2PictureBox1.ImageFlip = Guna.UI2.WinForms.Enums.FlipOrientation.Normal;
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // label1
            // 
            this.label1.Size = new System.Drawing.Size(137, 25);
            this.label1.Text = "Masa Detayları";
            // 
            // btnKaydet
            // 
            this.btnKaydet.CustomizableEdges.TopRight = false;
            this.btnKaydet.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnKaydet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnKaydet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnKaydet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnKaydet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            // 
            // btnCikis
            // 
            this.btnCikis.CustomizableEdges.TopRight = false;
            this.btnCikis.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnCikis.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCikis.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCikis.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCikis.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Size = new System.Drawing.Size(346, 100);
            this.guna2Panel1.TabIndex = 3;
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Location = new System.Drawing.Point(0, 263);
            this.guna2Panel2.Size = new System.Drawing.Size(346, 73);
            this.guna2Panel2.TabIndex = 2;
            // 
            // txtIsım
            // 
            this.txtIsım.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIsım.DefaultText = "";
            this.txtIsım.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtIsım.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtIsım.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtIsım.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtIsım.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtIsım.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtIsım.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtIsım.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtIsım.Location = new System.Drawing.Point(24, 149);
            this.txtIsım.Name = "txtIsım";
            this.txtIsım.PasswordChar = '\0';
            this.txtIsım.PlaceholderText = "";
            this.txtIsım.SelectedText = "";
            this.txtIsım.Size = new System.Drawing.Size(242, 36);
            this.txtIsım.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "İsim";
            // 
            // frmTableAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 336);
            this.Controls.Add(this.txtIsım);
            this.Controls.Add(this.label2);
            this.Name = "frmTableAdd";
            this.Text = "frmTableAdd";
            this.Controls.SetChildIndex(this.guna2Panel1, 0);
            this.Controls.SetChildIndex(this.guna2Panel2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtIsım, 0);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Guna.UI2.WinForms.Guna2TextBox txtIsım;
        private System.Windows.Forms.Label label2;
    }
}