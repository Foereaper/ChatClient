using System;
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
