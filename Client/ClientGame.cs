using System;
using Client;
using Client.UI;

namespace BotFarm
{
    class ClientInit : AutomatedGame
    {
        public bool SettingUp
        {
            get;
            set;
        }

        public ClientInit(string hostname, int port, string username, string password, int realmId, int character)
            : base(hostname, port, username, password, realmId, character) {}



        public override void Start()
        {
            base.Start();
        }
    }
}
