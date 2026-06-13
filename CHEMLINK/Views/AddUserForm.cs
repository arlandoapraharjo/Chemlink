using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class AddUserForm : Form
    {
        public string NewUsername => txtUsername.Text.Trim();
        public string NewPassword => txtPassword.Text;
        public string NewRole => cbRole.SelectedItem?.ToString() ?? "Kasir";

        public AddUserForm(List<User> existingUsers)
        {
            InitializeComponent();

            dgvReference.DataSource = null;
            dgvReference.Columns.Clear();
            dgvReference.DataSource = existingUsers;

            cbRole.SelectedIndex = 1; // Default to "Kasir"
            btnTambah.Click += BtnTambah_Click;
        }

        private void BtnTambah_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username dan Password wajib diisi untuk user baru.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Color.FromArgb(37, 103, 30),   // #25671E
                Color.FromArgb(72, 161, 17),   // #48A111
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, pnlHeader.ClientRectangle);
            }
        }
    }
}
