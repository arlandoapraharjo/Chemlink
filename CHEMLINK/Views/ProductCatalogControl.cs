using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class ProductCatalogControl : UserControl
    {
        public event EventHandler<ProductEventArgs>? AddProductEvent;
        public event EventHandler<ProductEventArgs>? EditProductEvent;
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
        }

        public void SetData(List<Product> products, bool isAdmin)
        {
            _products = products;
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = products;
            pnlToolbar.Visible = isAdmin;
        }

        public void SetCategories(List<Category> categories)
        {
            _categories = categories;
        }

        private void BtnTambah_Click(object? sender, EventArgs e)
        {
            using (var form = new AddProductForm(_products, _categories))
            {
                if (form.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    AddProductEvent?.Invoke(this, new ProductEventArgs
                    {
                        Name = form.ProductName,
                        Category = form.CategoryName,
                        Stock = form.Stock,
                        Price = form.Price
                    });
                }
            }
        }

        private void BtnEdit_Click(object? sender, EventArgs e)
        {
            Product? selected = dgvMain.CurrentRow?.DataBoundItem as Product;
            using (var form = new EditProductForm(_products, _categories, selected))
            {
                if (form.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    EditProductEvent?.Invoke(this, new ProductEventArgs
                    {
                        Id = form.ProductId,
                        Name = form.ProductName,
                        Category = form.CategoryName,
                        Stock = form.Stock,
                        Price = form.Price
                    });
                }
            }
        }

        private void BtnHapus_Click(object? sender, EventArgs e)
        {
            using (var form = new DeleteProductForm(_products))
            {
                if (form.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    DeleteProductEvent?.Invoke(this, form.SelectedProductId);
                }
            }
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
