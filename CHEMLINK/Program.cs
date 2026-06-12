using CHEMLINK.Controllers;
using CHEMLINK.Views;
using System;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Forms;
// Memastikan kita menggunakan model User yang benar
using User = CHEMLINK.Models.User;

namespace CHEMLINK
{
    internal static class Program
    {
        /// <summary>
        /// Titik masuk utama (Entry Point) untuk aplikasi.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) => Console.WriteLine($"Crash: {e.ExceptionObject}");
            CHEMLINK.Helpers.ConnectDB.UpdateDatabaseObjects();
            ApplicationConfiguration.Initialize();

            bool appIsRunning = true;

            // Loop aplikasi agar saat Logout, sistem tidak mati, tapi kembali ke Halaman Login
            while (appIsRunning)
            {
                // 1. TAMPILKAN FORM LOGIN PERTAMA KALI
                LoginForm loginForm = new LoginForm();
                LoginController loginController = new LoginController(loginForm);

                // ShowDialog() akan menahan eksekusi kode di bawahnya sampai form login ditutup.
                // Jika user menekan tombol "X" di pojok kanan atas form login, kita matikan aplikasi.
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    appIsRunning = false;
                    break;
                }

                // 2. AMBIL DATA USER (JIKA LOGIN SUKSES)
                // Jika program sampai ke baris ini, berarti login sukses dan DialogResult.OK terpicu
                User loggedInUser = loginController.AuthenticatedUser!;

                // 3. TAMPILKAN FORM DASHBOARD UTAMA
                // Kita buat form utama dan memberikan data user yang login ke Controllernya
                MainForm mainForm = new MainForm();
                UserController mainController = new UserController(mainForm, loggedInUser);

                // Tampilkan Dashboard. Kode akan berhenti di sini sampai Dashboard ditutup/logout.
                DialogResult mainResult = mainForm.ShowDialog();

                // Jika user menutup dashboard dengan tombol "X" (Close), matikan aplikasi.
                // Jika user menekan tombol "Logout" (yang mengirimkan sinyal DialogResult.Retry),
                // maka loop 'while' akan berputar kembali ke langkah 1 (memunculkan form login).
                if (mainResult != DialogResult.Retry)
                {
                    appIsRunning = false;
                }
            }
        }
    }
}