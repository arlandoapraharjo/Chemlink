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
            // Logika Autentikasi Dummy
            if (_view.Username == "admin" && _view.Password == "admin")
            {
                AuthenticatedUser = new User { Username = "Admin Utama", Role = "Admin" };
                _view.CloseView(); // Tutup form login
            }
            else if (_view.Username == "kasir" && _view.Password == "kasir")
            {
                AuthenticatedUser = new User { Username = "Kasir Shift 1", Role = "Kasir" };
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