using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class UserManagementControl : UserControl
    {
        public event EventHandler<UserEventArgs>? AddUserEvent;
        public event EventHandler<UserEventArgs>? UpdateUserEvent;
        public event EventHandler<int>? DeleteUserEvent;

        private bool _isPasswordMaskVisible = false;

        public UserManagementControl()
        {
            InitializeComponent();
            btnTambah.Click += BtnTambah_Click;
            btnUbah.Click += BtnUbah_Click;
            btnHapus.Click += BtnHapus_Click;
            btnTogglePass.Click += BtnTogglePass_Click;
            dgvMain.CellFormatting += DgvMain_CellFormatting_PasswordMask;
        }

        public void SetData(List<User> users, bool isAdmin)
        {
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = users;
            pnlCrud.Visible = isAdmin;
            cbRole.SelectedIndex = 1;
        }

        private void BtnTambah_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Username dan Password wajib diisi untuk user baru.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddUserEvent?.Invoke(this, new UserEventArgs { Username = txtUser.Text, Password = txtPass.Text, Role = cbRole.SelectedItem?.ToString() ?? "Kasir" });
        }

        private void BtnUbah_Click(object? sender, EventArgs e)
        {
            if (dgvMain.CurrentRow != null && dgvMain.CurrentRow.Cells["Id"].Value is int id)
            {
                if (string.IsNullOrWhiteSpace(txtUser.Text))
                {
                    MessageBox.Show("Username wajib diisi saat edit.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                UpdateUserEvent?.Invoke(this, new UserEventArgs { Id = id, Username = txtUser.Text, Password = txtPass.Text, Role = cbRole.SelectedItem?.ToString() ?? "Kasir" });
            }
        }

        private void BtnHapus_Click(object? sender, EventArgs e)
        {
            if (dgvMain.CurrentRow != null && dgvMain.CurrentRow.Cells["Id"].Value is int id)
            {
                DeleteUserEvent?.Invoke(this, id);
            }
        }

        private void BtnTogglePass_Click(object? sender, EventArgs e)
        {
            _isPasswordMaskVisible = !_isPasswordMaskVisible;
            dgvMain.Refresh();
        }

        private void DgvMain_CellFormatting_PasswordMask(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvMain.Columns[e.ColumnIndex].Name == "Password" && e.Value != null)
            {
                if (!_isPasswordMaskVisible)
                {
                    e.Value = new string('*', 8);
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
