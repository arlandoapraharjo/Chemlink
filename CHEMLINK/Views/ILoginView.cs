using System;

namespace CHEMLINK.Views
{
    public interface ILoginView
    {
        string Username { get; set; }
        string Password { get; set; }

        event EventHandler LoginAttemptEvent;

        void ShowError(string message);
        void CloseView();
    }
}