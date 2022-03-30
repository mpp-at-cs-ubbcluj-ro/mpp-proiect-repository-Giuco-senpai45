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
            System.Windows.Forms.ColumnHeader t1Column;
            this.textMatchId = new System.Windows.Forms.TextBox();
            this.textQuantity = new System.Windows.Forms.TextBox();
            this.textCustomerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sellTicketBtn = new System.Windows.Forms.Button();
            this.matchesListView = new System.Windows.Forms.ListView();
            this.idColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.t2Column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nrSeatsColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filterMatchesBtn = new System.Windows.Forms.Button();
            t1Column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // textMatchId
            // 
            this.textMatchId.Location = new System.Drawing.Point(119, 40);
            this.textMatchId.Name = "textMatchId";
            this.textMatchId.Size = new System.Drawing.Size(148, 22);
            this.textMatchId.TabIndex = 0;
            // 
            // textQuantity
            // 
            this.textQuantity.Location = new System.Drawing.Point(119, 93);
            this.textQuantity.Name = "textQuantity";
            this.textQuantity.Size = new System.Drawing.Size(148, 22);
            this.textQuantity.TabIndex = 1;
            // 
            // textCustomerName
            // 
            this.textCustomerName.Location = new System.Drawing.Point(119, 155);
            this.textCustomerName.Name = "textCustomerName";
            this.textCustomerName.Size = new System.Drawing.Size(148, 22);
            this.textCustomerName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "MatchId";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Quantity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "CustomerName";
            // 
            // sellTicketBtn
            // 
            this.sellTicketBtn.Location = new System.Drawing.Point(144, 203);
            this.sellTicketBtn.Name = "sellTicketBtn";
            this.sellTicketBtn.Size = new System.Drawing.Size(91, 32);
            this.sellTicketBtn.TabIndex = 6;
            this.sellTicketBtn.Text = "Sell Ticket";
            this.sellTicketBtn.UseVisualStyleBackColor = true;
            this.sellTicketBtn.Click += new System.EventHandler(this.sellTicketBtn_Click);
            // 
            // matchesListView
            // 
            this.matchesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.matchesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idColumn,
            t1Column,
            this.t2Column,
            this.typeColumn,
            this.nrSeatsColumn,
            this.priceColumn,
            this.dateColumn});
            this.matchesListView.GridLines = true;
            this.matchesListView.HideSelection = false;
            this.matchesListView.Location = new System.Drawing.Point(273, 12);
            this.matchesListView.MultiSelect = false;
            this.matchesListView.Name = "matchesListView";
            this.matchesListView.Size = new System.Drawing.Size(755, 474);
            this.matchesListView.TabIndex = 7;
            this.matchesListView.UseCompatibleStateImageBehavior = false;
            this.matchesListView.View = System.Windows.Forms.View.Details;
            this.matchesListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.matchesListView_ItemSelectionChanged);
            // 
            // idColumn
            // 
            this.idColumn.Text = "Mid";
            // 
            // t1Column
            // 
            t1Column.Text = "Team1";
            t1Column.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            t1Column.Width = 133;
            // 
            // t2Column
            // 
            this.t2Column.Text = "Team2";
            this.t2Column.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t2Column.Width = 141;
            // 
            // typeColumn
            // 
            this.typeColumn.Text = "Type";
            this.typeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.typeColumn.Width = 130;
            // 
            // nrSeatsColumn
            // 
            this.nrSeatsColumn.Text = "NrOfSeats";
            this.nrSeatsColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nrSeatsColumn.Width = 109;
            // 
            // priceColumn
            // 
            this.priceColumn.Text = "Price";
            this.priceColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.priceColumn.Width = 0;
            // 
            // dateColumn
            // 
            this.dateColumn.Text = "Date";
            this.dateColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dateColumn.Width = 177;
            // 
            // filterMatchesBtn
            // 
            this.filterMatchesBtn.Location = new System.Drawing.Point(573, 504);
            this.filterMatchesBtn.Name = "filterMatchesBtn";
            this.filterMatchesBtn.Size = new System.Drawing.Size(185, 41);
            this.filterMatchesBtn.TabIndex = 8;
            this.filterMatchesBtn.Text = "Filter";
            this.filterMatchesBtn.UseVisualStyleBackColor = true;
            this.filterMatchesBtn.Click += new System.EventHandler(this.filterMatchesBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 619);
            this.Controls.Add(this.filterMatchesBtn);
            this.Controls.Add(this.matchesListView);
            this.Controls.Add(this.sellTicketBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textCustomerName);
            this.Controls.Add(this.textQuantity);
            this.Controls.Add(this.textMatchId);
            this.Name = "MainForm";
            this.Text = "s";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textMatchId;
        private System.Windows.Forms.TextBox textQuantity;
        private System.Windows.Forms.TextBox textCustomerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button sellTicketBtn;
        private System.Windows.Forms.ListView matchesListView;
        private System.Windows.Forms.ColumnHeader idColumn;
        private System.Windows.Forms.ColumnHeader t2Column;
        private System.Windows.Forms.ColumnHeader typeColumn;
        private System.Windows.Forms.ColumnHeader nrSeatsColumn;
        private System.Windows.Forms.ColumnHeader priceColumn;
        private System.Windows.Forms.ColumnHeader dateColumn;
        private System.Windows.Forms.Button filterMatchesBtn;
    }
}