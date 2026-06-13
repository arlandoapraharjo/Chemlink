using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class AddProductForm : Form
    {
        public new string ProductName => txtNama.Text.Trim();
        public string CategoryName => cbKategori.SelectedItem is Category c ? c.Name : "";
        public int Stock => int.TryParse(txtStok.Text, out int s) ? s : 0;
        public decimal Price => decimal.TryParse(txtHarga.Text, out decimal p) ? p : 0m;

        public AddProductForm(List<Product> existingProducts, List<Category> categories)
        {
            InitializeComponent();
            cbKategori.DataSource = categories;
            cbKategori.DisplayMember = "Name";
            cbKategori.ValueMember = "Id";

            dgvReference.DataSource = null;
            dgvReference.Columns.Clear();
            dgvReference.DataSource = existingProducts;

            btnTambah.Click += BtnTambah_Click;
        }

        private void BtnTambah_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text) || cbKategori.SelectedItem == null ||
                !int.TryParse(txtStok.Text, out _) || !decimal.TryParse(txtHarga.Text, out _))
            {
                MessageBox.Show("Pastikan semua field sudah diisi dengan benar.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
