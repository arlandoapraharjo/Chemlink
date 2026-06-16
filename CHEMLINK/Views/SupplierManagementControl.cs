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
        public event EventHandler<SupplierEventArgs>? AddSupplierEvent;
        public event EventHandler<SupplierEventArgs>? UpdateSupplierEvent;
        public event EventHandler<int>? DeleteSupplierEvent;

        private List<Supplier> _suppliers = new();

        public SupplierManagementControl()
        {
            InitializeComponent();
            btnAddSup.Click += BtnAddSup_Click;
            btnEditSup1.Click += BtnEditSup1_Click;
            btnDelSup1.Click += BtnDelSup1_Click;
            this.Paint += Control_Paint;
        }

        private void Control_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            using var pen = new Pen(Color.FromArgb(2, 44, 34), 2f);
            g.DrawRectangle(pen, dgvMain.Bounds);
            g.DrawRectangle(pen, pnlCrud.Bounds);
        }

        public void SetData(List<Supplier> suppliers)
        {
            _suppliers = suppliers;
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = new BindingList<Supplier>(suppliers);

            // Atur ulang kolom untuk menampilkan semua data dari Supplier
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

                // Atur lebar kolom
                dgvMain.Columns["Id"]!.Width = 60;
                dgvMain.Columns["Name"]!.Width = 150;
                dgvMain.Columns["KontakPerson"]!.Width = 120;
                dgvMain.Columns["Phone"]!.Width = 120;
                dgvMain.Columns["Email"]!.Width = 150;
                dgvMain.Columns["Address"]!.Width = 150;
                dgvMain.Columns["Kota"]!.Width = 100;
                dgvMain.Columns["Status"]!.Width = 80;
            }
            catch
            {
                // Jika kolom tidak ada, lanjutkan dengan default display
            }
        }

        private void BtnAddSup_Click(object? sender, EventArgs e)
        {
            using (var dialog = new EditSupplierDialog())
            {
                // empty by default for new supplier
                dialog.EditName = "";
                dialog.EditPhone = "";
                dialog.EditAddress = "";

                if (dialog.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    // Validate phone contains only digits (if provided)
                    if (!string.IsNullOrWhiteSpace(dialog.EditPhone))
                    {
                        foreach (char c in dialog.EditPhone)
                        {
                            if (!char.IsDigit(c))
                            {
                                MessageBox.Show("Nomor telepon harus berupa angka dan tanpa simbol!", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    AddSupplierEvent?.Invoke(this, new SupplierEventArgs
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

        private void BtnEditSup1_Click(object? sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null || dgvMain.CurrentRow.Cells["Id"].Value is not int id)
            {
                MessageBox.Show("Pilih baris supplier yang ingin diedit terlebih dahulu.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get detailed supplier data from the database
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
                    UpdateSupplierEvent?.Invoke(this, new SupplierEventArgs
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

        private void BtnDelSup1_Click(object? sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null || dgvMain.CurrentRow.Cells["Id"].Value is not int id)
            {
                MessageBox.Show("Pilih baris supplier yang ingin dihapus terlebih dahulu.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dr = MessageBox.Show("Apakah Anda yakin ingin menghapus supplier ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                DeleteSupplierEvent?.Invoke(this, id);
            }
        }
    }
}
