namespace CHEMLINK
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnProduk;
        private System.Windows.Forms.Button btnTransaksi;
        private System.Windows.Forms.Button btnSupplier;
        private System.Windows.Forms.Button btnLaporan;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.PictureBox picLogo;

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
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnProduk = new System.Windows.Forms.Button();
            this.btnTransaksi = new System.Windows.Forms.Button();
            this.btnSupplier = new System.Windows.Forms.Button();
            this.btnLaporan = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblUser = new System.Windows.Forms.Label();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.sidebarPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.mainContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(103)))), ((int)(((byte)(30)))));
            this.sidebarPanel.Controls.Add(this.btnDashboard);
            this.sidebarPanel.Controls.Add(this.btnProduk);
            this.sidebarPanel.Controls.Add(this.btnTransaksi);
            this.sidebarPanel.Controls.Add(this.btnSupplier);
            this.sidebarPanel.Controls.Add(this.btnLaporan);
            this.sidebarPanel.Controls.Add(this.btnUser);
            this.sidebarPanel.Controls.Add(this.btnLogout);
            this.sidebarPanel.Controls.Add(this.picLogo);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Size = new System.Drawing.Size(230, 750);
            this.sidebarPanel.TabIndex = 0;
            this.sidebarPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.sidebarPanel_Paint);
            // 
            // picLogo
            // 
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(230, 80);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(0, 70);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(230, 50);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "   Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = true;
            // 
            // btnProduk
            // 
            this.btnProduk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProduk.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProduk.FlatAppearance.BorderSize = 0;
            this.btnProduk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProduk.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnProduk.ForeColor = System.Drawing.Color.White;
            this.btnProduk.Location = new System.Drawing.Point(0, 120);
            this.btnProduk.Name = "btnProduk";
            this.btnProduk.Size = new System.Drawing.Size(230, 50);
            this.btnProduk.TabIndex = 2;
            this.btnProduk.Text = "   Katalog Produk";
            this.btnProduk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProduk.UseVisualStyleBackColor = true;
            // 
            // btnTransaksi
            // 
            this.btnTransaksi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransaksi.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTransaksi.FlatAppearance.BorderSize = 0;
            this.btnTransaksi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransaksi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTransaksi.ForeColor = System.Drawing.Color.White;
            this.btnTransaksi.Location = new System.Drawing.Point(0, 170);
            this.btnTransaksi.Name = "btnTransaksi";
            this.btnTransaksi.Size = new System.Drawing.Size(230, 50);
            this.btnTransaksi.TabIndex = 3;
            this.btnTransaksi.Text = "   Transaksi / Kasir";
            this.btnTransaksi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransaksi.UseVisualStyleBackColor = true;
            // 
            // btnSupplier
            // 
            this.btnSupplier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSupplier.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSupplier.FlatAppearance.BorderSize = 0;
            this.btnSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupplier.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSupplier.ForeColor = System.Drawing.Color.White;
            this.btnSupplier.Location = new System.Drawing.Point(0, 220);
            this.btnSupplier.Name = "btnSupplier";
            this.btnSupplier.Size = new System.Drawing.Size(230, 50);
            this.btnSupplier.TabIndex = 4;
            this.btnSupplier.Text = "   Manajemen Supplier";
            this.btnSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSupplier.UseVisualStyleBackColor = true;
            // 
            // btnLaporan
            // 
            this.btnLaporan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLaporan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLaporan.FlatAppearance.BorderSize = 0;
            this.btnLaporan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLaporan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLaporan.ForeColor = System.Drawing.Color.White;
            this.btnLaporan.Location = new System.Drawing.Point(0, 270);
            this.btnLaporan.Name = "btnLaporan";
            this.btnLaporan.Size = new System.Drawing.Size(230, 50);
            this.btnLaporan.TabIndex = 5;
            this.btnLaporan.Text = "   Laporan Keuangan";
            this.btnLaporan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLaporan.UseVisualStyleBackColor = true;
            // 
            // btnUser
            // 
            this.btnUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUser.FlatAppearance.BorderSize = 0;
            this.btnUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnUser.ForeColor = System.Drawing.Color.White;
            this.btnUser.Location = new System.Drawing.Point(0, 320);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(230, 50);
            this.btnUser.TabIndex = 6;
            this.btnUser.Text = "   Manajemen User";
            this.btnUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUser.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(0, 700);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(230, 50);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "   Keluar Sistem";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.Controls.Add(this.lblUser);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(230, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(920, 60);
            this.headerPanel.TabIndex = 1;
            // 
            // lblUser
            // 
            this.lblUser.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblUser.Location = new System.Drawing.Point(620, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Padding = new System.Windows.Forms.Padding(0, 0, 20, 0);
            this.lblUser.Size = new System.Drawing.Size(300, 60);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "👤 Login: -";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.Controls.Add(this.lblTitle);
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(230, 60);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Padding = new System.Windows.Forms.Padding(20);
            this.mainContentPanel.Size = new System.Drawing.Size(920, 690);
            this.mainContentPanel.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(103)))), ((int)(((byte)(30)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(880, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dashboard Utama";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1150, 750);
            this.Controls.Add(this.mainContentPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.sidebarPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChemLink - Manajemen Kios Pertanian v2.0";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.sidebarPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.mainContentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
