using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class ManageCategoryForm : Form
    {
        public event EventHandler<Category>? AddCategoryEvent;
        public event EventHandler<Category>? UpdateCategoryEvent;
        public event EventHandler<int>? DeleteCategoryEvent;

        public ManageCategoryForm(List<Category> categories)
        {
            InitializeComponent();
            LoadCategories(categories);

            btnTambah.Click += BtnTambah_Click;
            btnUbah.Click += BtnUbah_Click;
            btnHapus.Click += BtnHapus_Click;
            dgvCategories.SelectionChanged += DgvCategories_SelectionChanged;
        }

        public void LoadCategories(List<Category> categories)
        {
            dgvCategories.DataSource = null;
            dgvCategories.Columns.Clear();
            dgvCategories.DataSource = categories;
            txtNama.Clear();
        }

        private void DgvCategories_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow?.DataBoundItem is Category cat)
            {
                txtNama.Text = cat.Name;
            }
        }

        private void BtnTambah_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Nama kategori tidak boleh kosong.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddCategoryEvent?.Invoke(this, new Category { Name = txtNama.Text.Trim() });
        }

        private void BtnUbah_Click(object? sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow?.DataBoundItem is Category cat)
            {
                if (string.IsNullOrWhiteSpace(txtNama.Text))
                {
                    MessageBox.Show("Nama kategori tidak boleh kosong.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                UpdateCategoryEvent?.Invoke(this, new Category { Id = cat.Id, Name = txtNama.Text.Trim() });
            }
            else
            {
                MessageBox.Show("Pilih kategori yang akan diubah.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnHapus_Click(object? sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow?.DataBoundItem is Category cat)
            {
                var confirm = MessageBox.Show(
                    $"Apakah Anda yakin ingin menghapus kategori '{cat.Name}'?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (confirm == DialogResult.Yes)
                {
                    DeleteCategoryEvent?.Invoke(this, cat.Id);
                }
            }
            else
            {
                MessageBox.Show("Pilih kategori yang akan dihapus.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
