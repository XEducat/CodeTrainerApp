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

			Theme.ThemeChanged += OnThemeChanged;
			this.Disposed += (s, e) => Theme.ThemeChanged -= OnThemeChanged;
			
			// Прив'язуємо форматування комірок один раз на початку
			dgvHistory.CellFormatting += DgvHistory_CellFormatting;
			dgvHistory.DataBindingComplete += DgvHistory_DataBindingComplete;
			
			_historyService = new UserHistoryService();

			cbPeriod.SelectedIndexChanged += FilterHistory;
			dtpCustomDate.ValueChanged += FilterHistory;
			cbPeriod.SelectedItem = "Весь час";

			OnThemeChanged();
			LoadHistoryAsync();
		}

		private void OnThemeChanged()
		{
			ApplyStyles();
			dgvHistory.Refresh();
		}

		private async void LoadHistoryAsync()
		{
			try
			{
				var history = await _historyService.GetUserHistoryAsync();
				_allHistory = history ?? new List<Model.UserHistory>();

				// UTC -> LocalTime
				foreach (var h in _allHistory)
				{
					if (h.CompletedAt.Kind == DateTimeKind.Utc || h.CompletedAt.Kind == DateTimeKind.Unspecified)
					{
						h.CompletedAt = DateTime.SpecifyKind(h.CompletedAt, DateTimeKind.Utc).ToLocalTime();
					}
				}

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
					AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
				});
				
				dgvHistory.Columns[2].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
			}

			foreach (DataGridViewColumn col in dgvHistory.Columns)
			{
				var fmt = col.DefaultCellStyle.Format;
				col.DefaultCellStyle = new DataGridViewCellStyle 
				{ 
					BackColor = Color.Empty, 
					ForeColor = Color.Empty,
					SelectionBackColor = Theme.GridSelection,
					SelectionForeColor = Theme.GridSelectionText,
					Format = fmt
				};
			}

			// ПРИМУСОВО оновлюємо стилі після зміни даних
			ApplyStyles();

			dgvHistory.ClearSelection();
			dgvHistory.CurrentCell = null;
		}

		private void UpdateStatistics(List<Model.UserHistory> history)
		{
			int total = history.Count;
			double avg = total > 0 ? history.Average(h => (double)h.Score / h.MaxScore * 100) : 0;

			lblTotalValue.Text = total.ToString();
			lblAverageValue.Text = avg.ToString("0.##") + "%";

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
			// Примусова стилізація кожної комірки (фінальний шар захисту)
			// Використовуємо чергування кольорів для кращої читабельності
			e.CellStyle.BackColor = (e.RowIndex % 2 == 0) ? Theme.Surface : Theme.GridAlternate;
			e.CellStyle.ForeColor = Theme.TextPrimary;
			e.CellStyle.SelectionBackColor = Theme.GridSelection;
			e.CellStyle.SelectionForeColor = Theme.GridSelectionText;

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

		private void DgvHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			// ПРИМУСОВО перемальовуємо все після завантаження даних
			ApplyStyles();
			dgvHistory.Invalidate();
		}

		private async void btnClearHistory_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Ви впевнені, що хочете очистити історію?", "Підтвердження",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
			{
				if (await _historyService.ClearHistoryAsync()) LoadHistoryAsync();
			}
		}

		private void TbNameFilter_TextChanged(object sender, EventArgs e) => ApplyFilters();
		private void FilterHistory(object sender, EventArgs e) => ApplyFilters();

		private void ApplyFilters()
		{
			if (_allHistory == null) return;

			string filterText = tbNameFilter.Text.ToLower();
			DateTime today = DateTime.Today;

			IEnumerable<Model.UserHistory> filtered = _allHistory;

			switch (cbPeriod.SelectedItem?.ToString())
			{
				case "Сьогодні": filtered = filtered.Where(h => h.CompletedAt.Date == today); break;
				case "Вчора": filtered = filtered.Where(h => h.CompletedAt.Date == today.AddDays(-1)); break;
				case "За тиждень": filtered = filtered.Where(h => h.CompletedAt.Date >= today.AddDays(-7)); break;
				case "2 тижні": filtered = filtered.Where(h => h.CompletedAt.Date >= today.AddDays(-14)); break;
				case "Місяць": filtered = filtered.Where(h => h.CompletedAt.Date >= today.AddMonths(-1)); break;
				case "Обрана дата": 
					filtered = filtered.Where(h => h.CompletedAt.Date == dtpCustomDate.Value.Date);
					dtpCustomDate.Visible = true;
					break;
				default: dtpCustomDate.Visible = false; break;
			}

			if (!string.IsNullOrWhiteSpace(filterText))
				filtered = filtered.Where(h => h.QuizTitle != null && h.QuizTitle.ToLower().Contains(filterText));

			var list = filtered.ToList();
			UpdateGrid(list);
			UpdateStatistics(list);
		}
	}
}