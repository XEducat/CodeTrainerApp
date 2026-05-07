using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CodeTrainerApp.Model;
using CodeTrainerApp.Services;

using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views.RegisteredUserViews
{
	public partial class UserHistoryView : Form
	{
		private readonly UserHistoryService _historyService;
		private List<Model.UserHistory> _allHistory = new List<Model.UserHistory>();

		public UserHistoryView()
		{
			InitializeComponent();
			_historyService = new UserHistoryService();

			ApplyStyles();

			cbPeriod.SelectedIndexChanged += FilterHistory;
			dtpCustomDate.ValueChanged += FilterHistory;
			cbPeriod.SelectedItem = "Весь час";

			LoadHistoryAsync();
		}

		private void ApplyStyles()
		{
			this.BackColor = Theme.Background;
			mainPanel.BackColor = Theme.Background;
			mainPanel.Padding = new Padding(30);

			headerPanel.BackColor = Theme.Background;
			headerPanel.Height = 80;

			statsPanel.BackColor = Theme.Background;
			statsPanel.Height = 120;

			dgvContainer.BackColor = Theme.Border;
			dgvContainer.Padding = new Padding(1);

			lblTitle.ForeColor = Theme.TextPrimary;
			lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);

			StyleCard(pnlStatTotal, lblTotalTitle, lblTotalValue);
			StyleCard(pnlStatAverage, lblAverageTitle, lblAverageValue);
			StyleCard(pnlStatBest, lblBestTitle, lblBestValue);

			filterPanel.BackColor = Theme.Surface;
			filterPanel.Width = 300;
			filterPanel.Padding = new Padding(20);

			lblNameFilter.ForeColor = Theme.TextSecondary;
			lblNameFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

			lblFilter.ForeColor = Theme.TextSecondary;
			lblFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblFilter.Margin = new Padding(0, 20, 0, 0);

			tbNameFilter.Font = new Font("Segoe UI", 11F);
			tbNameFilter.Width = 260;

			cbPeriod.Font = new Font("Segoe UI", 11F);
			cbPeriod.Width = 260;

			dtpCustomDate.Font = new Font("Segoe UI", 11F);
			dtpCustomDate.Width = 260;

			btnClearHistory.Height = 45;
			btnClearHistory.Width = 260;
			btnClearHistory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			StyleHelper.ApplyDangerButton(btnClearHistory);

			StyleGrid(dgvHistory);
		}

		private void StyleCard(Panel pnl, Label title, Label value)
		{
			pnl.BackColor = Theme.Surface;
			pnl.BorderStyle = BorderStyle.None;
			pnl.Margin = new Padding(0, 0, 20, 0);

			title.ForeColor = Theme.TextSecondary;
			title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			title.Location = new Point(15, 15);

			value.ForeColor = Theme.Primary;
			value.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
			value.Location = new Point(15, 40);

			pnl.Paint += (s, e) =>
			{
				using (var pen = new Pen(Theme.Border, 1))
				{
					e.Graphics.DrawRectangle(pen, 0, 0, pnl.Width - 1, pnl.Height - 1);
				}
			};
		}

		private void StyleGrid(DataGridView dgv)
		{
			dgv.BackgroundColor = Theme.Surface;
			dgv.GridColor = Color.FromArgb(242, 243, 245);
			dgv.BorderStyle = BorderStyle.None;
			dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

			// Заголовки
			dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 251);
			dgv.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextSecondary;
			dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 249, 251);
			dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			dgv.ColumnHeadersHeight = 52;

			// Рядки (Default)
			dgv.DefaultCellStyle.BackColor = Theme.Surface;
			dgv.DefaultCellStyle.ForeColor = Theme.TextPrimary;
			dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255); // Ніжний індиго-відтінок (світлий Indigo)
			dgv.DefaultCellStyle.SelectionForeColor = Theme.Primary; // Текст стає акцентним Indigo
			dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
			dgv.DefaultCellStyle.Padding = new Padding(10, 0, 10, 0);

			// Рядки (Alternating)
			dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 253, 255);
			dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255); 
			dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Theme.Primary;

			// Загальний стиль для виділених рядків
			dgv.RowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 242, 255);
			dgv.RowsDefaultCellStyle.SelectionForeColor = Theme.Primary;

			dgv.RowTemplate.Height = 50;
			dgv.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
			dgv.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
			
			// Додамо підсвітку при наведенні (за бажанням, але стандартно dgv це не вміє без подій)
		}

		private async void LoadHistoryAsync()
		{
			try
			{
				var history = await _historyService.GetUserHistoryAsync();
				_allHistory = history ?? new List<Model.UserHistory>();

				// Виправляємо час: перетворюємо UTC в LocalTime
				foreach (var h in _allHistory)
				{
					if (h.CompletedAt.Kind == DateTimeKind.Utc || h.CompletedAt.Kind == DateTimeKind.Unspecified)
					{
						h.CompletedAt = DateTime.SpecifyKind(h.CompletedAt, DateTimeKind.Utc).ToLocalTime();
					}
				}

				UpdateGrid(_allHistory);
				UpdateStatistics(_allHistory);

				// Знімаємо виділення після завантаження
				dgvHistory.ClearSelection();
				dgvHistory.CurrentCell = null;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Помилка при завантаженні історії: " + ex.Message,
					"Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void UpdateGrid(List<Model.UserHistory> history)
		{
			dgvHistory.DataSource = null;
			dgvHistory.DataSource = history;

			// Створюємо колонки заново, щоб не було AutoGenerateColumns
			if (dgvHistory.Columns.Count == 0)
			{
				dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
				{
					DataPropertyName = "QuizTitle",
					HeaderText = "Назва тесту",
					AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
				});

				dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
				{
					Name = "ScoreColumn",
					HeaderText = "Результат",
					AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
				});

				dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
				{
					DataPropertyName = "CompletedAt",
					HeaderText = "Дата проходження",
					DefaultCellStyle = { Format = "dd.MM.yyyy HH:mm" },
					AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
				});

				dgvHistory.CellFormatting -= DgvHistory_CellFormatting;
				dgvHistory.CellFormatting += DgvHistory_CellFormatting;
			}

			// Знімаємо виділення після оновлення даних
			dgvHistory.ClearSelection();
			dgvHistory.CurrentCell = null;
		}

		private void UpdateStatistics(List<Model.UserHistory> history)
		{
			int total = history.Count;
			double avg = total > 0 ? history.Average(h => (double)h.Score / h.MaxScore * 100) : 0;
			double bestPercent = total > 0 ? history.Max(h => (double)h.Score / h.MaxScore * 100) : 0;

			lblTotalValue.Text = total.ToString();
			lblAverageValue.Text = avg.ToString("0.##") + "%";

			// Найкращий результат — максимальний відсоток виконання
			var bestItem = history.OrderByDescending(h => (double)h.Score / h.MaxScore).FirstOrDefault();
			if (bestItem != null)
			{
				double percent = (double)bestItem.Score / bestItem.MaxScore * 100;
				lblBestValue.Text = $"{bestItem.Score} / {bestItem.MaxScore} ({percent:0.#}%)";
			}
			else
			{
				lblBestValue.Text = "0 / 0 (0%)";
			}
		}

		private void DgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (dgvHistory.Columns[e.ColumnIndex].Name == "ScoreColumn")
			{
				var item = dgvHistory.Rows[e.RowIndex].DataBoundItem as Model.UserHistory;
				if (item != null)
				{
					e.Value = $"{item.Score} / {item.MaxScore}";
					e.FormattingApplied = true;
				}
			}
		}

		private async void btnClearHistory_Click(object sender, EventArgs e)
		{
			var confirm = MessageBox.Show(
				"Ви впевнені, що хочете очистити історію?",
				"Підтвердження",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning);

			if (confirm != DialogResult.Yes) return;

			bool success = await _historyService.ClearHistoryAsync();
			if (success) LoadHistoryAsync();
		}

		private void TbNameFilter_TextChanged(object sender, EventArgs e)
		{
			ApplyFilters();
		}

		private void FilterHistory(object sender, EventArgs e)
		{
			ApplyFilters();
		}

		// Загальний метод фільтрації
		private void ApplyFilters()
		{
			if (_allHistory == null || !_allHistory.Any())
				return;

			string filterText = tbNameFilter.Text.ToLower();
			DateTime today = DateTime.Today;

			IEnumerable<Model.UserHistory> filtered = _allHistory;

			// --- Фільтр по періоду/даті ---
			switch (cbPeriod.SelectedItem.ToString())
			{
				case "Сьогодні":
					filtered = filtered.Where(h => h.CompletedAt.Date == today);
					dtpCustomDate.Visible = false;
					break;
				case "Вчора":
					filtered = filtered.Where(h => h.CompletedAt.Date == today.AddDays(-1));
					dtpCustomDate.Visible = false;
					break;
				case "За тиждень":
					filtered = filtered.Where(h => h.CompletedAt.Date >= today.AddDays(-7));
					dtpCustomDate.Visible = false;
					break;
				case "2 тижні":
					filtered = filtered.Where(h => h.CompletedAt.Date >= today.AddDays(-14));
					dtpCustomDate.Visible = false;
					break;
				case "Місяць":
					filtered = filtered.Where(h => h.CompletedAt.Date >= today.AddMonths(-1));
					dtpCustomDate.Visible = false;
					break;
				case "Весь час":
					dtpCustomDate.Visible = false;
					break;
				case "Обрана дата":
					filtered = filtered.Where(h => h.CompletedAt.Date == dtpCustomDate.Value.Date);
					dtpCustomDate.Visible = true;
					break;
			}

			// --- Фільтр по назві ---
			if (!string.IsNullOrWhiteSpace(filterText))
			{
				filtered = filtered.Where(h => h.QuizTitle != null && h.QuizTitle.ToLower().Contains(filterText));
			}

			var filteredList = filtered.ToList();
			UpdateGrid(filteredList);
			UpdateStatistics(filteredList);
		}
	}
}