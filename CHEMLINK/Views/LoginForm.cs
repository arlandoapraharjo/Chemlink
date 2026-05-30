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

        private void SetupUI()
        {
            this.Text = "ChemLink - Login Operator";
            this.Size = new Size(400, 450);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);

            Panel header = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.FromArgb(46, 125, 50) };
            Label lblLogo = new Label { Text = "🌿 ChemLink", ForeColor = Color.White, Font = new Font("Segoe UI", 24F, FontStyle.Bold), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter };
            header.Controls.Add(lblLogo);
            this.Controls.Add(header);

            Label lblInfo = new Label { Text = "Masukkan kredensial akun kasir atau pemilik.\n(Hint: admin/admin atau kasir/kasir)", Location = new Point(40, 120), AutoSize = true, ForeColor = Color.Gray };

            Label lblUser = new Label { Text = "Username", Location = new Point(40, 180), AutoSize = true };
            txtUser = new TextBox { Location = new Point(40, 205), Width = 300, Font = new Font("Segoe UI", 12F) };

            Label lblPass = new Label { Text = "Password", Location = new Point(40, 250), AutoSize = true };
            txtPass = new TextBox { Location = new Point(40, 275), Width = 300, Font = new Font("Segoe UI", 12F), PasswordChar = '•' };

            btnLogin = new Button { Text = "LOGIN SYSTEM", Location = new Point(40, 330), Width = 300, Height = 45, BackColor = Color.FromArgb(46, 125, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 12F, FontStyle.Bold), Cursor = Cursors.Hand };

            this.Controls.Add(lblInfo);
            this.Controls.Add(lblUser);
            this.Controls.Add(txtUser);
            this.Controls.Add(lblPass);
            this.Controls.Add(txtPass);
            this.Controls.Add(btnLogin);
        }
    }
}