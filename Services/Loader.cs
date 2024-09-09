using Newtonsoft.Json;
using Simple_PMS.Models;
using System.Collections.Generic;
using System.IO;
using System;

namespace Simple_PMS.Services
{
    internal static class Loader
    {
        internal static List<UserCommonData>? LoadUsers()
        {
            string dbPath = $"{Environment.CurrentDirectory}\\Data\\UsersDB.json";
            List<UserCommonData>? items;
            try
            {
                using (var r = new StreamReader(dbPath))
                {
                    var json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<UserCommonData>>(json);
                }
            }
            catch (Exception ex)
            {
                Message.Show("Ошибка", ex.Message, true);
                return new List<UserCommonData>();
            }
            return items ?? new List<UserCommonData>();
        }

        internal static List<UserAuthData>? LoadAuthData()
        {
            string dbPath = $"{Environment.CurrentDirectory}\\Data\\UsersAuthDB.json";
            List<UserAuthData>? items;
            try
            {
                using (var r = new StreamReader(dbPath))
                {
                    var json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<UserAuthData>>(json);
                }
            }
            catch (Exception ex) { items = null; }
            return items ?? new List<UserAuthData>();
        }

        internal static List<ProjectTask>? LoadTasks()
        {
            string dbPath = $"{Environment.CurrentDirectory}\\Data\\TasksDB.json";
            List<ProjectTask>? items;
            try
            {
                using (var r = new StreamReader(dbPath))
                {
                    var json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<ProjectTask>>(json);
                }
            }
            catch (Exception ex)
            {
                Message.Show("Ошибка", ex.Message, true);
                return new List<ProjectTask>();
            }
            return items ?? new List<ProjectTask>();
        }
    }
}
