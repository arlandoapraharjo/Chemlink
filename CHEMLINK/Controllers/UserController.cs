using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using CHEMLINK.Contexts;
using CHEMLINK.Models;
using CHEMLINK.Views;

namespace CHEMLINK.Controllers
{
    /// <summary>
    /// Handles user management (CRUD) operations only.
    /// </summary>
    public class UserController
    {
        private readonly MainForm _view;
        private readonly User _currentUser;

        // Context
        private readonly UserContext _userContext;

        // In-memory state
        private List<User> _users;

        public UserController(MainForm view, User user)
        {
            _view = view;
            _currentUser = user;

            _userContext = new UserContext();
            _users = _userContext.Read();

            // Wire user management events
            _view.AddUserEvent += HandleAddUser;
            _view.UpdateUserEvent += HandleUpdateUser;
            _view.DeleteUserEvent += HandleDeleteUser;
        }

        public void ShowUserManagement()
        {
            _users = _userContext.Read();
            _view.ShowUserManagement(_users, _currentUser.Role == "Admin");
        }

        private void HandleAddUser(object? sender, User e)
        {
            try
            {
                _userContext.Create(e);
                _view.ShowMessage("User berhasil ditambahkan!");
                ShowUserManagement();
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                _view.ShowMessage("Username sudah terdaftar. Gunakan username lain.");
            }
            catch (Exception ex)
            {
                _view.ShowMessage("Gagal menambahkan user: " + ex.Message);
            }
        }

        private void HandleUpdateUser(object? sender, User e)
        {
            // Cegah perubahan role admin terakhir menjadi non-admin
            var existingUser = _users.FirstOrDefault(u => u.Id == e.Id);
            if (existingUser != null && existingUser.Role == "Admin" && e.Role != "Admin")
            {
                int adminCount = _users.Count(u => u.Role == "Admin");
                if (adminCount <= 1)
                {
                    _view.ShowMessage("Role admin ini tidak dapat diubah karena merupakan satu-satunya admin yang terdaftar.\nMinimal harus ada 1 akun admin di dalam sistem.");
                    return;
                }
            }

            _userContext.Update(e);
            _view.ShowMessage("User berhasil diupdate!");
            ShowUserManagement();
        }

        private void HandleDeleteUser(object? sender, int id)
        {
            _userContext.Delete(id);
            _view.ShowMessage("User berhasil dihapus!");
            ShowUserManagement();
        }
    }
}
