using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class SupplierManagementControl : UserControl
    {
        public event EventHandler<SupplierEventArgs>? AddSupplierEvent;
        public event EventHandler<SupplierEventArgs>? UpdateSupplierEvent;
        public event EventHandler<int>? DeleteSupplierEvent;

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
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = suppliers;
        }

        private void BtnAddSup_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaSup.Text)) return;
            // Validate phone contains only digits (if provided)
            if (!string.IsNullOrWhiteSpace(txtTelp.Text))
            {
                foreach (char c in txtTelp.Text)
                {
                    if (!char.IsDigit(c))
                    {
                        MessageBox.Show("Nomor telepon harus berupa angka dan tanpa simbol!", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            AddSupplierEvent?.Invoke(this, new SupplierEventArgs { Name = txtNamaSup.Text, Phone = txtTelp.Text, Address = txtAlamat.Text });
            txtNamaSup.Clear(); txtTelp.Clear(); txtAlamat.Clear();
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

            using (var dialog = new EditSupplierDialog())
            {
                dialog.EditName = currentName;
                dialog.EditPhone = currentPhone;
                dialog.EditAddress = currentAddress;

                if (dialog.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    // Validate phone on edit
                    if (!string.IsNullOrWhiteSpace(dialog.EditPhone))
                    {
                        bool ok = true;
                        foreach (char c in dialog.EditPhone)
                        {
                            if (!char.IsDigit(c)) { ok = false; break; }
                        }
                        if (!ok)
                        {
                            MessageBox.Show("Nomor telepon harus berupa angka dan tanpa simbol!", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    UpdateSupplierEvent?.Invoke(this, new SupplierEventArgs
                    {
                        Id = id,
                        Name = dialog.EditName,
                        Phone = dialog.EditPhone,
                        Address = dialog.EditAddress
                    });
                }
            }
        }

        private void BtnDelSup1_Click(object sender, EventArgs e)
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
