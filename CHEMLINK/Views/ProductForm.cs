using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class ProductForm : Form
    {
        private readonly bool _isEditMode;

        public int ProductId { get; private set; }
        public new string ProductName => txtNama.Text.Trim();
        public string CategoryName => cbKategori.SelectedItem is Category c ? c.Name : "";
        public int CategoryId => cbKategori.SelectedItem is Category c ? c.Id : 0;
        public string SupplierName => cbSupplier.SelectedItem is Supplier s ? s.Name : "";
        public int SupplierId => cbSupplier.SelectedItem is Supplier s ? s.Id : 0;
        public int Stock => int.TryParse(txtStok.Text, out int s) ? s : 0;
        public decimal Price => decimal.TryParse(txtHarga.Text, out decimal p) ? p : 0m;
        public string Description => txtKeterangan.Text.Trim();

        /// <summary>
        /// Unified Add/Edit product form.
        /// Pass selectedProduct = null for Add mode, or a Product for Edit mode.
        /// </summary>
        public ProductForm(List<Product> existingProducts, List<Category> categories, List<Supplier> suppliers, Product? selectedProduct)
        {
            InitializeComponent();

            _isEditMode = selectedProduct != null;

            // Header & button text
            lblTitle.Text = _isEditMode ? "Edit Obat" : "Tambah Obat Baru";
            btnSubmit.Text = _isEditMode ? "Simpan" : "Tambah";
            this.Text = _isEditMode ? "Edit Obat" : "Tambah Obat";

            // Category dropdown
            cbKategori.DataSource = categories;
            cbKategori.DisplayMember = "Name";
            cbKategori.ValueMember = "Id";

            // Supplier dropdown
            cbSupplier.DataSource = suppliers;
            cbSupplier.DisplayMember = "Name";
            cbSupplier.ValueMember = "Id";

            // Reference grid
            dgvReference.DataSource = null;
            dgvReference.Columns.Clear();
            dgvReference.DataSource = existingProducts;

            try
            {
                dgvReference.Columns["Id"]!.HeaderText = "ID Produk";
                dgvReference.Columns["Name"]!.HeaderText = "Nama Produk";
                dgvReference.Columns["Category"]!.HeaderText = "Kategori";
                dgvReference.Columns["Price"]!.HeaderText = "Harga";
                dgvReference.Columns["Stock"]!.HeaderText = "Stok";

                // Sembunyikan detail lainnya yang tidak diperlukan untuk referensi cepat
                if (dgvReference.Columns["Description"] != null) dgvReference.Columns["Description"]!.Visible = false;
                if (dgvReference.Columns["SupplierName"] != null) dgvReference.Columns["SupplierName"]!.Visible = false;
                if (dgvReference.Columns["CategoryId"] != null) dgvReference.Columns["CategoryId"]!.Visible = false;
                if (dgvReference.Columns["SupplierId"] != null) dgvReference.Columns["SupplierId"]!.Visible = false;
            }
            catch
            {
                // Lanjutkan jika ada kolom yang tidak ditemukan
            }

            // Edit mode: pre-fill fields + wire grid selection
            if (_isEditMode && selectedProduct != null)
            {
                ProductId = selectedProduct.Id;
                txtNama.Text = selectedProduct.Name;
                txtStok.Text = selectedProduct.Stock.ToString();
                txtHarga.Text = selectedProduct.Price.ToString();
                txtKeterangan.Text = selectedProduct.Description;

                foreach (var cat in categories)
                {
                    if (cat.Name == selectedProduct.Category)
                    {
                        cbKategori.SelectedItem = cat;
                        break;
                    }
                }

                foreach (var sup in suppliers)
                {
                    if (sup.Name == selectedProduct.SupplierName)
                    {
                        cbSupplier.SelectedItem = sup;
                        break;
                    }
                }

                dgvReference.SelectionChanged += DgvReference_SelectionChanged;
            }

            btnSubmit.Click += BtnSubmit_Click;
        }

        private void DgvReference_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvReference.CurrentRow?.DataBoundItem is Product p)
            {
                ProductId = p.Id;
                txtNama.Text = p.Name;
                txtStok.Text = p.Stock.ToString();
                txtHarga.Text = p.Price.ToString();
                txtKeterangan.Text = p.Description;
                foreach (var cat in (cbKategori.DataSource as List<Category>) ?? new List<Category>())
                {
                    if (cat.Name == p.Category)
                    {
                        cbKategori.SelectedItem = cat;
                        break;
                    }
                }
                foreach (var sup in (cbSupplier.DataSource as List<Supplier>) ?? new List<Supplier>())
                {
                    if (sup.Name == p.SupplierName)
                    {
                        cbSupplier.SelectedItem = sup;
                        break;
                    }
                }
            }
        }

        private void BtnSubmit_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) || cbKategori.SelectedItem == null ||
                !int.TryParse(txtStok.Text, out _) || !decimal.TryParse(txtHarga.Text, out _))
            {
                MessageBox.Show("Pastikan semua field sudah diisi dengan benar.", "ChemLink Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_isEditMode && ProductId == 0)
            {
                MessageBox.Show("Pilih produk yang akan diedit dari tabel.", "ChemLink Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pnlHeader_Paint(object? sender, PaintEventArgs e)
        {
            using (var brush = new LinearGradientBrush(
                pnlHeader.ClientRectangle,
                Color.FromArgb(37, 103, 30),
                Color.FromArgb(72, 161, 17),
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, pnlHeader.ClientRectangle);
            }
        }
    }
}
