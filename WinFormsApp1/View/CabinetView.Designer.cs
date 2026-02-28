namespace CodeTrainerApp.View
{
	partial class CabinetView
	{
		private System.ComponentModel.IContainer components = null;

		private DataGridView dgvHistory;
		private Button btnProfile;
		private Button btnLogout;
		private Label lblTitle;
		private TableLayoutPanel tableLayout;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();

			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.tableLayout = new TableLayoutPanel();
			this.lblTitle = new Label();
			this.dgvHistory = new DataGridView();
			this.btnProfile = new Button();
			this.btnLogout = new Button();

			this.SuspendLayout();

			this.Text = "Cabinet";
			this.BackColor = System.Drawing.Color.White;

			// Layout
			this.tableLayout.Dock = DockStyle.Fill;
			this.tableLayout.RowCount = 4;
			this.tableLayout.ColumnCount = 2;
			this.tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			this.tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

			// Title
			this.lblTitle.Text = "User Cabinet";
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
			this.lblTitle.Dock = DockStyle.Fill;
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.tableLayout.Controls.Add(this.lblTitle, 0, 0);
			this.tableLayout.SetColumnSpan(this.lblTitle, 2);

			// DataGrid
			this.dgvHistory.Dock = DockStyle.Fill;
			this.dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvHistory.ReadOnly = true;
			this.dgvHistory.AllowUserToAddRows = false;
			this.tableLayout.Controls.Add(this.dgvHistory, 0, 1);
			this.tableLayout.SetColumnSpan(this.dgvHistory, 2);

			this.btnLogout.Text = "Logout";
			this.btnLogout.Dock = DockStyle.Fill;
			this.btnLogout.Height = 40;
			this.btnLogout.Click += new EventHandler(this.btnLogout_Click);

			this.tableLayout.Controls.Add(this.btnProfile, 0, 2);
			this.tableLayout.Controls.Add(this.btnLogout, 1, 2);

			this.Controls.Add(this.tableLayout);

			this.ResumeLayout(false);
		}
	}
}