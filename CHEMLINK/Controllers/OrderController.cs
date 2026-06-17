using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CHEMLINK.Contexts;
using CHEMLINK.Models;
using CHEMLINK.Views;

namespace CHEMLINK.Controllers
{
    /// <summary>
    /// Handles POS (Point of Sales), cart operations, checkout, and financial reports.
    /// </summary>
    public class OrderController
    {
        private readonly MainForm _view;
        private readonly User _currentUser;

        // Contexts
        private readonly ProductContext _productContext;
        private readonly OrderContext _orderContext;

        // In-memory state
        private List<Product> _products;
        private readonly List<CartItem> _cart;
        private string _currentCategoryFilter = "";
        private string _currentSearchQuery = "";

        public OrderController(MainForm view, User user)
        {
            _view = view;
            _currentUser = user;

            _productContext = new ProductContext();
            _orderContext = new OrderContext();
            _products = _productContext.Read();
            _cart = new List<CartItem>();

            // Wire POS / cart / search events
            _view.AddCartEvent += HandleAddCart;
            _view.DeleteCartEvent += HandleDeleteCart;
            _view.CheckoutEvent += HandleCheckout;
            _view.SearchProductEvent += HandleSearchProduct;
            _view.FilterCategoryEvent += HandleFilterCategory;
        }

        /// <summary>Compute display stock = DB stock minus items reserved in cart.</summary>
        private List<Product> GetDisplayProducts(IEnumerable<Product> source)
        {
            return source.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Stock = p.Stock - _cart.Where(ci => ci.ProductId == p.Id).Sum(ci => ci.Qty),
                Description = p.Description,
                SupplierName = p.SupplierName,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId
            }).ToList();
        }

        public void ShowPOS()
        {
            _products = _productContext.Read();
            _cart.Clear();
            _currentCategoryFilter = "";
            _currentSearchQuery = "";
            _view.ShowPOS(GetDisplayProducts(_products), _cart);
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
            var filtered = _products.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(_currentCategoryFilter) && _currentCategoryFilter != "Semua Kategori")
            {
                filtered = filtered.Where(p => string.Equals(p.Category, _currentCategoryFilter, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(_currentSearchQuery))
            {
                filtered = filtered.Where(p => p.Name.Contains(_currentSearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            _view.ShowPOS(GetDisplayProducts(filtered), _cart);
        }

        private void HandleAddCart(object? sender, CartItemEventArgs e)
        {
            if (e.SelectedProduct == null || e.Qty <= 0)
            {
                _view.ShowMessage("Masukkan produk dan kuantitas dengan benar!");
                return;
            }

            // Check available stock from DB minus what's already in cart
            var dbProduct = _products.FirstOrDefault(p => p.Id == e.SelectedProduct.Id);
            if (dbProduct == null) return;

            int inCart = _cart.Where(ci => ci.ProductId == e.SelectedProduct.Id).Sum(ci => ci.Qty);
            int available = dbProduct.Stock - inCart;

            if (e.Qty > available)
            {
                _view.ShowMessage($"Stok tidak mencukupi! Sisa stok {e.SelectedProduct.Name} hanya {available}.");
                return;
            }

            _cart.Add(new CartItem { ProductId = e.SelectedProduct.Id, ProductName = e.SelectedProduct.Name, Qty = e.Qty, Price = e.SelectedProduct.Price });

            // Refresh display — stock shown = DB stock minus cart qty (no mutation needed)
            _view.ShowPOS(GetDisplayProducts(_products), _cart);
        }

        private void HandleDeleteCart(object? sender, CartItem e)
        {
            // Remove from cart — display stock auto-recalculates via GetDisplayProducts
            var cartItem = _cart.FirstOrDefault(c => c.ProductId == e.ProductId && c.Qty == e.Qty);
            if (cartItem != null)
            {
                _cart.Remove(cartItem);
            }

            _view.ShowPOS(GetDisplayProducts(_products), _cart);
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

        public void ShowFinancialReport()
        {
            DataTable dtLaporan = _orderContext.GetFinancialReport();
            DataTable dtKategori = _orderContext.GetCategoryBreakdown();
            _view.ShowFinancialReport(dtLaporan, dtKategori);
        }
    }
}
