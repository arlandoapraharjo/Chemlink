using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace CHEMLINK.Views
{
    public partial class FinancialReportControl : UserControl
    {
        // Agro palette
        private static readonly Color Agro950 = Color.FromArgb(2, 44, 34);
        private static readonly Color Agro800 = Color.FromArgb(22, 101, 52);
        private static readonly Color Agro600 = Color.FromArgb(22, 163, 74);
        private static readonly Color TextDark = Color.FromArgb(30, 41, 59);

        // Cached background image (loaded once)
        private Image? _bgImage;

        public FinancialReportControl()
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

            pnlBanner.Paint += PnlBanner_Paint;

            // Dark green border paint handlers
            pnlMonthlyGrid.Paint += DrawPanelBorder;
            pnlCategoryGrid.Paint += DrawPanelBorder;

            // Responsive: reflow banner description width on resize
            this.Resize += (s, e) =>
            {
                int descWidth = Math.Max(300, pnlBanner.Width - 48);
                lblBannerDesc.Size = new Size(descWidth, 30);
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            _bgImage?.Dispose();
            base.Dispose(disposing);
        }

        public void SetData(DataTable monthlyReport, DataTable categoryBreakdown)
        {
            dgvMonthly.DataSource = null;
            dgvMonthly.Columns.Clear();
            dgvMonthly.DataSource = monthlyReport;

            dgvCategory.DataSource = null;
            dgvCategory.Columns.Clear();
            dgvCategory.DataSource = categoryBreakdown;

            StyleDataGridView(dgvMonthly);
            StyleDataGridView(dgvCategory);

            // Format currency columns
            FormatCurrencyColumn(dgvMonthly, "Omzet Bersih");
            FormatCurrencyColumn(dgvCategory, "Total Pendapatan");

            ComputeSummary(monthlyReport);
        }

        private void ComputeSummary(DataTable report)
        {
            decimal totalOmzet = 0;
            int totalTransaksi = 0;
            decimal bestOmzet = 0;
            string bestMonth = "-";

            foreach (DataRow row in report.Rows)
            {
                // Parse omzet (stored as varchar from the view)
                string omzetStr = row["Omzet Bersih"]?.ToString()?.Replace(",", "").Trim() ?? "0";
                if (decimal.TryParse(omzetStr, out decimal omzet))
                {
                    totalOmzet += omzet;
                    if (omzet > bestOmzet)
                    {
                        bestOmzet = omzet;
                        bestMonth = FormatMonth(row["Bulan"]?.ToString() ?? "");
                    }
                }

                // Parse transaksi
                string trxStr = row["Total Transaksi"]?.ToString()?.Trim() ?? "0";
                if (int.TryParse(trxStr, out int trx))
                {
                    totalTransaksi += trx;
                }
            }

            int monthCount = report.Rows.Count > 0 ? report.Rows.Count : 1;
            decimal rataRata = totalOmzet / monthCount;

            lblOmzetVal.Text = $"Rp {totalOmzet:N0}";
            lblTransaksiVal.Text = totalTransaksi.ToString("N0");
            lblRataRataVal.Text = $"Rp {rataRata:N0}";
            lblBestMonthVal.Text = bestMonth;
            lblBestMonthStatus.Text = $"\u25CF  Omzet: Rp {bestOmzet:N0}";
        }

        private string FormatMonth(string yyyyMm)
        {
            if (yyyyMm.Length == 7 && yyyyMm.Contains('-'))
            {
                string[] parts = yyyyMm.Split('-');
                if (int.TryParse(parts[1], out int month) && int.TryParse(parts[0], out int year))
                {
                    string[] monthNames = { "", "Jan", "Feb", "Mar", "Apr", "Mei", "Jun", "Jul", "Agu", "Sep", "Okt", "Nov", "Des" };
                    if (month >= 1 && month <= 12)
                        return $"{monthNames[month]} {year}";
                }
            }
            return yyyyMm;
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

        private void FormatCurrencyColumn(DataGridView dgv, string columnName)
        {
            if (dgv.Columns.Contains(columnName))
            {
                var col = dgv.Columns[columnName];
                if (col != null)
                {
                    col.DefaultCellStyle.Format = "";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.DefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
                }
            }
        }

        private void PnlBanner_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = pnlBanner.ClientRectangle;
            if (rect.Width <= 0 || rect.Height <= 0) return;

            // Draw background image (cached, no disk I/O)
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

            // Draw overlay gradient
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
    }
}
