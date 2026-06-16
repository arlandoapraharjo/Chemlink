using System;
using CHEMLINK.Views;

// PERBAIKAN CS0104: Kita kunci kata "User" agar sistem HANYA membaca class User buatan kita.
using User = CHEMLINK.Models.User;

namespace CHEMLINK.Controllers
{
    public class LoginController
    {
        private readonly LoginForm _view;
        public User? AuthenticatedUser { get; private set; }

        public LoginController(LoginForm view)
        {
            _view = view;
            _view.LoginAttemptEvent += HandleLogin;
        }

        private void HandleLogin(object? sender, EventArgs e)
        {
            try
            {
                var userContext = new Contexts.UserContext();
                var user = userContext.AuthenticateUser(_view.Username, _view.Password);

                if (user != null)
                {
                    AuthenticatedUser = user;
                    _view.CloseView();
                }
                else
                {
                    _view.ShowError("Username atau password salah!");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError("Gagal terhubung ke database:\n" + ex.Message);
            }
        }
    }
}

//test commit c_login