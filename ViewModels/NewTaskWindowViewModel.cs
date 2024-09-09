using Simple_PMS.Services;
using System;

namespace Simple_PMS.ViewModels
{
    internal class NewTaskWindowViewModel : ViewModelBase
    {
        public EventHandler CloseHandler;

        private string? name;
        private string? description;

        public string? Name { get { return name; } set { name = value; } }
        public string? Description { get { return description; } set { description = value; } }

        public void SaveTask()
        {
            if (Name != "" & Description != "" & Name != null & Description != null)
            {
                TaskHandler th = new TaskHandler();
                th.SaveNewTask(name, description);
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
    }
}
