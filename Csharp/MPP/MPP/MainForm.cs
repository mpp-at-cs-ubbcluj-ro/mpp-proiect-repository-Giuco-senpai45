using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MPP.model;
using MPP.service;

namespace MPP
{
    public partial class MainForm : Form
    {
        private MasterService ServiceMaster
        {
            get;
            set;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(MasterService masterService)
        {
            ServiceMaster = masterService;
            InitializeComponent();
            reloadMatchesList();
        }

        private void reloadMatchesList()
        {
            ICollection<Match> matches = ServiceMaster.MatchService.getAllMatches();
            matchesListView.Items.Clear();
            foreach (Match match in matches)
            {
                ListViewItem item = new ListViewItem(match.Id.ToString());
                item.SubItems.Add(match.Team1);
                item.SubItems.Add(match.Team2);
                if (match.NrOfSeats > 0)
                {
                    item.SubItems.Add(match.MatchType);
                    item.SubItems.Add(match.NrOfSeats.ToString());
                    item.SubItems.Add(match.Price.ToString());
                    item.SubItems.Add(match.Date.ToString());
                }
                else
                {
                    item.ForeColor = Color.Red;
                    item.SubItems.Add("SOLD OUT");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                }

                matchesListView.Items.Add(item);
            }
        }

        private void matchesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (matchesListView.SelectedItems.Count > 0)
            {
                ListViewItem item = matchesListView.SelectedItems[0];
                textMatchId.Text = item.SubItems[0].Text;
            }
        }

        private void filterMatchesBtn_Click(object sender, EventArgs e)
        {
            ICollection<Match> matches = ServiceMaster.MatchService.getDescdendingMatchesNoOfSeats();
            matchesListView.Items.Clear();
            foreach (Match match in matches)
            {
                ListViewItem item = new ListViewItem(match.Id.ToString());
                item.SubItems.Add(match.Team1);
                item.SubItems.Add(match.Team2);
                if (match.NrOfSeats > 0)
                {
                    item.SubItems.Add(match.MatchType);
                    item.SubItems.Add(match.NrOfSeats.ToString());
                    item.SubItems.Add(match.Price.ToString());
                    item.SubItems.Add(match.Date.ToString());
                }
                else
                {
                    item.ForeColor = Color.Red;
                    item.SubItems.Add("SOLD OUT");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                    item.SubItems.Add("");
                }
                matchesListView.Items.Add(item);
            }
        }

        private void sellTicketBtn_Click(object sender, EventArgs e)
        {
            int quantity = Int32.Parse(textQuantity.Text);
            string customerName = textCustomerName.Text;
            int mid = Int32.Parse(textMatchId.Text);
            try
            {
                ServiceMaster.sellTicketForMatch(mid, quantity, customerName);
                reloadMatchesList();
                resetTextFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error selling ticket", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void resetTextFields()
        {
            textCustomerName.Text = null;
            textMatchId.Text = null;
            textQuantity.Text = null;
        }
    }
}
