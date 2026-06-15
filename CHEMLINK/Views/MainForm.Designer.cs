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
            topbarPanel = new Panel();
            navPanel = new FlowLayoutPanel();
            btnDashboard = new Button();
            btnProduk = new Button();
            btnTransaksi = new Button();
            btnSupplier = new Button();
            btnLaporan = new Button();
            btnUser = new Button();
            brandPanel = new Panel();
            lblBrandSub = new Label();
            lblBrand = new Label();
            picLogo = new PictureBox();
            userPanel = new Panel();
            pnlAvatar = new Panel();
            lblAvatar = new Label();
            lblUsername = new Label();
            lblGreeting = new Label();
            btnLogout = new Button();
            mainContentPanel = new Panel();
            lblTitle = new Label();
            topbarPanel.SuspendLayout();
            navPanel.SuspendLayout();
            brandPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            userPanel.SuspendLayout();
            pnlAvatar.SuspendLayout();
            mainContentPanel.SuspendLayout();
            SuspendLayout();
            // 
            // topbarPanel
            // 
            topbarPanel.BackColor = Color.FromArgb(2, 44, 34);
            topbarPanel.Controls.Add(navPanel);
            topbarPanel.Controls.Add(brandPanel);
            topbarPanel.Controls.Add(userPanel);
            topbarPanel.Dock = DockStyle.Top;
            topbarPanel.Location = new Point(0, 0);
            topbarPanel.Name = "topbarPanel";
            topbarPanel.Padding = new Padding(20, 0, 20, 0);
            topbarPanel.Size = new Size(1200, 64);
            topbarPanel.TabIndex = 1;
            // 
            // navPanel
            // 
            navPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            navPanel.BackColor = Color.Transparent;
            navPanel.Controls.Add(btnDashboard);
            navPanel.Controls.Add(btnProduk);
            navPanel.Controls.Add(btnTransaksi);
            navPanel.Controls.Add(btnSupplier);
            navPanel.Controls.Add(btnLaporan);
            navPanel.Controls.Add(btnUser);
            navPanel.Dock = DockStyle.Fill;
            navPanel.Location = new Point(230, 0);
            navPanel.Name = "navPanel";
            navPanel.Size = new Size(700, 64);
            navPanel.TabIndex = 0;
            navPanel.WrapContents = false;
            // 
            // btnDashboard
            // 
            btnDashboard.Location = new Point(3, 3);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(75, 23);
            btnDashboard.TabIndex = 0;
            // 
            // btnProduk
            // 
            btnProduk.Location = new Point(84, 3);
            btnProduk.Name = "btnProduk";
            btnProduk.Size = new Size(75, 23);
            btnProduk.TabIndex = 1;
            // 
            // btnTransaksi
            // 
            btnTransaksi.Location = new Point(165, 3);
            btnTransaksi.Name = "btnTransaksi";
            btnTransaksi.Size = new Size(75, 23);
            btnTransaksi.TabIndex = 2;
            // 
            // btnSupplier
            // 
            btnSupplier.Location = new Point(246, 3);
            btnSupplier.Name = "btnSupplier";
            btnSupplier.Size = new Size(75, 23);
            btnSupplier.TabIndex = 3;
            // 
            // btnLaporan
            // 
            btnLaporan.Location = new Point(327, 3);
            btnLaporan.Name = "btnLaporan";
            btnLaporan.Size = new Size(75, 23);
            btnLaporan.TabIndex = 4;
            // 
            // btnUser
            // 
            btnUser.Location = new Point(408, 3);
            btnUser.Name = "btnUser";
            btnUser.Size = new Size(75, 23);
            btnUser.TabIndex = 5;
            // Apply consistent navigation button style
            NavButtonStyle(btnDashboard, "Dashboard");
            NavButtonStyle(btnProduk, "Produk");
            NavButtonStyle(btnTransaksi, "Transaksi");
            NavButtonStyle(btnSupplier, "Supplier");
            NavButtonStyle(btnLaporan, "Laporan");
            NavButtonStyle(btnUser, "User");
            // 
            // brandPanel
            // 
            brandPanel.BackColor = Color.Transparent;
            brandPanel.Controls.Add(lblBrandSub);
            brandPanel.Controls.Add(lblBrand);
            brandPanel.Controls.Add(picLogo);
            brandPanel.Dock = DockStyle.Left;
            brandPanel.Location = new Point(20, 0);
            brandPanel.Name = "brandPanel";
            brandPanel.Size = new Size(210, 64);
            brandPanel.TabIndex = 1;
            // 
            // lblBrandSub
            // 
            lblBrandSub.AutoSize = true;
            lblBrandSub.Font = new Font("Segoe UI Semibold", 7F, FontStyle.Bold);
            lblBrandSub.ForeColor = Color.FromArgb(134, 239, 172);
            lblBrandSub.Location = new Point(48, 38);
            lblBrandSub.Name = "lblBrandSub";
            lblBrandSub.Size = new Size(129, 12);
            lblBrandSub.TabIndex = 0;
            lblBrandSub.Text = "SISTEM STOK & PENJUALAN";
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblBrand.ForeColor = Color.White;
            lblBrand.Location = new Point(48, 14);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(97, 25);
            lblBrand.TabIndex = 1;
            lblBrand.Text = "ChemLink";
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Location = new Point(4, 14);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(36, 36);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 2;
            picLogo.TabStop = false;
            // 
            // userPanel
            // 
            userPanel.BackColor = Color.Transparent;
            userPanel.Controls.Add(pnlAvatar);
            userPanel.Controls.Add(lblUsername);
            userPanel.Controls.Add(lblGreeting);
            userPanel.Controls.Add(btnLogout);
            userPanel.Dock = DockStyle.Right;
            userPanel.Location = new Point(930, 0);
            userPanel.Name = "userPanel";
            userPanel.Size = new Size(250, 64);
            userPanel.TabIndex = 2;
            // 
            // pnlAvatar
            // 
            pnlAvatar.BackColor = Color.FromArgb(22, 101, 52);
            pnlAvatar.Controls.Add(lblAvatar);
            pnlAvatar.Location = new Point(168, 13);
            pnlAvatar.Name = "pnlAvatar";
            pnlAvatar.Size = new Size(38, 38);
            pnlAvatar.TabIndex = 0;
            // 
            // lblAvatar
            // 
            lblAvatar.Dock = DockStyle.Fill;
            lblAvatar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblAvatar.ForeColor = Color.White;
            lblAvatar.Location = new Point(0, 0);
            lblAvatar.Name = "lblAvatar";
            lblAvatar.Size = new Size(38, 38);
            lblAvatar.TabIndex = 0;
            lblAvatar.Text = "ADM";
            lblAvatar.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lblUsername.ForeColor = Color.White;
            lblUsername.Location = new Point(0, 32);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(50, 19);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Admin";
            // 
            // lblGreeting
            // 
            lblGreeting.AutoSize = true;
            lblGreeting.Font = new Font("Segoe UI", 8F);
            lblGreeting.ForeColor = Color.FromArgb(134, 239, 172);
            lblGreeting.Location = new Point(0, 14);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new Size(57, 13);
            lblGreeting.TabIndex = 2;
            lblGreeting.Text = "Welcome,";
            lblGreeting.TextAlign = ContentAlignment.MiddleRight;
            lblGreeting.Click += lblGreeting_Click;
            // 
            // btnLogout
            // 
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogout.ForeColor = Color.FromArgb(134, 239, 172);
            btnLogout.Location = new Point(214, 18);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(30, 28);
            btnLogout.TabIndex = 3;
            btnLogout.Text = "\u23fb";
            btnLogout.UseVisualStyleBackColor = true;
            // 
            // mainContentPanel
            // 
            mainContentPanel.AutoScroll = true;
            mainContentPanel.BackColor = Color.FromArgb(248, 250, 252);
            mainContentPanel.Controls.Add(lblTitle);
            mainContentPanel.Dock = DockStyle.Fill;
            mainContentPanel.Location = new Point(0, 64);
            mainContentPanel.Name = "mainContentPanel";
            mainContentPanel.Size = new Size(1200, 685);
            mainContentPanel.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(24, 10, 0, 0);
            lblTitle.Size = new Size(1200, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Dashboard";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(1200, 749);
            Controls.Add(mainContentPanel);
            Controls.Add(topbarPanel);
            Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            MinimumSize = new Size(1000, 650);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ChemLink - Manajemen Kios Pertanian v2.0";
            topbarPanel.ResumeLayout(false);
            navPanel.ResumeLayout(false);
            brandPanel.ResumeLayout(false);
            brandPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            userPanel.ResumeLayout(false);
            userPanel.PerformLayout();
            pnlAvatar.ResumeLayout(false);
            mainContentPanel.ResumeLayout(false);
            ResumeLayout(false);
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
