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
                ListViewItem item = new ListViewItem();
                item.SubItems.Add(match.Id.ToString());
                item.SubItems.Add(match.Team1);
                item.SubItems.Add(match.Team2);
                item.SubItems.Add(match.MatchType);
                item.SubItems.Add(match.NrOfSeats.ToString());
                item.SubItems.Add(match.Price.ToString());
                item.SubItems.Add(match.Date.ToString());
                matchesListView.Items.Add(item);
            }
        }

        private void matchesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
              
        }

        private void filterMatchesBtn_Click(object sender, EventArgs e)
        {

        }

        private void sellTicketBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
