using System;
using System.Windows.Forms;
using Client.Util;

namespace BotFarm
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length != 0)
            {
                switch (args[0])
                {
                    default:
                        Application.Run(new FrmLogin(false));
                        break;
                    case "autologin":
                        Application.Run(new FrmLogin(true));
                        break;
                    case "timed-out":
                        Application.Run(new FrmLogin(false, 
                            new ConnectionTimeoutException("The connection with the server was lost.")));
                        break;
                }
            }
            else
            {
                Application.Run(new FrmLogin(false));
            }
        }
    }
}
