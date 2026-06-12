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
        }

        public void SetData(List<Product> searchResults, List<CartItem> cart)
        {
            _searchResults = searchResults;
            dgvMain.DataSource = null;
            dgvMain.DataSource = searchResults;

            dgvCart.DataSource = null;
            dgvCart.DataSource = cart;
            
            if (cbCategoryFilter.SelectedIndex == -1)
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
