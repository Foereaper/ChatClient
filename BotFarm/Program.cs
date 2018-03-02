using BotFarm.Properties;
using Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotFarm
{
    class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }

        /*
        try
        {


            using (BotFactory factory = new BotFactory())
            {
                Random random = new Random();
                factory.SetupFactory(random.Next(Settings.Default.MinBotsCount, Settings.Default.MaxBotsCount));
                GC.KeepAlive(factory);
            }
        }
        catch(UnauthorizedAccessException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Try running the application as Administrator or check if the files have the Read-Only flag set");
            Console.ReadLine();
        }
        catch(ConfigurationException ex)
        {
            Console.WriteLine(ex.Message);
            if (ex.InnerException != null)
                Console.WriteLine(ex.InnerException.Message);
            Console.ReadLine();
        }*/
  
    }
}
