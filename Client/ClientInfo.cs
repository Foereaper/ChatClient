namespace BotFarm
{
    public class ClientInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ClientInfo() { }

        public ClientInfo(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
