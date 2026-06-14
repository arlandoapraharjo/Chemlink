using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CHEMLINK.Views
{
    public partial class EditUserDialog : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditUsername
        {
            get => txtEditUsername.Text.Trim();
            set => txtEditUsername.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditPassword
        {
            get => txtEditPassword.Text.Trim();
            set => txtEditPassword.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditRole
        {
            get => cbEditRole.SelectedItem?.ToString() ?? "Kasir";
            set
            {
                int idx = cbEditRole.Items.IndexOf(value);
                cbEditRole.SelectedIndex = idx >= 0 ? idx : 1;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditAlamat
        {
            get => txtEditAlamat.Text.Trim();
            set => txtEditAlamat.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditNoTelp
        {
            get => txtEditNoTelp.Text.Trim();
            set => txtEditNoTelp.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditEmail
        {
            get => txtEditEmail.Text.Trim();
            set => txtEditEmail.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditKota
        {
            get => txtEditKota.Text.Trim();
            set => txtEditKota.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditKecamatan
        {
            get => txtEditKecamatan.Text.Trim();
            set => txtEditKecamatan.Text = value;
        }

        public EditUserDialog()
        {
            InitializeComponent();
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEditUsername.Text))
            {
                MessageBox.Show("Username tidak boleh kosong.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate phone (digits only if provided)
            if (!string.IsNullOrWhiteSpace(txtEditNoTelp.Text))
            {
                foreach (char c in txtEditNoTelp.Text)
                {
                    if (!char.IsDigit(c))
                    {
                        MessageBox.Show("Nomor telepon harus berupa angka!", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void header_Paint(object? sender, PaintEventArgs e)
        {
            using (var brush = new LinearGradientBrush(
                header.ClientRectangle,
                Color.FromArgb(37, 103, 30),
                Color.FromArgb(72, 161, 17),
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, header.ClientRectangle);
            }

            using (var iconFont = new Font("Segoe UI", 18F, FontStyle.Bold))
            using (var textFont = new Font("Segoe UI", 13F, FontStyle.Bold))
            {
                TextRenderer.DrawText(e.Graphics, "\u270F\uFE0F", iconFont,
                    new Point(20, 18), Color.White);
                TextRenderer.DrawText(e.Graphics, "Edit Akun Operator", textFont,
                    new Point(62, 24), Color.White);
            }
        }
    }
}
