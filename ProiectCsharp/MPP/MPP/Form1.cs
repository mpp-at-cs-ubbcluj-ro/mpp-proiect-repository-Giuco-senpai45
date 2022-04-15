using System.Runtime.InteropServices;
using MPP.service;

namespace MPP
{
    public partial class Form1 : Form
    {
        /*[DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();*/
        private MasterService masterService;
        public Form1()
        {
            //AllocConsole();
            InitializeComponent();
        }

        public Form1(MasterService masterService)
        {
            this.masterService = masterService;
            this.masterService.OrganiserService = masterService.OrganiserService;
            this.masterService.MatchService = masterService.MatchService;
            this.masterService.TicketService = masterService.TicketService;
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm(masterService);
            mainForm.Show();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(this.masterService);
            registerForm.Show();
        }
    }
}