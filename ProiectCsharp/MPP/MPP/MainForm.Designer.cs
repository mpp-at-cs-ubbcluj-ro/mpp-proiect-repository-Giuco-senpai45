namespace MPP
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.matchesListView = new System.Windows.Forms.ListView();
            this.textMatchId = new System.Windows.Forms.TextBox();
            this.textQuantity = new System.Windows.Forms.TextBox();
            this.textCustomerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sellTicketBtn = new System.Windows.Forms.Button();
            this.filterMatchesBtn = new System.Windows.Forms.Button();
            this.t1Column = new System.Windows.Forms.ColumnHeader();
            this.t2Column = new System.Windows.Forms.ColumnHeader();
            this.typeColumn = new System.Windows.Forms.ColumnHeader();
            this.nrSeatsColumn = new System.Windows.Forms.ColumnHeader();
            this.priceColumn = new System.Windows.Forms.ColumnHeader();
            this.dateColumn = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // matchesListView
            // 
            this.matchesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.matchesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.t1Column,
            this.t2Column,
            this.typeColumn,
            this.nrSeatsColumn,
            this.priceColumn,
            this.dateColumn});
            this.matchesListView.GridLines = true;
            this.matchesListView.Location = new System.Drawing.Point(326, 12);
            this.matchesListView.MultiSelect = false;
            this.matchesListView.Name = "matchesListView";
            this.matchesListView.Size = new System.Drawing.Size(544, 458);
            this.matchesListView.TabIndex = 0;
            this.matchesListView.UseCompatibleStateImageBehavior = false;
            this.matchesListView.View = System.Windows.Forms.View.Details;
            this.matchesListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.matchesListView_ItemSelectionChanged);
            // 
            // textMatchId
            // 
            this.textMatchId.Location = new System.Drawing.Point(136, 37);
            this.textMatchId.Name = "textMatchId";
            this.textMatchId.Size = new System.Drawing.Size(172, 27);
            this.textMatchId.TabIndex = 1;
            // 
            // textQuantity
            // 
            this.textQuantity.Location = new System.Drawing.Point(136, 100);
            this.textQuantity.Name = "textQuantity";
            this.textQuantity.Size = new System.Drawing.Size(172, 27);
            this.textQuantity.TabIndex = 2;
            // 
            // textCustomerName
            // 
            this.textCustomerName.Location = new System.Drawing.Point(136, 164);
            this.textCustomerName.Name = "textCustomerName";
            this.textCustomerName.Size = new System.Drawing.Size(172, 27);
            this.textCustomerName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "MatchID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Quantity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "CustomerName";
            // 
            // sellTicketBtn
            // 
            this.sellTicketBtn.Location = new System.Drawing.Point(146, 218);
            this.sellTicketBtn.Name = "sellTicketBtn";
            this.sellTicketBtn.Size = new System.Drawing.Size(135, 44);
            this.sellTicketBtn.TabIndex = 7;
            this.sellTicketBtn.Text = "Sell Ticket";
            this.sellTicketBtn.UseVisualStyleBackColor = true;
            this.sellTicketBtn.Click += new System.EventHandler(this.sellTicketBtn_Click);
            // 
            // filterMatchesBtn
            // 
            this.filterMatchesBtn.Location = new System.Drawing.Point(551, 488);
            this.filterMatchesBtn.Name = "filterMatchesBtn";
            this.filterMatchesBtn.Size = new System.Drawing.Size(241, 42);
            this.filterMatchesBtn.TabIndex = 8;
            this.filterMatchesBtn.Text = "Filter Matches";
            this.filterMatchesBtn.UseVisualStyleBackColor = true;
            this.filterMatchesBtn.Click += new System.EventHandler(this.filterMatchesBtn_Click);
            // 
            // t1Column
            // 
            this.t1Column.Text = "Team 1";
            this.t1Column.Width = 100;
            // 
            // t2Column
            // 
            this.t2Column.Text = "Team 2";
            this.t2Column.Width = 100;
            // 
            // typeColumn
            // 
            this.typeColumn.Text = "Type";
            this.typeColumn.Width = 80;
            // 
            // nrSeatsColumn
            // 
            this.nrSeatsColumn.Text = "NoOfSeats";
            this.nrSeatsColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nrSeatsColumn.Width = 100;
            // 
            // priceColumn
            // 
            this.priceColumn.Text = "Price";
            // 
            // dateColumn
            // 
            this.dateColumn.Text = "Date";
            this.dateColumn.Width = 100;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 589);
            this.Controls.Add(this.filterMatchesBtn);
            this.Controls.Add(this.sellTicketBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textCustomerName);
            this.Controls.Add(this.textQuantity);
            this.Controls.Add(this.textMatchId);
            this.Controls.Add(this.matchesListView);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView matchesListView;
        private TextBox textMatchId;
        private TextBox textQuantity;
        private TextBox textCustomerName;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button sellTicketBtn;
        private Button filterMatchesBtn;
        private ColumnHeader t1Column;
        private ColumnHeader t2Column;
        private ColumnHeader typeColumn;
        private ColumnHeader nrSeatsColumn;
        private ColumnHeader priceColumn;
        private ColumnHeader dateColumn;
    }
}