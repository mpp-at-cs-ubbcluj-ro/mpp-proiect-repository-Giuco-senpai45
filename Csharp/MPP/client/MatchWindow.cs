using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using model2;
using services2;

namespace client
{
    public partial class MatchWindow : Form
    {
        private ClientCtrl ctrl;
        private Match currMatch;

        int currMatchId = -1;
        public MatchWindow(ClientCtrl ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
            reloadMatchesList();

            ctrl.updateEvent += userUpdate;
        }

        private void reloadMatchesList()
        {
            ICollection<Match> matches = ctrl.getMatches();
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
            string customerName = textCutomerName.Text;
            int quantity;
            try
            {
                quantity = int.Parse(textQuantity.Text);
                try
                {
                    if (currMatch.Id != -1)
                    {
                        if (currMatch.NrOfSeats - quantity < 0)
                        {
                            MessageBox.Show("not enough tickets");
                        }
                        else
                        {
                            Ticket ticket = new Ticket(quantity, currMatch, customerName);
                            ctrl.sendUpdate(ticket);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("invalid quantity");
            }
        }

        private void updatedMatchesList(ListView listViewMatches, Match[] matches)
        {
            //listViewMatches.Items.Clear();
            matchesListView.Items.Clear();
            Console.WriteLine("Ajung in update in form {0}", ctrl.getCurrentUser());
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

                //listViewMatches.Items.Add(item);
                matchesListView.Items.Add(item);
            }
        }

        private void filterBtn_Click(object sender, EventArgs e)
        {

        }

        public void userUpdate(object sender, UserEventArgs e)
        {
            if (e.UserEventType == UserEvent.NewMatchList)
            {
                Console.WriteLine("Intru in update event in view");
                Match[] matches = (Match[])e.Data;
                matchesListView.BeginInvoke(new UpdateListCallback(this.updatedMatchesList), new Object[] { matchesListView, matches });

            }
        }
        //for updating the GUI

        //1. define a method for updating the ListBox

        //2. define a delegate to be called back by the GUI Thread
        public delegate void UpdateListCallback(ListView list, Match[] data);

        private void MatchWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("ChatWindow closing " + e.CloseReason);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                ctrl.logout();
                ctrl.updateEvent -= userUpdate;
                Application.Exit();
            }
        }

        private void MatchWindow_Load(object sender, EventArgs e)
        {

        }

        private void matchesListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (matchesListView.SelectedItems.Count > 0)
            {
                ListViewItem item = matchesListView.SelectedItems[0];

                currMatchId = int.Parse(item.SubItems[0].Text);
                textMatchID.Text = currMatchId.ToString();
                string team1 = item.SubItems[1].Text;
                string team2 = item.SubItems[2].Text;
                string type = item.SubItems[3].Text;
                int numTickets = int.Parse(item.SubItems[4].Text);
                double ticketPrice = double.Parse(item.SubItems[5].Text);
                DateTime date = DateTime.Parse(item.SubItems[6].Text);
                currMatch = new Match(currMatchId,team1,team2,type,numTickets,ticketPrice,date);
                Console.WriteLine(currMatch);
            }
        }
    }
}
