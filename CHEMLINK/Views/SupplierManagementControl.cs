using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CHEMLINK.Models;

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

            // Atur ulang kolom untuk menampilkan data lengkap dari database
            try
            {
                dgvMain.Columns["Id"]!.HeaderText = "ID Supplier";
                dgvMain.Columns["Name"]!.HeaderText = "Nama Perusahaan";
                dgvMain.Columns["ContactPerson"]!.HeaderText = "Kontak Person";
                dgvMain.Columns["Phone"]!.HeaderText = "Nomor Telepon";
                dgvMain.Columns["Email"]!.HeaderText = "Email";
                dgvMain.Columns["Address"]!.HeaderText = "Alamat Supplier";
                dgvMain.Columns["City"]!.HeaderText = "Kota";
                dgvMain.Columns["Status"]!.HeaderText = "Status";

                // Atur lebar kolom
                dgvMain.Columns["Id"]!.Width = 50;
                dgvMain.Columns["Name"]!.Width = 120;
                dgvMain.Columns["ContactPerson"]!.Width = 120;
                dgvMain.Columns["Phone"]!.Width = 100;
                dgvMain.Columns["Email"]!.Width = 120;
                dgvMain.Columns["Address"]!.Width = 150;
                dgvMain.Columns["City"]!.Width = 100;
                dgvMain.Columns["Status"]!.Width = 70;
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
                        Kota = dialog.EditKota
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

            string currentName = dgvMain.CurrentRow.Cells["Name"].Value?.ToString() ?? "";
            string currentPhone = dgvMain.CurrentRow.Cells["Phone"].Value?.ToString() ?? "";
            string currentAddress = dgvMain.CurrentRow.Cells["Address"].Value?.ToString() ?? "";

            // Get existing detail fields from the supplier object
            var currentSupplier = _suppliers.FirstOrDefault(s => s.Id == id);
            string currentKontakPerson = currentSupplier?.KontakPerson ?? "";
            string currentEmail = currentSupplier?.Email ?? "";
            string currentKota = currentSupplier?.Kota ?? "";

            using (var dialog = new EditSupplierDialog())
            {
                dialog.EditName = currentName;
                dialog.EditKontakPerson = currentKontakPerson;
                dialog.EditPhone = currentPhone;
                dialog.EditEmail = currentEmail;
                dialog.EditAddress = currentAddress;
                dialog.EditKota = currentKota;

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
                        Kota = dialog.EditKota
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
