using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using CodeTrainerApp.Model; // Для UserHistory

namespace CodeTrainerApp.View
{
	public partial class UserHistoryView : Form
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

		private Panel dgvContainer; // Панель для обводки
		private DataGridView dgvHistory;

		private Panel filterPanel;
		private Label lblNameFilter;
		private TextBox tbNameFilter;
		private Label lblFilter;
		private ComboBox cbPeriod;
		private DateTimePicker dtpCustomDate;
		private Button btnClearHistory;

		private void InitializeComponent()
		{
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
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
			// 
			// mainPanel
			// 
			mainPanel.BackColor = Color.White;
			mainPanel.Controls.Add(dgvContainer);
			mainPanel.Controls.Add(filterPanel);
			mainPanel.Controls.Add(statsPanel);
			mainPanel.Controls.Add(headerPanel);
			mainPanel.Dock = DockStyle.Fill;
			mainPanel.Location = new Point(0, 0);
			mainPanel.Name = "mainPanel";
			mainPanel.Padding = new Padding(20);
			mainPanel.Size = new Size(1200, 700);
			mainPanel.TabIndex = 0;
			// 
			// dgvContainer
			// 
			dgvContainer.BackColor = Color.FromArgb(200, 200, 200);
			dgvContainer.Controls.Add(dgvHistory);
			dgvContainer.Dock = DockStyle.Fill;
			dgvContainer.Location = new Point(20, 200);
			dgvContainer.Name = "dgvContainer";
			dgvContainer.Padding = new Padding(1);
			dgvContainer.Size = new Size(910, 480);
			dgvContainer.TabIndex = 0;
			// 
			// dgvHistory
			// 
			dgvHistory.AllowUserToAddRows = false;
			dgvHistory.AllowUserToDeleteRows = false;
			dgvHistory.AllowUserToResizeColumns = false;
			dgvHistory.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 249, 250);
			dgvHistory.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvHistory.AutoGenerateColumns = false;
			dgvHistory.BackgroundColor = Color.White;
			dgvHistory.BorderStyle = BorderStyle.None;
			dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = Color.FromArgb(33, 37, 41);
			dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(33, 37, 41);
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			dgvHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			dgvHistory.ColumnHeadersHeight = 45;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.BackColor = SystemColors.Window;
			dataGridViewCellStyle3.Font = new Font("Segoe UI", 11F);
			dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle3.Padding = new Padding(8);
			dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(13, 110, 253);
			dataGridViewCellStyle3.SelectionForeColor = Color.White;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
			dgvHistory.DefaultCellStyle = dataGridViewCellStyle3;
			dgvHistory.Dock = DockStyle.Fill;
			dgvHistory.EnableHeadersVisualStyles = false;
			dgvHistory.GridColor = Color.FromArgb(230, 230, 230);
			dgvHistory.Location = new Point(1, 1);
			dgvHistory.MultiSelect = false;
			dgvHistory.Name = "dgvHistory";
			dgvHistory.ReadOnly = true;
			dgvHistory.RowHeadersVisible = false;
			dgvHistory.RowTemplate.Height = 40;
			dgvHistory.ScrollBars = ScrollBars.Vertical;
			dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvHistory.Size = new Size(908, 478);
			dgvHistory.TabIndex = 0;
			// 
			// filterPanel
			// 
			filterPanel.BackColor = Color.FromArgb(248, 249, 250);
			filterPanel.Controls.Add(lblNameFilter);
			filterPanel.Controls.Add(tbNameFilter);
			filterPanel.Controls.Add(lblFilter);
			filterPanel.Controls.Add(cbPeriod);
			filterPanel.Controls.Add(dtpCustomDate);
			filterPanel.Controls.Add(btnClearHistory);
			filterPanel.Dock = DockStyle.Right;
			filterPanel.Location = new Point(930, 200);
			filterPanel.Name = "filterPanel";
			filterPanel.Padding = new Padding(15);
			filterPanel.Size = new Size(250, 480);
			filterPanel.TabIndex = 1;
			// 
			// lblNameFilter
			// 
			lblNameFilter.AutoSize = true;
			lblNameFilter.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblNameFilter.ForeColor = Color.FromArgb(33, 37, 41);
			lblNameFilter.Location = new Point(10, 10);
			lblNameFilter.Name = "lblNameFilter";
			lblNameFilter.Size = new Size(101, 21);
			lblNameFilter.TabIndex = 0;
			lblNameFilter.Text = "Назва квізу";
			// 
			// tbNameFilter
			// 
			tbNameFilter.Font = new Font("Segoe UI", 11F);
			tbNameFilter.Location = new Point(10, 40);
			tbNameFilter.Name = "tbNameFilter";
			tbNameFilter.Size = new Size(220, 27);
			tbNameFilter.TabIndex = 1;
			tbNameFilter.TextChanged += TbNameFilter_TextChanged;
			// 
			// lblFilter
			// 
			lblFilter.AutoSize = true;
			lblFilter.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblFilter.ForeColor = Color.FromArgb(33, 37, 41);
			lblFilter.Location = new Point(10, 80);
			lblFilter.Name = "lblFilter";
			lblFilter.Size = new Size(105, 21);
			lblFilter.TabIndex = 2;
			lblFilter.Text = "Показати за";
			// 
			// cbPeriod
			// 
			cbPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
			cbPeriod.Font = new Font("Segoe UI", 11F);
			cbPeriod.Items.AddRange(new object[] { "Сьогодні", "Вчора", "За тиждень", "2 тижні", "Місяць", "Весь час", "Обрана дата" });
			cbPeriod.Location = new Point(10, 110);
			cbPeriod.Name = "cbPeriod";
			cbPeriod.Size = new Size(220, 28);
			cbPeriod.TabIndex = 3;
			// 
			// dtpCustomDate
			// 
			dtpCustomDate.Font = new Font("Segoe UI", 11F);
			dtpCustomDate.Format = DateTimePickerFormat.Short;
			dtpCustomDate.Location = new Point(10, 145);
			dtpCustomDate.Name = "dtpCustomDate";
			dtpCustomDate.Size = new Size(220, 27);
			dtpCustomDate.TabIndex = 4;
			dtpCustomDate.Visible = false;
			// 
			// btnClearHistory
			// 
			btnClearHistory.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			btnClearHistory.BackColor = Color.FromArgb(220, 53, 69);
			btnClearHistory.FlatAppearance.BorderSize = 0;
			btnClearHistory.FlatStyle = FlatStyle.Flat;
			btnClearHistory.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			btnClearHistory.ForeColor = Color.White;
			btnClearHistory.Location = new Point(10, 480);
			btnClearHistory.Name = "btnClearHistory";
			btnClearHistory.Size = new Size(220, 45);
			btnClearHistory.TabIndex = 5;
			btnClearHistory.Text = "Очистити історію";
			btnClearHistory.UseVisualStyleBackColor = false;
			btnClearHistory.Click += btnClearHistory_Click;
			// 
			// statsPanel
			// 
			statsPanel.Controls.Add(pnlStatTotal);
			statsPanel.Controls.Add(pnlStatAverage);
			statsPanel.Controls.Add(pnlStatBest);
			statsPanel.Dock = DockStyle.Top;
			statsPanel.Location = new Point(20, 90);
			statsPanel.Name = "statsPanel";
			statsPanel.Padding = new Padding(0, 10, 0, 10);
			statsPanel.Size = new Size(1160, 110);
			statsPanel.TabIndex = 2;
			statsPanel.WrapContents = false;
			// 
			// pnlStatTotal
			// 
			pnlStatTotal.Location = new Point(3, 13);
			pnlStatTotal.Name = "pnlStatTotal";
			pnlStatTotal.Size = new Size(200, 100);
			pnlStatTotal.TabIndex = 0;
			// 
			// pnlStatAverage
			// 
			pnlStatAverage.Location = new Point(209, 13);
			pnlStatAverage.Name = "pnlStatAverage";
			pnlStatAverage.Size = new Size(200, 100);
			pnlStatAverage.TabIndex = 1;
			// 
			// pnlStatBest
			// 
			pnlStatBest.Location = new Point(415, 13);
			pnlStatBest.Name = "pnlStatBest";
			pnlStatBest.Size = new Size(200, 100);
			pnlStatBest.TabIndex = 2;
			// 
			// headerPanel
			// 
			headerPanel.Controls.Add(lblTitle);
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Location = new Point(20, 20);
			headerPanel.Name = "headerPanel";
			headerPanel.Size = new Size(1160, 70);
			headerPanel.TabIndex = 3;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
			lblTitle.Location = new Point(0, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(285, 37);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Історія проходжень";
			// 
			// lblTotalTitle
			// 
			lblTotalTitle.Location = new Point(0, 0);
			lblTotalTitle.Name = "lblTotalTitle";
			lblTotalTitle.Size = new Size(100, 23);
			lblTotalTitle.TabIndex = 0;
			// 
			// lblTotalValue
			// 
			lblTotalValue.Location = new Point(0, 0);
			lblTotalValue.Name = "lblTotalValue";
			lblTotalValue.Size = new Size(100, 23);
			lblTotalValue.TabIndex = 0;
			// 
			// lblAverageTitle
			// 
			lblAverageTitle.Location = new Point(0, 0);
			lblAverageTitle.Name = "lblAverageTitle";
			lblAverageTitle.Size = new Size(100, 23);
			lblAverageTitle.TabIndex = 0;
			// 
			// lblAverageValue
			// 
			lblAverageValue.Location = new Point(0, 0);
			lblAverageValue.Name = "lblAverageValue";
			lblAverageValue.Size = new Size(100, 23);
			lblAverageValue.TabIndex = 0;
			// 
			// lblBestTitle
			// 
			lblBestTitle.Location = new Point(0, 0);
			lblBestTitle.Name = "lblBestTitle";
			lblBestTitle.Size = new Size(100, 23);
			lblBestTitle.TabIndex = 0;
			// 
			// lblBestValue
			// 
			lblBestValue.Location = new Point(0, 0);
			lblBestValue.Name = "lblBestValue";
			lblBestValue.Size = new Size(100, 23);
			lblBestValue.TabIndex = 0;
			// 
			// UserHistoryView
			// 
			BackColor = Color.FromArgb(245, 246, 250);
			ClientSize = new Size(1200, 700);
			Controls.Add(mainPanel);
			Name = "UserHistoryView";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Історя проходжень";
			WindowState = FormWindowState.Maximized;
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