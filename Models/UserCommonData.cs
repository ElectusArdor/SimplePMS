namespace Simple_PMS.Models
{
    internal class UserCommonData: UserData
    {
        private string name;
        private string role;

        public string Name { get { return name; } set { name = value; } }
        public string Role { get { return role; } set { role = value; } }

        public UserCommonData(uint id, string name, string role)
        {
            this.id = id;
            this.name = name;
            this.role = role;
        }
    }
}
