namespace CHEMLINK.Views
{
    partial class AddSupplierForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblNama;
        public System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.Label lblKontakPerson;
        public System.Windows.Forms.TextBox txtKontakPerson;
        private System.Windows.Forms.Label lblNoTelp;
        public System.Windows.Forms.TextBox txtNoTelp;
        private System.Windows.Forms.Label lblEmail;
        public System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblAlamat;
        public System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.Label lblKota;
        public System.Windows.Forms.TextBox txtKota;
        private System.Windows.Forms.Panel pnlBottom;
        public System.Windows.Forms.Button btnTambah;
        public System.Windows.Forms.Button btnBatal;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblNama = new System.Windows.Forms.Label();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.lblKontakPerson = new System.Windows.Forms.Label();
            this.txtKontakPerson = new System.Windows.Forms.TextBox();
            this.lblNoTelp = new System.Windows.Forms.Label();
            this.txtNoTelp = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblAlamat = new System.Windows.Forms.Label();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.lblKota = new System.Windows.Forms.Label();
            this.txtKota = new System.Windows.Forms.TextBox();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(500, 70);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lblTitle.Size = new System.Drawing.Size(350, 70);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tambah Supplier Baru";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNama
            // 
            this.lblNama.AutoSize = true;
            this.lblNama.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNama.Location = new System.Drawing.Point(24, 82);
            this.lblNama.Name = "lblNama";
            this.lblNama.Size = new System.Drawing.Size(120, 17);
            this.lblNama.TabIndex = 0;
            this.lblNama.Text = "Nama Perusahaan";
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(24, 102);
            this.txtNama.Name = "txtNama";
            this.txtNama.PlaceholderText = "Masukkan nama perusahaan";
            this.txtNama.Size = new System.Drawing.Size(450, 25);
            this.txtNama.TabIndex = 1;
            // 
            // lblKontakPerson
            // 
            this.lblKontakPerson.AutoSize = true;
            this.lblKontakPerson.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKontakPerson.Location = new System.Drawing.Point(24, 137);
            this.lblKontakPerson.Name = "lblKontakPerson";
            this.lblKontakPerson.Size = new System.Drawing.Size(98, 17);
            this.lblKontakPerson.TabIndex = 2;
            this.lblKontakPerson.Text = "Kontak Person";
            // 
            // txtKontakPerson
            // 
            this.txtKontakPerson.Location = new System.Drawing.Point(24, 157);
            this.txtKontakPerson.Name = "txtKontakPerson";
            this.txtKontakPerson.PlaceholderText = "Masukkan nama kontak person";
            this.txtKontakPerson.Size = new System.Drawing.Size(215, 25);
            this.txtKontakPerson.TabIndex = 3;
            // 
            // lblNoTelp
            // 
            this.lblNoTelp.AutoSize = true;
            this.lblNoTelp.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNoTelp.Location = new System.Drawing.Point(259, 137);
            this.lblNoTelp.Name = "lblNoTelp";
            this.lblNoTelp.Size = new System.Drawing.Size(90, 17);
            this.lblNoTelp.TabIndex = 4;
            this.lblNoTelp.Text = "No. Telepon";
            // 
            // txtNoTelp
            // 
            this.txtNoTelp.Location = new System.Drawing.Point(259, 157);
            this.txtNoTelp.Name = "txtNoTelp";
            this.txtNoTelp.PlaceholderText = "Masukkan nomor telepon";
            this.txtNoTelp.Size = new System.Drawing.Size(215, 25);
            this.txtNoTelp.TabIndex = 5;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Location = new System.Drawing.Point(24, 192);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(42, 17);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(24, 212);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "Masukkan email";
            this.txtEmail.Size = new System.Drawing.Size(450, 25);
            this.txtEmail.TabIndex = 7;
            // 
            // lblAlamat
            // 
            this.lblAlamat.AutoSize = true;
            this.lblAlamat.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAlamat.Location = new System.Drawing.Point(24, 247);
            this.lblAlamat.Name = "lblAlamat";
            this.lblAlamat.Size = new System.Drawing.Size(52, 17);
            this.lblAlamat.TabIndex = 8;
            this.lblAlamat.Text = "Alamat";
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(24, 267);
            this.txtAlamat.Multiline = true;
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.PlaceholderText = "Masukkan alamat lengkap";
            this.txtAlamat.Size = new System.Drawing.Size(450, 55);
            this.txtAlamat.TabIndex = 9;
            // 
            // lblKota
            // 
            this.lblKota.AutoSize = true;
            this.lblKota.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKota.Location = new System.Drawing.Point(24, 332);
            this.lblKota.Name = "lblKota";
            this.lblKota.Size = new System.Drawing.Size(35, 17);
            this.lblKota.TabIndex = 10;
            this.lblKota.Text = "Kota";
            // 
            // txtKota
            // 
            this.txtKota.Location = new System.Drawing.Point(24, 352);
            this.txtKota.Name = "txtKota";
            this.txtKota.PlaceholderText = "Masukkan kota";
            this.txtKota.Size = new System.Drawing.Size(450, 25);
            this.txtKota.TabIndex = 11;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.btnTambah);
            this.pnlBottom.Controls.Add(this.btnBatal);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 400);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(500, 60);
            this.pnlBottom.TabIndex = 12;
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(260, 13);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(110, 35);
            this.btnTambah.TabIndex = 0;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = false;
            // 
            // btnBatal
            // 
            this.btnBatal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBatal.ForeColor = System.Drawing.Color.White;
            this.btnBatal.Location = new System.Drawing.Point(380, 13);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(100, 35);
            this.btnBatal.TabIndex = 1;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = false;
            this.btnBatal.Click += new System.EventHandler((s, e) => { this.DialogResult = System.Windows.Forms.DialogResult.Cancel; this.Close(); });
            // 
            // AddSupplierForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(500, 460);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.txtKota);
            this.Controls.Add(this.lblKota);
            this.Controls.Add(this.txtAlamat);
            this.Controls.Add(this.lblAlamat);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtNoTelp);
            this.Controls.Add(this.lblNoTelp);
            this.Controls.Add(this.txtKontakPerson);
            this.Controls.Add(this.lblKontakPerson);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.lblNama);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddSupplierForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tambah Supplier";
            this.pnlHeader.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
