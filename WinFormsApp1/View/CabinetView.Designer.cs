using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using CodeTrainerApp.Model; // Для UserHistory

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

			dgvContainer = new Panel();
			dgvHistory = new DataGridView();

			filterPanel = new Panel();
			lblNameFilter = new Label();
			tbNameFilter = new TextBox();
			lblFilter = new Label();
			cbPeriod = new ComboBox();
			dtpCustomDate = new DateTimePicker();
			btnClearHistory = new Button();

			SuspendLayout();

			// ================= MAIN PANEL =================
			mainPanel.Dock = DockStyle.Fill;
			mainPanel.BackColor = Color.White;
			mainPanel.Padding = new Padding(20);
			mainPanel.Controls.Add(dgvContainer);
			mainPanel.Controls.Add(filterPanel);
			mainPanel.Controls.Add(statsPanel);
			mainPanel.Controls.Add(headerPanel);

			// ================= HEADER =================
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Height = 70;

			lblTitle.Text = "Кабінет користувача";
			lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
			lblTitle.AutoSize = true;
			lblTitle.Location = new Point(0, 0);

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

			// ================= FILTER PANEL =================
			filterPanel.Dock = DockStyle.Right;
			filterPanel.Width = 250;
			filterPanel.Padding = new Padding(15);
			filterPanel.BackColor = Color.FromArgb(248, 249, 250);

			// --- Фільтр за назвою ---
			lblNameFilter.Text = "Назва квізу";
			lblNameFilter.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblNameFilter.ForeColor = Color.FromArgb(33, 37, 41);
			lblNameFilter.AutoSize = true;
			lblNameFilter.Location = new Point(10, 10);

			tbNameFilter.Width = 220;
			tbNameFilter.Height = 30;
			tbNameFilter.Location = new Point(10, 40);
			tbNameFilter.Font = new Font("Segoe UI", 11F);
			tbNameFilter.TextChanged += TbNameFilter_TextChanged;

			// --- Фільтр за період/дату ---
			lblFilter.Text = "Показати за";
			lblFilter.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblFilter.ForeColor = Color.FromArgb(33, 37, 41);
			lblFilter.AutoSize = true;
			lblFilter.Location = new Point(10, 80);

			cbPeriod.DropDownStyle = ComboBoxStyle.DropDownList;
			cbPeriod.Items.AddRange(new string[]
			{
				"Сьогодні",
				"Вчора",
				"За тиждень",
				"2 тижні",
				"Місяць",
				"Весь час",
				"Обрана дата"
			});
			cbPeriod.SelectedIndex = 0;
			cbPeriod.Location = new Point(10, 110);
			cbPeriod.Width = 220;
			cbPeriod.Font = new Font("Segoe UI", 11F);

			dtpCustomDate.Format = DateTimePickerFormat.Short;
			dtpCustomDate.Location = new Point(10, 145);
			dtpCustomDate.Width = 220;
			dtpCustomDate.Height = 30;
			dtpCustomDate.Font = new Font("Segoe UI", 11F);
			dtpCustomDate.Visible = false;

			// --- Кнопка очистити історію ---
			btnClearHistory.Text = "Очистити історію";
			btnClearHistory.Width = 220;
			btnClearHistory.Height = 45;
			btnClearHistory.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			btnClearHistory.BackColor = Color.FromArgb(220, 53, 69);
			btnClearHistory.ForeColor = Color.White;
			btnClearHistory.FlatStyle = FlatStyle.Flat;
			btnClearHistory.FlatAppearance.BorderSize = 0;
			btnClearHistory.Location = new Point(10, filterPanel.Height - 70);
			btnClearHistory.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			btnClearHistory.Click += btnClearHistory_Click;

			filterPanel.Controls.Add(lblNameFilter);
			filterPanel.Controls.Add(tbNameFilter);
			filterPanel.Controls.Add(lblFilter);
			filterPanel.Controls.Add(cbPeriod);
			filterPanel.Controls.Add(dtpCustomDate);
			filterPanel.Controls.Add(btnClearHistory);

			// ================= DATAGRIDVIEW CONTAINER =================
			dgvContainer.Dock = DockStyle.Fill;
			dgvContainer.Padding = new Padding(1);
			dgvContainer.BackColor = Color.FromArgb(200, 200, 200); // сірий для рамки
			dgvContainer.Controls.Add(dgvHistory);

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
			dgvHistory.AutoGenerateColumns = false;
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
			dgvHistory.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgvHistory.ColumnHeadersDefaultCellStyle.BackColor;

			dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 11F); // трохи більший шрифт для читабельності
			dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(13, 110, 253);
			dgvHistory.DefaultCellStyle.SelectionForeColor = Color.White;
			dgvHistory.DefaultCellStyle.Padding = new Padding(8);
			dgvHistory.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dgvHistory.RowTemplate.Height = 40;

			dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
			dgvHistory.ScrollBars = ScrollBars.Vertical;

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