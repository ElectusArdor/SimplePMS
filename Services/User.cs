using Simple_PMS.Models;
using System.Collections.Generic;

namespace Simple_PMS.Services
{
    internal class UserRoles
    {
        public enum Roles { User, Admin }
        public static readonly Dictionary<string, Dictionary<string, bool>> RolesMap = new() {
            { "User", new() { { "UserList", true}, { "TaskList", true }, { "FreeTaskList", false }, { "ShowAllUsers", false }, { "AdminControl", false } } },
            { "Admin",new() { { "UserList", true}, { "TaskList", true }, { "FreeTaskList", true }, { "ShowAllUsers", true }, { "AdminControl", true } } } };
    }

    internal class User
    {
        public Dictionary<string, bool> AccessList { get; set; }
        private UserCommonData uData;

        public UserCommonData UData { get { return uData; } }

        public User(UserCommonData uData)
        {
            this.uData = uData;
            this.AccessList = UserRoles.RolesMap[uData.Role];

            if (AccessList == null)
                AccessList = UserRoles.RolesMap["User"];
        }
    }
}
