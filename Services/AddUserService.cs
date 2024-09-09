using Newtonsoft.Json;
using Simple_PMS.Models;
using System.Collections.Generic;
using System.IO;
using System;

namespace Simple_PMS.Services
{
    internal class AddUserService
    {
        public void AddUser(string username, string password, string name, string role)
        {
            var users = Loader.LoadUsers();
            uint nextid = users[users.Count - 1].Id + 1;
            UserAuthData uAuthData = new(nextid, username, SecurityService.GetHash(password));
            UserCommonData uData = new(nextid, name, role);

            string dbDirectory = $"{Environment.CurrentDirectory}\\Data\\";
            string dbAuthFilePath = Path.Combine(dbDirectory, "UsersAuthDB.json");
            string dbDataFilePath = Path.Combine(dbDirectory, "UsersDB.json");
            SaveUserAuthData(uAuthData, dbAuthFilePath);
            SaveUserData(uData, dbDataFilePath);
        }

        private void SaveUserAuthData(UserAuthData uData, string path)
        {
            List<UserAuthData>? items = Loader.LoadAuthData();
            if (items == null) items = new List<UserAuthData>();

            items.Add(uData);

            JsonSerializer serializer = new JsonSerializer();
            string st = JsonConvert.SerializeObject(items);
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                serializer.Serialize(jsonWriter, items);
            }
        }

        private void SaveUserData(UserCommonData uData, string path)
        {
            List<UserCommonData>? items = Loader.LoadUsers();
            if (items == null) items = new List<UserCommonData>();
            items.Add(uData);

            JsonSerializer serializer = new JsonSerializer();
            string st = JsonConvert.SerializeObject(items);
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                serializer.Serialize(jsonWriter, items);
            }
        }
    }
}
