using Newtonsoft.Json;
using Simple_PMS.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Simple_PMS.Services
{
    internal class TaskHandler
    {
        private string dbPath = $"{Environment.CurrentDirectory}\\Data\\TasksDB.json";

        internal void SaveNewTask(string name, string description)
        {
            uint nextId;
            ObservableCollection<ProjectTask>? items = new ObservableCollection<ProjectTask>(Loader.LoadTasks().ToArray());
            if (items == null)
            {
                items = new ObservableCollection<ProjectTask>();
                nextId = 0;
            }
            else { nextId = items[items.Count - 1].ProjectId + 1; }

            ProjectTask task = new(nextId, name, description);
            items.Add(task);
            SaveTasks(items);
        }

        internal void SaveTasks(ObservableCollection<ProjectTask> items)
        {
            JsonSerializer serializer = new JsonSerializer();
            string st = JsonConvert.SerializeObject(items);
            using (StreamWriter sw = new StreamWriter(dbPath))
            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                serializer.Serialize(jsonWriter, items);
            }
        }
    }
}
