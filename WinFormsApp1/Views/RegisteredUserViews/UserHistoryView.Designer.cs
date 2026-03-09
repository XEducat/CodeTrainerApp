using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeTrainerApp.Views.RegisteredUserViews
{
	partial class UserHistoryView
	{
		private System.ComponentModel.IContainer components = null;

		private Panel mainPanel;
		private Panel headerPanel;
		private Label lblTitle;

		private FlowLayoutPanel statsPanel;
		private Panel pnlStatTotal;
		private Panel pnlStatAverage;
		private Panel pnlStatBest;

		private Label lblTotalTitle;
		private Label lblTotalValue;
		private Label lblAverageTitle;
		private Label lblAverageValue;
		private Label lblBestTitle;
		private Label lblBestValue;

		private Panel dgvContainer;
		private DataGridView dgvHistory;

		private Panel filterPanel;
		private Label lblNameFilter;
		private TextBox tbNameFilter;
		private Label lblFilter;
		private ComboBox cbPeriod;
		private DateTimePicker dtpCustomDate;
		private Button btnClearHistory;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			DataGridViewCellStyle style1 = new DataGridViewCellStyle();
			DataGridViewCellStyle style2 = new DataGridViewCellStyle();
			DataGridViewCellStyle style3 = new DataGridViewCellStyle();

			mainPanel = new Panel();
			dgvContainer = new Panel();
			dgvHistory = new DataGridView();
			filterPanel = new Panel();
			lblNameFilter = new Label();
			tbNameFilter = new TextBox();
			lblFilter = new Label();
			cbPeriod = new ComboBox();
			dtpCustomDate = new DateTimePicker();
			btnClearHistory = new Button();
			statsPanel = new FlowLayoutPanel();
			pnlStatTotal = new Panel();
			pnlStatAverage = new Panel();
			pnlStatBest = new Panel();
			headerPanel = new Panel();
			lblTitle = new Label();
			lblTotalTitle = new Label();
			lblTotalValue = new Label();
			lblAverageTitle = new Label();
			lblAverageValue = new Label();
			lblBestTitle = new Label();
			lblBestValue = new Label();

			mainPanel.SuspendLayout();
			dgvContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
			filterPanel.SuspendLayout();
			statsPanel.SuspendLayout();
			headerPanel.SuspendLayout();
			SuspendLayout();

			// mainPanel
			mainPanel.BackColor = Color.White;
			mainPanel.Dock = DockStyle.Fill;
			mainPanel.Padding = new Padding(20);
			mainPanel.Controls.Add(dgvContainer);
			mainPanel.Controls.Add(filterPanel);
			mainPanel.Controls.Add(statsPanel);
			mainPanel.Controls.Add(headerPanel);

			// headerPanel
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Height = 70;
			headerPanel.Controls.Add(lblTitle);

			lblTitle.Text = "Історія проходжень";
			lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			lblTitle.AutoSize = true;

			// statsPanel
			statsPanel.Dock = DockStyle.Top;
			statsPanel.Height = 110;
			statsPanel.Padding = new Padding(0, 10, 0, 10);
			statsPanel.WrapContents = false;

			statsPanel.Controls.Add(pnlStatTotal);
			statsPanel.Controls.Add(pnlStatAverage);
			statsPanel.Controls.Add(pnlStatBest);

			// pnlStatTotal
			pnlStatTotal.Width = 250;
			pnlStatTotal.Height = 90;
			pnlStatTotal.Margin = new Padding(0, 0, 20, 0);
			pnlStatTotal.BackColor = Color.White;
			pnlStatTotal.BorderStyle = BorderStyle.FixedSingle;

			lblTotalTitle.Text = "Всього тестів";
			lblTotalTitle.Location = new Point(10, 10);
			lblTotalTitle.AutoSize = true;

			lblTotalValue.Text = "0";
			lblTotalValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
			lblTotalValue.Location = new Point(10, 30);
			lblTotalValue.AutoSize = true;

			pnlStatTotal.Controls.Add(lblTotalTitle);
			pnlStatTotal.Controls.Add(lblTotalValue);

			// pnlStatAverage
			pnlStatAverage.Width = 250;
			pnlStatAverage.Height = 90;
			pnlStatAverage.Margin = new Padding(0, 0, 20, 0);
			pnlStatAverage.BackColor = Color.White;
			pnlStatAverage.BorderStyle = BorderStyle.FixedSingle;

			lblAverageTitle.Text = "Середній результат";
			lblAverageTitle.Location = new Point(10, 10);
			lblAverageTitle.AutoSize = true;

			lblAverageValue.Text = "0%";
			lblAverageValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
			lblAverageValue.Location = new Point(10, 30);
			lblAverageValue.AutoSize = true;

			pnlStatAverage.Controls.Add(lblAverageTitle);
			pnlStatAverage.Controls.Add(lblAverageValue);

			// pnlStatBest
			pnlStatBest.Width = 250;
			pnlStatBest.Height = 90;
			pnlStatBest.Margin = new Padding(0, 0, 20, 0);
			pnlStatBest.BackColor = Color.White;
			pnlStatBest.BorderStyle = BorderStyle.FixedSingle;

			lblBestTitle.Text = "Найкращий результат";
			lblBestTitle.Location = new Point(10, 10);
			lblBestTitle.AutoSize = true;

			lblBestValue.Text = "0 / 0";
			lblBestValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
			lblBestValue.Location = new Point(10, 30);
			lblBestValue.AutoSize = true;

			pnlStatBest.Controls.Add(lblBestTitle);
			pnlStatBest.Controls.Add(lblBestValue);

			// dgvContainer
			dgvContainer.Dock = DockStyle.Fill;
			dgvContainer.Padding = new Padding(1);
			dgvContainer.BackColor = Color.LightGray;

			// ===== СТИЛЬ ТАБЛИЦІ (перенесений) =====

			dgvHistory.AllowUserToAddRows = false;
			dgvHistory.AllowUserToDeleteRows = false;
			dgvHistory.AllowUserToResizeColumns = false;
			dgvHistory.AllowUserToResizeRows = false;

			style1.BackColor = Color.FromArgb(248, 249, 250);
			dgvHistory.AlternatingRowsDefaultCellStyle = style1;

			dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvHistory.AutoGenerateColumns = false;
			dgvHistory.BackgroundColor = Color.White;
			dgvHistory.BorderStyle = BorderStyle.None;
			dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

			style2.Alignment = DataGridViewContentAlignment.MiddleCenter;
			style2.BackColor = Color.FromArgb(33, 37, 41);
			style2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			style2.ForeColor = Color.White;
			style2.SelectionBackColor = Color.FromArgb(33, 37, 41);
			style2.WrapMode = DataGridViewTriState.True;

			dgvHistory.ColumnHeadersDefaultCellStyle = style2;
			dgvHistory.ColumnHeadersHeight = 45;

			style3.Alignment = DataGridViewContentAlignment.MiddleCenter;
			style3.Font = new Font("Segoe UI", 11F);
			style3.Padding = new Padding(8);
			style3.SelectionBackColor = Color.FromArgb(13, 110, 253);
			style3.SelectionForeColor = Color.White;

			dgvHistory.DefaultCellStyle = style3;

			dgvHistory.EnableHeadersVisualStyles = false;
			dgvHistory.GridColor = Color.FromArgb(230, 230, 230);
			dgvHistory.MultiSelect = false;
			dgvHistory.ReadOnly = true;
			dgvHistory.RowHeadersVisible = false;
			dgvHistory.RowTemplate.Height = 40;
			dgvHistory.ScrollBars = ScrollBars.Vertical;
			dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvHistory.Dock = DockStyle.Fill;

			dgvContainer.Controls.Add(dgvHistory);

			// filterPanel
			filterPanel.Dock = DockStyle.Right;
			filterPanel.Width = 250;
			filterPanel.Padding = new Padding(15);
			filterPanel.BackColor = Color.FromArgb(248, 249, 250);

			lblNameFilter.Text = "Назва квізу";
			lblNameFilter.Location = new Point(10, 10);
			lblNameFilter.AutoSize = true;

			tbNameFilter.Location = new Point(10, 40);
			tbNameFilter.Width = 220;
			tbNameFilter.TextChanged += TbNameFilter_TextChanged;

			lblFilter.Text = "Показати за";
			lblFilter.Location = new Point(10, 80);
			lblFilter.AutoSize = true;

			cbPeriod.Location = new Point(10, 110);
			cbPeriod.Width = 220;
			cbPeriod.DropDownStyle = ComboBoxStyle.DropDownList;

			cbPeriod.Items.AddRange(new object[]
			{
				"Сьогодні",
				"Вчора",
				"За тиждень",
				"2 тижні",
				"Місяць",
				"Весь час",
				"Обрана дата"
			});

			dtpCustomDate.Location = new Point(10, 145);
			dtpCustomDate.Width = 220;
			dtpCustomDate.Format = DateTimePickerFormat.Short;
			dtpCustomDate.Visible = false;

			btnClearHistory.Text = "Очистити історію";
			btnClearHistory.Width = 220;
			btnClearHistory.Height = 40;
			btnClearHistory.Location = new Point(10, 200);
			btnClearHistory.BackColor = Color.FromArgb(220, 53, 69);
			btnClearHistory.ForeColor = Color.White;
			btnClearHistory.FlatStyle = FlatStyle.Flat;

			filterPanel.Controls.Add(lblNameFilter);
			filterPanel.Controls.Add(tbNameFilter);
			filterPanel.Controls.Add(lblFilter);
			filterPanel.Controls.Add(cbPeriod);
			filterPanel.Controls.Add(dtpCustomDate);
			filterPanel.Controls.Add(btnClearHistory);

			// UserHistoryView
			ClientSize = new Size(1200, 700);
			Controls.Add(mainPanel);
			Text = "Історія проходжень";
			StartPosition = FormStartPosition.CenterScreen;

			mainPanel.ResumeLayout(false);
			dgvContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
			filterPanel.ResumeLayout(false);
			filterPanel.PerformLayout();
			statsPanel.ResumeLayout(false);
			headerPanel.ResumeLayout(false);
			headerPanel.PerformLayout();

			ResumeLayout(false);
		}
	}
}