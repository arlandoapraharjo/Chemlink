using System;
using System.Collections.Generic;
using Npgsql;
using CHEMLINK.Contexts;
using CHEMLINK.Models;
using CHEMLINK.Views;

namespace CHEMLINK.Controllers
{
    /// <summary>
    /// Handles supplier CRUD operations.
    /// </summary>
    public class SupplierController
    {
        private readonly MainForm _view;
        private readonly User _currentUser;

        // Context
        private readonly SupplierContext _supplierContext;

        // In-memory state
        private List<Supplier> _suppliers;

        public SupplierController(MainForm view, User user)
        {
            _view = view;
            _currentUser = user;

            _supplierContext = new SupplierContext();
            _suppliers = _supplierContext.Read();

            // Wire supplier events
            _view.AddSupplierEvent += HandleAddSupplier;
            _view.UpdateSupplierEvent += HandleUpdateSupplier;
            _view.DeleteSupplierEvent += HandleDeleteSupplier;
        }

        public void ShowSupplierManagement()
        {
            _suppliers = _supplierContext.Read();
            _view.ShowSupplierManagement(_suppliers);
        }

        private void HandleAddSupplier(object? sender, Supplier e)
        {
            try
            {
                _supplierContext.Create(e);
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

        private void HandleUpdateSupplier(object? sender, Supplier e)
        {
            try
            {
                _supplierContext.Update(e.Id, e);
                _view.ShowMessage("Data supplier berhasil diupdate!");
                ShowSupplierManagement();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Gagal mengupdate supplier: " + ex.Message);
            }
        }

        private void HandleDeleteSupplier(object? sender, int id)
        {
            try
            {
                _supplierContext.Delete(id);
                _view.ShowMessage("Supplier berhasil dihapus.");
                ShowSupplierManagement();
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Gagal menghapus supplier: " + ex.Message);
            }
        }
    }
}
