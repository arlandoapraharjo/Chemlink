using System;
using System.Windows.Forms;

namespace CHEMLINK.Views.Interfaces
{
    public interface IMainView
    {
        // Event Navigasi Menu Utama
        event EventHandler ShowDashboardEvent;
        event EventHandler ShowProductEvent;
        event EventHandler ShowTransactionEvent;
        event EventHandler ShowSupplierEvent;
        event EventHandler ShowReportEvent;
        event EventHandler ShowUserManagementEvent;
        event EventHandler LogoutEvent;

        // Properti Data Binding & Elemen Kontrol UI
        DataGridView MainDataGrid { get; }
        Label TitleLabel { get; }
        Panel DynamicControlPanel { get; }

        // Method Pembatasan Hak Akses
        void SetActiveUser(string username, string role);
        void ApplyRoleRestrictions(bool isAdmin);
        void ShowMessage(string message);
    }
}
