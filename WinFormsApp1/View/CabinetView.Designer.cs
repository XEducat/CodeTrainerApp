using System;
using System.Drawing;
using System.Windows.Forms;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.View
{
	public partial class CabinetView : Form
	{
		private Panel mainPanel;
		private Panel headerPanel;
		private Label lblTitle;
		private Label lblEmail;
		private Label lblRole;

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
		private DataGridView dgvHistory;

		private void InitializeComponent()
		{
			mainPanel = new Panel();
			headerPanel = new Panel();
			lblTitle = new Label();
			lblEmail = new Label();
			lblRole = new Label();

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
			dgvHistory = new DataGridView();

			mainPanel.SuspendLayout();
			headerPanel.SuspendLayout();
			statsPanel.SuspendLayout();
			pnlStatTotal.SuspendLayout();
			pnlStatAverage.SuspendLayout();
			pnlStatBest.SuspendLayout();
			footerPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
			SuspendLayout();

			// mainPanel
			mainPanel.BackColor = Color.White;
			mainPanel.Controls.Add(dgvHistory);
			mainPanel.Controls.Add(statsPanel);
			mainPanel.Controls.Add(footerPanel);
			mainPanel.Controls.Add(headerPanel);
			mainPanel.Dock = DockStyle.Fill;
			mainPanel.Location = new Point(0, 0);
			mainPanel.Name = "mainPanel";
			mainPanel.Padding = new Padding(20);
			mainPanel.TabIndex = 0;

			// headerPanel
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Height = 100;
			headerPanel.BackColor = Color.Transparent;
			headerPanel.Controls.Add(lblTitle);
			headerPanel.Controls.Add(lblEmail);
			headerPanel.Controls.Add(lblRole);
			headerPanel.Padding = new Padding(0, 0, 0, 10);

			// lblTitle
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
			lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
			lblTitle.Location = new Point(0, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(259, 32);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Кабінет користувача";

			// lblEmail
			lblEmail.AutoSize = true;
			lblEmail.Font = new Font("Segoe UI", 12F);
			lblEmail.ForeColor = Color.FromArgb(33, 37, 41);
			lblEmail.Location = new Point(0, 42);
			lblEmail.Name = "lblEmail";
			lblEmail.Size = new Size(0, 21);
			lblEmail.TabIndex = 1;

			// lblRole
			lblRole.AutoSize = true;
			lblRole.Font = new Font("Segoe UI", 12F);
			lblRole.ForeColor = Color.FromArgb(33, 37, 41);
			lblRole.Location = new Point(0, 65);
			lblRole.Name = "lblRole";
			lblRole.Size = new Size(0, 21);
			lblRole.TabIndex = 2;

			// statsPanel
			statsPanel.Dock = DockStyle.Top;
			statsPanel.Height = 100;
			statsPanel.FlowDirection = FlowDirection.LeftToRight;
			statsPanel.AutoSize = false;
			statsPanel.Padding = new Padding(0, 10, 0, 10);
			statsPanel.WrapContents = false;
			statsPanel.Margin = new Padding(0);
			statsPanel.BackColor = Color.Transparent;

			// pnlStatTotal
			pnlStatTotal.Width = 220;
			pnlStatTotal.Height = 80;
			pnlStatTotal.Margin = new Padding(0, 0, 10, 0);
			pnlStatTotal.BackColor = Color.FromArgb(250, 250, 252);
			pnlStatTotal.BorderStyle = BorderStyle.FixedSingle;
			pnlStatTotal.Padding = new Padding(10);
			pnlStatTotal.Controls.Add(lblTotalTitle);
			pnlStatTotal.Controls.Add(lblTotalValue);

			lblTotalTitle.AutoSize = true;
			lblTotalTitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblTotalTitle.ForeColor = Color.Gray;
			lblTotalTitle.Location = new Point(6, 6);
			lblTotalTitle.Text = "Всього спроб";

			lblTotalValue.AutoSize = true;
			lblTotalValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			lblTotalValue.ForeColor = Color.FromArgb(33, 37, 41);
			lblTotalValue.Location = new Point(6, 26);
			lblTotalValue.Text = "0";

			// pnlStatAverage
			pnlStatAverage.Width = 220;
			pnlStatAverage.Height = 80;
			pnlStatAverage.Margin = new Padding(0, 0, 10, 0);
			pnlStatAverage.BackColor = Color.FromArgb(250, 250, 252);
			pnlStatAverage.BorderStyle = BorderStyle.FixedSingle;
			pnlStatAverage.Padding = new Padding(10);
			pnlStatAverage.Controls.Add(lblAverageTitle);
			pnlStatAverage.Controls.Add(lblAverageValue);

			lblAverageTitle.AutoSize = true;
			lblAverageTitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblAverageTitle.ForeColor = Color.Gray;
			lblAverageTitle.Location = new Point(6, 6);
			lblAverageTitle.Text = "Середній результат";

			lblAverageValue.AutoSize = true;
			lblAverageValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			lblAverageValue.ForeColor = Color.FromArgb(33, 37, 41);
			lblAverageValue.Location = new Point(6, 26);
			lblAverageValue.Text = "0.0";

			// pnlStatBest
			pnlStatBest.Width = 220;
			pnlStatBest.Height = 80;
			pnlStatBest.Margin = new Padding(0, 0, 10, 0);
			pnlStatBest.BackColor = Color.FromArgb(250, 250, 252);
			pnlStatBest.BorderStyle = BorderStyle.FixedSingle;
			pnlStatBest.Padding = new Padding(10);
			pnlStatBest.Controls.Add(lblBestTitle);
			pnlStatBest.Controls.Add(lblBestValue);

			lblBestTitle.AutoSize = true;
			lblBestTitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblBestTitle.ForeColor = Color.Gray;
			lblBestTitle.Location = new Point(6, 6);
			lblBestTitle.Text = "Найкращий результат";

			lblBestValue.AutoSize = true;
			lblBestValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			lblBestValue.ForeColor = Color.FromArgb(33, 37, 41);
			lblBestValue.Location = new Point(6, 26);
			lblBestValue.Text = "0";

			// add stat panels to flow
			statsPanel.Controls.Add(pnlStatTotal);
			statsPanel.Controls.Add(pnlStatAverage);
			statsPanel.Controls.Add(pnlStatBest);

			// footerPanel
			footerPanel.Dock = DockStyle.Bottom;
			footerPanel.Height = 64;
			footerPanel.Padding = new Padding(0, 10, 0, 0);
			footerPanel.BackColor = Color.Transparent;
			footerPanel.Controls.Add(btnLogout);

			// btnLogout
			btnLogout.BackColor = Color.FromArgb(220, 53, 69);
			btnLogout.FlatAppearance.BorderSize = 0;
			btnLogout.FlatStyle = FlatStyle.Flat;
			btnLogout.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnLogout.ForeColor = Color.White;
			btnLogout.Anchor = AnchorStyles.Right | AnchorStyles.Top;
			btnLogout.Location = new Point(0, 10);
			btnLogout.Name = "btnLogout";
			btnLogout.Size = new Size(120, 40);
			btnLogout.TabIndex = 4;
			btnLogout.Text = "Вийти";
			btnLogout.UseVisualStyleBackColor = false;
			btnLogout.Click += BtnLogout_Click;
			btnLogout.Location = new Point(footerPanel.ClientSize.Width - btnLogout.Width - 10, 10);
			//footerPanel.Resize += (s, e) =>
			//{
			//	btnLogout.Location = new Point(Math.Max(10, footerPanel.ClientSize.Width - btnLogout.Width - 10), 10);
			//};

			// dgvHistory
			dgvHistory.Dock = DockStyle.Fill;
			dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvHistory.BackgroundColor = Color.FromArgb(248, 249, 250);
			dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvHistory.ReadOnly = true;
			dgvHistory.AllowUserToAddRows = false;
			dgvHistory.AllowUserToDeleteRows = false;
			dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvHistory.RowHeadersVisible = false;
			dgvHistory.AutoGenerateColumns = true;
			dgvHistory.Margin = new Padding(0);
			dgvHistory.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
			dgvHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
			dgvHistory.ScrollBars = ScrollBars.Both; // вертикальная и горизонтальная при необходимости
			dgvHistory.MinimumSize = new Size(200, 120);

			// CabinetView
			BackColor = Color.FromArgb(245, 246, 250);
			ClientSize = new Size(900, 600);
			Controls.Add(mainPanel);
			Name = "CabinetView";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Кабінет користувача";
			WindowState = FormWindowState.Maximized;
			mainPanel.ResumeLayout(false);
			headerPanel.ResumeLayout(false);
			headerPanel.PerformLayout();
			statsPanel.ResumeLayout(false);
			pnlStatTotal.ResumeLayout(false);
			pnlStatTotal.PerformLayout();
			pnlStatAverage.ResumeLayout(false);
			pnlStatAverage.PerformLayout();
			pnlStatBest.ResumeLayout(false);
			pnlStatBest.PerformLayout();
			footerPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
			ResumeLayout(false);
		}
	}
}