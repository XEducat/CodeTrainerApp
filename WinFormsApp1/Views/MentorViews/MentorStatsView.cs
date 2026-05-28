using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using CodeTrainerApp.UI;
using System.Text.Json;

namespace CodeTrainerApp.Views.MentorViews
{
    public partial class MentorStatsView : Form
    {
        private List<UserHistory> _allStats;
        private readonly UserHistoryService _historyService;
        private readonly QuizService _quizService;
        private readonly int? _quizId;

        public MentorStatsView(int? quizId = null)
        {
            InitializeComponent();
            _historyService = new UserHistoryService();
            _quizService = new QuizService();
            _quizId = quizId;
            
            Theme.ThemeChanged += OnThemeChanged;
            this.Disposed += (s, e) => Theme.ThemeChanged -= OnThemeChanged;
            OnThemeChanged();
        }

        private void OnThemeChanged()
        {
            StyleHelper.ApplyFormStyle(this);
            ApplyModernStyles();
        }

        private void ApplyModernStyles()
        {
            StatsDataGridView.BackgroundColor = Theme.Surface;
            StatsDataGridView.ForeColor = Theme.TextPrimary;
            StatsDataGridView.GridColor = Theme.Border;

            // Збільшуємо шрифт для комірок та заголовків
            StatsDataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            StatsDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            StatsDataGridView.DefaultCellStyle.BackColor = Theme.Surface;
            StatsDataGridView.DefaultCellStyle.SelectionBackColor = Theme.GridSelection;
            StatsDataGridView.DefaultCellStyle.SelectionForeColor = Theme.TextPrimary;
            StatsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Theme.Background;
            StatsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextPrimary;
            StatsDataGridView.EnableHeadersVisualStyles = false;
        }

        private async void MentorStatsView_Load(object sender, EventArgs e)
        {
            await LoadStats();
        }

        private async Task LoadStats()
        {
            try
            {
                _allStats = await _historyService.GetAllStatsAsync();

                var filteredStats = _allStats;
                if (_quizId.HasValue)
                {
                    filteredStats = filteredStats.Where(x => x.QuizId == _quizId.Value).ToList();
                }

                StatsDataGridView.DataSource = filteredStats.Select(x => new
                {
                    x.Id,
                    Користувач = x.UserEmail ?? "Анонім",
                    Квіз = x.QuizTitle,
                    Результат = $"{x.Score} / {x.MaxScore}",
                    Дата = x.CompletedAt.ToLocalTime().ToString("g")
                }).ToList();

                if (StatsDataGridView.Columns["Id"] != null)
                {
                    StatsDataGridView.Columns["Id"].Visible = false;
                }

                // Збільшуємо ширину полів
                if (StatsDataGridView.Columns["Користувач"] != null) StatsDataGridView.Columns["Користувач"].Width = 200;
                if (StatsDataGridView.Columns["Квіз"] != null) StatsDataGridView.Columns["Квіз"].Width = 250;
                if (StatsDataGridView.Columns["Дата"] != null) StatsDataGridView.Columns["Дата"].Width = 150;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не вдалося завантажити статистику: " + ex.Message);
            }
        }

        private async void StatsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var historyId = (int)StatsDataGridView.Rows[e.RowIndex].Cells["Id"].Value;
            var history = _allStats.FirstOrDefault(x => x.Id == historyId);

            if (history != null && !string.IsNullOrEmpty(history.UserAnswersJson))
            {
                await ShowDetailedAnswers(history);
            }
            else
            {
                MessageBox.Show("Детальні відповіді відсутні для цього запису.");
            }
        }

        private async Task ShowDetailedAnswers(UserHistory history)
        {
            try
            {
                var answers = JsonSerializer.Deserialize<Dictionary<int, string>>(history.UserAnswersJson);
                var quiz = await _quizService.GetQuizAsync(history.QuizId);

                string message = $"Статистика для: {history.UserEmail}\nТест: {history.QuizTitle}\n\n";

                foreach (var kvp in answers)
                {
                    var task = quiz?.Tasks?.FirstOrDefault(t => t.Id == kvp.Key);
                    string taskTitle = task?.Title ?? $"ID {kvp.Key}";
                    string taskDescription = task?.Description ?? "Опис відсутній";
                    
                    // Перевіряємо чи було пропущено (якщо код порожній або збігається з шаблоном)
                    string userCode = kvp.Value?.Trim() ?? "";
                    string template = task?.CodeTemplate?.Trim() ?? "";
                    bool isSkipped = string.IsNullOrEmpty(userCode) || userCode == template;
                    
                    string titleSuffix = isSkipped ? " --- (ПРОПУЩЕНО)" : "";

                    message += $"--- Завдання: {taskTitle}{titleSuffix} ---\n{taskDescription}\n{kvp.Value}\n\n";
                }

                // Показуємо в простому вікні для перегляду коду
                Form detailsForm = new Form();
                detailsForm.Text = $"Відповіді: {history.UserEmail}";
                detailsForm.Size = new Size(800, 600);
                detailsForm.StartPosition = FormStartPosition.CenterParent;

                RichTextBox rtb = new RichTextBox();
                rtb.Dock = DockStyle.Fill;
                rtb.Text = message;
                rtb.ReadOnly = true;
                rtb.Font = new Font("Consolas", 10);
                rtb.BackColor = Theme.CodeBackground;
                rtb.ForeColor = Theme.TextPrimary;

                detailsForm.Controls.Add(rtb);
                detailsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при отриманні деталей: " + ex.Message);
            }
        }
    }
}
