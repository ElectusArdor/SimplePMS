using Avalonia.Controls;
using Simple_PMS.ViewModels;
using System;
using System.Threading.Tasks;

namespace Simple_PMS.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartAuthAsync();
            FreeTaskList.GotFocus += EnableAddButton;
            FreeTaskList.GotFocus += DisableRemoveButton;
            TaskList.GotFocus += EnableRemoveButton;
            TaskList.GotFocus += DisableAddButton;

            AuthBtn.Click += StartNewAuth;
            NewTaskBtn.Click += StartCreateNewTask;
            NewUserBtn.Click += StartCreateNewUser;
        }

        public async Task StartAuthAsync()
        {
            await Task.Run(async () =>
            {
                await Task.Delay(1000);
            });
            StartNewAuth(null, null);
        }

        public void StartNewAuth(Object? args, EventArgs? e)
        {
            var window = new AuthWindow();
            var vm = new AuthWindowViewModel();
            window.DataContext = vm;
            vm.CloseHandler += (sender, args) => window.Close();
            window.ShowDialog(this);
        }

        public void StartCreateNewTask(Object? args, EventArgs? e)
        {
            var window = new NewTaskWindow();
            var vm = new NewTaskWindowViewModel();
            window.DataContext = vm;
            vm.CloseHandler += (sender, args) => window.Close();
            window.ShowDialog(this);
        }

        public void StartCreateNewUser(Object? args, EventArgs? e)
        {
            var window = new NewUserWindow();
            var vm = new NewUserWindowViewModel();
            window.DataContext = vm;
            vm.CloseHandler += (sender, args) => window.Close();
            window.ShowDialog(this);
        }

        private void DisableAddButton(Object? args, EventArgs e)
        {
            AddBtn.IsEnabled = false;
        }

        private void DisableRemoveButton(Object? args, EventArgs e)
        {
            RemoveBtn.IsEnabled = false;
        }

        private void EnableAddButton(Object? args, EventArgs e)
        {
            AddBtn.IsEnabled = true;
        }

        private void EnableRemoveButton(Object? args, EventArgs e)
        {
            RemoveBtn.IsEnabled = true;
        }
    }
}