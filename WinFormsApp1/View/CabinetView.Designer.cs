using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeTrainerApp.View
{
	public partial class CabinetView : Form
	{
		private Panel mainPanel;
		private Panel headerPanel;
		private Label lblTitle;

		private FlowLayoutPanel statsPanel;

		private Panel pnlStatTotal;
		private Label lblTotalTitle;
		private Label lblTotalValue;

		private Panel pnlStatAverage;
		private Label lblAverageTitle;
		private Label lblAverageValue;

		private Panel pnlStatBest;
		private Label lblBestTitle;
		private Label lblBestValue;

		private Panel footerPanel;
		private Button btnLogout;
		private Button btnClearHistory;

		private DataGridView dgvHistory;

		private void InitializeComponent()
		{
			mainPanel = new Panel();
			headerPanel = new Panel();
			lblTitle = new Label();

			statsPanel = new FlowLayoutPanel();

			pnlStatTotal = new Panel();
			lblTotalTitle = new Label();
			lblTotalValue = new Label();

			pnlStatAverage = new Panel();
			lblAverageTitle = new Label();
			lblAverageValue = new Label();

			pnlStatBest = new Panel();
			lblBestTitle = new Label();
			lblBestValue = new Label();

			footerPanel = new Panel();
			btnLogout = new Button();
			btnClearHistory = new Button();

			dgvHistory = new DataGridView();

			SuspendLayout();

			// ================= MAIN PANEL =================
			mainPanel.Dock = DockStyle.Fill;
			mainPanel.BackColor = Color.White;
			mainPanel.Padding = new Padding(30);
			mainPanel.Controls.Add(dgvHistory);
			mainPanel.Controls.Add(statsPanel);
			mainPanel.Controls.Add(footerPanel);
			mainPanel.Controls.Add(headerPanel);

			// ================= HEADER =================
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Height = 70;

			lblTitle.Text = "Кабінет користувача";
			lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
			lblTitle.Location = new Point(0, 0);
			lblTitle.AutoSize = true;

			headerPanel.Controls.Add(lblTitle);

			// ================= STATS PANEL =================
			statsPanel.Dock = DockStyle.Top;
			statsPanel.Height = 110;
			statsPanel.FlowDirection = FlowDirection.LeftToRight;
			statsPanel.Padding = new Padding(0, 10, 0, 10);
			statsPanel.WrapContents = false;

			CreateStatPanel(pnlStatTotal, lblTotalTitle, lblTotalValue, "Всього спроб");
			CreateStatPanel(pnlStatAverage, lblAverageTitle, lblAverageValue, "Середній результат");
			CreateStatPanel(pnlStatBest, lblBestTitle, lblBestValue, "Найкращий результат");

			statsPanel.Controls.Add(pnlStatTotal);
			statsPanel.Controls.Add(pnlStatAverage);
			statsPanel.Controls.Add(pnlStatBest);

			// ================= FOOTER =================
			footerPanel.Dock = DockStyle.Bottom;
			footerPanel.Height = 80;

			btnLogout.Text = "Вийти";
			btnLogout.Width = 130;
			btnLogout.Height = 42;
			btnLogout.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnLogout.BackColor = Color.FromArgb(220, 53, 69);
			btnLogout.ForeColor = Color.White;
			btnLogout.FlatStyle = FlatStyle.Flat;
			btnLogout.FlatAppearance.BorderSize = 0;
			btnLogout.Location = new Point(30, 18);
			btnLogout.Click += BtnLogout_Click;

			btnClearHistory.Text = "Очистити історію";
			btnClearHistory.Width = 180;
			btnClearHistory.Height = 42;
			btnClearHistory.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnClearHistory.BackColor = Color.FromArgb(108, 117, 125);
			btnClearHistory.ForeColor = Color.White;
			btnClearHistory.FlatStyle = FlatStyle.Flat;
			btnClearHistory.FlatAppearance.BorderSize = 0;
			btnClearHistory.Location = new Point(200, 18);
			btnClearHistory.Click += btnClearHistory_Click;

			footerPanel.Controls.Add(btnLogout);
			footerPanel.Controls.Add(btnClearHistory);

			// ================= DATAGRIDVIEW =================
			dgvHistory.Dock = DockStyle.Fill;
			dgvHistory.BackgroundColor = Color.White;
			dgvHistory.BorderStyle = BorderStyle.None;
			dgvHistory.ReadOnly = true;

			dgvHistory.AllowUserToAddRows = false;
			dgvHistory.AllowUserToDeleteRows = false;
			dgvHistory.AllowUserToResizeRows = false;
			dgvHistory.AllowUserToResizeColumns = false;

			dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvHistory.MultiSelect = false;

			dgvHistory.RowHeadersVisible = false;
			dgvHistory.AutoGenerateColumns = true;
			dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

			dgvHistory.EnableHeadersVisualStyles = false;
			dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvHistory.GridColor = Color.FromArgb(230, 230, 230);

			dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 37, 41);
			dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			dgvHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvHistory.ColumnHeadersHeight = 45;

			dgvHistory.ColumnHeadersDefaultCellStyle.SelectionBackColor =
				dgvHistory.ColumnHeadersDefaultCellStyle.BackColor;

			dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
			dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(13, 110, 253);
			dgvHistory.DefaultCellStyle.SelectionForeColor = Color.White;
			dgvHistory.DefaultCellStyle.Padding = new Padding(8);
			dgvHistory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvHistory.RowTemplate.Height = 40;

			dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
			dgvHistory.ScrollBars = ScrollBars.Vertical;

			dgvHistory.DataBindingComplete += (s, e) =>
			{
				dgvHistory.ClearSelection();
				dgvHistory.CurrentCell = null;
			};

			// ================= FORM =================
			BackColor = Color.FromArgb(245, 246, 250);
			ClientSize = new Size(1200, 700);
			Controls.Add(mainPanel);
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Кабінет користувача";
			WindowState = FormWindowState.Maximized;

			ResumeLayout(false);
		}

		private void CreateStatPanel(Panel panel, Label title, Label value, string text)
		{
			panel.Width = 250;
			panel.Height = 90;
			panel.Margin = new Padding(0, 0, 20, 0);
			panel.BackColor = Color.FromArgb(248, 249, 250);
			panel.Padding = new Padding(15);
			panel.BorderStyle = BorderStyle.FixedSingle;

			title.Text = text;
			title.Font = new Font("Segoe UI", 9F);
			title.ForeColor = Color.Gray;
			title.Location = new Point(10, 10);
			title.AutoSize = true;

			value.Text = "0";
			value.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
			value.ForeColor = Color.FromArgb(33, 37, 41);
			value.Location = new Point(10, 30);
			value.AutoSize = true;

			panel.Controls.Add(title);
			panel.Controls.Add(value);
		}
	}
}