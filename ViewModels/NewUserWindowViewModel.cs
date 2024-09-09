using Simple_PMS.Models;
using Simple_PMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simple_PMS.ViewModels
{
    internal class NewUserWindowViewModel
    {
        public EventHandler CloseHandler;
        public List<string> Roles { get; private set; } = UserRoles.RolesMap.Keys.ToList();

        private string? name;
        private string? role;

        private string? username;
        private string? password;

        public string? Name { get { return name; } set { name = value; } }
        public string? Role { get { return role; } set { role = value; } }

        public string? Username { get { return username; } set { username = value; } }
        public string? Password { get { return password; } set { password = value; } }

        private char[] userNameAlloedChars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'w', 'v', 'x', 'y', 'z'];
        private char[] nameAlloedChars = ['а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р',
            'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', ' '];

        public void SaveUser()
        {
            if (Name != "" & Role != "" & Name != null & Role != null & Username != "" & Password != "" & Username != null & Password != null)
            {
                if (!Check(Username.ToLower(), userNameAlloedChars))
                {
                    Message.Show("Ошибка", "Недопустимые символы в логине\nДопустимы только латинские буквы и цифры", true);
                    return;
                }
                if (!Check(Name.ToLower(), nameAlloedChars))
                {
                    Message.Show("Ошибка", "Недопустимые символы в имени\nДопустимы только русские буквы и знак пробела", true);
                    return;
                }
                AddUserService aus = new AddUserService();
                aus.AddUser(Username, Password, Name, Role);
                MainWindowViewModel.GetInstance().LoadData();

                var closeHandler = CloseHandler;
                closeHandler?.Invoke(this, EventArgs.Empty);
            }
            else { Message.Show("Ошибка", "Все поля должны быть заполнены", true); }
        }

        public void Cancel()
        {
            var closeHandler = CloseHandler;
            closeHandler?.Invoke(this, EventArgs.Empty);
        }

        private bool Check(string str, char[] alloedChars)
        {
            foreach (char c in str)
            {
                if (!alloedChars.Contains(c)) return false;
            }
            return true;
        }
    }
}
