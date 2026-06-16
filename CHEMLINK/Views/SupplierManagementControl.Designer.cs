namespace CHEMLINK.Views
{
    partial class SupplierManagementControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlGrid;
        public System.Windows.Forms.DataGridView dgvMain;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblToolbar;
        public System.Windows.Forms.Button btnTambah;
        public System.Windows.Forms.Button btnUbah;
        public System.Windows.Forms.Button btnHapus;

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
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblToolbar = new System.Windows.Forms.Label();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.pnlGrid.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGrid
            // 
            this.pnlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvMain);
            this.pnlGrid.Location = new System.Drawing.Point(20, 20);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1070, 520);
            this.pnlGrid.TabIndex = 0;
            // 
            // dgvMain
            // 
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.BackgroundColor = System.Drawing.Color.White;
            this.dgvMain.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(161)))), ((int)(((byte)(17)))));
            this.dgvMain.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.ReadOnly = true;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.TabIndex = 0;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.lblToolbar);
            this.pnlToolbar.Controls.Add(this.btnTambah);
            this.pnlToolbar.Controls.Add(this.btnUbah);
            this.pnlToolbar.Controls.Add(this.btnHapus);
            this.pnlToolbar.Location = new System.Drawing.Point(20, 560);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1070, 60);
            this.pnlToolbar.TabIndex = 1;
            // 
            // lblToolbar
            // 
            this.lblToolbar.AutoSize = true;
            this.lblToolbar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblToolbar.Location = new System.Drawing.Point(15, 20);
            this.lblToolbar.Name = "lblToolbar";
            this.lblToolbar.Size = new System.Drawing.Size(118, 19);
            this.lblToolbar.TabIndex = 0;
            this.lblToolbar.Text = "Kelola Supplier:";
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(161)))), ((int)(((byte)(17)))));
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(145, 14);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(140, 30);
            this.btnTambah.TabIndex = 1;
            this.btnTambah.Text = "+ Tambah Supplier";
            this.btnTambah.UseVisualStyleBackColor = false;
            // 
            // btnUbah
            // 
            this.btnUbah.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnUbah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUbah.ForeColor = System.Drawing.Color.White;
            this.btnUbah.Location = new System.Drawing.Point(295, 14);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(140, 30);
            this.btnUbah.TabIndex = 2;
            this.btnUbah.Text = "✏ Edit Supplier";
            this.btnUbah.UseVisualStyleBackColor = false;
            // 
            // btnHapus
            // 
            this.btnHapus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.ForeColor = System.Drawing.Color.White;
            this.btnHapus.Location = new System.Drawing.Point(445, 14);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(140, 30);
            this.btnHapus.TabIndex = 3;
            this.btnHapus.Text = "🗑 Hapus Supplier";
            this.btnHapus.UseVisualStyleBackColor = false;
            // 
            // SupplierManagementControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlGrid);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "SupplierManagementControl";
            this.Size = new System.Drawing.Size(1110, 670);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
