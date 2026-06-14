using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CHEMLINK.Models;

namespace CHEMLINK.Views
{
    public partial class AddSupplierForm : Form
    {
        public string NewName => txtNama.Text.Trim();
        public string NewKontakPerson => txtKontakPerson.Text.Trim();
        public string NewPhone => txtNoTelp.Text.Trim();
        public string NewEmail => txtEmail.Text.Trim();
        public string NewAddress => txtAlamat.Text.Trim();
        public string NewKota => txtKota.Text.Trim();

        public AddSupplierForm()
        {
            InitializeComponent();
            btnTambah.Click += BtnTambah_Click;
        }

        private void BtnTambah_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Nama supplier wajib diisi.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Validate phone (digits only if provided)
            if (!string.IsNullOrWhiteSpace(txtNoTelp.Text))
            {
                foreach (char c in txtNoTelp.Text)
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
