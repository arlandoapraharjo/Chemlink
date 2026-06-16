namespace CHEMLINK.Views
{
    partial class DashboardControl
    {
        private System.ComponentModel.IContainer components = null;

        // Master layout (responsive wrapper)
        private System.Windows.Forms.TableLayoutPanel tblMaster;

        // Banner
        private System.Windows.Forms.Panel pnlBanner;
        private System.Windows.Forms.Panel pnlBadge;
        private System.Windows.Forms.Label lblBadge;
        private System.Windows.Forms.Label lblBannerTitle;
        private System.Windows.Forms.Label lblBannerDesc;

        // KPI Cards
        private System.Windows.Forms.TableLayoutPanel tblKPI;
        private System.Windows.Forms.Panel cardTotalProduk;
        private System.Windows.Forms.Panel cardTotalStok;
        private System.Windows.Forms.Panel cardStokKritis;
        private System.Windows.Forms.Panel cardKategori;
        private System.Windows.Forms.Label lblTotalProdukVal;
        private System.Windows.Forms.Label lblTotalProdukLbl;
        private System.Windows.Forms.Label lblTotalStokVal;
        private System.Windows.Forms.Label lblTotalStokLbl;
        private System.Windows.Forms.Label lblStokKritisVal;
        private System.Windows.Forms.Label lblStokKritisLbl;
        private System.Windows.Forms.Label lblKategoriVal;
        private System.Windows.Forms.Label lblKategoriLbl;

        // Status labels
        private System.Windows.Forms.Label lblStatusProduk;
        private System.Windows.Forms.Label lblStatusStok;
        private System.Windows.Forms.Label lblStatusKritis;
        private System.Windows.Forms.Label lblStatusKategori;

        // Notification
        private System.Windows.Forms.Label lblNotifTitle;

        private void InitializeComponent()
        {
            tblMaster = new TableLayoutPanel();
            pnlBanner = new Panel();
            lblBannerDesc = new Label();
            lblBannerTitle = new Label();
            pnlBadge = new Panel();
            lblBadge = new Label();
            tblKPI = new TableLayoutPanel();
            cardTotalProduk = new Panel();
            lblTotalProdukLbl = new Label();
            lblTotalProdukVal = new Label();
            lblStatusProduk = new Label();
            cardTotalStok = new Panel();
            lblTotalStokLbl = new Label();
            lblTotalStokVal = new Label();
            lblStatusStok = new Label();
            cardStokKritis = new Panel();
            lblStokKritisLbl = new Label();
            lblStokKritisVal = new Label();
            lblStatusKritis = new Label();
            cardKategori = new Panel();
            lblKategoriLbl = new Label();
            lblKategoriVal = new Label();
            lblStatusKategori = new Label();
            lblNotifTitle = new Label();
            dataGridView1 = new DataGridView();
            tblMaster.SuspendLayout();
            pnlBanner.SuspendLayout();
            pnlBadge.SuspendLayout();
            tblKPI.SuspendLayout();
            cardTotalProduk.SuspendLayout();
            cardTotalStok.SuspendLayout();
            cardStokKritis.SuspendLayout();
            cardKategori.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // tblMaster
            // 
            tblMaster.AutoScroll = true;
            tblMaster.ColumnCount = 1;
            tblMaster.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblMaster.Controls.Add(pnlBanner, 0, 0);
            tblMaster.Controls.Add(tblKPI, 0, 1);
            tblMaster.Controls.Add(lblNotifTitle, 0, 2);
            tblMaster.Controls.Add(dataGridView1, 0, 3);
            tblMaster.Dock = DockStyle.Fill;
            tblMaster.Location = new Point(24, 16);
            tblMaster.Name = "tblMaster";
            tblMaster.RowCount = 4;
            tblMaster.RowStyles.Add(new RowStyle(SizeType.Absolute, 130F));
            tblMaster.RowStyles.Add(new RowStyle(SizeType.Absolute, 105F));
            tblMaster.RowStyles.Add(new RowStyle());
            tblMaster.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblMaster.Size = new Size(1152, 668);
            tblMaster.TabIndex = 0;
            // 
            // pnlBanner
            // 
            pnlBanner.BackColor = Color.FromArgb(2, 44, 34);
            pnlBanner.Controls.Add(lblBannerDesc);
            pnlBanner.Controls.Add(lblBannerTitle);
            pnlBanner.Controls.Add(pnlBadge);
            pnlBanner.Dock = DockStyle.Fill;
            pnlBanner.Location = new Point(3, 3);
            pnlBanner.Name = "pnlBanner";
            pnlBanner.Size = new Size(1146, 124);
            pnlBanner.TabIndex = 0;
            // 
            // lblBannerDesc
            // 
            lblBannerDesc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblBannerDesc.BackColor = Color.Transparent;
            lblBannerDesc.Font = new Font("Segoe UI", 9.5F);
            lblBannerDesc.ForeColor = Color.FromArgb(226, 232, 240);
            lblBannerDesc.Location = new Point(32, 76);
            lblBannerDesc.Name = "lblBannerDesc";
            lblBannerDesc.Size = new Size(1596, 36);
            lblBannerDesc.TabIndex = 0;
            lblBannerDesc.Text = "Pantau pergerakan stok obat pertanian, pupuk, pestisida, dan peralatan tani Anda secara real-time untuk produktivitas optimal.";
            // 
            // lblBannerTitle
            // 
            lblBannerTitle.AutoSize = true;
            lblBannerTitle.BackColor = Color.Transparent;
            lblBannerTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblBannerTitle.ForeColor = Color.White;
            lblBannerTitle.Location = new Point(32, 44);
            lblBannerTitle.Name = "lblBannerTitle";
            lblBannerTitle.Size = new Size(303, 37);
            lblBannerTitle.TabIndex = 1;
            lblBannerTitle.Text = "Manajemen Inventaris";
            // 
            // pnlBadge
            // 
            pnlBadge.BackColor = Color.FromArgb(48, 255, 255, 255);
            pnlBadge.Controls.Add(lblBadge);
            pnlBadge.Location = new Point(32, 16);
            pnlBadge.Name = "pnlBadge";
            pnlBadge.Size = new Size(210, 22);
            pnlBadge.TabIndex = 2;
            // 
            // lblBadge
            // 
            lblBadge.BackColor = Color.Transparent;
            lblBadge.Dock = DockStyle.Fill;
            lblBadge.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold);
            lblBadge.ForeColor = Color.FromArgb(167, 243, 208);
            lblBadge.Location = new Point(0, 0);
            lblBadge.Name = "lblBadge";
            lblBadge.Size = new Size(210, 22);
            lblBadge.TabIndex = 0;
            lblBadge.Text = "SISTEM MONITORING UTAMA";
            lblBadge.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tblKPI
            // 
            tblKPI.ColumnCount = 4;
            tblKPI.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblKPI.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblKPI.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblKPI.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblKPI.Controls.Add(cardTotalProduk, 0, 0);
            tblKPI.Controls.Add(cardTotalStok, 1, 0);
            tblKPI.Controls.Add(cardStokKritis, 2, 0);
            tblKPI.Controls.Add(cardKategori, 3, 0);
            tblKPI.Dock = DockStyle.Fill;
            tblKPI.Location = new Point(3, 133);
            tblKPI.Name = "tblKPI";
            tblKPI.Padding = new Padding(0, 8, 0, 0);
            tblKPI.RowCount = 1;
            tblKPI.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblKPI.Size = new Size(1146, 99);
            tblKPI.TabIndex = 1;
            // 
            // cardTotalProduk
            // 
            cardTotalProduk.BackColor = Color.FromArgb(232, 245, 189);
            cardTotalProduk.Controls.Add(lblTotalProdukLbl);
            cardTotalProduk.Controls.Add(lblTotalProdukVal);
            cardTotalProduk.Controls.Add(lblStatusProduk);
            cardTotalProduk.Dock = DockStyle.Fill;
            cardTotalProduk.Location = new Point(6, 8);
            cardTotalProduk.Margin = new Padding(6, 0, 6, 0);
            cardTotalProduk.Name = "cardTotalProduk";
            cardTotalProduk.Padding = new Padding(12);
            cardTotalProduk.Size = new Size(274, 91);
            cardTotalProduk.TabIndex = 0;
            // 
            // lblTotalProdukLbl
            // 
            lblTotalProdukLbl.AutoSize = true;
            lblTotalProdukLbl.BackColor = Color.Transparent;
            lblTotalProdukLbl.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblTotalProdukLbl.ForeColor = Color.FromArgb(100, 116, 139);
            lblTotalProdukLbl.Location = new Point(12, 8);
            lblTotalProdukLbl.Name = "lblTotalProdukLbl";
            lblTotalProdukLbl.Size = new Size(104, 15);
            lblTotalProdukLbl.TabIndex = 0;
            lblTotalProdukLbl.Text = "JUMLAH PRODUK";
            // 
            // lblTotalProdukVal
            // 
            lblTotalProdukVal.AutoSize = true;
            lblTotalProdukVal.BackColor = Color.Transparent;
            lblTotalProdukVal.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalProdukVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblTotalProdukVal.Location = new Point(12, 28);
            lblTotalProdukVal.Name = "lblTotalProdukVal";
            lblTotalProdukVal.Size = new Size(35, 41);
            lblTotalProdukVal.TabIndex = 1;
            lblTotalProdukVal.Text = "0";
            // 
            // lblStatusProduk
            // 
            lblStatusProduk.AutoSize = true;
            lblStatusProduk.BackColor = Color.Transparent;
            lblStatusProduk.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblStatusProduk.ForeColor = Color.FromArgb(30, 41, 59);
            lblStatusProduk.Location = new Point(12, 64);
            lblStatusProduk.Name = "lblStatusProduk";
            lblStatusProduk.Size = new Size(123, 15);
            lblStatusProduk.TabIndex = 2;
            lblStatusProduk.Text = "●  Aktif dalam katalog";
            // 
            // cardTotalStok
            // 
            cardTotalStok.BackColor = Color.FromArgb(199, 234, 187);
            cardTotalStok.Controls.Add(lblTotalStokLbl);
            cardTotalStok.Controls.Add(lblTotalStokVal);
            cardTotalStok.Controls.Add(lblStatusStok);
            cardTotalStok.Dock = DockStyle.Fill;
            cardTotalStok.Location = new Point(292, 8);
            cardTotalStok.Margin = new Padding(6, 0, 6, 0);
            cardTotalStok.Name = "cardTotalStok";
            cardTotalStok.Padding = new Padding(12);
            cardTotalStok.Size = new Size(274, 91);
            cardTotalStok.TabIndex = 1;
            // 
            // lblTotalStokLbl
            // 
            lblTotalStokLbl.AutoSize = true;
            lblTotalStokLbl.BackColor = Color.Transparent;
            lblTotalStokLbl.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblTotalStokLbl.ForeColor = Color.FromArgb(100, 116, 139);
            lblTotalStokLbl.Location = new Point(12, 8);
            lblTotalStokLbl.Name = "lblTotalStokLbl";
            lblTotalStokLbl.Size = new Size(106, 15);
            lblTotalStokLbl.TabIndex = 0;
            lblTotalStokLbl.Text = "TOTAL STOK ITEM";
            // 
            // lblTotalStokVal
            // 
            lblTotalStokVal.AutoSize = true;
            lblTotalStokVal.BackColor = Color.Transparent;
            lblTotalStokVal.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalStokVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblTotalStokVal.Location = new Point(12, 28);
            lblTotalStokVal.Name = "lblTotalStokVal";
            lblTotalStokVal.Size = new Size(35, 41);
            lblTotalStokVal.TabIndex = 1;
            lblTotalStokVal.Text = "0";
            // 
            // lblStatusStok
            // 
            lblStatusStok.AutoSize = true;
            lblStatusStok.BackColor = Color.Transparent;
            lblStatusStok.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblStatusStok.ForeColor = Color.FromArgb(30, 41, 59);
            lblStatusStok.Location = new Point(12, 64);
            lblStatusStok.Name = "lblStatusStok";
            lblStatusStok.Size = new Size(120, 15);
            lblStatusStok.TabIndex = 2;
            lblStatusStok.Text = "●  Tersedia di gudang";
            // 
            // cardStokKritis
            // 
            cardStokKritis.BackColor = Color.FromArgb(232, 245, 189);
            cardStokKritis.Controls.Add(lblStokKritisLbl);
            cardStokKritis.Controls.Add(lblStokKritisVal);
            cardStokKritis.Controls.Add(lblStatusKritis);
            cardStokKritis.Dock = DockStyle.Fill;
            cardStokKritis.Location = new Point(578, 8);
            cardStokKritis.Margin = new Padding(6, 0, 6, 0);
            cardStokKritis.Name = "cardStokKritis";
            cardStokKritis.Padding = new Padding(12);
            cardStokKritis.Size = new Size(274, 91);
            cardStokKritis.TabIndex = 2;
            // 
            // lblStokKritisLbl
            // 
            lblStokKritisLbl.AutoSize = true;
            lblStokKritisLbl.BackColor = Color.Transparent;
            lblStokKritisLbl.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblStokKritisLbl.ForeColor = Color.FromArgb(100, 116, 139);
            lblStokKritisLbl.Location = new Point(12, 8);
            lblStokKritisLbl.Name = "lblStokKritisLbl";
            lblStokKritisLbl.Size = new Size(76, 15);
            lblStokKritisLbl.TabIndex = 0;
            lblStokKritisLbl.Text = "STOK KRITIS";
            // 
            // lblStokKritisVal
            // 
            lblStokKritisVal.AutoSize = true;
            lblStokKritisVal.BackColor = Color.Transparent;
            lblStokKritisVal.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblStokKritisVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblStokKritisVal.Location = new Point(12, 28);
            lblStokKritisVal.Name = "lblStokKritisVal";
            lblStokKritisVal.Size = new Size(35, 41);
            lblStokKritisVal.TabIndex = 1;
            lblStokKritisVal.Text = "0";
            // 
            // lblStatusKritis
            // 
            lblStatusKritis.AutoSize = true;
            lblStatusKritis.BackColor = Color.Transparent;
            lblStatusKritis.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblStatusKritis.ForeColor = Color.FromArgb(30, 41, 59);
            lblStatusKritis.Location = new Point(12, 64);
            lblStatusKritis.Name = "lblStatusKritis";
            lblStatusKritis.Size = new Size(119, 15);
            lblStatusKritis.TabIndex = 2;
            lblStatusKritis.Text = "●  Perlu restok segera";
            // 
            // cardKategori
            // 
            cardKategori.BackColor = Color.FromArgb(199, 234, 187);
            cardKategori.Controls.Add(lblKategoriLbl);
            cardKategori.Controls.Add(lblKategoriVal);
            cardKategori.Controls.Add(lblStatusKategori);
            cardKategori.Dock = DockStyle.Fill;
            cardKategori.Location = new Point(864, 8);
            cardKategori.Margin = new Padding(6, 0, 6, 0);
            cardKategori.Name = "cardKategori";
            cardKategori.Padding = new Padding(12);
            cardKategori.Size = new Size(276, 91);
            cardKategori.TabIndex = 3;
            // 
            // lblKategoriLbl
            // 
            lblKategoriLbl.AutoSize = true;
            lblKategoriLbl.BackColor = Color.Transparent;
            lblKategoriLbl.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            lblKategoriLbl.ForeColor = Color.FromArgb(100, 116, 139);
            lblKategoriLbl.Location = new Point(12, 8);
            lblKategoriLbl.Name = "lblKategoriLbl";
            lblKategoriLbl.Size = new Size(100, 15);
            lblKategoriLbl.TabIndex = 0;
            lblKategoriLbl.Text = "TOTAL KATEGORI";
            // 
            // lblKategoriVal
            // 
            lblKategoriVal.AutoSize = true;
            lblKategoriVal.BackColor = Color.Transparent;
            lblKategoriVal.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblKategoriVal.ForeColor = Color.FromArgb(30, 41, 59);
            lblKategoriVal.Location = new Point(12, 28);
            lblKategoriVal.Name = "lblKategoriVal";
            lblKategoriVal.Size = new Size(35, 41);
            lblKategoriVal.TabIndex = 1;
            lblKategoriVal.Text = "0";
            // 
            // lblStatusKategori
            // 
            lblStatusKategori.AutoSize = true;
            lblStatusKategori.BackColor = Color.Transparent;
            lblStatusKategori.Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold);
            lblStatusKategori.ForeColor = Color.FromArgb(30, 41, 59);
            lblStatusKategori.Location = new Point(12, 64);
            lblStatusKategori.Name = "lblStatusKategori";
            lblStatusKategori.Size = new Size(113, 15);
            lblStatusKategori.TabIndex = 2;
            lblStatusKategori.Text = "●  Jenis produk aktif";
            // 
            // lblNotifTitle
            // 
            lblNotifTitle.AutoSize = true;
            lblNotifTitle.Dock = DockStyle.Fill;
            lblNotifTitle.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            lblNotifTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblNotifTitle.Location = new Point(3, 235);
            lblNotifTitle.Name = "lblNotifTitle";
            lblNotifTitle.Padding = new Padding(0, 20, 0, 8);
            lblNotifTitle.Size = new Size(1146, 53);
            lblNotifTitle.TabIndex = 2;
            lblNotifTitle.Text = "  Notifikasi & Peringatan Stok";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(3, 291);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1146, 166);
            dataGridView1.TabIndex = 3;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // DashboardControl
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(tblMaster);
            Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Name = "DashboardControl";
            Padding = new Padding(24, 16, 24, 16);
            Size = new Size(1200, 700);
            tblMaster.ResumeLayout(false);
            tblMaster.PerformLayout();
            pnlBanner.ResumeLayout(false);
            pnlBanner.PerformLayout();
            pnlBadge.ResumeLayout(false);
            tblKPI.ResumeLayout(false);
            cardTotalProduk.ResumeLayout(false);
            cardTotalProduk.PerformLayout();
            cardTotalStok.ResumeLayout(false);
            cardTotalStok.PerformLayout();
            cardStokKritis.ResumeLayout(false);
            cardStokKritis.PerformLayout();
            cardKategori.ResumeLayout(false);
            cardKategori.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        private DataGridView dataGridView1;
    }
}
