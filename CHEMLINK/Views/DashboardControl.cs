using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
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

            // Responsive: reflow banner description width on resize
            this.Resize += (s, e) =>
            {
                int descWidth = Math.Max(300, pnlBanner.Width - 64);
                lblBannerDesc.Size = new Size(descWidth, 36);
            };
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
            dataGridView1.DataSource = dtNotif;

            // Apply styling to the DataGridView
            ApplyDataGridViewStyling();
        }

        public void SetData(List<Models.Product> products, DataTable dtNotif)
        {
            int totalProduk = products.Count;
            int totalStok = products.Sum(p => p.Stock);
            int stokKritis = products.Count(p => p.Stock <= 5);
            int totalKategori = products.Select(p => p.Category).Distinct().Count();

            lblTotalProdukVal.Text = totalProduk.ToString();
            lblTotalStokVal.Text = totalStok.ToString();
            lblStokKritisVal.Text = stokKritis.ToString();
            lblKategoriVal.Text = totalKategori.ToString();

            dataGridView1.DataSource = dtNotif;
            ApplyDataGridViewStyling();
        }

        private void ApplyDataGridViewStyling()
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Agro950;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(8);
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.RowTemplate.Height = 36;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dataGridView1.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Agro600;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dataGridView1.GridColor = Color.FromArgb(226, 232, 240);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
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

                dataGridView1.DataSource = dt;
                ApplyDataGridViewStyling();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Apply styling when cell is clicked
            ApplyDataGridViewStyling();
        }

    }
}
