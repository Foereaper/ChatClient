using System;
using System.Windows.Forms;

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
                if (args[0] == "autologin")
                {
                    Application.Run(new FrmLogin(true));
                }
            }
            else
            {
                Application.Run(new FrmLogin(false));
            }
        }
    }
}
