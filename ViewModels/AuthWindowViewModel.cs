using Simple_PMS.Services;
using System;

namespace Simple_PMS.ViewModels
{
    public partial class AuthWindowViewModel : ViewModelBase
    {
        public string WindowText => "Введите логин и пароль";
        public EventHandler CloseHandler;

        private string? username;
        private string? password;

        public string? Username { get { return username; } set { username = value; } }
        public string? Password { get { return password; } set { password = value; } }

        public void Login()
        {
            if (username != "" & password != "" & username != null & password != null)
            {
                AuthService auth = new AuthService(username, SecurityService.GetHash(password));
                User? user = auth.Login();
                if (user != null)
                {
                    MainWindowViewModel.GetInstance().CurrentUser = user;
                    var closeHandler = CloseHandler;
                    closeHandler?.Invoke(this, EventArgs.Empty);
                }
            }
            else { Message.Show("Ошибка", "Введите данные пользователя", true); }
        }
    }
}