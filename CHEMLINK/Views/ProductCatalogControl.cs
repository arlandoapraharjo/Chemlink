using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class ProductCatalogControl : UserControl
    {
        public event EventHandler<ProductEventArgs>? AddProductEvent;
        public event EventHandler<int>? DeleteProductEvent;

        public ProductCatalogControl()
        {
            InitializeComponent();
            btnTambah.Click += BtnTambah_Click;
            btnHapus.Click += BtnHapus_Click;
        }

        public void SetData(List<Product> products, bool isAdmin)
        {
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = products;
            pnlCrud.Visible = isAdmin;
        }

        private void BtnTambah_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) || !int.TryParse(txtStok.Text, out int stok) || !decimal.TryParse(txtHarga.Text, out decimal harga))
            {
                MessageBox.Show("Pastikan input data nama, stok, dan harga sudah diisi dengan benar.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddProductEvent?.Invoke(this, new ProductEventArgs { Name = txtNama.Text, Category = txtKategori.Text, Stock = stok, Price = harga });
        }

        private void BtnHapus_Click(object? sender, EventArgs e)
        {
            if (dgvMain.CurrentRow != null)
            {
                var cellValue = dgvMain.CurrentRow.Cells["Id"].Value;
                if (cellValue is int id)
                {
                    DeleteProductEvent?.Invoke(this, id);
                }
            }
        }
    }
}
