namespace CHEMLINK.Views
{
    partial class DeleteUserForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblUsernameLbl;
        private System.Windows.Forms.Label lblInfoUsername;
        private System.Windows.Forms.Label lblRoleLbl;
        private System.Windows.Forms.Label lblInfoRole;
        private System.Windows.Forms.Label lblWarning;
        public System.Windows.Forms.DataGridView dgvReference;
        public System.Windows.Forms.Button btnHapus;
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
            this.lblUsernameLbl = new System.Windows.Forms.Label();
            this.lblInfoUsername = new System.Windows.Forms.Label();
            this.lblRoleLbl = new System.Windows.Forms.Label();
            this.lblInfoRole = new System.Windows.Forms.Label();
            this.lblWarning = new System.Windows.Forms.Label();
            this.dgvReference = new System.Windows.Forms.DataGridView();
            this.btnHapus = new System.Windows.Forms.Button();
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
            this.pnlHeader.Size = new System.Drawing.Size(800, 70);
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
            this.lblTitle.Text = "Hapus User";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lblWarning);
            this.pnlLeft.Controls.Add(this.lblUsernameLbl);
            this.pnlLeft.Controls.Add(this.lblInfoUsername);
            this.pnlLeft.Controls.Add(this.lblRoleLbl);
            this.pnlLeft.Controls.Add(this.lblInfoRole);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 70);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(20, 15, 10, 10);
            this.pnlLeft.Size = new System.Drawing.Size(300, 370);
            this.pnlLeft.TabIndex = 1;
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblWarning.Location = new System.Drawing.Point(20, 20);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(260, 40);
            this.lblWarning.TabIndex = 0;
            this.lblWarning.Text = "⚠ User berikut akan dihapus dari sistem:";
            // 
            // lblUsernameLbl
            // 
            this.lblUsernameLbl.AutoSize = true;
            this.lblUsernameLbl.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUsernameLbl.Location = new System.Drawing.Point(20, 75);
            this.lblUsernameLbl.Name = "lblUsernameLbl";
            this.lblUsernameLbl.Size = new System.Drawing.Size(70, 17);
            this.lblUsernameLbl.TabIndex = 1;
            this.lblUsernameLbl.Text = "Username";
            // 
            // lblInfoUsername
            // 
            this.lblInfoUsername.AutoSize = true;
            this.lblInfoUsername.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInfoUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblInfoUsername.Location = new System.Drawing.Point(20, 98);
            this.lblInfoUsername.Name = "lblInfoUsername";
            this.lblInfoUsername.Size = new System.Drawing.Size(50, 20);
            this.lblInfoUsername.TabIndex = 2;
            this.lblInfoUsername.Text = "-";
            // 
            // lblRoleLbl
            // 
            this.lblRoleLbl.AutoSize = true;
            this.lblRoleLbl.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRoleLbl.Location = new System.Drawing.Point(20, 135);
            this.lblRoleLbl.Name = "lblRoleLbl";
            this.lblRoleLbl.Size = new System.Drawing.Size(35, 17);
            this.lblRoleLbl.TabIndex = 3;
            this.lblRoleLbl.Text = "Role";
            // 
            // lblInfoRole
            // 
            this.lblInfoRole.AutoSize = true;
            this.lblInfoRole.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInfoRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblInfoRole.Location = new System.Drawing.Point(20, 158);
            this.lblInfoRole.Name = "lblInfoRole";
            this.lblInfoRole.Size = new System.Drawing.Size(50, 20);
            this.lblInfoRole.TabIndex = 4;
            this.lblInfoRole.Text = "-";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.dgvReference);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(300, 70);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10, 15, 20, 10);
            this.pnlRight.Size = new System.Drawing.Size(500, 370);
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
            this.dgvReference.Size = new System.Drawing.Size(470, 345);
            this.dgvReference.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.btnHapus);
            this.pnlBottom.Controls.Add(this.btnBatal);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 420);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlBottom.Size = new System.Drawing.Size(800, 60);
            this.pnlBottom.TabIndex = 3;
            // 
            // btnHapus
            // 
            this.btnHapus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnHapus.ForeColor = System.Drawing.Color.White;
            this.btnHapus.Location = new System.Drawing.Point(560, 13);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(110, 35);
            this.btnHapus.TabIndex = 0;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = false;
            // 
            // btnBatal
            // 
            this.btnBatal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.btnBatal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBatal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBatal.ForeColor = System.Drawing.Color.White;
            this.btnBatal.Location = new System.Drawing.Point(680, 13);
            this.btnBatal.Name = "btnBatal";
            this.btnBatal.Size = new System.Drawing.Size(100, 35);
            this.btnBatal.TabIndex = 1;
            this.btnBatal.Text = "Batal";
            this.btnBatal.UseVisualStyleBackColor = false;
            this.btnBatal.Click += new System.EventHandler((s, e) => { this.DialogResult = System.Windows.Forms.DialogResult.Cancel; this.Close(); });
            // 
            // DeleteUserForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleteUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hapus User";
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
