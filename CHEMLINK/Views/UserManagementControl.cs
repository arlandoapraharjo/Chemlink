using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class UserManagementControl : UserControl
    {
        public event EventHandler<User>? AddUserEvent;
        public event EventHandler<User>? UpdateUserEvent;
        public event EventHandler<int>? DeleteUserEvent;

        private List<User> _users = new();

        public UserManagementControl()
        {
            InitializeComponent();
            btnTambah.Click += BtnTambah_Click;
            btnUbah.Click += BtnUbah_Click;
            btnHapus.Click += BtnHapus_Click;
            this.Paint += Control_Paint;
        }

        private void Control_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            using var pen = new Pen(Color.FromArgb(2, 44, 34), 2f);
            g.DrawRectangle(pen, pnlGrid.Bounds);
            if (pnlToolbar.Visible)
                g.DrawRectangle(pen, pnlToolbar.Bounds);
        }

        public void SetData(List<User> users, bool isAdmin)
        {
            _users = users;
            dgvMain.DataSource = null;
            dgvMain.Columns.Clear();
            dgvMain.DataSource = users;

            try
            {
                dgvMain.Columns["Id"]!.HeaderText = "ID User";
                dgvMain.Columns["Username"]!.HeaderText = "Username";
                dgvMain.Columns["Role"]!.HeaderText = "Role";
                dgvMain.Columns["FullName"]!.HeaderText = "Nama Lengkap";
                dgvMain.Columns["Status"]!.HeaderText = "Status";
                dgvMain.Columns["Status"]!.Visible = false;
                dgvMain.Columns["Alamat"]!.HeaderText = "Alamat";
                dgvMain.Columns["NoTelp"]!.HeaderText = "Nomor Telepon";
                dgvMain.Columns["Email"]!.HeaderText = "Email";
                dgvMain.Columns["Kecamatan"]!.HeaderText = "Kecamatan";

                // Sembunyikan kolom password
                if (dgvMain.Columns["Password"] != null) dgvMain.Columns["Password"]!.Visible = false;
            }
            catch
            {
                // Jika terjadi kesalahan, abaikan dan gunakan layout bawaan
            }

            pnlToolbar.Visible = isAdmin;
        }

        private void BtnTambah_Click(object? sender, EventArgs e)
        {
            using (var form = new UserForm(_users))
            {
                if (form.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    AddUserEvent?.Invoke(this, new User
                    {
                        Username = form.Username,
                        Password = form.Password,
                        Role = form.Role,
                        FullName = form.FullName,
                        Status = form.Status,
                        Alamat = form.Alamat,
                        NoTelp = form.NoTelp,
                        Email = form.Email,
                        Kecamatan = form.Kecamatan
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
            string currentFullName = dgvMain.CurrentRow.Cells["FullName"].Value?.ToString() ?? "";
            string currentStatus = dgvMain.CurrentRow.Cells["Status"].Value?.ToString() ?? "Active";
            string currentAlamat = dgvMain.CurrentRow.Cells["Alamat"].Value?.ToString() ?? "";
            string currentNoTelp = dgvMain.CurrentRow.Cells["NoTelp"].Value?.ToString() ?? "";
            string currentEmail = dgvMain.CurrentRow.Cells["Email"].Value?.ToString() ?? "";
            string currentKecamatan = dgvMain.CurrentRow.Cells["Kecamatan"].Value?.ToString() ?? "";

            using (var dialog = new UserForm())
            {
                dialog.Username = currentUsername;
                dialog.Password = "";
                dialog.Role = currentRole;
                dialog.FullName = currentFullName;
                dialog.Status = currentStatus;
                dialog.Alamat = currentAlamat;
                dialog.NoTelp = currentNoTelp;
                dialog.Email = currentEmail;
                dialog.Kecamatan = currentKecamatan;

                if (dialog.ShowDialog(this.FindForm()) == DialogResult.OK)
                {
                    UpdateUserEvent?.Invoke(this, new User
                    {
                        Id = id,
                        Username = dialog.Username,
                        Password = dialog.Password,
                        Role = dialog.Role,
                        FullName = dialog.FullName,
                        Status = dialog.Status,
                        Alamat = dialog.Alamat,
                        NoTelp = dialog.NoTelp,
                        Email = dialog.Email,
                        Kecamatan = dialog.Kecamatan
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

            var userToDelete = _users.FirstOrDefault(u => u.Id == id);
            if (userToDelete == null)
            {
                MessageBox.Show("Data user tidak ditemukan.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Cegah penghapusan admin terakhir
            if (userToDelete.Role == "Admin")
            {
                int adminCount = _users.Count(u => u.Role == "Admin");
                if (adminCount <= 1)
                {
                    MessageBox.Show(
                        "Akun admin ini tidak dapat dihapus karena merupakan satu-satunya admin yang terdaftar.\nMinimal harus ada 1 akun admin di dalam sistem.",
                        "ChemLink Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var confirm = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus user \"{userToDelete.Username}\"?\nTindakan ini tidak dapat dibatalkan.",
                "Konfirmasi Hapus User", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
                DeleteUserEvent?.Invoke(this, id);
        }
    }
}
