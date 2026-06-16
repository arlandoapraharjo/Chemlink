using System;
using System.Collections.Generic;
using System.Drawing;
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
                    AddProductEvent?.Invoke(this, new ProductEventArgs
                    {
                        Name = form.ProductName,
                        Category = form.CategoryName,
                        Stock = form.Stock,
                        Price = form.Price,
                        Description = form.Description,
                        ExpiryDate = form.ExpiryDate
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
                    EditProductEvent?.Invoke(this, new ProductEventArgs
                    {
                        Id = form.ProductId,
                        Name = form.ProductName,
                        Category = form.CategoryName,
                        Stock = form.Stock,
                        Price = form.Price,
                        Description = form.Description,
                        ExpiryDate = form.ExpiryDate
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

        public void SetData(List<Product> products, bool isAdmin)
        {
            _products = products;
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = new System.ComponentModel.BindingList<Product>(products);

            // Atur ulang kolom untuk menampilkan data lengkap dari database
            try
            {
                dgvMain.Columns["Id"]!.HeaderText = "ID Produk";
                dgvMain.Columns["Name"]!.HeaderText = "Nama Produk";
                dgvMain.Columns["Category"]!.HeaderText = "Kategori";
                dgvMain.Columns["Description"]!.HeaderText = "Keterangan";
                dgvMain.Columns["Price"]!.HeaderText = "Harga";
                dgvMain.Columns["Stock"]!.HeaderText = "Stok";
                dgvMain.Columns["ExpiryDate"]!.HeaderText = "Tanggal Expired";
                dgvMain.Columns["SupplierName"]!.HeaderText = "Supplier";

                // Sembunyikan kolom ID internal yang tidak perlu ditampilkan
                dgvMain.Columns["CategoryId"]!.Visible = false;
                dgvMain.Columns["SupplierId"]!.Visible = false;

                // Atur lebar kolom
                dgvMain.Columns["Id"]!.Width = 60;
                dgvMain.Columns["Name"]!.Width = 150;
                dgvMain.Columns["Category"]!.Width = 100;
                dgvMain.Columns["Description"]!.Width = 150;
                dgvMain.Columns["Price"]!.Width = 80;
                dgvMain.Columns["Stock"]!.Width = 60;
                dgvMain.Columns["ExpiryDate"]!.Width = 120;
                dgvMain.Columns["SupplierName"]!.Width = 120;
            }
            catch
            {
                // Jika kolom tidak ada, lanjutkan dengan default display
            }

            pnlToolbar.Visible = isAdmin;
        }

        public void EnsureComboBoxSelectedIndex(System.Windows.Forms.ComboBox combo)
        {
            if (combo == null) return;
            if (combo.Items.Count == 0)
            {
                combo.SelectedIndex = -1;
                return;
            }
            if (combo.SelectedIndex < 0 || combo.SelectedIndex >= combo.Items.Count)
            {
                combo.SelectedIndex = 0;
            }
        }
    }
}
