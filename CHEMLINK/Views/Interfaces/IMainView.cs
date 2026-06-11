using System;
using System.Collections.Generic;
using System.Data;
using CHEMLINK.Models;

namespace CHEMLINK.Views.Interfaces
{
    public class UserEventArgs : EventArgs
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "";
    }

    public class ProductEventArgs : EventArgs
    {
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }

    public class CartItemEventArgs : EventArgs
    {
        public Product SelectedProduct { get; set; } = null!;
        public int Qty { get; set; }
    }

    public class SupplierEventArgs : EventArgs
    {
        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
    }

    public interface IMainView
    {
        // Menu Navigation Events
        event EventHandler ShowDashboardEvent;
        event EventHandler ShowProductEvent;
        event EventHandler ShowTransactionEvent;
        event EventHandler ShowSupplierEvent;
        event EventHandler ShowReportEvent;
        event EventHandler ShowUserManagementEvent;
        event EventHandler LogoutEvent;

        // Action Events
        event EventHandler<ProductEventArgs> AddProductEvent;
        event EventHandler<int> DeleteProductEvent;
        event EventHandler<CartItemEventArgs> AddCartEvent;
        event EventHandler CheckoutEvent;
        event EventHandler<SupplierEventArgs> AddSupplierEvent;
        event EventHandler<string> SearchProductEvent;
        event EventHandler<string> FilterCategoryEvent;

        // User Management Action Events
        event EventHandler<UserEventArgs> AddUserEvent;
        event EventHandler<UserEventArgs> UpdateUserEvent;
        event EventHandler<int> DeleteUserEvent;

        // Role & Info Methods
        void SetActiveUser(string username, string role);
        void ApplyRoleRestrictions(bool isAdmin);
        void ShowMessage(string message);

        // UI Update Methods
        void ShowDashboardData(int totalProduk, int stokKritis, DataTable dtNotif);
        void ShowProductCatalog(List<Product> products, bool isAdmin);
        void ShowPOS(List<Product> searchResults, List<CartItem> cart);
        void ShowSupplierManagement(List<Supplier> suppliers);
        void ShowFinancialReport(DataTable report);
        void ShowUserManagement(List<User> users, bool isAdmin);
        void PrintReceipt(string receiptContent);
    }
}
