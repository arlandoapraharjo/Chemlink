namespace CHEMLINK.Views
{
    partial class EditSupplierDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblNama;
        private System.Windows.Forms.TextBox txtEditNama;
        private System.Windows.Forms.Label lblKontakPerson;
        private System.Windows.Forms.TextBox txtEditKontakPerson;
        private System.Windows.Forms.Label lblNoTelp;
        private System.Windows.Forms.TextBox txtEditNoTelp;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEditEmail;
        private System.Windows.Forms.Label lblAlamat;
        private System.Windows.Forms.TextBox txtEditAlamat;
        private System.Windows.Forms.Label lblKota;
        private System.Windows.Forms.TextBox txtEditKota;

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
            this.lblNama = new System.Windows.Forms.Label();
            this.txtEditNama = new System.Windows.Forms.TextBox();
            this.lblKontakPerson = new System.Windows.Forms.Label();
            this.txtEditKontakPerson = new System.Windows.Forms.TextBox();
            this.lblNoTelp = new System.Windows.Forms.Label();
            this.txtEditNoTelp = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEditEmail = new System.Windows.Forms.TextBox();
            this.lblAlamat = new System.Windows.Forms.Label();
            this.txtEditAlamat = new System.Windows.Forms.TextBox();
            this.lblKota = new System.Windows.Forms.Label();
            this.txtEditKota = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ===================== HEADER =====================
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Size = new System.Drawing.Size(500, 70);
            this.header.Paint += new System.Windows.Forms.PaintEventHandler(this.header_Paint);

            // ===================== TITLE =====================
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.lblTitle.Location = new System.Drawing.Point(24, 80);
            this.lblTitle.Text = "Edit Data Supplier";

            // ===================== NAMA PERUSAHAAN =====================
            this.lblNama.AutoSize = true;
            this.lblNama.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNama.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblNama.Location = new System.Drawing.Point(24, 108);
            this.lblNama.Text = "Nama Perusahaan";

            this.txtEditNama.Location = new System.Drawing.Point(24, 126);
            this.txtEditNama.Size = new System.Drawing.Size(450, 25);
            this.txtEditNama.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ===================== KONTAK PERSON =====================
            this.lblKontakPerson.AutoSize = true;
            this.lblKontakPerson.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKontakPerson.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblKontakPerson.Location = new System.Drawing.Point(24, 157);
            this.lblKontakPerson.Text = "Kontak Person";

            this.txtEditKontakPerson.Location = new System.Drawing.Point(24, 175);
            this.txtEditKontakPerson.Size = new System.Drawing.Size(215, 25);
            this.txtEditKontakPerson.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ===================== NO. TELEPON =====================
            this.lblNoTelp.AutoSize = true;
            this.lblNoTelp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNoTelp.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblNoTelp.Location = new System.Drawing.Point(259, 157);
            this.lblNoTelp.Text = "No. Telepon";

            this.txtEditNoTelp.Location = new System.Drawing.Point(259, 175);
            this.txtEditNoTelp.Size = new System.Drawing.Size(215, 25);
            this.txtEditNoTelp.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ===================== EMAIL =====================
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblEmail.Location = new System.Drawing.Point(24, 206);
            this.lblEmail.Text = "Email";

            this.txtEditEmail.Location = new System.Drawing.Point(24, 224);
            this.txtEditEmail.Size = new System.Drawing.Size(450, 25);
            this.txtEditEmail.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ===================== ALAMAT =====================
            this.lblAlamat.AutoSize = true;
            this.lblAlamat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlamat.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblAlamat.Location = new System.Drawing.Point(24, 255);
            this.lblAlamat.Text = "Alamat";

            this.txtEditAlamat.Location = new System.Drawing.Point(24, 273);
            this.txtEditAlamat.Size = new System.Drawing.Size(450, 50);
            this.txtEditAlamat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEditAlamat.Multiline = true;
            this.txtEditAlamat.PlaceholderText = "Masukkan alamat lengkap";

            // ===================== KOTA =====================
            this.lblKota.AutoSize = true;
            this.lblKota.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKota.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblKota.Location = new System.Drawing.Point(24, 329);
            this.lblKota.Text = "Kota";

            this.txtEditKota.Location = new System.Drawing.Point(24, 347);
            this.txtEditKota.Size = new System.Drawing.Size(450, 25);
            this.txtEditKota.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ===================== BUTTONS =====================
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(72, 161, 17);
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(260, 390);
            this.btnSave.Size = new System.Drawing.Size(110, 38);
            this.btnSave.Text = "Simpan";
            this.btnSave.UseVisualStyleBackColor = false;

            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(380, 390);
            this.btnCancel.Size = new System.Drawing.Size(100, 38);
            this.btnCancel.Text = "Batal";
            this.btnCancel.UseVisualStyleBackColor = false;

            // ===================== FORM =====================
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtEditKota);
            this.Controls.Add(this.lblKota);
            this.Controls.Add(this.txtEditAlamat);
            this.Controls.Add(this.lblAlamat);
            this.Controls.Add(this.txtEditEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEditNoTelp);
            this.Controls.Add(this.lblNoTelp);
            this.Controls.Add(this.txtEditKontakPerson);
            this.Controls.Add(this.lblKontakPerson);
            this.Controls.Add(this.txtEditNama);
            this.Controls.Add(this.lblNama);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.header);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditSupplierDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ChemLink - Edit Data Supplier";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
