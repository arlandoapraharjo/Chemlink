using CHEMLINK.Views.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CHEMLINK.Views
{
    public class LoginForm : Form, ILoginView
    {
        private TextBox txtUser = null!;
        private TextBox txtPass = null!;
        private Button btnLogin = null!;

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
            SetupUI();
            btnLogin.Click += delegate { LoginAttemptEvent?.Invoke(this, EventArgs.Empty); };
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void CloseView()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {

        }

        private void SetupUI()
        {
            this.Text = "ChemLink - Login Operator";
            this.Size = new Size(400, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);

            Panel header = new Panel { Dock = DockStyle.Top, Height = 100 };
            header.Paint += (sender, e) =>
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
            };
            this.Controls.Add(header);

            Label lblInfo = new Label { Text = "Welcome to Chemlink!", Location = new Point(40, 120), AutoSize = true, ForeColor = Color.Gray };

            Label lblUser = new Label { Text = "Username", Location = new Point(40, 175), AutoSize = true, ForeColor = Color.FromArgb(100, 100, 100), Font = new Font("Segoe UI", 9.5F) };
            Panel pnlUser = new Panel { Location = new Point(40, 198), Width = 300, Height = 32, BackColor = Color.White };
            txtUser = new TextBox { BorderStyle = BorderStyle.None, Location = new Point(2, 4), Width = 296, Font = new Font("Segoe UI", 11F) };
            pnlUser.Controls.Add(txtUser);
            pnlUser.Paint += (s, e) =>
            {
                Color lineColor = txtUser.Focused ? Color.FromArgb(46, 125, 50) : Color.FromArgb(210, 210, 210);
                int lineWidth = txtUser.Focused ? 2 : 1;
                using (var pen = new Pen(lineColor, lineWidth))
                {
                    e.Graphics.DrawLine(pen, 0, pnlUser.Height - lineWidth, pnlUser.Width, pnlUser.Height - lineWidth);
                }
            };
            txtUser.GotFocus += (s, e) => pnlUser.Invalidate();
            txtUser.LostFocus += (s, e) => pnlUser.Invalidate();
            pnlUser.Click += (s, e) => txtUser.Focus();

            Label lblPass = new Label { Text = "Password", Location = new Point(40, 245), AutoSize = true, ForeColor = Color.FromArgb(100, 100, 100), Font = new Font("Segoe UI", 9.5F) };
            Panel pnlPass = new Panel { Location = new Point(40, 268), Width = 300, Height = 32, BackColor = Color.White };
            txtPass = new TextBox { BorderStyle = BorderStyle.None, Location = new Point(2, 4), Width = 296, Font = new Font("Segoe UI", 11F), PasswordChar = '•' };
            pnlPass.Controls.Add(txtPass);
            pnlPass.Paint += (s, e) =>
            {
                Color lineColor = txtPass.Focused ? Color.FromArgb(46, 125, 50) : Color.FromArgb(210, 210, 210);
                int lineWidth = txtPass.Focused ? 2 : 1;
                using (var pen = new Pen(lineColor, lineWidth))
                {
                    e.Graphics.DrawLine(pen, 0, pnlPass.Height - lineWidth, pnlPass.Width, pnlPass.Height - lineWidth);
                }
            };
            txtPass.GotFocus += (s, e) => pnlPass.Invalidate();
            txtPass.LostFocus += (s, e) => pnlPass.Invalidate();
            pnlPass.Click += (s, e) => txtPass.Focus();

            btnLogin = new Button 
            { 
                Text = "LOGIN", 
                Location = new Point(90, 330), 
                Width = 200, 
                Height = 45, 
                BackColor = Color.FromArgb(46, 125, 50), 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat, 
                Font = new Font("Segoe UI", 12F, FontStyle.Bold), 
                Cursor = Cursors.Hand 
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Region = new Region(GetRoundedRectanglePath(new Rectangle(0, 0, btnLogin.Width, btnLogin.Height), 24));

            this.Controls.Add(lblInfo);
            this.Controls.Add(lblUser);
            this.Controls.Add(pnlUser);
            this.Controls.Add(lblPass);
            this.Controls.Add(pnlPass);
            this.Controls.Add(btnLogin);
        }

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