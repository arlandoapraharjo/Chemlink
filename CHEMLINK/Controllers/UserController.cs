using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Npgsql;
using CHEMLINK.Models;
using CHEMLINK.Views;
using CHEMLINK.Contexts;

namespace CHEMLINK.Controllers
{
    public class UserController
    {
        private readonly MainForm _view;
        private readonly User _currentUser;

        // Contexts
        private readonly ProductContext _productContext;
        private readonly SupplierContext _supplierContext;
        private readonly UserContext _userContext;
        private readonly OrderContext _orderContext;

        // In-Memory state for the active session (still useful for cart)
        private List<Product> _products;
        private List<Supplier> _suppliers;
        private List<User> _users;
        private readonly List<CartItem> _cart;

        public UserController(MainForm view, User user)
        {
            _view = view;
            _currentUser = user;

            _productContext = new ProductContext();
            _supplierContext = new SupplierContext();
            _userContext = new UserContext();
            _orderContext = new OrderContext();

            _products = _productContext.Read();
            _suppliers = _supplierContext.Read();
            _users = _userContext.Read();
            _cart = new List<CartItem>();

            _view.SetActiveUser(_currentUser.Username, _currentUser.Role);
            _view.ApplyRoleRestrictions(_currentUser.Role == "Admin");

            // Wire Menu Navigations
            _view.ShowDashboardEvent += (s, e) => ShowDashboard();
            _view.ShowProductEvent += (s, e) => ShowProductCatalog();
            _view.ShowTransactionEvent += (s, e) => ShowPOS();
            _view.ShowSupplierEvent += (s, e) => ShowSupplierManagement();
            _view.ShowReportEvent += (s, e) => ShowFinancialReport();
            _view.ShowUserManagementEvent += (s, e) => ShowUserManagement();

            // Wire Actions
            _view.AddProductEvent += HandleAddProduct;
            _view.DeleteProductEvent += HandleDeleteProduct;
            _view.AddCartEvent += HandleAddCart;
            _view.CheckoutEvent += HandleCheckout;
            _view.AddSupplierEvent += HandleAddSupplier;
            _view.SearchProductEvent += HandleSearchProduct;
            _view.FilterCategoryEvent += HandleFilterCategory;

            // User CRUD
            _view.AddUserEvent += HandleAddUser;
            _view.UpdateUserEvent += HandleUpdateUser;
            _view.DeleteUserEvent += HandleDeleteUser;

            ShowDashboard();
        }

        private void ShowDashboard()
        {
            _products = _productContext.Read(); // Refresh
            int totalProduk = _products.Count;
            int stokKritis = _products.Count(p => p.Stock <= 5);

            DataTable dtNotif = new DataTable();
            dtNotif.Columns.Add("Jenis Peringatan", typeof(string));
            dtNotif.Columns.Add("Informasi Kritis", typeof(string));
            dtNotif.Columns.Add("Status Tindakan", typeof(string));

            foreach (var p in _products.Where(p => p.Stock <= 5))
            {
                dtNotif.Rows.Add("STOK MENIPIS!", $"Segera lakukan pemesanan ulang untuk {p.Name} (Sisa: {p.Stock})", "Butuh Reorder");
            }

            if (dtNotif.Rows.Count == 0)
            {
                dtNotif.Rows.Add("Sistem Aman", "Seluruh pasokan stok obat pertanian dalam keadaan aman.", "Normal");
            }

            _view.ShowDashboardData(totalProduk, stokKritis, dtNotif);
        }

        private void ShowProductCatalog()
        {
            _products = _productContext.Read();
            _view.ShowProductCatalog(_products, _currentUser.Role == "Admin");
        }

        private void HandleAddProduct(object? sender, ProductEventArgs e)
        {
            _productContext.Create(e.Name, e.Category, e.Stock, e.Price);
            _view.ShowMessage("Obat pertanian berhasil ditambahkan!");
            ShowProductCatalog();
        }

        private void HandleDeleteProduct(object? sender, int id)
        {
            _view.ShowMessage("Obat berhasil dihapus (simulasi).");
            ShowProductCatalog();
        }

        private string _currentCategoryFilter = "";
        private string _currentSearchQuery = "";

        private void ShowPOS()
        {
            _products = _productContext.Read();
            _cart.Clear();
            _currentCategoryFilter = "";
            _currentSearchQuery = "";
            _view.ShowPOS(_products, _cart);
        }

        private void HandleFilterCategory(object? sender, string category)
        {
            _currentCategoryFilter = category;
            ApplyPOSFilters();
        }

        private void HandleSearchProduct(object? sender, string query)
        {
            _currentSearchQuery = query;
            ApplyPOSFilters();
        }

        private void ApplyPOSFilters()
        {
            _products = _productContext.Read();
            var searchResults = _products.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(_currentCategoryFilter) && _currentCategoryFilter != "Semua Kategori")
            {
                searchResults = searchResults.Where(p => string.Equals(p.Category, _currentCategoryFilter, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(_currentSearchQuery))
            {
                searchResults = searchResults.Where(p => p.Name.Contains(_currentSearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            _view.ShowPOS(searchResults.ToList(), _cart);
        }

        private void HandleAddCart(object? sender, CartItemEventArgs e)
        {
            if (e.SelectedProduct == null || e.Qty <= 0)
            {
                _view.ShowMessage("Masukkan produk dan kuantitas dengan benar!");
                return;
            }

            if (e.Qty > e.SelectedProduct.Stock)
            {
                _view.ShowMessage($"Stok tidak mencukupi! Sisa stok {e.SelectedProduct.Name} hanya {e.SelectedProduct.Stock}.");
                return;
            }

            _cart.Add(new CartItem { ProductId = e.SelectedProduct.Id, ProductName = e.SelectedProduct.Name, Qty = e.Qty, Price = e.SelectedProduct.Price });
            e.SelectedProduct.Stock -= e.Qty; // Potong Stok (simulasi)

            _view.ShowPOS(_products, _cart);
        }

        private void HandleCheckout(object? sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                _view.ShowMessage("Keranjang belanja masih kosong!");
                return;
            }

            try
            {
                _orderContext.Checkout(_cart, _currentUser.Id, "Penjualan Kasir");
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Gagal memproses transaksi: " + ex.Message);
                return;
            }

            decimal total = _cart.Sum(c => c.Total);
            string struk = "============== STRUK CHEMLINK ==============\n";
            struk += $"Kasir: {_currentUser.FullName}\n";
            struk += $"Tanggal: {DateTime.Now:dd/MM/yyyy HH:mm}\n";
            struk += "--------------------------------------------\n";
            foreach (var item in _cart)
            {
                struk += $"{item.ProductName}\n   {item.Qty} x Rp{item.Price:N0} = Rp{item.Total:N0}\n";
            }
            struk += "--------------------------------------------\n";
            struk += $"TOTAL BELANJA: Rp{total:N0}\n";
            struk += "============================================\n";
            struk += "Terima kasih telah berbelanja di Kios Kami!";

            _cart.Clear();
            _view.PrintReceipt(struk);
            ShowPOS();
        }

        private void ShowSupplierManagement()
        {
            _suppliers = _supplierContext.Read();
            _view.ShowSupplierManagement(_suppliers);
        }

        private void HandleAddSupplier(object? sender, SupplierEventArgs e)
        {
            try
            {
                var supplier = new Supplier { Name = e.Name, Phone = e.Phone, Address = e.Address };
                _supplierContext.Create(supplier);
                _view.ShowMessage("Supplier baru berhasil dicatat!");
                ShowSupplierManagement();
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                string message = ex.ConstraintName switch
                {
                    "supplier_email_key" => "Email supplier sudah terdaftar.",
                    "supplier_no_telp_key" => "Nomor telepon supplier sudah terdaftar.",
                    _ => "Data supplier sudah ada di sistem."
                };
                _view.ShowMessage(message);
            }
        }

        private void ShowFinancialReport()
        {
            DataTable dtLaporan = _orderContext.GetFinancialReport();
            _view.ShowFinancialReport(dtLaporan);
        }

        private void ShowUserManagement()
        {
            _users = _userContext.Read();
            _view.ShowUserManagement(_users, _currentUser.Role == "Admin");
        }

        private void HandleAddUser(object? sender, UserEventArgs e)
        {
            var newUser = new User { Username = e.Username, Password = e.Password, Role = e.Role };
            _userContext.Create(newUser);
            _view.ShowMessage("User berhasil ditambahkan!");
            ShowUserManagement();
        }

        private void HandleUpdateUser(object? sender, UserEventArgs e)
        {
            var updatedUser = new User { Id = e.Id, Username = e.Username, Password = e.Password, Role = e.Role };
            _userContext.Update(updatedUser);
            _view.ShowMessage("User berhasil diupdate!");
            ShowUserManagement();
        }

        private void HandleDeleteUser(object? sender, int id)
        {
            _userContext.Delete(id);
            _view.ShowMessage("User berhasil dihapus!");
            ShowUserManagement();
        }
    }
}
