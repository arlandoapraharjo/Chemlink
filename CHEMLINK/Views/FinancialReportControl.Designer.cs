namespace CHEMLINK.Views
{
    partial class FinancialReportControl
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
        private System.Windows.Forms.Panel cardOmzet;
        private System.Windows.Forms.Panel cardTransaksi;
        private System.Windows.Forms.Panel cardRataRata;
        private System.Windows.Forms.Panel cardBestMonth;
        private System.Windows.Forms.Label lblOmzetLbl;
        private System.Windows.Forms.Label lblOmzetVal;
        private System.Windows.Forms.Label lblOmzetStatus;
        private System.Windows.Forms.Label lblTransaksiLbl;
        private System.Windows.Forms.Label lblTransaksiVal;
        private System.Windows.Forms.Label lblTransaksiStatus;
        private System.Windows.Forms.Label lblRataRataLbl;
        private System.Windows.Forms.Label lblRataRataVal;
        private System.Windows.Forms.Label lblRataRataStatus;
        private System.Windows.Forms.Label lblBestMonthLbl;
        private System.Windows.Forms.Label lblBestMonthVal;
        private System.Windows.Forms.Label lblBestMonthStatus;

        // Data grids section (side-by-side)
        private System.Windows.Forms.TableLayoutPanel tblGrids;
        private System.Windows.Forms.Label lblMonthlyTitle;
        public System.Windows.Forms.DataGridView dgvMonthly;
        private System.Windows.Forms.Panel pnlMonthlyGrid;
        private System.Windows.Forms.Label lblCategoryTitle;
        public System.Windows.Forms.DataGridView dgvCategory;
        private System.Windows.Forms.Panel pnlCategoryGrid;

        private void InitializeComponent()
        {
            this.tblMaster = new System.Windows.Forms.TableLayoutPanel();
            this.pnlBanner = new System.Windows.Forms.Panel();
            this.pnlBadge = new System.Windows.Forms.Panel();
            this.lblBadge = new System.Windows.Forms.Label();
            this.lblBannerTitle = new System.Windows.Forms.Label();
            this.lblBannerDesc = new System.Windows.Forms.Label();
            this.tblKPI = new System.Windows.Forms.TableLayoutPanel();
            this.cardOmzet = new System.Windows.Forms.Panel();
            this.cardTransaksi = new System.Windows.Forms.Panel();
            this.cardRataRata = new System.Windows.Forms.Panel();
            this.cardBestMonth = new System.Windows.Forms.Panel();
            this.lblOmzetLbl = new System.Windows.Forms.Label();
            this.lblOmzetVal = new System.Windows.Forms.Label();
            this.lblOmzetStatus = new System.Windows.Forms.Label();
            this.lblTransaksiLbl = new System.Windows.Forms.Label();
            this.lblTransaksiVal = new System.Windows.Forms.Label();
            this.lblTransaksiStatus = new System.Windows.Forms.Label();
            this.lblRataRataLbl = new System.Windows.Forms.Label();
            this.lblRataRataVal = new System.Windows.Forms.Label();
            this.lblRataRataStatus = new System.Windows.Forms.Label();
            this.lblBestMonthLbl = new System.Windows.Forms.Label();
            this.lblBestMonthVal = new System.Windows.Forms.Label();
            this.lblBestMonthStatus = new System.Windows.Forms.Label();
            this.tblGrids = new System.Windows.Forms.TableLayoutPanel();
            this.lblMonthlyTitle = new System.Windows.Forms.Label();
            this.dgvMonthly = new System.Windows.Forms.DataGridView();
            this.pnlMonthlyGrid = new System.Windows.Forms.Panel();
            this.lblCategoryTitle = new System.Windows.Forms.Label();
            this.dgvCategory = new System.Windows.Forms.DataGridView();
            this.pnlCategoryGrid = new System.Windows.Forms.Panel();
            this.tblMaster.SuspendLayout();
            this.tblKPI.SuspendLayout();
            this.cardOmzet.SuspendLayout();
            this.cardTransaksi.SuspendLayout();
            this.cardRataRata.SuspendLayout();
            this.cardBestMonth.SuspendLayout();
            this.pnlBanner.SuspendLayout();
            this.tblGrids.SuspendLayout();
            this.pnlMonthlyGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthly)).BeginInit();
            this.pnlCategoryGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).BeginInit();
            this.SuspendLayout();

            // ===================== MASTER TABLE (responsive root) =====================
            this.tblMaster.ColumnCount = 1;
            this.tblMaster.RowCount = 3;
            this.tblMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMaster.AutoScroll = true;
            this.tblMaster.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 155F));   // banner
            this.tblMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));   // KPI cards
            this.tblMaster.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));    // grids fill rest
            this.tblMaster.Controls.Add(this.pnlBanner, 0, 0);
            this.tblMaster.Controls.Add(this.tblKPI, 0, 1);
            this.tblMaster.Controls.Add(this.tblGrids, 0, 2);

            // ===================== BANNER =====================
            this.pnlBanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBanner.BackColor = System.Drawing.Color.FromArgb(2, 44, 34);
            this.pnlBanner.Controls.Add(this.lblBannerDesc);
            this.pnlBanner.Controls.Add(this.lblBannerTitle);
            this.pnlBanner.Controls.Add(this.pnlBadge);

            // pnlBadge
            this.pnlBadge.BackColor = System.Drawing.Color.FromArgb(48, 255, 255, 255);
            this.pnlBadge.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.pnlBadge.Location = new System.Drawing.Point(24, 14);
            this.pnlBadge.Size = new System.Drawing.Size(200, 22);
            this.pnlBadge.Controls.Add(this.lblBadge);

            // lblBadge
            this.lblBadge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBadge.BackColor = System.Drawing.Color.Transparent;
            this.lblBadge.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblBadge.ForeColor = System.Drawing.Color.FromArgb(167, 243, 208);
            this.lblBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBadge.Text = "ANALISIS PENJUALAN";

            // lblBannerTitle
            this.lblBannerTitle.AutoSize = true;
            this.lblBannerTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblBannerTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblBannerTitle.ForeColor = System.Drawing.Color.White;
            this.lblBannerTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblBannerTitle.Location = new System.Drawing.Point(24, 40);
            this.lblBannerTitle.Text = "Laporan Keuangan";

            // lblBannerDesc
            this.lblBannerDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblBannerDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBannerDesc.ForeColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.lblBannerDesc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.lblBannerDesc.Location = new System.Drawing.Point(24, 76);
            this.lblBannerDesc.AutoSize = false;
            this.lblBannerDesc.Size = new System.Drawing.Size(600, 30);
            this.lblBannerDesc.Text = "Ringkasan omzet, transaksi, dan performa kategori produk secara berkala.";

            // ===================== KPI TABLE (4 equal columns) =====================
            this.tblKPI.ColumnCount = 4;
            this.tblKPI.RowCount = 1;
            this.tblKPI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblKPI.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tblKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblKPI.Controls.Add(this.cardOmzet, 0, 0);
            this.tblKPI.Controls.Add(this.cardTransaksi, 1, 0);
            this.tblKPI.Controls.Add(this.cardRataRata, 2, 0);
            this.tblKPI.Controls.Add(this.cardBestMonth, 3, 0);

            // ===================== CARD 1: TOTAL OMZET =====================
            this.cardOmzet.BackColor = System.Drawing.Color.FromArgb(232, 245, 189);
            this.cardOmzet.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.cardOmzet.Padding = new System.Windows.Forms.Padding(14);
            this.cardOmzet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardOmzet.Controls.Add(this.lblOmzetStatus);
            this.cardOmzet.Controls.Add(this.lblOmzetVal);
            this.cardOmzet.Controls.Add(this.lblOmzetLbl);

            this.lblOmzetLbl.AutoSize = true;
            this.lblOmzetLbl.BackColor = System.Drawing.Color.Transparent;
            this.lblOmzetLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblOmzetLbl.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblOmzetLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblOmzetLbl.Location = new System.Drawing.Point(14, 10);
            this.lblOmzetLbl.Text = "TOTAL OMZET";

            this.lblOmzetVal.AutoSize = true;
            this.lblOmzetVal.BackColor = System.Drawing.Color.Transparent;
            this.lblOmzetVal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblOmzetVal.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblOmzetVal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblOmzetVal.Location = new System.Drawing.Point(14, 36);
            this.lblOmzetVal.Text = "Rp 0";

            this.lblOmzetStatus.AutoSize = true;
            this.lblOmzetStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblOmzetStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblOmzetStatus.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblOmzetStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblOmzetStatus.Location = new System.Drawing.Point(14, 72);
            this.lblOmzetStatus.Text = "\u25CF  Akumulasi seluruh bulan";

            // ===================== CARD 2: TOTAL TRANSAKSI =====================
            this.cardTransaksi.BackColor = System.Drawing.Color.FromArgb(199, 234, 187);
            this.cardTransaksi.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.cardTransaksi.Padding = new System.Windows.Forms.Padding(14);
            this.cardTransaksi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTransaksi.Controls.Add(this.lblTransaksiStatus);
            this.cardTransaksi.Controls.Add(this.lblTransaksiVal);
            this.cardTransaksi.Controls.Add(this.lblTransaksiLbl);

            this.lblTransaksiLbl.AutoSize = true;
            this.lblTransaksiLbl.BackColor = System.Drawing.Color.Transparent;
            this.lblTransaksiLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblTransaksiLbl.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTransaksiLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTransaksiLbl.Location = new System.Drawing.Point(14, 10);
            this.lblTransaksiLbl.Text = "TOTAL TRANSAKSI";

            this.lblTransaksiVal.AutoSize = true;
            this.lblTransaksiVal.BackColor = System.Drawing.Color.Transparent;
            this.lblTransaksiVal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTransaksiVal.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTransaksiVal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTransaksiVal.Location = new System.Drawing.Point(14, 36);
            this.lblTransaksiVal.Text = "0";

            this.lblTransaksiStatus.AutoSize = true;
            this.lblTransaksiStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblTransaksiStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTransaksiStatus.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblTransaksiStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblTransaksiStatus.Location = new System.Drawing.Point(14, 72);
            this.lblTransaksiStatus.Text = "\u25CF  Keseluruhan periode";

            // ===================== CARD 3: RATA-RATA / BULAN =====================
            this.cardRataRata.BackColor = System.Drawing.Color.FromArgb(232, 245, 189);
            this.cardRataRata.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.cardRataRata.Padding = new System.Windows.Forms.Padding(14);
            this.cardRataRata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardRataRata.Controls.Add(this.lblRataRataStatus);
            this.cardRataRata.Controls.Add(this.lblRataRataVal);
            this.cardRataRata.Controls.Add(this.lblRataRataLbl);

            this.lblRataRataLbl.AutoSize = true;
            this.lblRataRataLbl.BackColor = System.Drawing.Color.Transparent;
            this.lblRataRataLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblRataRataLbl.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblRataRataLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblRataRataLbl.Location = new System.Drawing.Point(14, 10);
            this.lblRataRataLbl.Text = "RATA-RATA / BULAN";

            this.lblRataRataVal.AutoSize = true;
            this.lblRataRataVal.BackColor = System.Drawing.Color.Transparent;
            this.lblRataRataVal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblRataRataVal.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblRataRataVal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblRataRataVal.Location = new System.Drawing.Point(14, 36);
            this.lblRataRataVal.Text = "Rp 0";

            this.lblRataRataStatus.AutoSize = true;
            this.lblRataRataStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblRataRataStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblRataRataStatus.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblRataRataStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblRataRataStatus.Location = new System.Drawing.Point(14, 72);
            this.lblRataRataStatus.Text = "\u25CF  Omzet rata-rata bulanan";

            // ===================== CARD 4: BULAN TERBAIK =====================
            this.cardBestMonth.BackColor = System.Drawing.Color.FromArgb(199, 234, 187);
            this.cardBestMonth.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.cardBestMonth.Padding = new System.Windows.Forms.Padding(14);
            this.cardBestMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardBestMonth.Controls.Add(this.lblBestMonthStatus);
            this.cardBestMonth.Controls.Add(this.lblBestMonthVal);
            this.cardBestMonth.Controls.Add(this.lblBestMonthLbl);

            this.lblBestMonthLbl.AutoSize = true;
            this.lblBestMonthLbl.BackColor = System.Drawing.Color.Transparent;
            this.lblBestMonthLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.lblBestMonthLbl.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblBestMonthLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblBestMonthLbl.Location = new System.Drawing.Point(14, 10);
            this.lblBestMonthLbl.Text = "BULAN TERBAIK";

            this.lblBestMonthVal.AutoSize = true;
            this.lblBestMonthVal.BackColor = System.Drawing.Color.Transparent;
            this.lblBestMonthVal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblBestMonthVal.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblBestMonthVal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblBestMonthVal.Location = new System.Drawing.Point(14, 36);
            this.lblBestMonthVal.Text = "-";

            this.lblBestMonthStatus.AutoSize = true;
            this.lblBestMonthStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblBestMonthStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblBestMonthStatus.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblBestMonthStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.lblBestMonthStatus.Location = new System.Drawing.Point(14, 72);
            this.lblBestMonthStatus.Text = "\u25CF  Omzet tertinggi";

            // ===================== SIDE-BY-SIDE GRID TABLE =====================
            this.tblGrids.ColumnCount = 2;
            this.tblGrids.RowCount = 3;
            this.tblGrids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblGrids.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tblGrids.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGrids.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGrids.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tblGrids.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGrids.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblGrids.Controls.Add(this.lblMonthlyTitle, 0, 0);
            this.tblGrids.Controls.Add(this.pnlMonthlyGrid, 0, 1);
            this.tblGrids.Controls.Add(this.lblCategoryTitle, 1, 0);
            this.tblGrids.Controls.Add(this.pnlCategoryGrid, 1, 1);

            // ===================== MONTHLY TITLE =====================
            this.lblMonthlyTitle.AutoSize = true;
            this.lblMonthlyTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblMonthlyTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblMonthlyTitle.Padding = new System.Windows.Forms.Padding(0, 8, 0, 4);
            this.lblMonthlyTitle.Text = "  Ringkasan Penjualan Bulanan";

            // ===================== MONTHLY GRID WRAPPER =====================
            this.pnlMonthlyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMonthlyGrid.BackColor = System.Drawing.Color.White;
            this.pnlMonthlyGrid.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlMonthlyGrid.Controls.Add(this.dgvMonthly);

            // ===================== DATAGRIDVIEW MONTHLY =====================
            this.dgvMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMonthly.AllowUserToAddRows = false;
            this.dgvMonthly.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMonthly.BackgroundColor = System.Drawing.Color.White;
            this.dgvMonthly.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMonthly.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMonthly.MultiSelect = false;
            this.dgvMonthly.ReadOnly = true;
            this.dgvMonthly.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMonthly.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.dgvMonthly.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvMonthly.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);

            // ===================== CATEGORY TITLE =====================
            this.lblCategoryTitle.AutoSize = true;
            this.lblCategoryTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblCategoryTitle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.lblCategoryTitle.Padding = new System.Windows.Forms.Padding(0, 8, 0, 4);
            this.lblCategoryTitle.Text = "  Performa Kategori Produk";

            // ===================== CATEGORY GRID WRAPPER =====================
            this.pnlCategoryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategoryGrid.BackColor = System.Drawing.Color.White;
            this.pnlCategoryGrid.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.pnlCategoryGrid.Controls.Add(this.dgvCategory);

            // ===================== DATAGRIDVIEW CATEGORY =====================
            this.dgvCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCategory.AllowUserToAddRows = false;
            this.dgvCategory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategory.BackgroundColor = System.Drawing.Color.White;
            this.dgvCategory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategory.MultiSelect = false;
            this.dgvCategory.ReadOnly = true;
            this.dgvCategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategory.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.dgvCategory.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.dgvCategory.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);

            // ===================== FINANCIAL REPORT CONTROL =====================
            this.Controls.Add(this.tblMaster);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.Name = "FinancialReportControl";
            this.Size = new System.Drawing.Size(1200, 700);
            this.Padding = new System.Windows.Forms.Padding(24, 12, 24, 16);

            this.tblMaster.ResumeLayout(false);
            this.tblMaster.PerformLayout();
            this.tblKPI.ResumeLayout(false);
            this.cardOmzet.ResumeLayout(false);
            this.cardOmzet.PerformLayout();
            this.cardTransaksi.ResumeLayout(false);
            this.cardTransaksi.PerformLayout();
            this.cardRataRata.ResumeLayout(false);
            this.cardRataRata.PerformLayout();
            this.cardBestMonth.ResumeLayout(false);
            this.cardBestMonth.PerformLayout();
            this.pnlBanner.ResumeLayout(false);
            this.pnlBanner.PerformLayout();
            this.pnlBadge.ResumeLayout(false);
            this.tblGrids.ResumeLayout(false);
            this.tblGrids.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthly)).EndInit();
            this.pnlMonthlyGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).EndInit();
            this.pnlCategoryGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
