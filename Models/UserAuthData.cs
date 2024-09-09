namespace Simple_PMS.Models
{
    internal class UserAuthData: UserData
    {
        private string username;
        private string password;

        public string Username { get { return username; } }
        public string Password { get { return password; } }

        public UserAuthData(uint id, string username, string password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        }
    }
}
