using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class ProductCatalogControl : UserControl
    {
        public event EventHandler<Product>? AddProductEvent;
        public event EventHandler<Product>? EditProductEvent;
        public event EventHandler<int>? DeleteProductEvent;

        private List<Category> _categories = new List<Category>();
        private List<Product> _products = new List<Product>();

        public ProductCatalogControl()
        {
            InitializeComponent();
            btnTambah.Click += BtnTambah_Click;
            btnEdit.Click += BtnEdit_Click;
            btnHapus.Click += BtnHapus_Click;
            btnKategori.Click += BtnKategori_Click;
            this.Paint += Control_Paint;
        }

        private void Control_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            using var pen = new Pen(Color.FromArgb(2, 44, 34), 2f);
            g.DrawRectangle(pen, dgvMain.Bounds);
            if (pnlToolbar.Visible)
                g.DrawRectangle(pen, pnlToolbar.Bounds);
        }

        public void SetData(List<Product> products, bool isAdmin)
        {
            _products = products;
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = products;

            // Hide internal FK columns
            if (dgvMain.Columns["CategoryId"] != null) dgvMain.Columns["CategoryId"]!.Visible = false;
            if (dgvMain.Columns["SupplierId"] != null) dgvMain.Columns["SupplierId"]!.Visible = false;

            pnlToolbar.Visible = isAdmin;
        }

        public void SetCategories(List<Category> categories)
        {
            _categories = categories;
        }

        private void BtnTambah_Click(object? sender, EventArgs e)
        {
            using (var form = new ProductForm(_products, _categories, null))
            {
                if (form.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    AddProductEvent?.Invoke(this, new Product
                    {
                        Name = form.ProductName,
                        Category = form.CategoryName,
                        Stock = form.Stock,
                        Price = form.Price,
                        Description = form.Description
                    });
                }
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            Product? selected = dgvMain.CurrentRow?.DataBoundItem as Product;
            using (var form = new ProductForm(_products, _categories, selected))
            {
                if (form.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    EditProductEvent?.Invoke(this, new Product
                    {
                        Id = form.ProductId,
                        Name = form.ProductName,
                        Category = form.CategoryName,
                        Stock = form.Stock,
                        Price = form.Price,
                        Description = form.Description
                    });
                }
            }
        }

        private void BtnHapus_Click(object? sender, EventArgs e)
        {
            if (dgvMain.CurrentRow?.DataBoundItem is not Product p)
            {
                MessageBox.Show("Pilih produk yang akan dihapus dari tabel.", "ChemLink Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var confirm = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus produk '{p.Name}'?",
                "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
                DeleteProductEvent?.Invoke(this, p.Id);
        }

        private void BtnKategori_Click(object? sender, EventArgs e)
        {
            // Category management is handled by the parent form/controller
            // We raise an event so the controller can open the ManageCategoryForm
            ManageCategoryEvent?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? ManageCategoryEvent;
    }
}
