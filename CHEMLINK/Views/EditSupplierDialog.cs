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
            get => txtEditUsername.Text;
            set => txtEditUsername.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditPhone
        {
            get => txtEditPassword.Text;
            set => txtEditPassword.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string EditAddress
        {
            get => txtEditAddress.Text;
            set => txtEditAddress.Text = value;
        }

        public EditSupplierDialog()
        {
            InitializeComponent();
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEditUsername.Text))
            {
                MessageBox.Show("Nama supplier tidak boleh kosong.", "ChemLink Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // --- Paint events (ChemLink theme) ---

        private void header_Paint(object? sender, PaintEventArgs e)
        {
            using (var brush = new LinearGradientBrush(
                header.ClientRectangle,
                Color.FromArgb(37, 103, 30),  // #25671E
                Color.FromArgb(72, 161, 17),  // #48A111
                LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, header.ClientRectangle);
            }

            using (var iconFont = new Font("Segoe UI", 18F, FontStyle.Bold))
            using (var textFont = new Font("Segoe UI", 13F, FontStyle.Bold))
            {
                TextRenderer.DrawText(e.Graphics, "🏷️", iconFont,
                    new Point(20, 18), Color.White);
                TextRenderer.DrawText(e.Graphics, "Edit Data Supplier", textFont,
                    new Point(62, 24), Color.White);
            }
        }

        private void pnlUsername_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is Panel panel)
                DrawUnderline(e, panel, txtEditUsername.Focused);
        }

        private void pnlPassword_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is Panel panel)
                DrawUnderline(e, panel, txtEditPassword.Focused);
        }

        private static void DrawUnderline(PaintEventArgs e, Panel panel, bool focused)
        {
            Color lineColor = focused ? Color.FromArgb(72, 161, 17) : Color.FromArgb(210, 210, 210);
            int lineWidth = focused ? 2 : 1;
            using (var pen = new Pen(lineColor, lineWidth))
            {
                e.Graphics.DrawLine(pen, 0, panel.Height - lineWidth, panel.Width, panel.Height - lineWidth);
            }
        }

        // --- Focus handlers for underline repaint ---

        private void txtEditUsername_GotFocus(object? sender, EventArgs e) => pnlUsername.Invalidate();
        private void txtEditUsername_LostFocus(object? sender, EventArgs e) => pnlUsername.Invalidate();
        private void pnlUsername_Click(object? sender, EventArgs e) => txtEditUsername.Focus();

        private void txtEditPassword_GotFocus(object? sender, EventArgs e) => pnlPassword.Invalidate();
        private void txtEditPassword_LostFocus(object? sender, EventArgs e) => pnlPassword.Invalidate();
        private void pnlPassword_Click(object? sender, EventArgs e) => txtEditPassword.Focus();
    }
}
