using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MPP.service;

namespace MPP
{
    public partial class RegisterForm : Form
    {
        public MasterService ServiceMaster
        {
            get;
            set;
        }

        public RegisterForm()
        {
            InitializeComponent();
        }

        public RegisterForm(MasterService masterService)
        {
            ServiceMaster = masterService;
            //errorLabel.Visible = true;
            InitializeComponent();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            if(textName.Text == "")
            {
                errorLabel.Text = "Name cannot be empty";
                errorLabel.Visible = true;
            }
            else if (textPassword.Text == "")
            {
                errorLabel.Text = "Password cannot be empty";
                errorLabel.Visible = true;
            }
            else
            {
                string name = textName.Text;
                string password = textPassword.Text;
                try
                {
                    ServiceMaster.OrganiserService.saveOrganiser(name,password);
                    errorLabel.Visible = false;
                    textName.Text = null;
                    textPassword.Text = null;
                    this.Close();
                }
                catch (Exception ex)
                {
                    errorLabel.Text = ex.Message;
                }
            }
        }
    }
}
