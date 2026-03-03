using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CodeTrainerApp.Model;
using CodeTrainerApp.Services;

namespace CodeTrainerApp.View
{
	public partial class CabinetView : Form
	{
		private readonly UserHistoryService _historyService;

		public CabinetView(User user)
		{
			InitializeComponent();
			_historyService = new UserHistoryService();
			LoadHistoryAsync();
		}

		// ================= ЗАВАНТАЖЕННЯ ІСТОРІЇ =================
		private async void LoadHistoryAsync()
		{
			try
			{
				var history = await _historyService.GetUserHistoryAsync();
				history = history ?? new List<UserHistory>();

				dgvHistory.DataSource = history;

				// Приховуємо технічні поля
				if (dgvHistory.Columns["UserId"] != null)
					dgvHistory.Columns["UserId"].Visible = false;

				if (dgvHistory.Columns["Id"] != null)
					dgvHistory.Columns["Id"].Visible = false;

				if (dgvHistory.Columns["QuizId"] != null)
					dgvHistory.Columns["QuizId"].Visible = false;

				// Налаштовуємо назви
				if (dgvHistory.Columns["QuizTitle"] != null)
					dgvHistory.Columns["QuizTitle"].HeaderText = "Назва тесту";

				if (dgvHistory.Columns["Score"] != null)
					dgvHistory.Columns["Score"].HeaderText = "Результат";

				if (dgvHistory.Columns["MaxScore"] != null)
					dgvHistory.Columns["MaxScore"].HeaderText = "Макс. бал";

				if (dgvHistory.Columns["CompletedAt"] != null)
				{
					dgvHistory.Columns["CompletedAt"].HeaderText = "Дата проходження";
					dgvHistory.Columns["CompletedAt"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
				}

				// Статистика
				int total = history.Count;
				double avg = total > 0 ? history.Average(h => (double)h.Score) : 0.0;
				int best = total > 0 ? history.Max(h => h.Score) : 0;

				lblTotalValue.Text = total.ToString();
				lblAverageValue.Text = avg.ToString("0.##");
				lblBestValue.Text = best.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					$"Помилка при завантаженні історії: {ex.Message}",
					"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
			}
		}

		// ================= ВИДАЛЕННЯ ОДНОГО ЗАПИСУ =================
		private async void btnDeleteSelected_Click(object sender, EventArgs e)
		{
			if (dgvHistory.CurrentRow == null) return;

			var attempt = dgvHistory.CurrentRow.DataBoundItem as UserHistory;
			if (attempt == null) return;

			var confirm = MessageBox.Show(
				$"Видалити запис '{attempt.QuizTitle}'?",
				"Підтвердження",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning
			);

			if (confirm == DialogResult.Yes)
			{
				bool success = await _historyService.DeleteHistoryAsync(attempt.Id);

				if (success)
				{
					MessageBox.Show("Запис видалено.");
					LoadHistoryAsync();
				}
			}
		}

		// ================= ОЧИЩЕННЯ ВСІЄЇ ІСТОРІЇ =================
		private async void btnClearHistory_Click(object sender, EventArgs e)
		{
			var confirm = MessageBox.Show(
				"Ви впевнені, що хочете повністю очистити історію?",
				"Підтвердження",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Warning
			);

			if (confirm != DialogResult.Yes)
				return;

			bool success = await _historyService.ClearHistoryAsync();

			if (success)
			{
				MessageBox.Show("Історію очищено.");
				LoadHistoryAsync();
			}
			else
			{
				MessageBox.Show("Не вдалося очистити історію.", "Error");
			}
		}

		private void BtnLogout_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}