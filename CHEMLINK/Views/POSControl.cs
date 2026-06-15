using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class POSControl : UserControl
    {
        public event EventHandler<CartItemEventArgs>? AddCartEvent;
        public event EventHandler<int>? DeleteCartItemEvent;
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
        }

        public void SetData(List<Product> searchResults, List<CartItem> cart)
        {
            _searchResults = searchResults;
            dgvMain.DataSource = null;
            dgvMain.DataSource = new System.ComponentModel.BindingList<Product>(searchResults);

            dgvCart.DataSource = null;
            dgvCart.DataSource = new System.ComponentModel.BindingList<CartItem>(cart);

            // Only set SelectedIndex when there are items to select
            if (cbCategoryFilter.Items.Count > 0 && cbCategoryFilter.SelectedIndex == -1)
            {
                cbCategoryFilter.SelectedIndex = 0;
            }
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

        private void BtnDelCart_Click(object? sender, EventArgs e)
        {
            if (dgvCart.CurrentRow == null)
            {
                MessageBox.Show("Pilih item di keranjang yang ingin dihapus.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Handle common DataSource types: List<CartItem>, BindingList<CartItem>, non-generic IList, or BindingSource
            var cell = dgvCart.CurrentRow.Cells["ProductId"];
            int? productId = null;
            if (cell != null && cell.Value is int id) productId = id;

            // 1) List<T>
            if (dgvCart.DataSource is List<CartItem> list)
            {
                CartItem? toRemove = null;
                if (productId.HasValue)
                {
                    toRemove = list.FirstOrDefault(ci => ci.ProductId == productId.Value);
                }
                else
                {
                    var nameCell = dgvCart.CurrentRow.Cells["ProductName"];
                    string name = nameCell?.Value?.ToString() ?? "";
                    toRemove = list.FirstOrDefault(ci => ci.ProductName == name);
                }

                if (toRemove != null)
                {
                    list.Remove(toRemove);
                    dgvCart.DataSource = null;
                    dgvCart.DataSource = list;
                    DeleteCartItemEvent?.Invoke(this, toRemove.ProductId);
                }
                else
                {
                    MessageBox.Show("Gagal menemukan item untuk dihapus.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            // 2) BindingList<T>
            else if (dgvCart.DataSource is System.ComponentModel.BindingList<CartItem> blist)
            {
                CartItem? toRemove = null;
                if (productId.HasValue)
                {
                    toRemove = blist.FirstOrDefault(ci => ci.ProductId == productId.Value);
                }
                else
                {
                    var nameCell = dgvCart.CurrentRow.Cells["ProductName"];
                    string name = nameCell?.Value?.ToString() ?? "";
                    toRemove = blist.FirstOrDefault(ci => ci.ProductName == name);
                }

                if (toRemove != null)
                {
                    blist.Remove(toRemove);
                    // data-binding will update automatically
                    DeleteCartItemEvent?.Invoke(this, toRemove.ProductId);
                }
                else
                {
                    MessageBox.Show("Gagal menemukan item untuk dihapus.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            // 3) non-generic IList (covers other binding types)
            else if (dgvCart.DataSource is System.Collections.IList ilist)
            {
                CartItem? toRemove = null;
                if (productId.HasValue)
                {
                    foreach (var obj in ilist)
                    {
                        if (obj is CartItem ci2 && ci2.ProductId == productId.Value)
                        {
                            toRemove = ci2;
                            break;
                        }
                    }
                }
                else
                {
                    var nameCell = dgvCart.CurrentRow.Cells["ProductName"];
                    string name = nameCell?.Value?.ToString() ?? "";
                    foreach (var obj in ilist)
                    {
                        if (obj is CartItem ci2 && ci2.ProductName == name)
                        {
                            toRemove = ci2;
                            break;
                        }
                    }
                }

                if (toRemove != null)
                {
                    ilist.Remove(toRemove);
                    dgvCart.DataSource = null;
                    dgvCart.DataSource = ilist;
                    DeleteCartItemEvent?.Invoke(this, toRemove.ProductId);
                }
                else
                {
                    MessageBox.Show("Gagal menemukan item untuk dihapus.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // If DataSource is a BindingSource
                if (dgvCart.DataSource is BindingSource bs && bs.Current is CartItem ci)
                {
                    bs.RemoveCurrent();
                    DeleteCartItemEvent?.Invoke(this, ci.ProductId);
                }
                else
                {
                    MessageBox.Show("Keranjang tidak dapat dimodifikasi saat ini.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
