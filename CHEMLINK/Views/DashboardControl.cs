using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CHEMLINK.Views
{
    public partial class DashboardControl : UserControl
    {
        // Agro palette
        private static readonly Color Agro950 = Color.FromArgb(2, 44, 34);
        private static readonly Color Agro800 = Color.FromArgb(22, 101, 52);
        private static readonly Color Agro600 = Color.FromArgb(22, 163, 74);
        private static readonly Color Agro500 = Color.FromArgb(34, 197, 94);
        private static readonly Color Agro400 = Color.FromArgb(74, 222, 128);
        private static readonly Color Agro100 = Color.FromArgb(220, 252, 231);
        private static readonly Color TextDark = Color.FromArgb(30, 41, 59);
        private static readonly Color TextMuted = Color.FromArgb(148, 163, 184);

        // Cached background image (loaded once)
        private Image? _bgImage;

        public DashboardControl()
        {
            InitializeComponent();

            // Enable double buffering
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);

            // Cache the background image once
            string imgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "kebun.png");
            if (File.Exists(imgPath))
            {
                _bgImage = Image.FromFile(imgPath);
            }

            // Custom paint handler for gradient banner
            pnlBanner.Paint += PnlBanner_Paint;

            // Dark green border paint handlers
            dgvMain.Paint += DgvBorder_Paint;
            dgvLogStok.Paint += DgvBorder_Paint;

            // Responsive: reflow banner description width on resize
            this.Resize += (s, e) =>
            {
                int descWidth = Math.Max(300, pnlBanner.Width - 64);
                lblBannerDesc.Size = new Size(descWidth, 80);
            };

            // Set initial description width
            lblBannerDesc.Size = new Size(Math.Max(300, pnlBanner.Width - 64), 80);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            _bgImage?.Dispose();
            base.Dispose(disposing);
        }

        public void SetData(int totalProduk, int stokKritis, DataTable dtNotif)
        {
            lblTotalProdukVal.Text = totalProduk.ToString();
            lblStokKritisVal.Text = stokKritis.ToString();
            dgvMain.DataSource = dtNotif;

            // Style the DataGridView
            StyleDataGridView(dgvMain);
        }

        public void SetData(List<Models.Product> products, DataTable dtNotif, DataTable? dtLogStok = null)
        {
            int totalProduk = products.Count;
            int totalStok = products.Sum(p => p.Stock);
            int stokKritis = products.Count(p => p.Stock <= 5);
            int totalKategori = products.Select(p => p.Category).Distinct().Count();

            lblTotalProdukVal.Text = totalProduk.ToString();
            lblTotalStokVal.Text = totalStok.ToString();
            lblStokKritisVal.Text = stokKritis.ToString();
            lblKategoriVal.Text = totalKategori.ToString();

            dgvMain.DataSource = dtNotif;
            StyleDataGridView(dgvMain);

            if (dtLogStok != null)
            {
                dgvLogStok.DataSource = dtLogStok;
                StyleDataGridView(dgvLogStok);
                RenameLogColumns();
            }
        }

        private void RenameLogColumns()
        {
            if (dgvLogStok.Columns.Contains("tipe_aktivitas")) dgvLogStok.Columns["tipe_aktivitas"]!.HeaderText = "Tipe";
            if (dgvLogStok.Columns.Contains("nama_user")) dgvLogStok.Columns["nama_user"]!.HeaderText = "User";
            if (dgvLogStok.Columns.Contains("nama_produk")) dgvLogStok.Columns["nama_produk"]!.HeaderText = "Produk";
            if (dgvLogStok.Columns.Contains("jumlah")) dgvLogStok.Columns["jumlah"]!.HeaderText = "Jumlah";
            if (dgvLogStok.Columns.Contains("time_stamp")) dgvLogStok.Columns["time_stamp"]!.HeaderText = "Waktu";
            if (dgvLogStok.Columns.Contains("id_log")) dgvLogStok.Columns["id_log"]!.Visible = false;
            if (dgvLogStok.Columns.Contains("id_user")) dgvLogStok.Columns["id_user"]!.Visible = false;
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Agro950;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6);
            dgv.ColumnHeadersHeight = 36;
            dgv.RowTemplate.Height = 32;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgv.DefaultCellStyle.Padding = new Padding(6, 2, 6, 2);
            dgv.DefaultCellStyle.SelectionBackColor = Agro600;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.GridColor = Color.FromArgb(226, 232, 240);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        public void LoadCriticalStockData()
        {
            try
            {
                var productContext = new Contexts.ProductContext();
                var criticalStocks = productContext.ReadCriticalStock();

                // Convert List<StockKritis> to DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("ID Produk", typeof(int));
                dt.Columns.Add("Nama Produk", typeof(string));
                dt.Columns.Add("Jumlah Stock", typeof(int));

                foreach (var stock in criticalStocks)
                {
                    dt.Rows.Add(stock.IdProduk, stock.NamaProduk, stock.JumlahStock);
                }

                dgvMain.DataSource = dt;
                StyleDataGridView(dgvMain);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading critical stock data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void PnlBanner_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = pnlBanner.ClientRectangle;

            // Skip painting when minimized or zero-sized (prevents LinearGradientBrush crash)
            if (rect.Width <= 0 || rect.Height <= 0) return;

            // 1. Draw background image (cached, no disk I/O)
            if (_bgImage != null)
            {
                float imgRatio = (float)_bgImage.Width / _bgImage.Height;
                float panelRatio = (float)rect.Width / rect.Height;
                Rectangle destRect;
                if (panelRatio > imgRatio)
                {
                    int drawH = (int)(rect.Width / imgRatio);
                    int cropY = (drawH - rect.Height) / 2;
                    destRect = new Rectangle(0, -cropY, rect.Width, drawH);
                }
                else
                {
                    int drawW = (int)(rect.Height * imgRatio);
                    int cropX = (drawW - rect.Width) / 2;
                    destRect = new Rectangle(-cropX, 0, drawW, rect.Height);
                }
                g.DrawImage(_bgImage, destRect);
            }

            // 2. Draw overlay gradient (dark green left → transparent right)
            using (var brush = new LinearGradientBrush(rect, Color.Empty, Color.Empty, LinearGradientMode.Horizontal))
            {
                var blend = new ColorBlend(3);
                blend.Colors = new[]
                {
                    Color.FromArgb(252, 2, 44, 34),
                    Color.FromArgb(192, 2, 44, 34),
                    Color.FromArgb(32, 0, 0, 0)
                };
                blend.Positions = new[] { 0f, 0.4f, 1f };
                brush.InterpolationColors = blend;
                g.FillRectangle(brush, rect);
            }
        }

        private void DrawPanelBorder(object? sender, PaintEventArgs e)
        {
            if (sender is not Panel pnl) return;
            var r = pnl.ClientRectangle;
            if (r.Width <= 0 || r.Height <= 0) return;
            using var pen = new Pen(Agro950, 2f);
            e.Graphics.DrawRectangle(pen, 0, 0, r.Width - 1, r.Height - 1);
        }

        private void DgvBorder_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not DataGridView dgv) return;
            var r = dgv.ClientRectangle;
            if (r.Width <= 0 || r.Height <= 0) return;
            using var pen = new Pen(Agro950, 2f);
            e.Graphics.DrawRectangle(pen, 0, 0, r.Width - 1, r.Height - 1);
        }
    }
}
