using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CHEMLINK.Views
{
    public partial class EditSupplierDialog : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditName
        {
            get => txtEditNama.Text.Trim();
            set => txtEditNama.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditKontakPerson
        {
            get => txtEditKontakPerson.Text.Trim();
            set => txtEditKontakPerson.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditPhone
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
        public string EditAddress
        {
            get => txtEditAlamat.Text.Trim();
            set => txtEditAlamat.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditKota
        {
            get => txtEditKota.Text.Trim();
            set => txtEditKota.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditStatus
        {
            get => txtEditStatus.SelectedItem?.ToString() ?? "Aktif";
            set
            {
                int idx = txtEditStatus.Items.IndexOf(value);
                txtEditStatus.SelectedIndex = idx >= 0 ? idx : 0;
            }
        }

        public EditSupplierDialog()
        {
            InitializeComponent();
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEditNama.Text))
            {
                MessageBox.Show("Nama supplier tidak boleh kosong.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                TextRenderer.DrawText(e.Graphics, "\U0001F3F7\uFE0F", iconFont,
                    new Point(20, 18), Color.White);
                TextRenderer.DrawText(e.Graphics, "Edit Data Supplier", textFont,
                    new Point(62, 24), Color.White);
            }
        }
    }
}
