using Simple_PMS.Models;
using Simple_PMS.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace Simple_PMS.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private static MainWindowViewModel? instance;
        public MainWindowViewModel() { }
        public static MainWindowViewModel GetInstance()
        { 
            return instance ?? (instance = new MainWindowViewModel());
        }

        public string WindowTitle => "Simple PMS";

        private List<UserCommonData>? usersList;

        private ObservableCollection<UserCommonData> selectedUsers;
        internal ObservableCollection<UserCommonData> SelectedUsers { get { return selectedUsers; } set { selectedUsers = value; OnPropertyChanged(); } }
        private ObservableCollection<string>? users;
        internal ObservableCollection<string>? Users { get { return users; } set { users = value; OnPropertyChanged(); } }

        private ObservableCollection<ProjectTask>? tasksList;

        private ObservableCollection<ProjectTask>? usersTasks;
        internal ObservableCollection<ProjectTask>? UsersTasks { get { return usersTasks; } set { usersTasks = value; OnPropertyChanged(); } }

        private ObservableCollection<ProjectTask>? freeTasks;
        internal ObservableCollection<ProjectTask>? FreeTasks { get { return freeTasks; } set { freeTasks = value; OnPropertyChanged(); } }

        private bool userListVisibility;
        private bool taskListVisibility;
        private bool adminControlVisibility;
        private bool showAllUsers;
        private bool adminControl;

        public bool UserListVisibility { get { return userListVisibility; } set { userListVisibility = value; OnPropertyChanged(); } }
        public bool TaskListVisibility { get { return taskListVisibility; } set { taskListVisibility = value; OnPropertyChanged(); } }
        public bool AdminControlVisibility { get { return adminControlVisibility; } set { adminControlVisibility = value; OnPropertyChanged(); } }
        public bool ShowAllUsers { get { return showAllUsers; } set { showAllUsers = value; OnPropertyChanged(); } }
        public bool AdminControl { get { return adminControl; } set {  adminControl = value; OnPropertyChanged(); } }

        private int selectedUserId;
        public int SelectedUserId { get { return selectedUserId; } set { selectedUserId = value; UpdateUsersTasks(); OnPropertyChanged(); } }

        private int selectedTaskId;
        public int SelectedTaskId { get { return selectedTaskId; } set { selectedTaskId = value; OnPropertyChanged(); } }

        private User? currentUser;
        internal User? CurrentUser { get { return currentUser; } set { currentUser = value; ApplyAccessMask(currentUser); LoadData(); } }

        private void ApplyAccessMask(User user)
        {
            UserListVisibility = user.AccessList["UserList"];
            TaskListVisibility = user.AccessList["TaskList"];
            AdminControlVisibility = user.AccessList["FreeTaskList"];
            ShowAllUsers = user.AccessList["ShowAllUsers"];
            AdminControl = user.AccessList["AdminControl"];
        }

        internal void LoadData()
        {
            usersList = Loader.LoadUsers();

            if (ShowAllUsers) SelectedUsers = new ObservableCollection<UserCommonData>(usersList);
            else SelectedUsers = [currentUser.UData];
            tasksList = new ObservableCollection<ProjectTask>(Loader.LoadTasks().ToArray());
            UpdateFreeTasks();
            UpdateUsersTasks();
        }

        private void UpdateFreeTasks()
        {
            if (AdminControlVisibility)
            {
                FreeTasks = new ObservableCollection<ProjectTask>(tasksList.Where(x => x.OwnerId == null).ToArray());
            }
        }

        private void UpdateUsersTasks()
        {
            if (TaskListVisibility)
            {
                if (SelectedUserId < 0 | SelectedUserId > SelectedUsers.Count - 1) SelectedUserId = 0;
                var userId = SelectedUsers[SelectedUserId].Id;
                UsersTasks = new ObservableCollection<ProjectTask>(tasksList.Where(x => x.OwnerId == userId).ToArray());
            }
        }

        internal void SaveTaskChanges()
        {
            TaskHandler th = new();
            th.SaveTasks(tasksList);
        }

        internal void SetTaskOwner()
        {
            FreeTasks[SelectedTaskId].OwnerId = SelectedUsers[SelectedUserId].Id;
            SaveTaskChanges();
            UpdateFreeTasks();
            UpdateUsersTasks();
        }

        internal void SetTaskAsFree()
        {
            UsersTasks[SelectedTaskId].OwnerId = null;
            SaveTaskChanges();
            UpdateFreeTasks();
            UpdateUsersTasks();
        }
    }
}
