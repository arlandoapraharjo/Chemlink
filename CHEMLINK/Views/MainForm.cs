using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using CHEMLINK.Models;
using CHEMLINK.Views;

namespace CHEMLINK
{
    public partial class MainForm : Form
    {
        private DashboardControl _dashboardControl;
        private ProductCatalogControl _productCatalogControl;
        private POSControl _posControl;
        private SupplierManagementControl _supplierManagementControl;
        private FinancialReportControl _financialReportControl;
        private UserManagementControl _userManagementControl;

        // Track active sidebar button for highlighting
        private Button? _activeSidebarButton;
        private readonly Color _activeBtnColor = Color.FromArgb(123, 201, 80);  // Bright lime green highlight
        private static readonly Color _inactiveBtnColor = Color.Transparent;

        // IMainView Events
        public event EventHandler? ShowDashboardEvent;
        public event EventHandler? ShowProductEvent;
        public event EventHandler? ShowTransactionEvent;
        public event EventHandler? ShowSupplierEvent;
        public event EventHandler? ShowReportEvent;
        public event EventHandler? ShowUserManagementEvent;
        public event EventHandler? LogoutEvent;

        public event EventHandler<ProductEventArgs>? AddProductEvent;
        public event EventHandler<ProductEventArgs>? EditProductEvent;
        public event EventHandler<int>? DeleteProductEvent;
        public event EventHandler? ManageCategoryEvent;
        public event EventHandler<CartItemEventArgs>? AddCartEvent;
        public event EventHandler? CheckoutEvent;
        public event EventHandler<SupplierEventArgs>? AddSupplierEvent;
        public event EventHandler<string>? SearchProductEvent;
        public event EventHandler<string>? FilterCategoryEvent;

        // User Management Events
        public event EventHandler<UserEventArgs>? AddUserEvent;
        public event EventHandler<UserEventArgs>? UpdateUserEvent;
        public event EventHandler<int>? DeleteUserEvent;

#pragma warning disable CS0067
        public event EventHandler<CategoryEventArgs>? AddCategoryEvent;
        public event EventHandler<CategoryEventArgs>? UpdateCategoryEvent;
        public event EventHandler<int>? DeleteCategoryEvent;
#pragma warning restore CS0067

        public MainForm()
        {
            InitializeComponent();

            // Load embedded logo image into sidebar PictureBox
            LoadLogoImage();

            // Fix sidebar gradient: paint once as background image (avoids repeated gradient per visible strip)
            UpdateSidebarGradient();

            // Initialize User Controls
            _dashboardControl = new DashboardControl { Dock = DockStyle.Fill };
            _productCatalogControl = new ProductCatalogControl { Dock = DockStyle.Fill };
            _posControl = new POSControl { Dock = DockStyle.Fill };
            _supplierManagementControl = new SupplierManagementControl { Dock = DockStyle.Fill };
            _financialReportControl = new FinancialReportControl { Dock = DockStyle.Fill };
            _userManagementControl = new UserManagementControl { Dock = DockStyle.Fill };

            WireUserControlEvents();
            AssociateViewEvents();

            // Set initial active button
            HighlightSidebarButton(btnDashboard);
        }

        private void WireUserControlEvents()
        {
            _productCatalogControl.AddProductEvent += (s, e) => AddProductEvent?.Invoke(this, e);
            _productCatalogControl.EditProductEvent += (s, e) => EditProductEvent?.Invoke(this, e);
            _productCatalogControl.DeleteProductEvent += (s, e) => DeleteProductEvent?.Invoke(this, e);
            _productCatalogControl.ManageCategoryEvent += (s, e) => ManageCategoryEvent?.Invoke(this, EventArgs.Empty);

            _posControl.AddCartEvent += (s, e) => AddCartEvent?.Invoke(this, e);
            _posControl.CheckoutEvent += (s, e) => CheckoutEvent?.Invoke(this, e);
            _posControl.SearchProductEvent += (s, e) => SearchProductEvent?.Invoke(this, e);
            _posControl.FilterCategoryEvent += (s, e) => FilterCategoryEvent?.Invoke(this, e);

            _supplierManagementControl.AddSupplierEvent += (s, e) => AddSupplierEvent?.Invoke(this, e);

            _userManagementControl.AddUserEvent += (s, e) => AddUserEvent?.Invoke(this, e);
            _userManagementControl.UpdateUserEvent += (s, e) => UpdateUserEvent?.Invoke(this, e);
            _userManagementControl.DeleteUserEvent += (s, e) => DeleteUserEvent?.Invoke(this, e);
        }

        public void SetActiveUser(string username, string role)
        {
            lblUser.Text = $"👤 Operator: {username} ({role})";
        }

        public void ApplyRoleRestrictions(bool isAdmin)
        {
            if (!isAdmin)
            {
                btnSupplier.Visible = false;
                btnLaporan.Visible = false;
                btnUser.Visible = false;
            }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AssociateViewEvents()
        {
            btnDashboard.Click += delegate { ShowDashboardEvent?.Invoke(this, EventArgs.Empty); };
            btnProduk.Click += delegate { ShowProductEvent?.Invoke(this, EventArgs.Empty); };
            btnTransaksi.Click += delegate { ShowTransactionEvent?.Invoke(this, EventArgs.Empty); };
            btnSupplier.Click += delegate { ShowSupplierEvent?.Invoke(this, EventArgs.Empty); };
            btnLaporan.Click += delegate { ShowReportEvent?.Invoke(this, EventArgs.Empty); };
            btnUser.Click += delegate { ShowUserManagementEvent?.Invoke(this, EventArgs.Empty); };

            btnLogout.Click += delegate {
                DialogResult dr = MessageBox.Show("Apakah Anda yakin ingin logout dari sistem?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    LogoutEvent?.Invoke(this, EventArgs.Empty);
                    this.DialogResult = DialogResult.Retry;
                    this.Close();
                }
            };
        }

        private void SwitchControl(UserControl control, string title)
        {
            mainContentPanel.Controls.Clear();
            mainContentPanel.Controls.Add(lblTitle);
            mainContentPanel.Controls.Add(control);
            control.BringToFront();
            lblTitle.Text = title;
        }

        // --- IMainView UI Implementations ---

        public void ShowDashboardData(int totalProduk, int stokKritis, DataTable dtNotif)
        {
            _dashboardControl.SetData(totalProduk, stokKritis, dtNotif);
            SwitchControl(_dashboardControl, "🌿 Ringkasan Dashboard & Notifikasi");
            HighlightSidebarButton(btnDashboard);
        }

        public void ShowProductCatalog(List<Product> products, bool isAdmin, List<Category>? categories = null)
        {
            _productCatalogControl.SetData(products, isAdmin);
            if (categories != null)
                _productCatalogControl.SetCategories(categories);
            SwitchControl(_productCatalogControl, "📦 Katalog Obat Pertanian");
            HighlightSidebarButton(btnProduk);
        }

        public void ShowPOS(List<Product> searchResults, List<CartItem> cart)
        {
            _posControl.SetData(searchResults, cart);
            SwitchControl(_posControl, "🛒 Point of Sales (Transaksi Kasir)");
            HighlightSidebarButton(btnTransaksi);
        }

        public void ShowSupplierManagement(List<Supplier> suppliers)
        {
            _supplierManagementControl.SetData(suppliers);
            SwitchControl(_supplierManagementControl, "🚛 Manajemen Mitra Supplier");
            HighlightSidebarButton(btnSupplier);
        }

        public void ShowFinancialReport(DataTable report)
        {
            _financialReportControl.SetData(report);
            SwitchControl(_financialReportControl, "📊 Laporan Analitik Keuangan Kios");
            HighlightSidebarButton(btnLaporan);
        }

        public void ShowUserManagement(List<User> users, bool isAdmin)
        {
            _userManagementControl.SetData(users, isAdmin);
            SwitchControl(_userManagementControl, "👥 Pengaturan Akun Operator & Karyawan");
            HighlightSidebarButton(btnUser);
        }

        public void PrintReceipt(string receiptContent)
        {
            MessageBox.Show(receiptContent, "Struk Pembelian Berhasil Dicetak", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- Load logo from Assets folder ---
        private void LoadLogoImage()
        {
            string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "logo.png");
            if (File.Exists(logoPath))
            {
                picLogo.Image = Image.FromFile(logoPath);
            }
        }

        // --- Sidebar active button highlighting ---
        private void HighlightSidebarButton(Button activeBtn)
        {
            // Reset all navigation buttons
            foreach (Control ctrl in sidebarPanel.Controls)
            {
                if (ctrl is Button btn && btn != btnLogout)
                {
                    btn.BackColor = _inactiveBtnColor;
                }
            }
            // Highlight the active button
            activeBtn.BackColor = _activeBtnColor;
            _activeSidebarButton = activeBtn;
        }

        // --- Sidebar gradient paint (palette: #25671E → #48A111 → #F2B50B) ---
        private void UpdateSidebarGradient()
        {
            var bmp = new Bitmap(sidebarPanel.Width, sidebarPanel.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                using (var brush = new LinearGradientBrush(
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    Color.FromArgb(37, 103, 30),   // #25671E
                    Color.FromArgb(242, 181, 11),  // #F2B50B
                    LinearGradientMode.Vertical))
                {
                    var blend = new ColorBlend(3);
                    blend.Colors = new[]
                    {
                        Color.FromArgb(37, 103, 30),  // #25671E
                        Color.FromArgb(72, 161, 17),  // #48A111
                        Color.FromArgb(242, 181, 11)  // #F2B50B
                    };
                    blend.Positions = new[] { 0f, 0.5f, 1f };
                    brush.InterpolationColors = blend;
                    g.FillRectangle(brush, new Rectangle(0, 0, bmp.Width, bmp.Height));
                }
            }
            sidebarPanel.BackgroundImage = bmp;
            sidebarPanel.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void sidebarPanel_Paint(object? sender, PaintEventArgs e)
        {
            // Gradient is handled by BackgroundImage; nothing to paint here
        }
    }
}