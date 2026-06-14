namespace CHEMLINK.Views
{
    partial class DeleteUserForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLabelUsername;
        private System.Windows.Forms.Label lblInfoUsername;
        private System.Windows.Forms.Label lblLabelRole;
        private System.Windows.Forms.Label lblInfoRole;
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
            this.lblLabelUsername = new System.Windows.Forms.Label();
            this.lblInfoUsername = new System.Windows.Forms.Label();
            this.lblLabelRole = new System.Windows.Forms.Label();
            this.lblInfoRole = new System.Windows.Forms.Label();
            this.dgvReference = new System.Windows.Forms.DataGridView();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnBatal = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReference)).BeginInit();
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
            this.pnlHeader.Size = new System.Drawing.Size(700, 70);
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
            this.lblTitle.Size = new System.Drawing.Size(250, 70);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Hapus User";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLabelUsername
            // 
            this.lblLabelUsername.AutoSize = true;
            this.lblLabelUsername.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLabelUsername.Location = new System.Drawing.Point(20, 85);
            this.lblLabelUsername.Name = "lblLabelUsername";
            this.lblLabelUsername.Size = new System.Drawing.Size(85, 19);
            this.lblLabelUsername.TabIndex = 1;
            this.lblLabelUsername.Text = "Username:";
            // 
            // lblInfoUsername
            // 
            this.lblInfoUsername.AutoSize = true;
            this.lblInfoUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInfoUsername.Location = new System.Drawing.Point(120, 85);
            this.lblInfoUsername.Name = "lblInfoUsername";
            this.lblInfoUsername.Size = new System.Drawing.Size(30, 19);
            this.lblInfoUsername.TabIndex = 2;
            this.lblInfoUsername.Text = "—";
            // 
            // lblLabelRole
            // 
            this.lblLabelRole.AutoSize = true;
            this.lblLabelRole.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLabelRole.Location = new System.Drawing.Point(20, 110);
            this.lblLabelRole.Name = "lblLabelRole";
            this.lblLabelRole.Size = new System.Drawing.Size(45, 19);
            this.lblLabelRole.TabIndex = 3;
            this.lblLabelRole.Text = "Role:";
            // 
            // lblInfoRole
            // 
            this.lblInfoRole.AutoSize = true;
            this.lblInfoRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblInfoRole.Location = new System.Drawing.Point(120, 110);
            this.lblInfoRole.Name = "lblInfoRole";
            this.lblInfoRole.Size = new System.Drawing.Size(30, 19);
            this.lblInfoRole.TabIndex = 4;
            this.lblInfoRole.Text = "—";
            // 
            // dgvReference
            // 
            this.dgvReference.AllowUserToAddRows = false;
            this.dgvReference.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReference.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReference.BackgroundColor = System.Drawing.Color.White;
            this.dgvReference.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReference.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReference.Location = new System.Drawing.Point(20, 145);
            this.dgvReference.MultiSelect = false;
            this.dgvReference.Name = "dgvReference";
            this.dgvReference.ReadOnly = true;
            this.dgvReference.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReference.Size = new System.Drawing.Size(660, 250);
            this.dgvReference.TabIndex = 5;
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
            this.pnlBottom.Size = new System.Drawing.Size(700, 60);
            this.pnlBottom.TabIndex = 6;
            // 
            // btnHapus
            // 
            this.btnHapus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnHapus.ForeColor = System.Drawing.Color.White;
            this.btnHapus.Location = new System.Drawing.Point(420, 13);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(120, 35);
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
            this.btnBatal.Location = new System.Drawing.Point(560, 13);
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
            this.ClientSize = new System.Drawing.Size(700, 480);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.dgvReference);
            this.Controls.Add(this.lblInfoRole);
            this.Controls.Add(this.lblLabelRole);
            this.Controls.Add(this.lblInfoUsername);
            this.Controls.Add(this.lblLabelUsername);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleteUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hapus User";
            this.pnlHeader.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReference)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
