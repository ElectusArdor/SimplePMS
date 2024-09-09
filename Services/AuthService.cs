using Simple_PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simple_PMS.Services
{
    internal class AuthService
    {
        private string login;
        private string password;

        public AuthService(string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public User? Login()
        {
            uint? id = FindUser();
            if (id == null) return null;
            List<UserCommonData>? items = Loader.LoadUsers();

            UserCommonData? currentUserData = items.Where(x => x.Id == id).FirstOrDefault();
            if (currentUserData == null)
            {
                Message.Show("Ошибка", "Данные пользователя не найдены", true);
                return null;
            }

            User user = new User(currentUserData);
            user.AccessList = UserRoles.RolesMap[currentUserData.Role];
            return new User(currentUserData);
        }

        private uint? FindUser()
        {
            
            List<UserAuthData>? items = Loader.LoadAuthData();

            if (items == null || items.Count == 0)
            {
                Message.Show("Ошибка", "Не удалось загрузить список сотрудников", true);
                return null;
            }

            UserAuthData? uData;
            try
            {
                uData = items.First(x => x.Username == login);
                if (uData == null || uData.Password != password)
                {
                    Message.Show("", "Неправильный логин или пароль", true);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Message.Show("Ошибка", ex.Message, true);
                return null;
            }

            return uData.Id;
        }
    }
}
