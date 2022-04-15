using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class LoginWindow : Form
    {
        private ClientCtrl ctrl;

        public LoginWindow(ClientCtrl ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            String user = textName.Text;
            String pass = textPassword.Text;
            try
            {
                ctrl.login(user, pass);
                //MessageBox.Show("Login succeded");
                MatchWindow matchWin = new MatchWindow(ctrl);
                matchWin.Text = "Match win " + user;
                matchWin.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Login Error " + ex.Message/*+ex.StackTrace*/, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void LoginWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
