using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CHEMLINK.Views
{
    public partial class LoginForm : Form
    {
        public event EventHandler? LoginAttemptEvent;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Username
        {
            get => txtUser.Text;
            set => txtUser.Text = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Password
        {
            get => txtPass.Text;
            set => txtPass.Text = value;
        }

        public LoginForm()
        {
            InitializeComponent();
            btnLogin.Click += delegate { LoginAttemptEvent?.Invoke(this, EventArgs.Empty); };
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void CloseView()
        {
            this.DialogResult = DialogResult.OK;
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {
            btnLogin.Region = new Region(GetRoundedRectanglePath(new Rectangle(0, 0, btnLogin.Width, btnLogin.Height), 24));
        }

        private void header_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                header.ClientRectangle,
                Color.FromArgb(46, 125, 50),
                Color.FromArgb(0, 150, 136),
                System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
            {
                e.Graphics.FillRectangle(brush, header.ClientRectangle);
            }

            using (var font = new Font("Segoe UI", 24F, FontStyle.Bold))
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    "🌿 ChemLink",
                    font,
                    header.ClientRectangle,
                    Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void pnlUser_Paint(object sender, PaintEventArgs e)
        {
            Color lineColor = txtUser.Focused ? Color.FromArgb(46, 125, 50) : Color.FromArgb(210, 210, 210);
            int lineWidth = txtUser.Focused ? 2 : 1;
            using (var pen = new Pen(lineColor, lineWidth))
            {
                e.Graphics.DrawLine(pen, 0, pnlUser.Height - lineWidth, pnlUser.Width, pnlUser.Height - lineWidth);
            }
        }

        private void txtUser_GotFocus(object sender, EventArgs e) => pnlUser.Invalidate();
        private void txtUser_LostFocus(object sender, EventArgs e) => pnlUser.Invalidate();
        private void pnlUser_Click(object sender, EventArgs e) => txtUser.Focus();

        private void pnlPass_Paint(object sender, PaintEventArgs e)
        {
            Color lineColor = txtPass.Focused ? Color.FromArgb(46, 125, 50) : Color.FromArgb(210, 210, 210);
            int lineWidth = txtPass.Focused ? 2 : 1;
            using (var pen = new Pen(lineColor, lineWidth))
            {
                e.Graphics.DrawLine(pen, 0, pnlPass.Height - lineWidth, pnlPass.Width, pnlPass.Height - lineWidth);
            }
        }

        private void txtPass_GotFocus(object sender, EventArgs e) => pnlPass.Invalidate();
        private void txtPass_LostFocus(object sender, EventArgs e) => pnlPass.Invalidate();
        private void pnlPass_Click(object sender, EventArgs e) => txtPass.Focus();

        private static System.Drawing.Drawing2D.GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int diameter = radius * 2;
            Rectangle arc = new Rectangle(rect.X, rect.Y, diameter, diameter);

            path.StartFigure();
            path.AddArc(arc, 180, 90);

            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            arc.X = rect.X;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}