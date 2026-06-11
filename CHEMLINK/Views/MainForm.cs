using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CHEMLINK.Models;
using CHEMLINK.Views.Interfaces;

namespace CHEMLINK
{
    public partial class MainForm : Form, IMainView
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

        // IMainView Events
        public event EventHandler? ShowDashboardEvent;
        public event EventHandler? ShowProductEvent;
        public event EventHandler? ShowTransactionEvent;
        public event EventHandler? ShowSupplierEvent;
        public event EventHandler? ShowReportEvent;
        public event EventHandler? ShowUserManagementEvent;
        public event EventHandler? LogoutEvent;

        public event EventHandler<ProductEventArgs>? AddProductEvent;
        public event EventHandler<int>? DeleteProductEvent;
        public event EventHandler<CartItemEventArgs>? AddCartEvent;
        public event EventHandler? CheckoutEvent;
        public event EventHandler<SupplierEventArgs>? AddSupplierEvent;
        public event EventHandler<string>? SearchProductEvent;
        public event EventHandler<string>? FilterCategoryEvent;

        // User Management Events
        public event EventHandler<UserEventArgs>? AddUserEvent;
        public event EventHandler<UserEventArgs>? UpdateUserEvent;
        public event EventHandler<int>? DeleteUserEvent;

        public MainForm()
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
                    LogoutEvent?.Invoke(this, EventArgs.Empty);
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

            // Sidebar Re-ordering: Dashboard is at the top
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

        private void SetupDataGrid()
        {
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.CellFormatting -= DgvMain_CellFormatting_PasswordMask;
        }

        // --- IMainView UI Implementations ---

        public void ShowDashboardData(int totalProduk, int stokKritis, DataTable dtNotif)
        {
            lblTitle.Text = "🌿 Ringkasan Dashboard & Notifikasi";
            dynamicControlPanel.Controls.Clear();
            SetupDataGrid();

            Panel pnlCardContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };

            pnlCardContainer.Controls.Add(CreateInfoCard("Stok Obat Kritis (<= 5)", $"{stokKritis} Item", 320, Color.FromArgb(244, 67, 54)));
            pnlCardContainer.Controls.Add(CreateInfoCard("Total Jenis Obat", $"{totalProduk} Item", 20, Color.FromArgb(33, 150, 243)));

            dynamicControlPanel.Controls.Add(pnlCardContainer);
            dgvMain.DataSource = dtNotif;
        }

        private Panel CreateInfoCard(string title, string val, int x, Color color)
        {
            Panel card = new Panel { Size = new Size(280, 120), Location = new Point(x, 15), BackColor = Color.White };
            card.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);

            Panel topBar = new Panel { Dock = DockStyle.Top, Height = 5, BackColor = color };
            Label lblTitleLocal = new Label { Text = title, Location = new Point(15, 20), AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 9F) };
            Label lblVal = new Label { Text = val, Location = new Point(15, 50), AutoSize = true, Font = new Font("Segoe UI", 20F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 50, 50) };

            card.Controls.Add(lblVal);
            card.Controls.Add(lblTitleLocal);
            card.Controls.Add(topBar);
            return card;
        }

        public void ShowProductCatalog(List<Product> products, bool isAdmin)
        {
            lblTitle.Text = "📦 Katalog Obat Pertanian";
            dynamicControlPanel.Controls.Clear();
            SetupDataGrid();

            dgvMain.DataSource = products;

            if (isAdmin)
            {
                FlowLayoutPanel pnlCrud = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(15) };

                TextBox txtNama = new TextBox { PlaceholderText = "Nama Obat", Width = 150 };
                // Dynamic Product Catalog Categories: TextBox instead of ComboBox
                TextBox txtKategori = new TextBox { PlaceholderText = "Kategori (Custom)", Width = 150 };
                TextBox txtStok = new TextBox { PlaceholderText = "Stok", Width = 80 };
                TextBox txtHarga = new TextBox { PlaceholderText = "Harga", Width = 100 };

                Button btnTambah = new Button { Text = "Tambah Obat", BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Height = 30, Width = 110 };
                Button btnHapus = new Button { Text = "Hapus Obat", BackColor = Color.FromArgb(244, 67, 54), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Height = 30, Width = 110 };

                btnTambah.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(txtNama.Text) || !int.TryParse(txtStok.Text, out int stok) || !decimal.TryParse(txtHarga.Text, out decimal harga))
                    {
                        ShowMessage("Pastikan input data nama, stok, dan harga sudah diisi dengan benar.");
                        return;
                    }
                    AddProductEvent?.Invoke(this, new ProductEventArgs { Name = txtNama.Text, Category = txtKategori.Text, Stock = stok, Price = harga });
                };

                btnHapus.Click += (s, e) =>
                {
                    if (dgvMain.CurrentRow != null)
                    {
                        var cellValue = dgvMain.CurrentRow.Cells["Id"].Value;
                        if (cellValue is int id)
                        {
                            DeleteProductEvent?.Invoke(this, id);
                        }
                    }
                };

                pnlCrud.Controls.Add(new Label { Text = "Kelola Obat:", AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) });
                pnlCrud.Controls.Add(txtNama);
                pnlCrud.Controls.Add(txtKategori);
                pnlCrud.Controls.Add(txtStok);
                pnlCrud.Controls.Add(txtHarga);
                pnlCrud.Controls.Add(btnTambah);
                pnlCrud.Controls.Add(btnHapus);

                dynamicControlPanel.Controls.Add(pnlCrud);
            }
        }

        private ComboBox cbCategoryFilter = null!;
        private DataGridView dgvCart = null!;

        public void ShowPOS(List<Product> searchResults, List<CartItem> cart)
        {
            if (lblTitle.Text != "🛒 Point of Sales (Transaksi Kasir)")
            {
                lblTitle.Text = "🛒 Point of Sales (Transaksi Kasir)";
                dynamicControlPanel.Controls.Clear();
                dynamicControlPanel.Height = 250; // Expand height to fit cart

                FlowLayoutPanel pnlPOS = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(15) };

                // Category Filter ComboBox
                cbCategoryFilter = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 150 };
                cbCategoryFilter.Items.AddRange(new string[] { "Semua Kategori", "Herbisida", "Fungisida", "Insektisida", "Pupuk" });
                cbCategoryFilter.SelectedIndex = 0;
                cbCategoryFilter.SelectedIndexChanged += (s, e) =>
                {
                    FilterCategoryEvent?.Invoke(this, cbCategoryFilter.SelectedItem?.ToString() ?? "");
                };

                // Search Bar
                TextBox txtSearch = new TextBox { PlaceholderText = "Cari Produk & Enter...", Width = 180 };
                txtSearch.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        SearchProductEvent?.Invoke(this, txtSearch.Text);
                    }
                };
                
                TextBox txtQty = new TextBox { PlaceholderText = "Jumlah", Width = 80 };
                Button btnAddCart = new Button { Text = "Tambah ke Keranjang", BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Height = 30, Width = 150 };
                Button btnCheckout = new Button { Text = "Bayar & Cetak", BackColor = Color.FromArgb(255, 152, 0), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Height = 30, Width = 120 };

                btnAddCart.Click += (s, e) =>
                {
                    if (dgvMain.CurrentRow != null && int.TryParse(txtQty.Text, out int qty))
                    {
                        var cellValue = dgvMain.CurrentRow.Cells["Id"].Value;
                        if (cellValue is int id)
                        {
                            var prod = searchResults.FirstOrDefault(p => p.Id == id);
                            if (prod != null)
                            {
                                AddCartEvent?.Invoke(this, new CartItemEventArgs { SelectedProduct = prod, Qty = qty });
                                txtQty.Clear();
                            }
                        }
                    }
                    else
                    {
                        ShowMessage("Pilih produk dari tabel di bawah dan masukkan kuantitas yang benar!");
                    }
                };

                btnCheckout.Click += (s, e) => CheckoutEvent?.Invoke(this, EventArgs.Empty);

                pnlPOS.Controls.Add(new Label { Text = "Kategori:", AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) });
                pnlPOS.Controls.Add(cbCategoryFilter);
                pnlPOS.Controls.Add(new Label { Text = "  Cari:", AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) });
                pnlPOS.Controls.Add(txtSearch);
                pnlPOS.Controls.Add(txtQty);
                pnlPOS.Controls.Add(btnAddCart);
                pnlPOS.Controls.Add(btnCheckout);

                // Add Cart DataGridView to the dynamic panel
                dgvCart = new DataGridView
                {
                    Width = 1000,
                    Height = 130,
                    BackgroundColor = Color.WhiteSmoke,
                    AllowUserToAddRows = false,
                    ReadOnly = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };
                pnlPOS.Controls.Add(dgvCart);

                dynamicControlPanel.Controls.Add(pnlPOS);
            }

            SetupDataGrid();
            dgvMain.DataSource = searchResults;
            dgvCart.DataSource = null; // force refresh
            dgvCart.DataSource = cart;
        }

        public void ShowSupplierManagement(List<Supplier> suppliers)
        {
            lblTitle.Text = "🚛 Manajemen Mitra Supplier";
            dynamicControlPanel.Controls.Clear();
            SetupDataGrid();
            dgvMain.DataSource = suppliers;

            FlowLayoutPanel pnlSupplier = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            TextBox txtNamaSup = new TextBox { PlaceholderText = "Nama Supplier", Width = 150 };
            TextBox txtTelp = new TextBox { PlaceholderText = "No Telepon", Width = 120 };
            TextBox txtAlamat = new TextBox { PlaceholderText = "Alamat", Width = 150 };
            Button btnAddSup = new Button { Text = "Tambah Supplier", BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Width = 130 };

            btnAddSup.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNamaSup.Text)) return;
                AddSupplierEvent?.Invoke(this, new SupplierEventArgs { Name = txtNamaSup.Text, Phone = txtTelp.Text, Address = txtAlamat.Text });
                txtNamaSup.Clear(); txtTelp.Clear(); txtAlamat.Clear();
            };

            pnlSupplier.Controls.Add(txtNamaSup);
            pnlSupplier.Controls.Add(txtTelp);
            pnlSupplier.Controls.Add(txtAlamat);
            pnlSupplier.Controls.Add(btnAddSup);

            dynamicControlPanel.Controls.Add(pnlSupplier);
        }

        public void ShowFinancialReport(DataTable report)
        {
            lblTitle.Text = "📊 Laporan Analitik Keuangan Kios";
            dynamicControlPanel.Controls.Clear();
            SetupDataGrid();

            Label lblInfo = new Label { Text = "Laporan Penjualan Agrokimia Bulanan (Musiman)", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 12F, FontStyle.Bold) };
            dynamicControlPanel.Controls.Add(lblInfo);

            dgvMain.DataSource = report;
        }

        private bool _isPasswordMaskVisible = false;

        public void ShowUserManagement(List<User> users, bool isAdmin)
        {
            lblTitle.Text = "👥 Pengaturan Akun Operator & Karyawan";
            dynamicControlPanel.Controls.Clear();
            dynamicControlPanel.Height = 200;
            SetupDataGrid();
            dgvMain.DataSource = users;

            if (isAdmin)
            {
                FlowLayoutPanel pnlCrud = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(15) };
                
                TextBox txtUser = new TextBox { PlaceholderText = "Username", Width = 150 };
                TextBox txtPass = new TextBox { PlaceholderText = "Password (kosongkan jika tidak diubah saat Edit)", Width = 250 };
                ComboBox cbRole = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 120 };
                cbRole.Items.AddRange(new string[] { "Admin", "Kasir" });
                cbRole.SelectedIndex = 1;

                Button btnTambah = new Button { Text = "Tambah User", BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Height = 30, Width = 110 };
                Button btnUbah = new Button { Text = "Edit User", BackColor = Color.FromArgb(33, 150, 243), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Height = 30, Width = 110 };
                Button btnHapus = new Button { Text = "Hapus User", BackColor = Color.FromArgb(244, 67, 54), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Height = 30, Width = 110 };
                Button btnTogglePass = new Button { Text = "Toggle Password Visibility", BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Height = 30, Width = 200 };

                btnTambah.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
                    {
                        ShowMessage("Username dan Password wajib diisi untuk user baru.");
                        return;
                    }
                    AddUserEvent?.Invoke(this, new UserEventArgs { Username = txtUser.Text, Password = txtPass.Text, Role = cbRole.SelectedItem?.ToString() ?? "Kasir" });
                };

                btnUbah.Click += (s, e) =>
                {
                    if (dgvMain.CurrentRow != null && dgvMain.CurrentRow.Cells["Id"].Value is int id)
                    {
                        if (string.IsNullOrWhiteSpace(txtUser.Text))
                        {
                            ShowMessage("Username wajib diisi saat edit.");
                            return;
                        }
                        UpdateUserEvent?.Invoke(this, new UserEventArgs { Id = id, Username = txtUser.Text, Password = txtPass.Text, Role = cbRole.SelectedItem?.ToString() ?? "Kasir" });
                    }
                };

                btnHapus.Click += (s, e) =>
                {
                    if (dgvMain.CurrentRow != null && dgvMain.CurrentRow.Cells["Id"].Value is int id)
                    {
                        DeleteUserEvent?.Invoke(this, id);
                    }
                };

                btnTogglePass.Click += (s, e) =>
                {
                    _isPasswordMaskVisible = !_isPasswordMaskVisible;
                    dgvMain.Refresh();
                };

                pnlCrud.Controls.Add(new Label { Text = "Kelola User:", AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) });
                pnlCrud.Controls.Add(txtUser);
                pnlCrud.Controls.Add(txtPass);
                pnlCrud.Controls.Add(cbRole);
                pnlCrud.Controls.Add(btnTambah);
                pnlCrud.Controls.Add(btnUbah);
                pnlCrud.Controls.Add(btnHapus);
                pnlCrud.Controls.Add(btnTogglePass);

                dynamicControlPanel.Controls.Add(pnlCrud);
            }

            dgvMain.CellFormatting += DgvMain_CellFormatting_PasswordMask;
        }

        private void DgvMain_CellFormatting_PasswordMask(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMain.Columns[e.ColumnIndex].Name == "Password" && e.Value != null)
            {
                if (!_isPasswordMaskVisible)
                {
                    e.Value = new string('*', 8); // Mask length arbitrary
                    e.FormattingApplied = true;
                }
            }
        }

        public void PrintReceipt(string receiptContent)
        {
            MessageBox.Show(receiptContent, "Struk Pembelian Berhasil Dicetak", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}