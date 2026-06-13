using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class EditProductForm : Form
    {
        public int ProductId { get; private set; }
        public new string ProductName => txtNama.Text.Trim();
        public string CategoryName => cbKategori.SelectedItem is Category c ? c.Name : "";
        public int CategoryId => cbKategori.SelectedItem is Category c ? c.Id : 0;
        public int Stock => int.TryParse(txtStok.Text, out int s) ? s : 0;
        public decimal Price => decimal.TryParse(txtHarga.Text, out decimal p) ? p : 0m;

        public EditProductForm(List<Product> existingProducts, List<Category> categories, Product? selectedProduct)
        {
            InitializeComponent();
            cbKategori.DataSource = categories;
            cbKategori.DisplayMember = "Name";
            cbKategori.ValueMember = "Id";

            dgvReference.DataSource = null;
            dgvReference.Columns.Clear();
            dgvReference.DataSource = existingProducts;

            dgvReference.SelectionChanged += DgvReference_SelectionChanged;

            if (selectedProduct != null)
            {
                ProductId = selectedProduct.Id;
                txtNama.Text = selectedProduct.Name;
                txtStok.Text = selectedProduct.Stock.ToString();
                txtHarga.Text = selectedProduct.Price.ToString();
                // Select matching category
                foreach (var cat in categories)
                {
                    if (cat.Name == selectedProduct.Category)
                    {
                        cbKategori.SelectedItem = cat;
                        break;
                    }
                }
            }

            btnSimpan.Click += BtnSimpan_Click;
        }

        private void DgvReference_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvReference.CurrentRow?.DataBoundItem is Product p)
            {
                ProductId = p.Id;
                txtNama.Text = p.Name;
                txtStok.Text = p.Stock.ToString();
                txtHarga.Text = p.Price.ToString();
                foreach (var cat in (cbKategori.DataSource as List<Category>) ?? new List<Category>())
                {
                    if (cat.Name == p.Category)
                    {
                        cbKategori.SelectedItem = cat;
                        break;
                    }
                }
            }
        }

        private void BtnSimpan_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) || cbKategori.SelectedItem == null ||
                !int.TryParse(txtStok.Text, out _) || !decimal.TryParse(txtHarga.Text, out _))
            {
                MessageBox.Show("Pastikan semua field sudah diisi dengan benar.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ProductId == 0)
            {
                MessageBox.Show("Pilih produk yang akan diedit dari tabel.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pnlHeader_Paint(object? sender, PaintEventArgs e)
        {
            using (var brush = new LinearGradientBrush(
                pnlHeader.ClientRectangle,
                Color.FromArgb(37, 103, 30),   // #25671E
                Color.FromArgb(72, 161, 17),   // #48A111
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, pnlHeader.ClientRectangle);
            }
        }
    }
}
