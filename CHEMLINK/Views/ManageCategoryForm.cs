using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class ManageCategoryForm : Form
    {
        public event EventHandler<CategoryEventArgs>? AddCategoryEvent;
        public event EventHandler<CategoryEventArgs>? UpdateCategoryEvent;
        public event EventHandler<int>? DeleteCategoryEvent;

        public ManageCategoryForm(List<Category> categories)
        {
            InitializeComponent();
            LoadCategories(categories);

            btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
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
            AddCategoryEvent?.Invoke(this, new CategoryEventArgs { Name = txtNama.Text.Trim() });
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
                UpdateCategoryEvent?.Invoke(this, new CategoryEventArgs { Id = cat.Id, Name = txtNama.Text.Trim() });
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
    }
}
