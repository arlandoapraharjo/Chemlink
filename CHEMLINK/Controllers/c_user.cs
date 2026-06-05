using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CHEMLINK.Models;
using CHEMLINK.Views.Interfaces;

namespace CHEMLINK.Controllers
{
    public class MainController
    {
        private readonly IMainView _view;
        private readonly User _currentUser;

        // Penyimpanan Data Sementara (In-Memory Database)
        private readonly List<Product> _products;
        private readonly List<Supplier> _suppliers;
        private readonly List<User> _users;
        private readonly List<CartItem> _cart;

        public MainController(IMainView view, User user)
        {
            _view = view;
            _currentUser = user;

            // Inisialisasi Data Default
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Gramoxone 276SL 1L", Category = "Herbisida", Stock = 25, Price = 85000 },
                new Product { Id = 2, Name = "Antracol 70WP 500gr", Category = "Fungisida", Stock = 5, Price = 95000 }, // Stok sedikit (kritis)
                new Product { Id = 3, Name = "Furadan 3GR 2kg", Category = "Insektisida", Stock = 40, Price = 45000 },
                new Product { Id = 4, Name = "Roundup 486SL 1L", Category = "Herbisida", Stock = 2, Price = 105000 }  // Stok kritis
            };

            _suppliers = new List<Supplier>
            {
                new Supplier { Id = 1, Name = "PT Agro Sentosa", Phone = "08123456789", Address = "Surabaya" },
                new Supplier { Id = 2, Name = "CV Tani Makmur", Phone = "08987654321", Address = "Malang" }
            };

            _users = new List<User>
            {
                new User { Username = "admin", FullName = "H. Mansur (Owner)", Role = "Admin" },
                new User { Username = "kasir", FullName = "Siti Aminah", Role = "Kasir" }
            };

            _cart = new List<CartItem>();

            // Setup Profile Pengguna
            _view.SetActiveUser(_currentUser.Username, _currentUser.Role);
            _view.ApplyRoleRestrictions(_currentUser.Role == "Admin");

            // Hubungkan Event Navigasi
            _view.ShowDashboardEvent += (s, e) => ShowDashboard();
            _view.ShowProductEvent += (s, e) => ShowProductKatalog();
            _view.ShowTransactionEvent += (s, e) => ShowKasirPOS();
            _view.ShowSupplierEvent += (s, e) => ShowSupplierManagement();
            _view.ShowReportEvent += (s, e) => ShowLaporanKeuangan();
            _view.ShowUserManagementEvent += (s, e) => ShowUserManagement();

            // Tampilkan Dashboard sebagai halaman utama bawaan
            ShowDashboard();
        }

        // ==========================================
        // 1. MODUL DASHBOARD
        // ==========================================
        private void ShowDashboard()
        {
            _view.TitleLabel.Text = "🌿 Ringkasan Dashboard & Notifikasi";
            _view.DynamicControlPanel.Controls.Clear();

            Panel pnlCardContainer = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };

            int totalProduk = _products.Count;
            int stokKritis = _products.FindAll(p => p.Stock <= 5).Count;

            pnlCardContainer.Controls.Add(CreateInfoCard("Stok Obat Kritis (<= 5)", $"{stokKritis} Item", 320, Color.FromArgb(244, 67, 54)));
            pnlCardContainer.Controls.Add(CreateInfoCard("Total Jenis Obat", $"{totalProduk} Item", 20, Color.FromArgb(33, 150, 243)));

            _view.DynamicControlPanel.Controls.Add(pnlCardContainer);

            DataTable dtNotif = new DataTable();
            dtNotif.Columns.Add("Jenis Peringatan", typeof(string));
            dtNotif.Columns.Add("Informasi Kritis", typeof(string));
            dtNotif.Columns.Add("Status Tindakan", typeof(string));

            foreach (var p in _products)
            {
                if (p.Stock <= 5)
                {
                    dtNotif.Rows.Add("STOK MENIPIS!", $"Segera lakukan pemesanan ulang untuk {p.Name} (Sisa: {p.Stock})", "Butuh Reorder");
                }
            }

            if (dtNotif.Rows.Count == 0)
            {
                dtNotif.Rows.Add("Sistem Aman", "Seluruh pasokan stok obat pertanian dalam keadaan aman.", "Normal");
            }

            _view.MainDataGrid.DataSource = dtNotif;
        }

        private Panel CreateInfoCard(string title, string val, int x, Color color)
        {
            Panel card = new Panel { Size = new Size(280, 120), Location = new Point(x, 15), BackColor = Color.White };
            card.Paint += (s, e) => ControlPaint.DrawBorder(e.Graphics, card.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);

            Panel topBar = new Panel { Dock = DockStyle.Top, Height = 5, BackColor = color };
            Label lblTitle = new Label { Text = title, Location = new Point(15, 20), AutoSize = true, ForeColor = Color.Gray, Font = new Font("Segoe UI", 9F) };
            Label lblVal = new Label { Text = val, Location = new Point(15, 50), AutoSize = true, Font = new Font("Segoe UI", 20F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 50, 50) };

            card.Controls.Add(lblVal);
            card.Controls.Add(lblTitle);
            card.Controls.Add(topBar);
            return card;
        }

        // ==========================================
        // 2. MODUL KATALOG PRODUK
        // ==========================================
        private void ShowProductKatalog()
        {
            _view.TitleLabel.Text = "📦 Katalog Obat Pertanian";
            _view.DynamicControlPanel.Controls.Clear();

            RefreshProductGrid();

            if (_currentUser.Role == "Admin")
            {
                FlowLayoutPanel pnlCrud = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(15) };

                TextBox txtNama = new TextBox { PlaceholderText = "Nama Obat", Width = 150 };
                ComboBox cbKategori = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 120 };
                cbKategori.Items.AddRange(new string[] { "Herbisida", "Fungisida", "Insektisida", "Pupuk" });
                cbKategori.SelectedIndex = 0;

                TextBox txtStok = new TextBox { PlaceholderText = "Stok", Width = 80 };
                TextBox txtHarga = new TextBox { PlaceholderText = "Harga", Width = 100 };

                Button btnTambah = new Button { Text = "Tambah Obat", BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Height = 30, Width = 110 };
                Button btnHapus = new Button { Text = "Hapus Obat", BackColor = Color.FromArgb(244, 67, 54), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Height = 30, Width = 110 };

                btnTambah.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(txtNama.Text) || !int.TryParse(txtStok.Text, out int stok) || !decimal.TryParse(txtHarga.Text, out decimal harga))
                    {
                        _view.ShowMessage("Pastikan input data nama, stok, dan harga sudah diisi dengan benar.");
                        return;
                    }

                    _products.Add(new Product { Id = _products.Count + 1, Name = txtNama.Text, Category = cbKategori.Text, Stock = stok, Price = harga });
                    RefreshProductGrid();
                    txtNama.Clear(); txtStok.Clear(); txtHarga.Clear();
                    _view.ShowMessage("Obat pertanian berhasil ditambahkan ke katalog!");
                };

                btnHapus.Click += (s, e) =>
                {
                    if (_view.MainDataGrid.CurrentRow != null)
                    {
                        int id = (int)_view.MainDataGrid.CurrentRow.Cells["Id"].Value;
                        _products.RemoveAll(p => p.Id == id);
                        RefreshProductGrid();
                        _view.ShowMessage("Obat berhasil dihapus!");
                    }
                };

                pnlCrud.Controls.Add(new Label { Text = "Kelola Obat:", AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) });
                pnlCrud.Controls.Add(txtNama);
                pnlCrud.Controls.Add(cbKategori);
                pnlCrud.Controls.Add(txtStok);
                pnlCrud.Controls.Add(txtHarga);
                pnlCrud.Controls.Add(btnTambah);
                pnlCrud.Controls.Add(btnHapus);

                _view.DynamicControlPanel.Controls.Add(pnlCrud);
            }
        }

        private void RefreshProductGrid()
        {
            _view.MainDataGrid.DataSource = null;
            _view.MainDataGrid.DataSource = _products;
        }

        // ==========================================
        // 3. MODUL TRANSAKSI KASIR
        // ==========================================
        private void ShowKasirPOS()
        {
            _view.TitleLabel.Text = "🛒 Point of Sales (Transaksi Kasir)";
            _view.DynamicControlPanel.Controls.Clear();
            _cart.Clear();

            FlowLayoutPanel pnlPOS = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(15) };

            ComboBox cbProduk = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 200 };
            foreach (var p in _products) cbProduk.Items.Add($"{p.Id} - {p.Name} (Stok: {p.Stock})");
            if (cbProduk.Items.Count > 0) cbProduk.SelectedIndex = 0;

            TextBox txtQty = new TextBox { PlaceholderText = "Jumlah", Width = 80 };
            Button btnAddCart = new Button { Text = "Tambah ke Keranjang", BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Width = 150 };
            Button btnCheckout = new Button { Text = "Bayar & Cetak", BackColor = Color.FromArgb(255, 152, 0), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Width = 120 };

            btnAddCart.Click += (s, e) =>
            {
                if (cbProduk.SelectedIndex == -1 || !int.TryParse(txtQty.Text, out int qty) || qty <= 0)
                {
                    _view.ShowMessage("Masukkan kuantitas pembelian dengan benar!");
                    return;
                }

                int selId = int.Parse(cbProduk.Text.Split('-')[0].Trim());
                Product p = _products.Find(prod => prod.Id == selId)!;

                if (qty > p.Stock)
                {
                    _view.ShowMessage($"Stok tidak mencukupi! Sisa stok {p.Name} hanya {p.Stock}.");
                    return;
                }

                _cart.Add(new CartItem { ProductId = p.Id, ProductName = p.Name, Qty = qty, Price = p.Price });
                p.Stock -= qty; // Potong Stok Langsung (Logic FIFO/Real-time)

                cbProduk.Items.Clear();
                foreach (var prod in _products) cbProduk.Items.Add($"{prod.Id} - {prod.Name} (Stok: {prod.Stock})");
                cbProduk.SelectedIndex = 0;

                RefreshCartGrid();
                txtQty.Clear();
            };

            btnCheckout.Click += (s, e) =>
            {
                if (_cart.Count == 0)
                {
                    _view.ShowMessage("Keranjang belanja masih kosong!");
                    return;
                }

                decimal total = 0;
                string struk = "============== STRUK CHEMLINK ==============\n";
                struk += $"Kasir: {_currentUser.FullName}\n";
                struk += $"Tanggal: {DateTime.Now:dd/MM/yyyy HH:mm}\n";
                struk += "--------------------------------------------\n";
                foreach (var item in _cart)
                {
                    struk += $"{item.ProductName}\n   {item.Qty} x Rp{item.Price:N0} = Rp{item.Total:N0}\n";
                    total += item.Total;
                }
                struk += "--------------------------------------------\n";
                struk += $"TOTAL BELANJA: Rp{total:N0}\n";
                struk += "============================================\n";
                struk += "Terima kasih telah berbelanja di Kios Kami!";

                MessageBox.Show(struk, "Struk Pembelian Berhasil Dicetak", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _cart.Clear();
                RefreshCartGrid();
            };

            pnlPOS.Controls.Add(new Label { Text = "Pilih Produk:", AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) });
            pnlPOS.Controls.Add(cbProduk);
            pnlPOS.Controls.Add(txtQty);
            pnlPOS.Controls.Add(btnAddCart);
            pnlPOS.Controls.Add(btnCheckout);

            _view.DynamicControlPanel.Controls.Add(pnlPOS);
            RefreshCartGrid();
        }

        private void RefreshCartGrid()
        {
            _view.MainDataGrid.DataSource = null;
            _view.MainDataGrid.DataSource = _cart;
        }

        // ==========================================
        // 4. MODUL SUPPLIER
        // ==========================================
        private void ShowSupplierManagement()
        {
            _view.TitleLabel.Text = "🚛 Manajemen Mitra Supplier";
            _view.DynamicControlPanel.Controls.Clear();

            RefreshSupplierGrid();

            FlowLayoutPanel pnlSupplier = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(15) };
            TextBox txtNamaSup = new TextBox { PlaceholderText = "Nama Supplier", Width = 150 };
            TextBox txtTelp = new TextBox { PlaceholderText = "No Telepon", Width = 120 };
            TextBox txtAlamat = new TextBox { PlaceholderText = "Alamat", Width = 150 };
            Button btnAddSup = new Button { Text = "Tambah Supplier", BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Width = 130 };

            btnAddSup.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNamaSup.Text)) return;
                _suppliers.Add(new Supplier { Id = _suppliers.Count + 1, Name = txtNamaSup.Text, Phone = txtTelp.Text, Address = txtAlamat.Text });
                RefreshSupplierGrid();
                txtNamaSup.Clear(); txtTelp.Clear(); txtAlamat.Clear();
                _view.ShowMessage("Supplier baru berhasil dicatat.");
            };

            pnlSupplier.Controls.Add(txtNamaSup);
            pnlSupplier.Controls.Add(txtTelp);
            pnlSupplier.Controls.Add(txtAlamat);
            pnlSupplier.Controls.Add(btnAddSup);

            _view.DynamicControlPanel.Controls.Add(pnlSupplier);
        }

        private void RefreshSupplierGrid()
        {
            _view.MainDataGrid.DataSource = null;
            _view.MainDataGrid.DataSource = _suppliers;
        }

        // ==========================================
        // 5. MODUL LAPORAN KEUANGAN
        // ==========================================
        private void ShowLaporanKeuangan()
        {
            _view.TitleLabel.Text = "📊 Laporan Analitik Keuangan Kios";
            _view.DynamicControlPanel.Controls.Clear();

            Label lblInfo = new Label { Text = "Laporan Penjualan Agrokimia Bulanan (Musiman)", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 12F, FontStyle.Bold) };
            _view.DynamicControlPanel.Controls.Add(lblInfo);

            DataTable dtLaporan = new DataTable();
            dtLaporan.Columns.Add("Bulan", typeof(string));
            dtLaporan.Columns.Add("Total Transaksi", typeof(string));
            dtLaporan.Columns.Add("Kategori Terlaris", typeof(string));
            dtLaporan.Columns.Add("Omzet Bersih", typeof(string));

            dtLaporan.Rows.Add("Januari", "120 Kali", "Fungisida (Musim Hujan)", "Rp14.500.000");
            dtLaporan.Rows.Add("Februari", "95 Kali", "Fungisida (Musim Hujan)", "Rp11.200.000");
            dtLaporan.Rows.Add("Maret", "140 Kali", "Insektisida (Ulat Grayak)", "Rp18.900.000");
            dtLaporan.Rows.Add("April", "80 Kali", "Herbisida (Pratanam)", "Rp9.000.000");

            _view.MainDataGrid.DataSource = dtLaporan;
        }

        // ==========================================
        // 6. MODUL USER MANAGEMENT
        // ==========================================
        private void ShowUserManagement()
        {
            _view.TitleLabel.Text = "👥 Pengaturan Akun Operator & Karyawan";
            _view.DynamicControlPanel.Controls.Clear();

            _view.MainDataGrid.DataSource = null;
            _view.MainDataGrid.DataSource = _users;
        }
    }
}
