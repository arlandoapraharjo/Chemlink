using System;
using System.Collections.Generic;
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

            btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            btnTambah.Click += BtnTambah_Click;
            pnlHeader.MouseDown += PnlHeader_MouseDown;
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

        // Drag support
        private System.Drawing.Point _dragStart;
        private void PnlHeader_MouseDown(object? sender, MouseEventArgs e)
        {
            _dragStart = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            // only drag from header
        }
    }
}
