namespace client
{
    partial class MatchWindow
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
            this.textMatchID = new System.Windows.Forms.TextBox();
            this.textQuantity = new System.Windows.Forms.TextBox();
            this.textCutomerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.matchesListView = new System.Windows.Forms.ListView();
            this.idColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.t1Column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.t2Column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nrSeatsColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sellTicketBtn = new System.Windows.Forms.Button();
            this.filterBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textMatchID
            // 
            this.textMatchID.Location = new System.Drawing.Point(154, 53);
            this.textMatchID.Name = "textMatchID";
            this.textMatchID.Size = new System.Drawing.Size(166, 22);
            this.textMatchID.TabIndex = 0;
            // 
            // textQuantity
            // 
            this.textQuantity.Location = new System.Drawing.Point(154, 122);
            this.textQuantity.Name = "textQuantity";
            this.textQuantity.Size = new System.Drawing.Size(166, 22);
            this.textQuantity.TabIndex = 1;
            // 
            // textCutomerName
            // 
            this.textCutomerName.Location = new System.Drawing.Point(154, 187);
            this.textCutomerName.Name = "textCutomerName";
            this.textCutomerName.Size = new System.Drawing.Size(166, 22);
            this.textCutomerName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Match Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Quantity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cutsomer Name";
            // 
            // matchesListView
            // 
            this.matchesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idColumn,
            this.t1Column,
            this.t2Column,
            this.typeColumn,
            this.nrSeatsColumn,
            this.priceColumn,
            this.dateColumn});
            this.matchesListView.GridLines = true;
            this.matchesListView.HideSelection = false;
            this.matchesListView.Location = new System.Drawing.Point(341, 12);
            this.matchesListView.MultiSelect = false;
            this.matchesListView.Name = "matchesListView";
            this.matchesListView.Size = new System.Drawing.Size(695, 448);
            this.matchesListView.TabIndex = 6;
            this.matchesListView.UseCompatibleStateImageBehavior = false;
            this.matchesListView.View = System.Windows.Forms.View.Details;
            this.matchesListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.matchesListView_ItemSelectionChanged);
            // 
            // idColumn
            // 
            this.idColumn.Text = "Id";
            this.idColumn.Width = 50;
            // 
            // t1Column
            // 
            this.t1Column.Text = "Team 1";
            this.t1Column.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t1Column.Width = 100;
            // 
            // t2Column
            // 
            this.t2Column.Text = "Team 2";
            this.t2Column.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.t2Column.Width = 100;
            // 
            // typeColumn
            // 
            this.typeColumn.Text = "Type";
            this.typeColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.typeColumn.Width = 100;
            // 
            // nrSeatsColumn
            // 
            this.nrSeatsColumn.Text = "No Of Seats";
            this.nrSeatsColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nrSeatsColumn.Width = 100;
            // 
            // priceColumn
            // 
            this.priceColumn.Text = "Price";
            this.priceColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dateColumn
            // 
            this.dateColumn.Text = "Date";
            this.dateColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dateColumn.Width = 200;
            // 
            // sellTicketBtn
            // 
            this.sellTicketBtn.Location = new System.Drawing.Point(185, 230);
            this.sellTicketBtn.Name = "sellTicketBtn";
            this.sellTicketBtn.Size = new System.Drawing.Size(106, 52);
            this.sellTicketBtn.TabIndex = 7;
            this.sellTicketBtn.Text = "Sell Ticket";
            this.sellTicketBtn.UseVisualStyleBackColor = true;
            this.sellTicketBtn.Click += new System.EventHandler(this.sellTicketBtn_Click);
            // 
            // filterBtn
            // 
            this.filterBtn.Location = new System.Drawing.Point(593, 466);
            this.filterBtn.Name = "filterBtn";
            this.filterBtn.Size = new System.Drawing.Size(225, 61);
            this.filterBtn.TabIndex = 8;
            this.filterBtn.Text = "Filter Matches";
            this.filterBtn.UseVisualStyleBackColor = true;
            this.filterBtn.Visible = false;
            this.filterBtn.Click += new System.EventHandler(this.filterBtn_Click);
            // 
            // MatchWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 601);
            this.Controls.Add(this.filterBtn);
            this.Controls.Add(this.sellTicketBtn);
            this.Controls.Add(this.matchesListView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textCutomerName);
            this.Controls.Add(this.textQuantity);
            this.Controls.Add(this.textMatchID);
            this.Name = "MatchWindow";
            this.Text = "MatchWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MatchWindow_FormClosing);
            this.Load += new System.EventHandler(this.MatchWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textMatchID;
        private System.Windows.Forms.TextBox textQuantity;
        private System.Windows.Forms.TextBox textCutomerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView matchesListView;
        private System.Windows.Forms.Button sellTicketBtn;
        private System.Windows.Forms.Button filterBtn;
        private System.Windows.Forms.ColumnHeader idColumn;
        private System.Windows.Forms.ColumnHeader t1Column;
        private System.Windows.Forms.ColumnHeader t2Column;
        private System.Windows.Forms.ColumnHeader typeColumn;
        private System.Windows.Forms.ColumnHeader nrSeatsColumn;
        private System.Windows.Forms.ColumnHeader priceColumn;
        private System.Windows.Forms.ColumnHeader dateColumn;
    }
}