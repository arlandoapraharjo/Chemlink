namespace CHEMLINK.Views
{
    partial class EditUserDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtEditUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtEditPassword;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cbEditRole;

        private System.Windows.Forms.Label lblAlamat;
        private System.Windows.Forms.TextBox txtEditAlamat;
        private System.Windows.Forms.Label lblNoTelp;
        private System.Windows.Forms.TextBox txtEditNoTelp;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEditEmail;
        private System.Windows.Forms.Label lblKota;
        private System.Windows.Forms.TextBox txtEditKota;
        private System.Windows.Forms.Label lblKecamatan;
        private System.Windows.Forms.TextBox txtEditKecamatan;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.header = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtEditUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtEditPassword = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cbEditRole = new System.Windows.Forms.ComboBox();
            this.lblAlamat = new System.Windows.Forms.Label();
            this.txtEditAlamat = new System.Windows.Forms.TextBox();
            this.lblNoTelp = new System.Windows.Forms.Label();
            this.txtEditNoTelp = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEditEmail = new System.Windows.Forms.TextBox();
            this.lblKota = new System.Windows.Forms.Label();
            this.txtEditKota = new System.Windows.Forms.TextBox();
            this.lblKecamatan = new System.Windows.Forms.Label();
            this.txtEditKecamatan = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ===================== HEADER =====================
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Size = new System.Drawing.Size(440, 70);
            this.header.Paint += new System.Windows.Forms.PaintEventHandler(this.header_Paint);

            // ===================== TITLE =====================
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblTitle.Location = new System.Drawing.Point(24, 80);
            this.lblTitle.Text = "Edit Data Akun Operator";

            // ===================== USERNAME =====================
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblUsername.Location = new System.Drawing.Point(24, 108);
            this.lblUsername.Text = "Username";

            this.txtEditUsername.Location = new System.Drawing.Point(24, 126);
            this.txtEditUsername.Size = new System.Drawing.Size(390, 25);
            this.txtEditUsername.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ===================== PASSWORD =====================
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblPassword.Location = new System.Drawing.Point(24, 157);
            this.lblPassword.Text = "Password";

            this.txtEditPassword.Location = new System.Drawing.Point(24, 175);
            this.txtEditPassword.Size = new System.Drawing.Size(390, 25);
            this.txtEditPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEditPassword.PasswordChar = '\u2022';
            this.txtEditPassword.PlaceholderText = "Kosongkan jika tidak ingin mengubah";

            // ===================== ROLE =====================
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblRole.Location = new System.Drawing.Point(24, 206);
            this.lblRole.Text = "Role";

            this.cbEditRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEditRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEditRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbEditRole.Items.AddRange(new object[] { "Admin", "Kasir" });
            this.cbEditRole.Location = new System.Drawing.Point(24, 224);
            this.cbEditRole.Size = new System.Drawing.Size(390, 25);

            // ===================== ALAMAT =====================
            this.lblAlamat.AutoSize = true;
            this.lblAlamat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlamat.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblAlamat.Location = new System.Drawing.Point(24, 255);
            this.lblAlamat.Text = "Alamat";

            this.txtEditAlamat.Location = new System.Drawing.Point(24, 273);
            this.txtEditAlamat.Size = new System.Drawing.Size(390, 42);
            this.txtEditAlamat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEditAlamat.Multiline = true;
            this.txtEditAlamat.PlaceholderText = "Masukkan alamat lengkap";

            // ===================== NO. TELEPON =====================
            this.lblNoTelp.AutoSize = true;
            this.lblNoTelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNoTelp.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblNoTelp.Location = new System.Drawing.Point(24, 321);
            this.lblNoTelp.Text = "No. Telepon";

            this.txtEditNoTelp.Location = new System.Drawing.Point(24, 339);
            this.txtEditNoTelp.Size = new System.Drawing.Size(390, 25);
            this.txtEditNoTelp.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ===================== EMAIL =====================
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblEmail.Location = new System.Drawing.Point(24, 370);
            this.lblEmail.Text = "Email";

            this.txtEditEmail.Location = new System.Drawing.Point(24, 388);
            this.txtEditEmail.Size = new System.Drawing.Size(390, 25);
            this.txtEditEmail.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ===================== KOTA =====================
            this.lblKota.AutoSize = true;
            this.lblKota.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKota.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblKota.Location = new System.Drawing.Point(24, 419);
            this.lblKota.Text = "Kota";

            this.txtEditKota.Location = new System.Drawing.Point(24, 437);
            this.txtEditKota.Size = new System.Drawing.Size(185, 25);
            this.txtEditKota.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ===================== KECAMATAN =====================
            this.lblKecamatan.AutoSize = true;
            this.lblKecamatan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKecamatan.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblKecamatan.Location = new System.Drawing.Point(229, 419);
            this.lblKecamatan.Text = "Kecamatan";

            this.txtEditKecamatan.Location = new System.Drawing.Point(229, 437);
            this.txtEditKecamatan.Size = new System.Drawing.Size(185, 25);
            this.txtEditKecamatan.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ===================== BUTTONS =====================
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(72, 161, 17);
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(24, 480);
            this.btnSave.Size = new System.Drawing.Size(185, 38);
            this.btnSave.Text = "Simpan Perubahan";
            this.btnSave.UseVisualStyleBackColor = false;

            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(229, 480);
            this.btnCancel.Size = new System.Drawing.Size(185, 38);
            this.btnCancel.Text = "Batal";
            this.btnCancel.UseVisualStyleBackColor = false;

            // ===================== FORM =====================
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(440, 540);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtEditKecamatan);
            this.Controls.Add(this.lblKecamatan);
            this.Controls.Add(this.txtEditKota);
            this.Controls.Add(this.lblKota);
            this.Controls.Add(this.txtEditEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEditNoTelp);
            this.Controls.Add(this.lblNoTelp);
            this.Controls.Add(this.txtEditAlamat);
            this.Controls.Add(this.lblAlamat);
            this.Controls.Add(this.cbEditRole);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.txtEditPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtEditUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.header);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditUserDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChemLink - Edit Akun Operator";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
