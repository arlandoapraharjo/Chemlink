namespace CHEMLINK.Views
{
    partial class UserForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblUsername;
        public System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        public System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblRole;
        public System.Windows.Forms.ComboBox cbRole;
        private System.Windows.Forms.Label lblNoTelp;
        public System.Windows.Forms.TextBox txtNoTelp;
        private System.Windows.Forms.Label lblEmail;
        public System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblAlamat;
        public System.Windows.Forms.TextBox txtAlamat;
        private System.Windows.Forms.Label lblKota;
        public System.Windows.Forms.TextBox txtKota;
        private System.Windows.Forms.Label lblKecamatan;
        public System.Windows.Forms.TextBox txtKecamatan;
        public System.Windows.Forms.DataGridView dgvReference;
        public System.Windows.Forms.Button btnSubmit;
        public System.Windows.Forms.Button btnBatal;
        private System.Windows.Forms.Panel pnlBottom;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cbRole = new System.Windows.Forms.ComboBox();
            this.lblNoTelp = new System.Windows.Forms.Label();
            this.txtNoTelp = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblAlamat = new System.Windows.Forms.Label();
            this.txtAlamat = new System.Windows.Forms.TextBox();
            this.lblKota = new System.Windows.Forms.Label();
            this.txtKota = new System.Windows.Forms.TextBox();
            this.lblKecamatan = new System.Windows.Forms.Label();
            this.txtKecamatan = new System.Windows.Forms.TextBox();
            this.dgvReference = new System.Windows.Forms.DataGridView();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReference)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(850, 70);
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
            this.lblTitle.Size = new System.Drawing.Size(300, 70);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tambah User Baru";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlLeft
            // 
            this.pnlLeft.AutoScroll = true;
            this.pnlLeft.Controls.Add(this.lblUsername);
            this.pnlLeft.Controls.Add(this.txtUsername);
            this.pnlLeft.Controls.Add(this.lblPassword);
            this.pnlLeft.Controls.Add(this.txtPassword);
            this.pnlLeft.Controls.Add(this.lblRole);
            this.pnlLeft.Controls.Add(this.cbRole);
            this.pnlLeft.Controls.Add(this.lblNoTelp);
            this.pnlLeft.Controls.Add(this.txtNoTelp);
            this.pnlLeft.Controls.Add(this.lblEmail);
            this.pnlLeft.Controls.Add(this.txtEmail);
            this.pnlLeft.Controls.Add(this.lblAlamat);
            this.pnlLeft.Controls.Add(this.txtAlamat);
            this.pnlLeft.Controls.Add(this.lblKota);
            this.pnlLeft.Controls.Add(this.txtKota);
            this.pnlLeft.Controls.Add(this.lblKecamatan);
            this.pnlLeft.Controls.Add(this.txtKecamatan);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 70);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(20, 15, 10, 10);
            this.pnlLeft.Size = new System.Drawing.Size(350, 420);
            this.pnlLeft.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblUsername.Location = new System.Drawing.Point(20, 15);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(70, 17);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(20, 35);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PlaceholderText = "Masukkan username";
            this.txtUsername.Size = new System.Drawing.Size(310, 25);
            this.txtUsername.TabIndex = 1;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPassword.Location = new System.Drawing.Point(20, 68);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(67, 17);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(20, 88);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '\u2022';
            this.txtPassword.PlaceholderText = "Masukkan password";
            this.txtPassword.Size = new System.Drawing.Size(310, 25);
            this.txtPassword.TabIndex = 3;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblRole.Location = new System.Drawing.Point(20, 121);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(35, 17);
            this.lblRole.TabIndex = 4;
            this.lblRole.Text = "Role";
            // 
            // cbRole
            // 
            this.cbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRole.FormattingEnabled = true;
            this.cbRole.Items.AddRange(new object[] { "Admin", "Kasir" });
            this.cbRole.Location = new System.Drawing.Point(20, 141);
            this.cbRole.Name = "cbRole";
            this.cbRole.Size = new System.Drawing.Size(310, 25);
            this.cbRole.TabIndex = 5;
            // 
            // lblNoTelp
            // 
            this.lblNoTelp.AutoSize = true;
            this.lblNoTelp.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNoTelp.Location = new System.Drawing.Point(20, 174);
            this.lblNoTelp.Name = "lblNoTelp";
            this.lblNoTelp.Size = new System.Drawing.Size(90, 17);
            this.lblNoTelp.TabIndex = 6;
            this.lblNoTelp.Text = "No. Telepon";
            // 
            // txtNoTelp
            // 
            this.txtNoTelp.Location = new System.Drawing.Point(20, 194);
            this.txtNoTelp.Name = "txtNoTelp";
            this.txtNoTelp.PlaceholderText = "Masukkan nomor telepon";
            this.txtNoTelp.Size = new System.Drawing.Size(310, 25);
            this.txtNoTelp.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Location = new System.Drawing.Point(20, 227);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(42, 17);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(20, 247);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "Masukkan email";
            this.txtEmail.Size = new System.Drawing.Size(310, 25);
            this.txtEmail.TabIndex = 9;
            // 
            // lblAlamat
            // 
            this.lblAlamat.AutoSize = true;
            this.lblAlamat.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAlamat.Location = new System.Drawing.Point(20, 280);
            this.lblAlamat.Name = "lblAlamat";
            this.lblAlamat.Size = new System.Drawing.Size(52, 17);
            this.lblAlamat.TabIndex = 10;
            this.lblAlamat.Text = "Alamat";
            // 
            // txtAlamat
            // 
            this.txtAlamat.Location = new System.Drawing.Point(20, 300);
            this.txtAlamat.Multiline = true;
            this.txtAlamat.Name = "txtAlamat";
            this.txtAlamat.PlaceholderText = "Masukkan alamat lengkap";
            this.txtAlamat.Size = new System.Drawing.Size(310, 45);
            this.txtAlamat.TabIndex = 11;
            // 
            // lblKota
            // 
            this.lblKota.AutoSize = true;
            this.lblKota.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKota.Location = new System.Drawing.Point(20, 353);
            this.lblKota.Name = "lblKota";
            this.lblKota.Size = new System.Drawing.Size(35, 17);
            this.lblKota.TabIndex = 12;
            this.lblKota.Text = "Kota";
            // 
            // txtKota
            // 
            this.txtKota.Location = new System.Drawing.Point(20, 373);
            this.txtKota.Name = "txtKota";
            this.txtKota.PlaceholderText = "Masukkan kota";
            this.txtKota.Size = new System.Drawing.Size(148, 25);
            this.txtKota.TabIndex = 13;
            // 
            // lblKecamatan
            // 
            this.lblKecamatan.AutoSize = true;
            this.lblKecamatan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblKecamatan.Location = new System.Drawing.Point(182, 353);
            this.lblKecamatan.Name = "lblKecamatan";
            this.lblKecamatan.Size = new System.Drawing.Size(75, 17);
            this.lblKecamatan.TabIndex = 14;
            this.lblKecamatan.Text = "Kecamatan";
            // 
            // txtKecamatan
            // 
            this.txtKecamatan.Location = new System.Drawing.Point(182, 373);
            this.txtKecamatan.Name = "txtKecamatan";
            this.txtKecamatan.PlaceholderText = "Masukkan kecamatan";
            this.txtKecamatan.Size = new System.Drawing.Size(148, 25);
            this.txtKecamatan.TabIndex = 15;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.dgvReference);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(350, 70);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10, 15, 20, 10);
            this.pnlRight.Size = new System.Drawing.Size(500, 420);
            this.pnlRight.TabIndex = 2;
            // 
            // dgvReference
            // 
            this.dgvReference.AllowUserToAddRows = false;
            this.dgvReference.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReference.BackgroundColor = System.Drawing.Color.White;
            this.dgvReference.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReference.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReference.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReference.Location = new System.Drawing.Point(10, 15);
            this.dgvReference.MultiSelect = false;
            this.dgvReference.Name = "dgvReference";
            this.dgvReference.ReadOnly = true;
            this.dgvReference.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReference.Size = new System.Drawing.Size(470, 395);
            this.dgvReference.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.btnSubmit);
            this.pnlBottom.Controls.Add(this.btnBatal);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 490);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlBottom.Size = new System.Drawing.Size(850, 60);
            this.pnlBottom.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(560, 13);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(110, 35);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Tambah";
            this.btnSubmit.UseVisualStyleBackColor = false;
            // 
            // btnBatal
            // 
            this.btnBatal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBatal.ForeColor = System.Drawing.Color.White;
            this.btnBatal.Location = new System.Drawing.Point(730, 13);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(100, 35);
            this.btnBatal.TabIndex = 1;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = false;
            this.btnBatal.Click += new System.EventHandler((s, e) => { this.DialogResult = System.Windows.Forms.DialogResult.Cancel; this.Close(); });
            // 
            // UserForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(850, 550);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "User";
            this.pnlHeader.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReference)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
