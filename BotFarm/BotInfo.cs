﻿namespace BotFarm
{
    public class BotInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public BotInfo() { }

        public BotInfo(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}
