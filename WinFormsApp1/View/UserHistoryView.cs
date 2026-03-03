using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CodeTrainerApp.Model;
using CodeTrainerApp.Services;

namespace CodeTrainerApp.View
{
	public partial class UserHistoryView : Form
	{
		private readonly UserHistoryService _historyService;
		private List<Model.UserHistory> _allHistory = new List<Model.UserHistory>();

		public UserHistoryView()
		{
			InitializeComponent();
			_historyService = new UserHistoryService();

			cbPeriod.SelectedIndexChanged += FilterHistory;
			dtpCustomDate.ValueChanged += FilterHistory;

			LoadHistoryAsync();
		}

		private async void LoadHistoryAsync()
		{
			try
			{
				var history = await _historyService.GetUserHistoryAsync();
				_allHistory = history ?? new List<Model.UserHistory>();

				UpdateGrid(_allHistory);
				UpdateStatistics(_allHistory);
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