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
        private ContextMenuStrip _contextMenu;

        private Panel _filterPanel;
        private TextBox _searchTextBox;
        private DateTimePicker _customDatePicker;
        private Label _lblCustomDate;
        private Button _btnReset;
        private ComboBox _dateFilterComboBox;

        public MentorStatsView(int? quizId = null)
        {
            InitializeComponent();
            _historyService = new UserHistoryService();
            _quizService = new QuizService();
            _quizId = quizId;

            InitializeFilterPanel();
            InitializeContextMenu();
            
            Theme.ThemeChanged += OnThemeChanged;
            this.Disposed += (s, e) => Theme.ThemeChanged -= OnThemeChanged;
            OnThemeChanged();
        }

        private void InitializeFilterPanel()
        {
            _filterPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(0, 10, 0, 10)
            };

            Label lblSearch = new Label { Text = "Пошук (логін):", AutoSize = true, Location = new Point(0, 15) };
            _searchTextBox = new TextBox 
            { 
                Location = new Point(100, 12), 
                Width = 180, 
                Font = new Font("Segoe UI", 11)
            };
            _searchTextBox.TextChanged += (s, e) => ApplyFilters();

            Label lblDate = new Label { Text = "Період:", AutoSize = true, Location = new Point(300, 15) };
            _dateFilterComboBox = new ComboBox
            {
                Location = new Point(355, 12),
                Width = 130,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10)
            };
            _dateFilterComboBox.Items.AddRange(new object[] { 
                "Сьогодні", 
                "Вчора", 
                "За тиждень", 
                "За місяць", 
                "За весь час", 
                "За датою" 
            });
            _dateFilterComboBox.SelectedIndex = 4; // За весь час

            _lblCustomDate = new Label { Text = "Дата:", AutoSize = true, Location = new Point(500, 15), Visible = false };
            _customDatePicker = new DateTimePicker
            {
                Location = new Point(540, 12),
                Width = 120,
                Format = DateTimePickerFormat.Short,
                Visible = false
            };
            _customDatePicker.ValueChanged += (s, e) => ApplyFilters();

            _btnReset = new Button
            {
                Text = "Скинути",
                Size = new Size(80, 32),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(500, 10)
            };
            StyleHelper.ApplyPrimaryButton(_btnReset);
            _btnReset.Click += (s, e) => {
                _searchTextBox.Text = "";
                _dateFilterComboBox.SelectedIndex = 4;
                ApplyFilters();
            };

            _dateFilterComboBox.SelectedIndexChanged += (s, e) => {
                bool isCustom = _dateFilterComboBox.SelectedIndex == 5;
                _lblCustomDate.Visible = isCustom;
                _customDatePicker.Visible = isCustom;
                
                // Зміщуємо кнопку скидання
                _btnReset.Location = isCustom ? new Point(680, 10) : new Point(500, 10);
                
                ApplyFilters();
            };

            _filterPanel.Controls.Add(lblSearch);
            _filterPanel.Controls.Add(_searchTextBox);
            _filterPanel.Controls.Add(lblDate);
            _filterPanel.Controls.Add(_dateFilterComboBox);
            _filterPanel.Controls.Add(_lblCustomDate);
            _filterPanel.Controls.Add(_customDatePicker);
            _filterPanel.Controls.Add(_btnReset);

            this.Controls.Add(_filterPanel);
            _filterPanel.BringToFront();
            StatsDataGridView.BringToFront();

            this.Text = "Статистика користувачів";
            if (this.HeaderLabel != null) this.HeaderLabel.Text = "Статистика користувачів";
        }

        private void OnThemeChanged()
        {
            StyleHelper.ApplyFormStyle(this);
            ApplyModernStyles();

            if (_filterPanel != null)
            {
                foreach (Control c in _filterPanel.Controls)
                {
                    if (c is Label) c.ForeColor = Theme.TextSecondary;
                    if (c is TextBox || c is DateTimePicker || c is ComboBox)
                    {
                        c.BackColor = Theme.Surface;
                        c.ForeColor = Theme.TextPrimary;
                    }
                }
            }
        }

        private void ApplyFilters()
        {
            if (_allStats == null) return;

            string searchText = _searchTextBox.Text.Trim().ToLower();
            
            DateTime start = DateTime.MinValue;
            DateTime end = DateTime.MaxValue;

            switch (_dateFilterComboBox.SelectedIndex)
            {
                case 0: // Сьогодні
                    start = DateTime.Now.Date;
                    end = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                    break;
                case 1: // Вчора
                    start = DateTime.Now.Date.AddDays(-1);
                    end = DateTime.Now.Date.AddSeconds(-1);
                    break;
                case 2: // За тиждень
                    start = DateTime.Now.Date.AddDays(-7);
                    end = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                    break;
                case 3: // За місяць
                    start = DateTime.Now.Date.AddMonths(-1);
                    end = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                    break;
                case 4: // За весь час
                    start = DateTime.MinValue;
                    end = DateTime.MaxValue;
                    break;
                case 5: // За датою
                    start = _customDatePicker.Value.Date;
                    end = _customDatePicker.Value.Date.AddDays(1).AddSeconds(-1);
                    break;
            }

            var filtered = _allStats.AsEnumerable();

            if (_quizId.HasValue)
            {
                filtered = filtered.Where(x => x.QuizId == _quizId.Value);
            }

            // Пошук ТІЛЬКИ за логіном
            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(x => 
                    x.UserName != null && x.UserName.ToLower().Contains(searchText)
                );
            }

            filtered = filtered.Where(x => x.CompletedAt.ToLocalTime() >= start && x.CompletedAt.ToLocalTime() <= end);

            StatsDataGridView.DataSource = filtered.Select(x => new
            {
                x.Id,
                Користувач = x.UserName ?? x.UserEmail ?? "Анонім",
                Квіз = x.QuizTitle,
                Результат = $"{x.Score} / {x.MaxScore}",
                Дата = x.CompletedAt.ToLocalTime().ToString("g")
            }).ToList();
        }

        private async void MentorStatsView_Load(object sender, EventArgs e)
        {
            AddHintLabel();
            await LoadStats();
        }

        private void AddHintLabel()
        {
            Label lblHint = new Label
            {
                Text = "💡 ПКМ — керування спробами | Подвійний клік ЛКМ — перегляд коду",
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Theme.TextSecondary,
                Font = new Font("Segoe UI", 9, FontStyle.Italic)
            };
            this.Controls.Add(lblHint);
            lblHint.BringToFront();

            StatsDataGridView.CellMouseDown += StatsDataGridView_CellMouseDown;
        }

        private async Task LoadStats()
        {
            try
            {
                _allStats = await _historyService.GetAllStatsAsync();
                ApplyFilters();

                if (StatsDataGridView.Columns["Id"] != null)
                {
                    StatsDataGridView.Columns["Id"].Visible = false;
                }

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

        private void InitializeContextMenu()
        {
            _contextMenu = new ContextMenuStrip();
            var grantItem = new ToolStripMenuItem("Керувати спробами");
            grantItem.Click += GrantAttempt_Click;
            _contextMenu.Items.Add(grantItem);
            StatsDataGridView.ContextMenuStrip = _contextMenu;
        }

        private async void GrantAttempt_Click(object? sender, EventArgs e)
        {
            if (StatsDataGridView.CurrentRow == null) return;

            var historyId = (int)StatsDataGridView.CurrentRow.Cells["Id"].Value;
            var history = _allStats.FirstOrDefault(x => x.Id == historyId);

            if (history == null || string.IsNullOrEmpty(history.UserEmail))
            {
                MessageBox.Show("Неможливо визначити користувача.");
                return;
            }

            int currentAllowed = await _quizService.GetAllowedAttemptsAsync(history.QuizId, history.UserEmail);

            using (var form = new GrantAttemptsForm(history.UserEmail, history.UserName ?? history.UserEmail, history.QuizTitle, currentAllowed))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.Count == currentAllowed)
                    {
                        MessageBox.Show("Кількість спроб не змінилася.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    try
                    {
                        await _quizService.GrantExtraAttemptsAsync(history.UserEmail, history.QuizId, form.Count);
                        MessageBox.Show($"Кількість спроб до тесту \"{history.QuizTitle}\" для юзера \"{history.UserName}\" змінено на {form.Count}", 
                            "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка при зміні кількості спроб: " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ApplyModernStyles()
        {
            StatsDataGridView.BackgroundColor = Theme.Surface;
            StatsDataGridView.ForeColor = Theme.TextPrimary;
            StatsDataGridView.GridColor = Theme.Border;
            StatsDataGridView.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            StatsDataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            StatsDataGridView.DefaultCellStyle.BackColor = Theme.Surface;
            StatsDataGridView.DefaultCellStyle.SelectionBackColor = Theme.GridSelection;
            StatsDataGridView.DefaultCellStyle.SelectionForeColor = Theme.TextPrimary;
            StatsDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Theme.Background;
            StatsDataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextPrimary;
            StatsDataGridView.EnableHeadersVisualStyles = false;
        }

        private void StatsDataGridView_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                StatsDataGridView.ClearSelection();
                StatsDataGridView.Rows[e.RowIndex].Selected = true;
                StatsDataGridView.CurrentCell = StatsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private async Task ShowDetailedAnswers(UserHistory history)
        {
            try
            {
                var answers = JsonSerializer.Deserialize<Dictionary<int, string>>(history.UserAnswersJson);
                var quiz = await _quizService.GetQuizAsync(history.QuizId);

                string message = $"Статистика для: {history.UserName ?? history.UserEmail}\nТест: {history.QuizTitle}\n\n";

                foreach (var kvp in answers)
                {
                    var task = quiz?.Tasks?.FirstOrDefault(t => t.Id == kvp.Key);
                    string taskTitle = task?.Title ?? $"ID {kvp.Key}";
                    string taskDescription = task?.Description ?? "Опис відсутній";
                    string userCode = kvp.Value?.Trim() ?? "";
                    string template = task?.CodeTemplate?.Trim() ?? "";
                    bool isSkipped = string.IsNullOrEmpty(userCode) || userCode == template;
                    string titleSuffix = isSkipped ? " --- (ПРОПУЩЕНО)" : "";
                    message += $"--- Завдання: {taskTitle}{titleSuffix} ---\n{taskDescription}\n{kvp.Value}\n\n";
                }

                Form detailsForm = new Form();
                detailsForm.Text = $"Відповіді: {history.UserName ?? history.UserEmail}";
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