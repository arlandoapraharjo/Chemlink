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
            dgvMain.DataSource = suppliers;
        }

        private void BtnAddSup_Click(object? sender, EventArgs e)
        {
            using (var form = new AddSupplierForm())
            {
                if (form.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    AddSupplierEvent?.Invoke(this, new SupplierEventArgs
                    {
                        Name = form.NewName,
                        Phone = form.NewPhone,
                        Address = form.NewAddress,
                        KontakPerson = form.NewKontakPerson,
                        Email = form.NewEmail,
                        Kota = form.NewKota
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
