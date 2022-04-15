using System.Runtime.InteropServices;
using MPP.model;
using MPP.service;

namespace MPP
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        
        MasterService ServiceMaster
        {
            get;
            set;
        }

        public Form1()
        {
            //AllocConsole();
            InitializeComponent();
        }

        public Form1(MasterService masterService)
        {
            ServiceMaster = masterService;
            /*ServiceMaster.OrganiserService = masterService.OrganiserService;
            ServiceMaster.MatchService = masterService.MatchService;
            ServiceMaster.TicketService = masterService.TicketService;*/
            AllocConsole();
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string name = textName.Text;
            string pass = textPassword.Text;
            try
            {
                Organiser loggedOrganiser = ServiceMaster.OrganiserService.findOrganiserByLogin(name, pass);
                if (loggedOrganiser.Id == 0 || loggedOrganiser.Name == null || loggedOrganiser.Password == null)
                {
                    MessageBox.Show("We couldn't find that user");
                    resetTextFields();
                    return;
                }
                else
                {
                    MainForm mainForm = new MainForm(ServiceMaster);
                    mainForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            resetTextFields();
        }

        private void resetTextFields()
        {
            textName.Text = null;
            textPassword.Text = null;
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(ServiceMaster);
            registerForm.Show();
        }
    }
}