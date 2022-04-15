using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace basket.client
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            /*IBasketService server = new ChatServerProxy("127.0.0.1", 55555);
            ChatClientCtrl ctrl = new ChatClientCtrl(server);
            LoginWindow win = new LoginWindow(ctrl);*/
            Application.Run();
        }
    }
}
