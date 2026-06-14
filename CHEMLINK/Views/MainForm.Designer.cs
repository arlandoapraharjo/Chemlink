namespace CHEMLINK
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Topbar
        private System.Windows.Forms.Panel topbarPanel;
        private System.Windows.Forms.FlowLayoutPanel navPanel;
        private System.Windows.Forms.Panel brandPanel;
        private System.Windows.Forms.Panel userPanel;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblBrandSub;

        // Navigation buttons
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnProduk;
        private System.Windows.Forms.Button btnTransaksi;
        private System.Windows.Forms.Button btnSupplier;
        private System.Windows.Forms.Button btnLaporan;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnLogout;

        // User profile
        private System.Windows.Forms.Label lblGreeting;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Panel pnlAvatar;
        private System.Windows.Forms.Label lblAvatar;

        // Content
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.Label lblTitle;

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
            this.topbarPanel = new System.Windows.Forms.Panel();
            this.navPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.brandPanel = new System.Windows.Forms.Panel();
            this.userPanel = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblBrandSub = new System.Windows.Forms.Label();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnProduk = new System.Windows.Forms.Button();
            this.btnTransaksi = new System.Windows.Forms.Button();
            this.btnSupplier = new System.Windows.Forms.Button();
            this.btnLaporan = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblGreeting = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pnlAvatar = new System.Windows.Forms.Panel();
            this.lblAvatar = new System.Windows.Forms.Label();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.topbarPanel.SuspendLayout();
            this.brandPanel.SuspendLayout();
            this.navPanel.SuspendLayout();
            this.userPanel.SuspendLayout();
            this.pnlAvatar.SuspendLayout();
            this.mainContentPanel.SuspendLayout();
            this.SuspendLayout();

            // ===================== TOPBAR PANEL =====================
            this.topbarPanel.BackColor = System.Drawing.Color.FromArgb(2, 44, 34); // Agro950 #022C22
            this.topbarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topbarPanel.Height = 64;
            this.topbarPanel.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.topbarPanel.Controls.Add(this.navPanel);
            this.topbarPanel.Controls.Add(this.brandPanel);
            this.topbarPanel.Controls.Add(this.userPanel);

            // ===================== BRAND PANEL (Left) =====================
            this.brandPanel.BackColor = System.Drawing.Color.Transparent;
            this.brandPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.brandPanel.Width = 210;
            this.brandPanel.Controls.Add(this.lblBrandSub);
            this.brandPanel.Controls.Add(this.lblBrand);
            this.brandPanel.Controls.Add(this.picLogo);

            // picLogo - transparent logo from Assets
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Location = new System.Drawing.Point(4, 14);
            this.picLogo.Size = new System.Drawing.Size(36, 36);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

            // lblBrand
            this.lblBrand.AutoSize = true;
            this.lblBrand.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblBrand.ForeColor = System.Drawing.Color.White;
            this.lblBrand.Location = new System.Drawing.Point(45, 8);
            this.lblBrand.Text = "ChemLink";

            // lblBrandSub
            this.lblBrandSub.AutoSize = true;
            this.lblBrandSub.Font = new System.Drawing.Font("Segoe UI Semibold", 7F, System.Drawing.FontStyle.Bold);
            this.lblBrandSub.ForeColor = System.Drawing.Color.FromArgb(134, 239, 172); // Agro300
            this.lblBrandSub.Location = new System.Drawing.Point(48, 38);
            this.lblBrandSub.Text = "SISTEM STOK & PENJUALAN";

            // ===================== NAV PANEL (Center - FlowLayout) =====================
            this.navPanel.BackColor = System.Drawing.Color.Transparent;
            this.navPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.navPanel.WrapContents = false;
            this.navPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.navPanel.Padding = new System.Windows.Forms.Padding(0);
            this.navPanel.Controls.Add(this.btnDashboard);
            this.navPanel.Controls.Add(this.btnProduk);
            this.navPanel.Controls.Add(this.btnTransaksi);
            this.navPanel.Controls.Add(this.btnSupplier);
            this.navPanel.Controls.Add(this.btnLaporan);
            this.navPanel.Controls.Add(this.btnUser);

            // Common nav button style
            NavButtonStyle(this.btnDashboard, "Dashboard");
            NavButtonStyle(this.btnProduk, "Produk");
            NavButtonStyle(this.btnTransaksi, "Transaksi");
            NavButtonStyle(this.btnSupplier, "Supplier");
            NavButtonStyle(this.btnLaporan, "Laporan");
            NavButtonStyle(this.btnUser, "User");

            // ===================== USER PANEL (Right) =====================
            this.userPanel.BackColor = System.Drawing.Color.Transparent;
            this.userPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.userPanel.Width = 250;
            this.userPanel.Controls.Add(this.pnlAvatar);
            this.userPanel.Controls.Add(this.lblUsername);
            this.userPanel.Controls.Add(this.lblGreeting);
            this.userPanel.Controls.Add(this.btnLogout);

            // lblGreeting
            this.lblGreeting.AutoSize = true;
            this.lblGreeting.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblGreeting.ForeColor = System.Drawing.Color.FromArgb(134, 239, 172); // Agro300
            this.lblGreeting.Location = new System.Drawing.Point(0, 14);
            this.lblGreeting.Text = "Selamat Bekerja,";
            this.lblGreeting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Location = new System.Drawing.Point(0, 32);
            this.lblUsername.Text = "Admin";

            // pnlAvatar (circle)
            this.pnlAvatar.BackColor = System.Drawing.Color.FromArgb(22, 101, 52); // Agro800
            this.pnlAvatar.Location = new System.Drawing.Point(168, 13);
            this.pnlAvatar.Size = new System.Drawing.Size(38, 38);
            this.pnlAvatar.Controls.Add(this.lblAvatar);

            // lblAvatar
            this.lblAvatar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAvatar.ForeColor = System.Drawing.Color.White;
            this.lblAvatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAvatar.Text = "ADM";

            // btnLogout (power icon)
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(134, 239, 172); // Agro300
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.Location = new System.Drawing.Point(214, 18);
            this.btnLogout.Size = new System.Drawing.Size(30, 28);
            this.btnLogout.Text = "⏻";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLogout.UseVisualStyleBackColor = true;

            // ===================== MAIN CONTENT =====================
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.AutoScroll = true;
            this.mainContentPanel.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); // #F8FAFC
            this.mainContentPanel.Padding = new System.Windows.Forms.Padding(0);
            this.mainContentPanel.Controls.Add(this.lblTitle);

            // lblTitle
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Height = 45;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59); // TextDark #1E293B
            this.lblTitle.Padding = new System.Windows.Forms.Padding(24, 10, 0, 0);
            this.lblTitle.Text = "Dashboard";

            // ===================== MAIN FORM =====================
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252); // #F8FAFC
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.mainContentPanel);
            this.Controls.Add(this.topbarPanel);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChemLink - Manajemen Kios Pertanian v2.0";

            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.topbarPanel.ResumeLayout(false);
            this.brandPanel.ResumeLayout(false);
            this.brandPanel.PerformLayout();
            this.navPanel.ResumeLayout(false);
            this.userPanel.ResumeLayout(false);
            this.userPanel.PerformLayout();
            this.pnlAvatar.ResumeLayout(false);
            this.mainContentPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void NavButtonStyle(System.Windows.Forms.Button btn, string text)
        {
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            btn.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184); // TextMuted #94A3B8
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.Size = new System.Drawing.Size(110, 36);
            btn.Margin = new System.Windows.Forms.Padding(4, 14, 4, 0);
            btn.Text = text;
            btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            btn.UseVisualStyleBackColor = true;
            btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(16, 255, 255, 255); // 10% white hover
        }
    }
}
