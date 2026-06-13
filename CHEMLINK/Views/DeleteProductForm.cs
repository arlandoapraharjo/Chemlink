using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class DeleteProductForm : Form
    {
        public int SelectedProductId { get; private set; }

        public DeleteProductForm(List<Product> existingProducts)
        {
            InitializeComponent();
            dgvProducts.DataSource = null;
            dgvProducts.Columns.Clear();
            dgvProducts.DataSource = existingProducts;

            btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            btnHapus.Click += BtnHapus_Click;
        }

        private void BtnHapus_Click(object? sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow?.DataBoundItem is Product p)
            {
                SelectedProductId = p.Id;
                var confirm = MessageBox.Show(
                    $"Apakah Anda yakin ingin menghapus produk '{p.Name}'?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Pilih produk yang akan dihapus dari tabel.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
