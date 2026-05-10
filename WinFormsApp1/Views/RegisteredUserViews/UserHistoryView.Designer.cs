using CodeTrainerApp.UI;
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
			mainPanel.Name = "mainPanel";
			mainPanel.Dock = DockStyle.Fill;
			mainPanel.Padding = new Padding(20);
			mainPanel.Controls.Add(dgvContainer);
			mainPanel.Controls.Add(filterPanel);
			mainPanel.Controls.Add(statsPanel);
			mainPanel.Controls.Add(headerPanel);

			// headerPanel
			headerPanel.Name = "headerPanel";
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Height = 70;
			headerPanel.Controls.Add(lblTitle);

			lblTitle.Text = "Історія проходжень";
			lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			lblTitle.AutoSize = true;

			// statsPanel
			statsPanel.Name = "statsPanel";
			statsPanel.Dock = DockStyle.Top;
			statsPanel.Height = 110;
			statsPanel.Padding = new Padding(0, 10, 0, 10);
			statsPanel.WrapContents = false;

			statsPanel.Controls.Add(pnlStatTotal);
			statsPanel.Controls.Add(pnlStatAverage);
			statsPanel.Controls.Add(pnlStatBest);

			// pnlStatTotal
			pnlStatTotal.Name = "pnlStatTotalCard";
			pnlStatTotal.Width = 250;
			pnlStatTotal.Height = 90;
			pnlStatTotal.Margin = new Padding(0, 0, 20, 0);
			pnlStatTotal.BorderStyle = BorderStyle.None;

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
			pnlStatAverage.Name = "pnlStatAverageCard";
			pnlStatAverage.Width = 250;
			pnlStatAverage.Height = 90;
			pnlStatAverage.Margin = new Padding(0, 0, 20, 0);
			pnlStatAverage.BorderStyle = BorderStyle.None;

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
			pnlStatBest.Name = "pnlStatBestCard";
			pnlStatBest.Width = 250;
			pnlStatBest.Height = 90;
			pnlStatBest.Margin = new Padding(0, 0, 20, 0);
			pnlStatBest.BorderStyle = BorderStyle.None;

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
			dgvContainer.Name = "dgvContainer";
			dgvContainer.Dock = DockStyle.Fill;
			dgvContainer.Padding = new Padding(0); // Прибираємо рамку для Flat-дизайну
			dgvContainer.BackColor = Theme.Surface;

			dgvHistory.Name = "dgvHistory";
			dgvHistory.AutoGenerateColumns = false;
			dgvHistory.Dock = DockStyle.Fill;
			dgvHistory.AllowUserToAddRows = false;
			dgvHistory.AllowUserToDeleteRows = false;
			dgvHistory.AllowUserToResizeColumns = false;
			dgvHistory.AllowUserToResizeRows = false;
			dgvHistory.AllowUserToOrderColumns = false;
			dgvHistory.ReadOnly = true;
			dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvHistory.MultiSelect = false;

			dgvContainer.Controls.Add(dgvHistory);

			// filterPanel
			filterPanel.Name = "filterPanelSurface";
			filterPanel.Dock = DockStyle.Right;
			filterPanel.Width = 250;
			filterPanel.Padding = new Padding(15);

			lblNameFilter.Text = "Назва квізу";
			lblNameFilter.Location = new Point(10, 10);
			lblNameFilter.AutoSize = true;

			tbNameFilter.Name = "tbNameFilter";
			tbNameFilter.Location = new Point(10, 40);
			tbNameFilter.Width = 220;
			tbNameFilter.TextChanged += TbNameFilter_TextChanged;

			lblFilter.Text = "Показати за";
			lblFilter.Location = new Point(10, 80);
			lblFilter.AutoSize = true;

			cbPeriod.Name = "cbPeriod";
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

			dtpCustomDate.Name = "dtpCustomDate";
			dtpCustomDate.Location = new Point(10, 145);
			dtpCustomDate.Width = 220;
			dtpCustomDate.Format = DateTimePickerFormat.Short;
			dtpCustomDate.Visible = false;

			btnClearHistory.Name = "btnClearHistory";
			btnClearHistory.Text = "Очистити історію";
			btnClearHistory.Width = 220;
			btnClearHistory.Height = 40;
			btnClearHistory.Location = new Point(10, 200);

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
			WindowState = FormWindowState.Maximized;

			mainPanel.ResumeLayout(false);
			dgvContainer.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
			filterPanel.ResumeLayout(false);
			filterPanel.PerformLayout();
			statsPanel.ResumeLayout(false);
			headerPanel.ResumeLayout(false);
			headerPanel.PerformLayout();
			
			ApplyStyles();
			
			ResumeLayout(false);
		}

		private void ApplyStyles()
		{
			StyleHelper.ApplyFormStyle(this);

			// ПРИМУСОВО фарбуємо поля фільтрів та таблицю (фінальний шар захисту)
			tbNameFilter.BackColor = Theme.Surface;
			tbNameFilter.ForeColor = Theme.TextPrimary;
			cbPeriod.BackColor = Theme.Surface;
			cbPeriod.ForeColor = Theme.TextPrimary;

			dgvHistory.BackgroundColor = Theme.Surface;
			dgvHistory.GridColor = Theme.Border;

			mainPanel.Padding = new Padding(30);
			headerPanel.Height = 80;
			statsPanel.Height = 120;
			dgvContainer.Padding = new Padding(0); // Повний Flat - без відступів

			lblTitle.ForeColor = Theme.TextPrimary;
			lblTitle.Font = new Font("Segoe UI Semibold", 24F); // Сучасніший шрифт

			StyleCard(pnlStatTotal, lblTotalTitle, lblTotalValue);
			StyleCard(pnlStatAverage, lblAverageTitle, lblAverageValue);
			StyleCard(pnlStatBest, lblBestTitle, lblBestValue);

			filterPanel.Width = 300;
			filterPanel.Padding = new Padding(20);

			lblNameFilter.ForeColor = Theme.TextSecondary;
			lblNameFilter.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

			lblFilter.ForeColor = Theme.TextSecondary;
			lblFilter.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			lblFilter.Margin = new Padding(0, 20, 0, 0);

			tbNameFilter.Font = new Font("Segoe UI", 11F);
			tbNameFilter.Width = 260;

			cbPeriod.Font = new Font("Segoe UI", 11F);
			cbPeriod.Width = 260;

			dtpCustomDate.Font = new Font("Segoe UI", 11F);
			dtpCustomDate.Width = 260;
			dtpCustomDate.BackColor = Theme.Surface;
			dtpCustomDate.ForeColor = Theme.TextPrimary;

			btnClearHistory.Height = 45;
			btnClearHistory.Width = 260;
			btnClearHistory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			StyleHelper.ApplyDangerButton(btnClearHistory);
		}

		private void StyleCard(Panel pnl, Label title, Label value)
		{
			pnl.BackColor = Theme.Surface;
			pnl.BorderStyle = BorderStyle.None;
			pnl.Margin = new Padding(0, 0, 20, 0);

			title.ForeColor = Theme.TextSecondary;
			title.Font = new Font("Segoe UI Semibold", 10F); // Трішки тонший шрифт
			title.Location = new Point(25, 15);

			value.ForeColor = Theme.Primary;
			value.Font = new Font("Segoe UI", 24F, FontStyle.Bold); // Трішки більший розмір
			value.Location = new Point(25, 35);

			pnl.Paint += (s, e) =>
			{
				// Малюємо тільки сучасну акцентну лінію зліва
				using (var stripeBrush = new SolidBrush(Theme.Primary))
				{
					e.Graphics.FillRectangle(stripeBrush, 0, 0, 6, pnl.Height);
				}
			};
		}
	}
}