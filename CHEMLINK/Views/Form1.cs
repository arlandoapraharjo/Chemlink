using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CHEMLINK.Views.Interfaces;

namespace CHEMLINK
{
    public partial class Form1 : Form, IMainView
    {
        private Panel sidebarPanel = null!;
        private Panel headerPanel = null!;
        private Panel mainContentPanel = null!;
        private Panel dynamicControlPanel = null!;
        private Label lblUser = null!;
        private Label lblTitle = null!;
        private DataGridView dgvMain = null!;

        private Button btnDashboard = null!;
        private Button btnProduk = null!;
        private Button btnTransaksi = null!;
        private Button btnSupplier = null!;
        private Button btnLaporan = null!;
        private Button btnUser = null!;
        private Button btnLogout = null!;

        public event EventHandler? ShowDashboardEvent;
        public event EventHandler? ShowProductEvent;
        public event EventHandler? ShowTransactionEvent;
        public event EventHandler? ShowSupplierEvent;
        public event EventHandler? ShowReportEvent;
        public event EventHandler? ShowUserManagementEvent;
        public event EventHandler? LogoutEvent;

        public DataGridView MainDataGrid => dgvMain;
        public Label TitleLabel => lblTitle;
        public Panel DynamicControlPanel => dynamicControlPanel;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string ProductName { get; set; } = string.Empty;

        public Form1()
        {
            InitializeComponent();
            SetupMainLayout();
            AssociateViewEvents();
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
                    this.DialogResult = DialogResult.Retry;
                    this.Close();
                }
            };
        }

        private void SetupMainLayout()
        {
            this.Text = "ChemLink - Manajemen Kios Pertanian v2.0";
            this.Size = new Size(1150, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.FromArgb(240, 243, 246);

            sidebarPanel = new Panel { Dock = DockStyle.Left, Width = 230, BackColor = Color.FromArgb(46, 125, 50) };

            Label lblLogo = new Label { Text = "🌿 ChemLink", ForeColor = Color.White, Font = new Font("Segoe UI", 18F, FontStyle.Bold), Dock = DockStyle.Top, Height = 70, TextAlign = ContentAlignment.MiddleCenter };
            sidebarPanel.Controls.Add(lblLogo);

            btnLogout = CreateSidebarButton("Keluar Sistem");
            btnLogout.Dock = DockStyle.Bottom;
            sidebarPanel.Controls.Add(btnLogout);

            btnUser = CreateSidebarButton("Manajemen User");
            btnLaporan = CreateSidebarButton("Laporan Keuangan");
            btnSupplier = CreateSidebarButton("Manajemen Supplier");
            btnTransaksi = CreateSidebarButton("Transaksi / Kasir");
            btnProduk = CreateSidebarButton("Katalog Produk");
            btnDashboard = CreateSidebarButton("Dashboard");

            sidebarPanel.Controls.Add(btnUser);
            sidebarPanel.Controls.Add(btnLaporan);
            sidebarPanel.Controls.Add(btnSupplier);
            sidebarPanel.Controls.Add(btnTransaksi);
            sidebarPanel.Controls.Add(btnProduk);
            sidebarPanel.Controls.Add(btnDashboard);

            btnUser.BringToFront();
            btnLaporan.BringToFront();
            btnSupplier.BringToFront();
            btnTransaksi.BringToFront();
            btnProduk.BringToFront();
            btnDashboard.BringToFront();

            headerPanel = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.White };
            lblUser = new Label { Text = "👤 Login: -", Dock = DockStyle.Right, Width = 300, TextAlign = ContentAlignment.MiddleRight, Padding = new Padding(0, 0, 20, 0), Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(60, 60, 60) };
            headerPanel.Controls.Add(lblUser);

            mainContentPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };

            lblTitle = new Label { Text = "Dashboard Utama", Font = new Font("Segoe UI", 18F, FontStyle.Bold), ForeColor = Color.FromArgb(46, 125, 50), Dock = DockStyle.Top, Height = 40 };
            mainContentPanel.Controls.Add(lblTitle);

            dynamicControlPanel = new Panel { Dock = DockStyle.Top, Height = 180, BackColor = Color.White, Margin = new Padding(0, 0, 0, 15) };
            mainContentPanel.Controls.Add(dynamicControlPanel);
            dynamicControlPanel.BringToFront();

            dgvMain = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false,
                ReadOnly = true
            };
            mainContentPanel.Controls.Add(dgvMain);
            dgvMain.BringToFront();

            this.Controls.Add(mainContentPanel);
            this.Controls.Add(headerPanel);
            this.Controls.Add(sidebarPanel);
        }

        private Button CreateSidebarButton(string text)
        {
            Button btn = new Button { Text = "   " + text, Dock = DockStyle.Top, Height = 50, FlatStyle = FlatStyle.Flat, ForeColor = Color.White, TextAlign = ContentAlignment.MiddleLeft, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(27, 94, 32);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.Transparent;
            return btn;
        }
    }
}