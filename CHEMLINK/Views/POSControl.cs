using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class POSControl : UserControl
    {
        public event EventHandler<CartItemEventArgs>? AddCartEvent;
        public event EventHandler? CheckoutEvent;
        public event EventHandler<string>? SearchProductEvent;
        public event EventHandler<string>? FilterCategoryEvent;

        private List<Product> _searchResults = new List<Product>();

        public POSControl()
        {
            InitializeComponent();
            cbCategoryFilter.SelectedIndexChanged += CbCategoryFilter_SelectedIndexChanged;
            txtSearch.KeyDown += TxtSearch_KeyDown;
            btnAddCart.Click += BtnAddCart_Click;
            btnCheckout.Click += BtnCheckout_Click;
            btnDelCart.Click += BtnDelCart_Click;
            this.Paint += Control_Paint;
        }

        private void Control_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            using var pen = new Pen(Color.FromArgb(2, 44, 34), 2f);
            g.DrawRectangle(pen, pnlCrud.Bounds);
            g.DrawRectangle(pen, dgvMain.Bounds);
        }

        public void SetData(List<Product> searchResults, List<CartItem> cart)
        {
            _searchResults = searchResults;
            dgvMain.DataSource = null;
            dgvMain.DataSource = searchResults;

            try
            {
                dgvMain.Columns["Id"]!.HeaderText = "ID Produk";
                dgvMain.Columns["Name"]!.HeaderText = "Nama Produk";
                dgvMain.Columns["Category"]!.HeaderText = "Kategori";
                dgvMain.Columns["Price"]!.HeaderText = "Harga";
                dgvMain.Columns["Stock"]!.HeaderText = "Stok";

                // Sembunyikan kolom detail yang tidak diperlukan untuk transaksi POS
                if (dgvMain.Columns["Description"] != null) dgvMain.Columns["Description"]!.Visible = false;
                if (dgvMain.Columns["ExpiryDate"] != null) dgvMain.Columns["ExpiryDate"]!.Visible = false;
                if (dgvMain.Columns["SupplierName"] != null) dgvMain.Columns["SupplierName"]!.Visible = false;
                if (dgvMain.Columns["CategoryId"] != null) dgvMain.Columns["CategoryId"]!.Visible = false;
                if (dgvMain.Columns["SupplierId"] != null) dgvMain.Columns["SupplierId"]!.Visible = false;
            }
            catch
            {
                // Abaikan jika ada kolom yang hilang
            }

            dgvCart.DataSource = null;
            dgvCart.DataSource = new System.ComponentModel.BindingList<CartItem>(cart);

            try
            {
                dgvCart.Columns["ProductId"]!.HeaderText = "ID Produk";
                dgvCart.Columns["ProductName"]!.HeaderText = "Nama Produk";
                dgvCart.Columns["Qty"]!.HeaderText = "Kuantitas";
                dgvCart.Columns["Price"]!.HeaderText = "Harga Satuan";
                dgvCart.Columns["Total"]!.HeaderText = "Total";
            }
            catch
            {
                // Abaikan jika ada kolom yang hilang
            }

            // Only set SelectedIndex when there are items to select
            if (cbCategoryFilter.Items.Count > 0 && cbCategoryFilter.SelectedIndex == -1)
            {
                cbCategoryFilter.SelectedIndex = 0;
        }

        private void CbCategoryFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            FilterCategoryEvent?.Invoke(this, cbCategoryFilter.SelectedItem?.ToString() ?? "");
        }

        private void TxtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SearchProductEvent?.Invoke(this, txtSearch.Text);
            }
        }

        private void BtnAddCart_Click(object? sender, EventArgs e)
        {
            if (dgvMain.CurrentRow != null && int.TryParse(txtQty.Text, out int qty))
            {
                var cellValue = dgvMain.CurrentRow.Cells["Id"].Value;
                if (cellValue is int id)
                {
                    var prod = _searchResults.FirstOrDefault(p => p.Id == id);
                    if (prod != null)
                    {
                        AddCartEvent?.Invoke(this, new CartItemEventArgs { SelectedProduct = prod, Qty = qty });
                        txtQty.Clear();
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih produk dari tabel di bawah dan masukkan kuantitas yang benar!", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnCheckout_Click(object? sender, EventArgs e)
        {
            CheckoutEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
