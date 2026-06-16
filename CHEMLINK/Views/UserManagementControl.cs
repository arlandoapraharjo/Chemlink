using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class UserManagementControl : UserControl
    {
        public event EventHandler<UserEventArgs>? AddUserEvent;
        public event EventHandler<UserEventArgs>? UpdateUserEvent;
        public event EventHandler<int>? DeleteUserEvent;

        private List<User> _users = new();

        public UserManagementControl()
        {
            InitializeComponent();
            btnTambah.Click += BtnTambah_Click;
            btnUbah.Click += BtnUbah_Click;
            btnHapus.Click += BtnHapus_Click;
        }

        public void SetData(List<User> users, bool isAdmin)
        {
            _users = users;
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = new System.ComponentModel.BindingList<User>(users);

            // Atur ulang kolom untuk menampilkan data lengkap dari database
            try
            {
                dgvMain.Columns["Id"]!.HeaderText = "ID User";
                dgvMain.Columns["Username"]!.HeaderText = "Username";
                dgvMain.Columns["FullName"]!.HeaderText = "Nama Lengkap";
                dgvMain.Columns["Role"]!.HeaderText = "Role";
                dgvMain.Columns["Phone"]!.HeaderText = "Nomor Telepon";
                dgvMain.Columns["Email"]!.HeaderText = "Email";
                dgvMain.Columns["Address"]!.HeaderText = "Alamat";
                dgvMain.Columns["District"]!.HeaderText = "Kecamatan";
                dgvMain.Columns["Status"]!.HeaderText = "Status";

                // Sembunyikan password karena tidak perlu ditampilkan
                dgvMain.Columns["Password"]!.Visible = false;

                // Atur lebar kolom
                dgvMain.Columns["Id"]!.Width = 50;
                dgvMain.Columns["Username"]!.Width = 80;
                dgvMain.Columns["FullName"]!.Width = 120;
                dgvMain.Columns["Role"]!.Width = 60;
                dgvMain.Columns["Phone"]!.Width = 100;
                dgvMain.Columns["Email"]!.Width = 120;
                dgvMain.Columns["Address"]!.Width = 150;
                dgvMain.Columns["District"]!.Width = 100;
                dgvMain.Columns["Status"]!.Width = 70;
            }
            catch
            {
                // Jika kolom tidak ada, lanjutkan dengan default display
            }

            pnlActions.Visible = isAdmin;
        }

        private void BtnTambah_Click(object? sender, EventArgs e)
        {
            using (var form = new AddUserForm(_users))
            {
                if (form.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    AddUserEvent?.Invoke(this, new UserEventArgs
                    {
                        Username = form.NewUsername,
                        Password = form.NewPassword,
                        Role = form.NewRole
                    });
                }
            }
        }

        private void BtnUbah_Click(object? sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null || dgvMain.CurrentRow.Cells["Id"].Value is not int id)
            {
                MessageBox.Show("Pilih baris user yang ingin diedit terlebih dahulu.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string currentUsername = dgvMain.CurrentRow.Cells["Username"].Value?.ToString() ?? "";
            string currentRole = dgvMain.CurrentRow.Cells["Role"].Value?.ToString() ?? "Kasir";

            using (var dialog = new EditUserDialog())
            {
                dialog.EditUsername = currentUsername;
                dialog.EditPassword = "";
                dialog.EditRole = currentRole;

                if (dialog.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    UpdateUserEvent?.Invoke(this, new UserEventArgs
                    {
                        Id = id,
                        Username = dialog.EditUsername,
                        Password = dialog.EditPassword,
                        Role = dialog.EditRole
                    });
                }
            }
        }

        private void BtnHapus_Click(object? sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null || dgvMain.CurrentRow.Cells["Id"].Value is not int id)
            {
                MessageBox.Show("Pilih baris user yang ingin dihapus terlebih dahulu.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Find the user object
            var userToDelete = _users.FirstOrDefault(u => u.Id == id);
            if (userToDelete == null)
            {
                MessageBox.Show("Data user tidak ditemukan.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new DeleteUserForm(userToDelete, _users))
            {
                if (form.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    DeleteUserEvent?.Invoke(this, id);
                }
            }
        }
    }
}
