using System;
using System.Data;
using System.Collections.Generic;
using CHEMLINK.Contexts;
using CHEMLINK.Models;
using CHEMLINK.Views;

namespace CHEMLINK.Controllers
{
    /// <summary>
    /// Top-level coordinator controller.
    /// Handles page navigation, dashboard display, and creates all domain controllers.
    /// </summary>
    public class MainController
    {
        private readonly MainForm _view;
        private readonly User _currentUser;

        // Contexts needed for dashboard
        private readonly ProductContext _productContext;
        private readonly OrderContext _orderContext;

        // In-memory state for dashboard
        private List<Product> _products;

        // Domain sub-controllers (created and held here)
        private readonly ProductController _productController;
        private readonly OrderController _orderController;
        private readonly SupplierController _supplierController;
        private readonly UserController _userController;

        public MainController(MainForm view, User user)
        {
            _view = view;
            _currentUser = user;

            _productContext = new ProductContext();
            _orderContext = new OrderContext();
            _products = _productContext.Read();

            _view.SetActiveUser(_currentUser.Username, _currentUser.Role);
            _view.ApplyRoleRestrictions(_currentUser.Role == "Admin");

            // Create domain controllers — each wires its own events from MainForm
            _productController = new ProductController(view, user);
            _orderController = new OrderController(view, user);
            _supplierController = new SupplierController(view, user);
            _userController = new UserController(view, user);

            // Wire navigation events
            _view.ShowDashboardEvent += (s, e) => ShowDashboard();
            _view.ShowProductEvent += (s, e) => _productController.ShowProductCatalog();
            _view.ShowTransactionEvent += (s, e) => _orderController.ShowPOS();
            _view.ShowSupplierEvent += (s, e) => _supplierController.ShowSupplierManagement();
            _view.ShowReportEvent += (s, e) => _orderController.ShowFinancialReport();
            _view.ShowUserManagementEvent += (s, e) => _userController.ShowUserManagement();

            // Show initial page
            ShowDashboard();
        }

        private void ShowDashboard()
        {
            _products = _productContext.Read(); // Refresh

            // Use DB view for critical stock to include id and name
            var dtNotif = _productContext.GetCriticalStockTable();
            // Rename columns for user-friendly headers
            if (dtNotif != null)
            {
                if (dtNotif.Columns.Contains("id_produk")) dtNotif.Columns["id_produk"]!.ColumnName = "ID";
                if (dtNotif.Columns.Contains("nama_produk")) dtNotif.Columns["nama_produk"]!.ColumnName = "Nama Produk";
                if (dtNotif.Columns.Contains("jumlah_stock")) dtNotif.Columns["jumlah_stock"]!.ColumnName = "Jumlah Stock";
            }

            // Fetch stock activity log
            var dtLogStok = _orderContext.GetStockLog();

            _view.ShowDashboardData(_products, dtNotif!, dtLogStok);
        }
    }
}
