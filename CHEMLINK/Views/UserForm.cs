using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class UserForm : Form
    {
        private readonly bool _isEditMode;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Username
        {
            get => txtUsername.Text.Trim();
            set => txtUsername.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Password
        {
            get => txtPassword.Text;
            set => txtPassword.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Role
        {
            get => cbRole.SelectedItem?.ToString() ?? "Kasir";
            set
            {
                int idx = cbRole.Items.IndexOf(value);
                cbRole.SelectedIndex = idx >= 0 ? idx : 1;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FullName
        {
            get => txtFullName.Text.Trim();
            set => txtFullName.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Status
        {
            get => cbStatus.SelectedIndex == 0; // 0 = "Active" = true
            set => cbStatus.SelectedIndex = value ? 0 : 1;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Alamat
        {
            get => txtAlamat.Text.Trim();
            set => txtAlamat.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string NoTelp
        {
            get => txtNoTelp.Text.Trim();
            set => txtNoTelp.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Email
        {
            get => txtEmail.Text.Trim();
            set => txtEmail.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Kecamatan
        {
            get => cbKecamatan.SelectedItem?.ToString() ?? "";
            set
            {
                int idx = cbKecamatan.Items.IndexOf(value);
                if (idx >= 0) cbKecamatan.SelectedIndex = idx;
            }
        }

        /// <summary>
        /// Unified Add/Edit user form.
        /// Pass existingUsers for Add mode (shows reference grid).
        /// Pass null for Edit mode (hides grid, pre-fill via properties).
        /// </summary>
        public UserForm(List<User>? existingUsers = null)
        {
            InitializeComponent();

            _isEditMode = existingUsers == null;

            // Header & button text
            lblTitle.Text = _isEditMode ? "Edit Akun Operator" : "Tambah User Baru";
            btnSubmit.Text = _isEditMode ? "Simpan" : "Tambah";
            this.Text = _isEditMode ? "Edit User" : "Tambah User";

            // Default role & status
            cbRole.SelectedIndex = 1; // "Kasir"
            cbStatus.SelectedIndex = 0; // "Active"

            // Populate kecamatan Jember
            cbKecamatan.Items.AddRange(new object[]
            {
                "Ajung", "Ambulu", "Arjasa", "Balung", "Bangsalsari", "Gumukmas", "Jelbuk",
                "Jenggawah", "Jombang", "Kalisat", "Kaliwates", "Kencong",
                "Ledokombo", "Mayang", "Mumbulsari", "Pakusari", "Panti",
                "Patrang", "Puger", "Rambipuji", "Semboro", "Silo",
                "Sukorambi", "Sukowono", "Sumberbaru", "Sumberjambe", "Sumbersari",
                "Tanggul", "Tempurejo", "Umbulsari", "Wuluhan"
            });

            if (!_isEditMode && existingUsers != null)
            {
                // Add mode: show reference grid
                dgvReference.DataSource = null;
                dgvReference.Columns.Clear();
                dgvReference.DataSource = existingUsers;

                try
                {
                    dgvReference.Columns["Id"]!.HeaderText = "ID User";
                    dgvReference.Columns["Username"]!.HeaderText = "Username";
                    dgvReference.Columns["Role"]!.HeaderText = "Role";
                    dgvReference.Columns["FullName"]!.HeaderText = "Nama Lengkap";
                    dgvReference.Columns["Status"]!.HeaderText = "Status";

                    // Sembunyikan detail sensitif atau panjang
                    if (dgvReference.Columns["Password"] != null) dgvReference.Columns["Password"]!.Visible = false;
                    if (dgvReference.Columns["Status"] != null) dgvReference.Columns["Status"]!.Visible = false;
                    if (dgvReference.Columns["Alamat"] != null) dgvReference.Columns["Alamat"]!.Visible = false;
                    if (dgvReference.Columns["NoTelp"] != null) dgvReference.Columns["NoTelp"]!.Visible = false;
                    if (dgvReference.Columns["Email"] != null) dgvReference.Columns["Email"]!.Visible = false;
                    if (dgvReference.Columns["Kecamatan"] != null) dgvReference.Columns["Kecamatan"]!.Visible = false;
                }
                catch
                {
                    // Lanjutkan jika ada kolom yang tidak ditemukan
                }
            }
            else
            {
                // Edit mode: hide the reference grid, give left panel more room visually
                pnlRight.Visible = false;
                pnlLeft.Dock = DockStyle.Fill;
                pnlLeft.Width = 800; // fill entire width
            }

            btnSubmit.Click += BtnSubmit_Click;
        }

        private void BtnSubmit_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username tidak boleh kosong.", "ChemLink Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!_isEditMode && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username dan Password wajib diisi untuk user baru.", "ChemLink Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pnlHeader_Paint(object? sender, PaintEventArgs e)
        {
            if (pnlHeader.ClientRectangle.Width <= 0 || pnlHeader.ClientRectangle.Height <= 0) return;
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
