using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CHEMLINK.Models;
using CHEMLINK.Contexts;

namespace CHEMLINK.Views
{
    public partial class SupplierManagementControl : UserControl
    {
        public event EventHandler<Supplier>? AddSupplierEvent;
        public event EventHandler<Supplier>? UpdateSupplierEvent;
        public event EventHandler<int>? DeleteSupplierEvent;

        private List<Supplier> _suppliers = new();

        public SupplierManagementControl()
        {
            InitializeComponent();
            btnTambah.Click += BtnTambah_Click;
            btnUbah.Click += BtnUbah_Click;
            btnHapus.Click += BtnHapus_Click;
            this.Paint += Control_Paint;
        }

        private void Control_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            using var pen = new Pen(Color.FromArgb(2, 44, 34), 2f);
            g.DrawRectangle(pen, pnlGrid.Bounds);
            if (pnlToolbar.Visible)
                g.DrawRectangle(pen, pnlToolbar.Bounds);
        }

        public void SetData(List<Supplier> suppliers, bool isAdmin = true)
        {
            _suppliers = suppliers;
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = new BindingList<Supplier>(suppliers);

            try
            {
                dgvMain.Columns["Id"]!.HeaderText = "ID Supplier";
                dgvMain.Columns["Name"]!.HeaderText = "Nama Perusahaan";
                dgvMain.Columns["KontakPerson"]!.HeaderText = "Kontak Person";
                dgvMain.Columns["Phone"]!.HeaderText = "Nomor Telepon";
                dgvMain.Columns["Email"]!.HeaderText = "Email";
                dgvMain.Columns["Address"]!.HeaderText = "Alamat";
                dgvMain.Columns["Kota"]!.HeaderText = "Kota";
                dgvMain.Columns["Status"]!.HeaderText = "Status";
                dgvMain.Columns["Status"]!.Visible = false;

                dgvMain.Columns["Id"]!.Width = 60;
                dgvMain.Columns["Name"]!.Width = 150;
                dgvMain.Columns["KontakPerson"]!.Width = 120;
                dgvMain.Columns["Phone"]!.Width = 120;
                dgvMain.Columns["Email"]!.Width = 150;
                dgvMain.Columns["Address"]!.Width = 150;
                dgvMain.Columns["Kota"]!.Width = 100;
            }
            catch
            {
                // If columns don't exist, continue with default display
            }

            pnlToolbar.Visible = isAdmin;
        }

        private void BtnTambah_Click(object? sender, EventArgs e)
        {
            using (var dialog = new EditSupplierDialog())
            {
                dialog.EditName = "";
                dialog.EditPhone = "";
                dialog.EditAddress = "";

                if (dialog.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    AddSupplierEvent?.Invoke(this, new Supplier
                    {
                        Name = dialog.EditName,
                        Phone = dialog.EditPhone,
                        Address = dialog.EditAddress,
                        KontakPerson = dialog.EditKontakPerson,
                        Email = dialog.EditEmail,
                        Kota = dialog.EditKota,
                        Status = dialog.EditStatus
                    });
                }
            }
        }

        private void BtnUbah_Click(object? sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null || dgvMain.CurrentRow.Cells["Id"].Value is not int id)
            {
                MessageBox.Show("Pilih baris supplier yang ingin diedit terlebih dahulu.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var supplierCtx = new SupplierContext();
            var currentSupplier = supplierCtx.GetById(id);
            string currentName = currentSupplier?.Name ?? "";
            string currentPhone = currentSupplier?.Phone ?? "";
            string currentAddress = currentSupplier?.Address ?? "";
            string currentKontakPerson = currentSupplier?.KontakPerson ?? "";
            string currentEmail = currentSupplier?.Email ?? "";
            string currentKota = currentSupplier?.Kota ?? "";
            string currentStatus = currentSupplier?.Status ?? "Aktif";

            using (var dialog = new EditSupplierDialog())
            {
                dialog.EditName = currentName;
                dialog.EditKontakPerson = currentKontakPerson;
                dialog.EditPhone = currentPhone;
                dialog.EditEmail = currentEmail;
                dialog.EditAddress = currentAddress;
                dialog.EditKota = currentKota;
                dialog.EditStatus = currentStatus;

                if (dialog.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    UpdateSupplierEvent?.Invoke(this, new Supplier
                    {
                        Id = id,
                        Name = dialog.EditName,
                        Phone = dialog.EditPhone,
                        Address = dialog.EditAddress,
                        KontakPerson = dialog.EditKontakPerson,
                        Email = dialog.EditEmail,
                        Kota = dialog.EditKota,
                        Status = dialog.EditStatus
                    });
                }
            }
        }

        private void BtnHapus_Click(object? sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null || dgvMain.CurrentRow.Cells["Id"].Value is not int id)
            {
                MessageBox.Show("Pilih baris supplier yang ingin dihapus terlebih dahulu.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var supplierToDelete = _suppliers.FirstOrDefault(s => s.Id == id);
            string supplierName = supplierToDelete?.Name ?? "supplier ini";

            var confirm = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus '{supplierName}'?",
                "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                DeleteSupplierEvent?.Invoke(this, id);
            }
        }
    }
}
