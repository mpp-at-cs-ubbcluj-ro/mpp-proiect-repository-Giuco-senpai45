using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using services2;
using networking2;
using model2;

namespace client
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {   
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IBasketService server= new ServerObjProxy("127.0.0.1", 55556);
            ClientCtrl ctrl = new ClientCtrl(server);
            LoginWindow win = new LoginWindow(ctrl);
            Application.Run(win);
        }
    }
}
