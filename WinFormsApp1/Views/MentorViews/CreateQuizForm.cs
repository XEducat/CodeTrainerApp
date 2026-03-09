using CodeTrainerApp.Model;
using CodeTrainerApp.Services;

namespace CodeTrainerApp.Views.MentorViews
{
	public partial class CreateQuizForm : Form
	{
		public Quiz CreatedQuiz { get; private set; } = new Quiz();
		private List<ProgrammingTask> _tasks = new();

		    public CreateQuizForm(Quiz? quiz = null)
    {
        InitializeComponent();

        if (quiz != null)
        {
            // Якщо передано існуючий Quiz, заповнюємо форму
            CreatedQuiz = quiz;

            txtTitle.Text = quiz.Title ?? string.Empty;
            txtDescription.Text = quiz.Description ?? string.Empty;

            if (quiz.Tasks != null && quiz.Tasks.Any())
            {
                _tasks = quiz.Tasks;

                lbTasks.Items.Clear();
                foreach (var task in _tasks)
                {
                    var testCount = task.Tests?.Count ?? 0;
                    lbTasks.Items.Add($"{task.Title} ({testCount} tests)");
                }
            }
        }
        else
        {
            // Якщо quiz == null, створюємо новий порожній об'єкт
            CreatedQuiz = new Quiz();
        }
    }

		private void btnAddTask_Click(object sender, EventArgs e)
		{
			var form = new AddTaskForm();

			this.Hide(); // скрываем только CreateQuizForm

			var result = form.ShowDialog();

			this.Show(); // показываем обратно CreateQuizForm

			if (result == DialogResult.OK)
			{
				var task = form.CreatedTask;

				_tasks.Add(task);
				lbTasks.Items.Add($"{task.Title} ({task.Tests.Count} tests)");
			}
		}

		private void lbTasks_DoubleClick(object sender, EventArgs e)
		{
			if (lbTasks.SelectedIndex == -1)
				return;

			int index = lbTasks.SelectedIndex;
			var selectedTask = _tasks[index];

			var form = new AddTaskForm(selectedTask);

			this.Hide(); // скрываем только CreateQuizForm

			var result = form.ShowDialog();

			this.Show(); // возвращаем форму

			if (result == DialogResult.OK)
			{
				var task = _tasks[index];
				lbTasks.Items[index] = $"{task.Title} ({task.Tests.Count} tests)";
			}
		}

		private void btnCreateQuiz_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtTitle.Text))
			{
				MessageBox.Show("Введіть назву квіза");
				return;
			}

			if (_tasks.Count == 0)
			{
				MessageBox.Show("Додайте хоча б одну задачу");
				return;
			}

			CreatedQuiz = new Quiz
			{
				Title = txtTitle.Text,
				Description = txtDescription.Text,
				Tasks = _tasks,
				MentorId = UserService.Instance.CurrentUser.Id
			};

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}