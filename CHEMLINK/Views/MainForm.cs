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

        // Track active nav button
        private Button? _activeNavButton;
        private UserControl? _currentControl;
        private readonly Color _activeNavColor = Color.FromArgb(74, 222, 128);   // Agro400
        private readonly Color _inactiveNavColor = Color.FromArgb(148, 163, 184); // TextMuted
        private readonly Color _activeNavBg = Color.FromArgb(16, 255, 255, 255);  // 10% white

        // IMainView Events
        public event EventHandler? ShowDashboardEvent;
        public event EventHandler? ShowProductEvent;
        public event EventHandler? ShowTransactionEvent;
        public event EventHandler? ShowSupplierEvent;
        public event EventHandler? ShowReportEvent;
        public event EventHandler? ShowUserManagementEvent;
        public event EventHandler? LogoutEvent;

        public event EventHandler<Product>? AddProductEvent;
        public event EventHandler<Product>? EditProductEvent;
        public event EventHandler<int>? DeleteProductEvent;
        public event EventHandler? ManageCategoryEvent;
        public event EventHandler<CartItemEventArgs>? AddCartEvent;
        public event EventHandler<CartItem>? DeleteCartEvent;
        public event EventHandler? CheckoutEvent;
        public event EventHandler<Supplier>? AddSupplierEvent;
        public event EventHandler<Supplier>? UpdateSupplierEvent;
        public event EventHandler<int>? DeleteSupplierEvent;
        public event EventHandler<string>? SearchProductEvent;
        public event EventHandler<string>? FilterCategoryEvent;

        // User Management Events
        public event EventHandler<User>? AddUserEvent;
        public event EventHandler<User>? UpdateUserEvent;
        public event EventHandler<int>? DeleteUserEvent;

#pragma warning disable CS0067
        public event EventHandler<Category>? AddCategoryEvent;
        public event EventHandler<Category>? UpdateCategoryEvent;
        public event EventHandler<int>? DeleteCategoryEvent;
#pragma warning restore CS0067

        public MainForm()
        {
            InitializeComponent();

            // Enable double buffering to reduce flicker
            EnableDoubleBuffering(this);
            EnableDoubleBuffering(mainContentPanel);

            // Load logo
            LoadLogoImage();

            // Initialize User Controls
            _dashboardControl = new DashboardControl { Dock = DockStyle.Fill };
            _productCatalogControl = new ProductCatalogControl { Dock = DockStyle.Fill };
            _posControl = new POSControl { Dock = DockStyle.Fill };
            _supplierManagementControl = new SupplierManagementControl { Dock = DockStyle.Fill };
            _financialReportControl = new FinancialReportControl { Dock = DockStyle.Fill };
            _userManagementControl = new UserManagementControl { Dock = DockStyle.Fill };

            // Pre-add all controls once (hidden), switch via Visible
            mainContentPanel.Controls.Add(_dashboardControl);
            mainContentPanel.Controls.Add(_productCatalogControl);
            mainContentPanel.Controls.Add(_posControl);
            mainContentPanel.Controls.Add(_supplierManagementControl);
            mainContentPanel.Controls.Add(_financialReportControl);
            mainContentPanel.Controls.Add(_userManagementControl);

            _dashboardControl.BringToFront();
            _currentControl = _dashboardControl;

            WireUserControlEvents();
            AssociateViewEvents();

            // Set initial active nav button
            HighlightNavButton(btnDashboard);
        }

        private void WireUserControlEvents()
        {
            _productCatalogControl.AddProductEvent += (s, e) => AddProductEvent?.Invoke(this, e);
            _productCatalogControl.EditProductEvent += (s, e) => EditProductEvent?.Invoke(this, e);
            _productCatalogControl.DeleteProductEvent += (s, e) => DeleteProductEvent?.Invoke(this, e);
            _productCatalogControl.ManageCategoryEvent += (s, e) => ManageCategoryEvent?.Invoke(this, EventArgs.Empty);

            _posControl.AddCartEvent += (s, e) => AddCartEvent?.Invoke(this, e);
            _posControl.DeleteCartEvent += (s, e) => DeleteCartEvent?.Invoke(this, e);
            _posControl.CheckoutEvent += (s, e) => CheckoutEvent?.Invoke(this, e);
            _posControl.SearchProductEvent += (s, e) => SearchProductEvent?.Invoke(this, e);
            _posControl.FilterCategoryEvent += (s, e) => FilterCategoryEvent?.Invoke(this, e);

            _supplierManagementControl.AddSupplierEvent += (s, e) => AddSupplierEvent?.Invoke(this, e);
            _supplierManagementControl.UpdateSupplierEvent += (s, e) => UpdateSupplierEvent?.Invoke(this, e);
            _supplierManagementControl.DeleteSupplierEvent += (s, e) => DeleteSupplierEvent?.Invoke(this, e);

            _userManagementControl.AddUserEvent += (s, e) => AddUserEvent?.Invoke(this, e);
            _userManagementControl.UpdateUserEvent += (s, e) => UpdateUserEvent?.Invoke(this, e);
            _userManagementControl.DeleteUserEvent += (s, e) => DeleteUserEvent?.Invoke(this, e);
        }

        public void SetActiveUser(string username, string role)
        {
            lblUsername.Text = username;
            lblGreeting.Text = "Selamat Bekerja,";
            // Set avatar initials
            string initials = username.Length >= 3 ? username.Substring(0, 3).ToUpper() : username.ToUpper();
            lblAvatar.Text = initials;
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

        // Fast tab switch: hide previous control, show new one
        // Avoids Controls.Clear() which destroys/recreates the control hierarchy
        private void SwitchControl(UserControl control, string title)
        {
            if (_currentControl == control) return;

            mainContentPanel.SuspendLayout();
            try
            {
                if (_currentControl != null)
                {
                    _currentControl.Visible = false;
                }
                control.Visible = true;
                control.BringToFront();
                _currentControl = control;
                lblTitle.Text = title;
            }
            finally
            {
                mainContentPanel.ResumeLayout(true);
            }
        }

        // --- IMainView UI Implementations ---

        public void ShowDashboardData(List<Product> products, DataTable dtNotif, DataTable? dtLogStok = null)
        {
            _dashboardControl.SetData(products, dtNotif, dtLogStok);
            SwitchControl(_dashboardControl, "Dashboard");
            HighlightNavButton(btnDashboard);
        }

        public void ShowProductCatalog(List<Product> products, bool isAdmin, List<Category>? categories = null)
        {
            _productCatalogControl.SetData(products, isAdmin);
            if (categories != null)
                _productCatalogControl.SetCategories(categories);
            SwitchControl(_productCatalogControl, "Katalog Produk");
            HighlightNavButton(btnProduk);
        }

        public void ShowPOS(List<Product> searchResults, List<CartItem> cart)
        {
            _posControl.SetData(searchResults, cart);
            SwitchControl(_posControl, "Point of Sales (Transaksi Kasir)");
            HighlightNavButton(btnTransaksi);
        }

        public void ShowSupplierManagement(List<Supplier> suppliers)
        {
            _supplierManagementControl.SetData(suppliers);
            SwitchControl(_supplierManagementControl, "Manajemen Supplier");
            HighlightNavButton(btnSupplier);
        }

        public void ShowFinancialReport(DataTable report, DataTable categoryBreakdown)
        {
            _financialReportControl.SetData(report, categoryBreakdown);
            SwitchControl(_financialReportControl, "Laporan Keuangan");
            HighlightNavButton(btnLaporan);
        }

        public void ShowUserManagement(List<User> users, bool isAdmin)
        {
            _userManagementControl.SetData(users, isAdmin);
            SwitchControl(_userManagementControl, "Manajemen User");
            HighlightNavButton(btnUser);
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

        // --- Topbar nav button highlighting ---
        private void HighlightNavButton(Button activeBtn)
        {
            foreach (Control ctrl in navPanel.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.ForeColor = _inactiveNavColor;
                    btn.BackColor = Color.Transparent;
                }
            }
            activeBtn.ForeColor = _activeNavColor;
            activeBtn.BackColor = _activeNavBg;
            _activeNavButton = activeBtn;
        }

        // Enable double buffering on a control to reduce flicker
        private static void EnableDoubleBuffering(Control control)
        {
            typeof(Control).InvokeMember(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, control, new object[] { true });
            typeof(Control).InvokeMember(
                "SetStyle",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.InvokeMethod,
                null, control, new object[] {
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint, true });
        }
    }
}
