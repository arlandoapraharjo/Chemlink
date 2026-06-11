using System;
using CHEMLINK.Views.Interfaces;

// PERBAIKAN CS0104: Kita kunci kata "User" agar sistem HANYA membaca class User buatan kita.
using User = CHEMLINK.Models.User;

namespace CHEMLINK.Controllers
{
    public class LoginController
    {
        private readonly ILoginView _view;
        public User? AuthenticatedUser { get; private set; }

        public LoginController(ILoginView view)
        {
            _view = view;
            _view.LoginAttemptEvent += HandleLogin;
        }

        private void HandleLogin(object? sender, EventArgs e)
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
    }
}

//test commit c_login