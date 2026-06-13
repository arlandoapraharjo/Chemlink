namespace CHEMLINK.Views
{
    partial class EditSupplierDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Panel pnlUsername;
        private System.Windows.Forms.TextBox txtEditUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel pnlPassword;
        private System.Windows.Forms.TextBox txtEditPassword;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.TextBox txtEditAddress;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            header = new Panel();
            lblTitle = new Label();
            lblUsername = new Label();
            pnlUsername = new Panel();
            txtEditUsername = new TextBox();
            lblPassword = new Label();
            pnlPassword = new Panel();
            txtEditPassword = new TextBox();
            lblRole = new Label();
            txtEditAddress = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            pnlUsername.SuspendLayout();
            pnlPassword.SuspendLayout();
            SuspendLayout();
            // 
            // header
            // 
            header.Dock = DockStyle.Top;
            header.Location = new Point(0, 0);
            header.Name = "header";
            header.Size = new Size(420, 70);
            header.TabIndex = 0;
            header.Paint += header_Paint;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(50, 50, 50);
            lblTitle.Location = new Point(30, 88);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(134, 20);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Edit Data Supplier";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9.5F);
            lblUsername.ForeColor = Color.FromArgb(100, 100, 100);
            lblUsername.Location = new Point(30, 125);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(67, 17);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Username";
            // 
            // pnlUsername
            // 
            pnlUsername.BackColor = Color.White;
            pnlUsername.Controls.Add(txtEditUsername);
            pnlUsername.Location = new Point(30, 145);
            pnlUsername.Name = "pnlUsername";
            pnlUsername.Size = new Size(360, 32);
            pnlUsername.TabIndex = 3;
            pnlUsername.Click += pnlUsername_Click;
            pnlUsername.Paint += pnlUsername_Paint;
            // 
            // txtEditUsername
            // 
            txtEditUsername.BorderStyle = BorderStyle.None;
            txtEditUsername.Font = new Font("Segoe UI", 11F);
            txtEditUsername.Location = new Point(2, 5);
            txtEditUsername.Name = "txtEditUsername";
            txtEditUsername.Size = new Size(356, 20);
            txtEditUsername.TabIndex = 0;
            txtEditUsername.GotFocus += txtEditUsername_GotFocus;
            txtEditUsername.LostFocus += txtEditUsername_LostFocus;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9.5F);
            lblPassword.ForeColor = Color.FromArgb(100, 100, 100);
            lblPassword.Location = new Point(30, 190);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(32, 17);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Telp";
            // 
            // pnlPassword
            // 
            pnlPassword.BackColor = Color.White;
            pnlPassword.Controls.Add(txtEditPassword);
            pnlPassword.Location = new Point(30, 210);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Size = new Size(360, 32);
            pnlPassword.TabIndex = 5;
            pnlPassword.Click += pnlPassword_Click;
            pnlPassword.Paint += pnlPassword_Paint;
            // 
            // txtEditPassword
            // 
            txtEditPassword.BorderStyle = BorderStyle.None;
            txtEditPassword.Font = new Font("Segoe UI", 11F);
            txtEditPassword.Location = new Point(2, 5);
            txtEditPassword.Name = "txtEditPassword";
            txtEditPassword.PlaceholderText = "Optional";
            txtEditPassword.Size = new Size(356, 20);
            txtEditPassword.TabIndex = 0;
            txtEditPassword.GotFocus += txtEditPassword_GotFocus;
            txtEditPassword.LostFocus += txtEditPassword_LostFocus;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9.5F);
            lblRole.ForeColor = Color.FromArgb(100, 100, 100);
            lblRole.Location = new Point(30, 255);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(48, 17);
            lblRole.TabIndex = 6;
            lblRole.Text = "Alamat";
            // 
            // txtEditAddress
            // 
            txtEditAddress.BorderStyle = BorderStyle.FixedSingle;
            txtEditAddress.Font = new Font("Segoe UI", 10F);
            txtEditAddress.Location = new Point(30, 277);
            txtEditAddress.Multiline = true;
            txtEditAddress.Name = "txtEditAddress";
            txtEditAddress.Size = new Size(360, 30);
            txtEditAddress.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(72, 161, 17);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(30, 325);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(170, 38);
            btnSave.TabIndex = 8;
            btnSave.Text = "Simpan Perubahan";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(180, 180, 180);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(220, 325);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(170, 38);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Batal";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // EditSupplierDialog
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(420, 385);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lblRole);
            Controls.Add(pnlPassword);
            Controls.Add(lblPassword);
            Controls.Add(pnlUsername);
            Controls.Add(lblUsername);
            Controls.Add(lblTitle);
            Controls.Add(header);
            Controls.Add(txtEditAddress);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditSupplierDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "ChemLink - Edit Data Supplier";
            pnlUsername.ResumeLayout(false);
            pnlUsername.PerformLayout();
            pnlPassword.ResumeLayout(false);
            pnlPassword.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
