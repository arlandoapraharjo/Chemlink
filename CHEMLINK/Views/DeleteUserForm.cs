using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class DeleteUserForm : Form
    {
        public int DeleteUserId { get; private set; }

        public DeleteUserForm(User userToDelete, List<User> allUsers)
        {
            InitializeComponent();

            DeleteUserId = userToDelete.Id;

            // Display user info to be deleted
            lblInfoUsername.Text = userToDelete.Username;
            lblInfoRole.Text = userToDelete.Role;

            // Show all users in reference grid
            dgvReference.DataSource = null;
            dgvReference.Columns.Clear();
            dgvReference.DataSource = allUsers;

            btnHapus.Click += BtnHapus_Click;
        }

        private void BtnHapus_Click(object? sender, EventArgs e)
        {
            string username = lblInfoUsername.Text;
            DialogResult dr = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus user \"{username}\"?\nTindakan ini tidak dapat dibatalkan.",
                "Konfirmasi Hapus User",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void pnlHeader_Paint(object? sender, PaintEventArgs e)
        {
            if (pnlHeader.ClientRectangle.Width <= 0 || pnlHeader.ClientRectangle.Height <= 0) return;
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
