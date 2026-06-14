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
        public System.Windows.Forms.DataGridView dgvMain;

        private void InitializeComponent()
        {
            this.tblMaster = new System.Windows.Forms.TableLayoutPanel();
            this.pnlBanner = new System.Windows.Forms.Panel();
            this.pnlBadge = new System.Windows.Forms.Panel();
            this.lblBadge = new System.Windows.Forms.Label();
            this.lblBannerTitle = new System.Windows.Forms.Label();
            this.lblBannerDesc = new System.Windows.Forms.Label();
            this.tblKPI = new System.Windows.Forms.TableLayoutPanel();
            this.cardTotalProduk = new System.Windows.Forms.Panel();
            this.cardTotalStok = new System.Windows.Forms.Panel();
            this.cardStokKritis = new System.Windows.Forms.Panel();
            this.cardKategori = new System.Windows.Forms.Panel();
            this.lblTotalProdukVal = new System.Windows.Forms.Label();
            this.lblTotalProdukLbl = new System.Windows.Forms.Label();
            this.lblTotalStokVal = new System.Windows.Forms.Label();
            this.lblTotalStokLbl = new System.Windows.Forms.Label();
            this.lblStokKritisVal = new System.Windows.Forms.Label();
            this.lblStokKritisLbl = new System.Windows.Forms.Label();
            this.lblKategoriVal = new System.Windows.Forms.Label();
            this.lblKategoriLbl = new System.Windows.Forms.Label();
            this.lblStatusProduk = new System.Windows.Forms.Label();
            this.lblStatusStok = new System.Windows.Forms.Label();
            this.lblStatusKritis = new System.Windows.Forms.Label();
            this.lblStatusKategori = new System.Windows.Forms.Label();
            this.lblNotifTitle = new System.Windows.Forms.Label();
            this.dgvMain = new System.Windows.Forms.DataGridView();
            this.tblMaster.SuspendLayout();
            this.tblKPI.SuspendLayout();
            this.cardTotalProduk.SuspendLayout();
            this.cardTotalStok.SuspendLayout();
            this.cardStokKritis.SuspendLayout();
            this.cardKategori.SuspendLayout();
            this.pnlBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();

            // ===================== MASTER TABLE (responsive root) =====================
            this.tblMaster.ColumnCount = 1;
            this.tblMaster.RowCount = 4;
            this.tblMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMaster.AutoScroll = true;
            this.tblMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 155F));   // banner
            this.tblMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));   // KPI cards
            this.tblMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));          // notif title
            this.tblMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));    // grid fills rest
            this.tblMaster.Controls.Add(this.pnlBanner, 0, 0);
            this.tblMaster.Controls.Add(this.tblKPI, 0, 1);
            this.tblMaster.Controls.Add(this.lblNotifTitle, 0, 2);
            this.tblMaster.Controls.Add(this.dgvMain, 0, 3);

            // ===================== BANNER =====================
            this.pnlBanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBanner.BackColor = System.Drawing.Color.FromArgb(2, 44, 34);
            this.pnlBanner.Controls.Add(this.lblBannerDesc);
            this.pnlBanner.Controls.Add(this.lblBannerTitle);
            this.pnlBanner.Controls.Add(this.pnlBadge);

            // pnlBadge
            this.pnlBadge.BackColor = System.Drawing.Color.FromArgb(48, 255, 255, 255);
            this.pnlBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.pnlBadge.Location = new System.Drawing.Point(32, 16);
            this.pnlBadge.Size = new System.Drawing.Size(210, 22);
            this.pnlBadge.Controls.Add(this.lblBadge);

            // lblBadge
            this.lblBadge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBadge.BackColor = System.Drawing.Color.Transparent;
            this.lblBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblBadge.ForeColor = System.Drawing.Color.FromArgb(167, 243, 208);
            this.lblBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBadge.Text = "SISTEM MONITORING UTAMA";

            // lblBannerTitle
            this.lblBannerTitle.AutoSize = true;
            this.lblBannerTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblBannerTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblBannerTitle.ForeColor = System.Drawing.Color.White;
            this.lblBannerTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblBannerTitle.Location = new System.Drawing.Point(32, 44);
            this.lblBannerTitle.Text = "Manajemen Inventaris";

            // lblBannerDesc
            this.lblBannerDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblBannerDesc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblBannerDesc.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.lblBannerDesc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblBannerDesc.Location = new System.Drawing.Point(32, 76);
            this.lblBannerDesc.AutoSize = false;
            this.lblBannerDesc.Size = new System.Drawing.Size(650, 36);
            this.lblBannerDesc.Text = "Pantau pergerakan stok obat pertanian, pupuk, pestisida, dan peralatan tani Anda secara real-time untuk produktivitas optimal.";

            // ===================== KPI TABLE (4 equal columns) =====================
            this.tblKPI.ColumnCount = 4;
            this.tblKPI.RowCount = 1;
            this.tblKPI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblKPI.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tblKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKPI.Controls.Add(this.cardTotalProduk, 0, 0);
            this.tblKPI.Controls.Add(this.cardTotalStok, 1, 0);
            this.tblKPI.Controls.Add(this.cardStokKritis, 2, 0);
            this.tblKPI.Controls.Add(this.cardKategori, 3, 0);

            // ===================== CARD 1: TOTAL PRODUK (#E8F5BD) =====================
            this.cardTotalProduk.BackColor = System.Drawing.Color.FromArgb(232, 245, 189);
            this.cardTotalProduk.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.cardTotalProduk.Padding = new System.Windows.Forms.Padding(12);
            this.cardTotalProduk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTotalProduk.Controls.Add(this.lblTotalProdukLbl);
            this.cardTotalProduk.Controls.Add(this.lblTotalProdukVal);
            this.cardTotalProduk.Controls.Add(this.lblStatusProduk);

            this.lblTotalProdukLbl.AutoSize = true;
            this.lblTotalProdukLbl.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalProdukLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalProdukLbl.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTotalProdukLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalProdukLbl.Location = new System.Drawing.Point(12, 8);
            this.lblTotalProdukLbl.Text = "JUMLAH PRODUK";

            this.lblTotalProdukVal.AutoSize = true;
            this.lblTotalProdukVal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalProdukVal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTotalProdukVal.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTotalProdukVal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalProdukVal.Location = new System.Drawing.Point(12, 36);
            this.lblTotalProdukVal.Text = "0";

            this.lblStatusProduk.AutoSize = true;
            this.lblStatusProduk.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusProduk.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusProduk.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblStatusProduk.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblStatusProduk.Location = new System.Drawing.Point(12, 64);
            this.lblStatusProduk.Text = "\u25CF  Aktif dalam katalog";

            // ===================== CARD 2: TOTAL STOK (#C7EABB) =====================
            this.cardTotalStok.BackColor = System.Drawing.Color.FromArgb(199, 234, 187);
            this.cardTotalStok.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.cardTotalStok.Padding = new System.Windows.Forms.Padding(12);
            this.cardTotalStok.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTotalStok.Controls.Add(this.lblTotalStokLbl);
            this.cardTotalStok.Controls.Add(this.lblTotalStokVal);
            this.cardTotalStok.Controls.Add(this.lblStatusStok);

            this.lblTotalStokLbl.AutoSize = true;
            this.lblTotalStokLbl.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalStokLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalStokLbl.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTotalStokLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalStokLbl.Location = new System.Drawing.Point(12, 8);
            this.lblTotalStokLbl.Text = "TOTAL STOK ITEM";

            this.lblTotalStokVal.AutoSize = true;
            this.lblTotalStokVal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalStokVal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTotalStokVal.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTotalStokVal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalStokVal.Location = new System.Drawing.Point(12, 36);
            this.lblTotalStokVal.Text = "0";

            this.lblStatusStok.AutoSize = true;
            this.lblStatusStok.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusStok.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusStok.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblStatusStok.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblStatusStok.Location = new System.Drawing.Point(12, 64);
            this.lblStatusStok.Text = "\u25CF  Tersedia di gudang";

            // ===================== CARD 3: STOK KRITIS (#E8F5BD) =====================
            this.cardStokKritis.BackColor = System.Drawing.Color.FromArgb(232, 245, 189);
            this.cardStokKritis.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.cardStokKritis.Padding = new System.Windows.Forms.Padding(12);
            this.cardStokKritis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardStokKritis.Controls.Add(this.lblStokKritisLbl);
            this.cardStokKritis.Controls.Add(this.lblStokKritisVal);
            this.cardStokKritis.Controls.Add(this.lblStatusKritis);

            this.lblStokKritisLbl.AutoSize = true;
            this.lblStokKritisLbl.BackColor = System.Drawing.Color.Transparent;
            this.lblStokKritisLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblStokKritisLbl.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblStokKritisLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblStokKritisLbl.Location = new System.Drawing.Point(12, 8);
            this.lblStokKritisLbl.Text = "STOK KRITIS";

            this.lblStokKritisVal.AutoSize = true;
            this.lblStokKritisVal.BackColor = System.Drawing.Color.Transparent;
            this.lblStokKritisVal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblStokKritisVal.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblStokKritisVal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblStokKritisVal.Location = new System.Drawing.Point(12, 36);
            this.lblStokKritisVal.Text = "0";

            this.lblStatusKritis.AutoSize = true;
            this.lblStatusKritis.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusKritis.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusKritis.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblStatusKritis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblStatusKritis.Location = new System.Drawing.Point(12, 64);
            this.lblStatusKritis.Text = "\u25CF  Perlu restok segera";

            // ===================== CARD 4: KATEGORI (#C7EABB) =====================
            this.cardKategori.BackColor = System.Drawing.Color.FromArgb(199, 234, 187);
            this.cardKategori.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.cardKategori.Padding = new System.Windows.Forms.Padding(12);
            this.cardKategori.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardKategori.Controls.Add(this.lblKategoriLbl);
            this.cardKategori.Controls.Add(this.lblKategoriVal);
            this.cardKategori.Controls.Add(this.lblStatusKategori);

            this.lblKategoriLbl.AutoSize = true;
            this.lblKategoriLbl.BackColor = System.Drawing.Color.Transparent;
            this.lblKategoriLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblKategoriLbl.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblKategoriLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblKategoriLbl.Location = new System.Drawing.Point(12, 8);
            this.lblKategoriLbl.Text = "TOTAL KATEGORI";

            this.lblKategoriVal.AutoSize = true;
            this.lblKategoriVal.BackColor = System.Drawing.Color.Transparent;
            this.lblKategoriVal.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblKategoriVal.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblKategoriVal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblKategoriVal.Location = new System.Drawing.Point(12, 36);
            this.lblKategoriVal.Text = "0";

            this.lblStatusKategori.AutoSize = true;
            this.lblStatusKategori.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusKategori.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusKategori.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblStatusKategori.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblStatusKategori.Location = new System.Drawing.Point(12, 64);
            this.lblStatusKategori.Text = "\u25CF  Jenis produk aktif";

            // ===================== NOTIFICATION TITLE =====================
            this.lblNotifTitle.AutoSize = true;
            this.lblNotifTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNotifTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblNotifTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblNotifTitle.Padding = new System.Windows.Forms.Padding(0, 20, 0, 8);
            this.lblNotifTitle.Text = "  Notifikasi & Peringatan Stok";

            // ===================== DATAGRIDVIEW =====================
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.BackgroundColor = System.Drawing.Color.White;
            this.dgvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMain.MultiSelect = false;
            this.dgvMain.ReadOnly = true;
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.RowTemplate.Height = 36;

            // ===================== DASHBOARD CONTROL =====================
            this.Controls.Add(this.tblMaster);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(1200, 700);
            this.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);

            this.tblMaster.ResumeLayout(false);
            this.tblMaster.PerformLayout();
            this.tblKPI.ResumeLayout(false);
            this.cardTotalProduk.ResumeLayout(false);
            this.cardTotalProduk.PerformLayout();
            this.cardTotalStok.ResumeLayout(false);
            this.cardTotalStok.PerformLayout();
            this.cardStokKritis.ResumeLayout(false);
            this.cardStokKritis.PerformLayout();
            this.cardKategori.ResumeLayout(false);
            this.cardKategori.PerformLayout();
            this.pnlBanner.ResumeLayout(false);
            this.pnlBanner.PerformLayout();
            this.pnlBadge.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
