using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using CodeTrainerApp.UI;

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
				btnCreateQuiz.Text = "Оновити";
				CreatedQuiz = quiz;

				txtTitle.Text = quiz.Title ?? string.Empty;
				txtDescription.Text = quiz.Description ?? string.Empty;

				if (quiz.Tasks != null && quiz.Tasks.Any())
				{
					_tasks = new List<ProgrammingTask>(quiz.Tasks);

					lbTasks.Items.Clear();
					foreach (var task in _tasks)
					{
						var testCount = task.Tests?.Count ?? 0;
						lbTasks.Items.Add($"{task.Title} ({testCount} tests)");
					}
				}
			}
			ValidateForm(this, EventArgs.Empty);
		}

		private void ValidateForm(object? sender, EventArgs e)
		{
			bool isTitleValid = !string.IsNullOrWhiteSpace(txtTitle.Text) && txtTitle.Text.Length > 3;
			bool isDescriptionValid = !string.IsNullOrWhiteSpace(txtDescription.Text) && txtDescription.Text.Length > 5;
			bool hasTasks = _tasks.Count > 0;

			if (!isTitleValid)
				errorProvider1.SetError(txtTitle, "Назва квіза занадто коротка або порожня (мін. 4 символи)");
			else
				errorProvider1.SetError(txtTitle, "");

			if (!isDescriptionValid)
				errorProvider1.SetError(txtDescription, "Опис занадто короткий або порожній (мін. 6 символів)");
			else
				errorProvider1.SetError(txtDescription, "");

			if (!hasTasks)
				errorProvider1.SetError(lbTasks, "Додайте хоча б одну задачу");
			else
				errorProvider1.SetError(lbTasks, "");

			btnCreateQuiz.Enabled = isTitleValid && isDescriptionValid && hasTasks;
			btnDeleteTask.Enabled = lbTasks.SelectedIndex != -1;
		}

		private void btnAddTask_Click(object sender, EventArgs e)
		{
			var form = new AddTaskForm();

			this.Hide(); 

			var result = form.ShowDialog();

			this.Show(); 

			if (result == DialogResult.OK)
			{
				var task = form.CreatedTask;

				_tasks.Add(task);
				lbTasks.Items.Add($"{task.Title} ({task.Tests.Count} tests)");
				ValidateForm(this, EventArgs.Empty);
			}
		}

		private void btnDeleteTask_Click(object sender, EventArgs e)
		{
			if (lbTasks.SelectedIndex == -1)
				return;

			int index = lbTasks.SelectedIndex;
			_tasks.RemoveAt(index);
			lbTasks.Items.RemoveAt(index);
			ValidateForm(this, EventArgs.Empty);
		}

		private void lbTasks_DoubleClick(object sender, EventArgs e)
		{
			if (lbTasks.SelectedIndex == -1)
				return;

			int index = lbTasks.SelectedIndex;
			var selectedTask = _tasks[index];

			var form = new AddTaskForm(selectedTask);

			this.Hide(); 

			var result = form.ShowDialog();

			this.Show(); 

			if (result == DialogResult.OK)
			{
				_tasks[index] = form.CreatedTask;
				lbTasks.Items[index] = $"{_tasks[index].Title} ({_tasks[index].Tests.Count} tests)";
				ValidateForm(this, EventArgs.Empty);
			}
		}

		private void btnCreateQuiz_Click(object sender, EventArgs e)
		{
			CreatedQuiz.Title = txtTitle.Text;
			CreatedQuiz.Description = txtDescription.Text;
			CreatedQuiz.Tasks = _tasks;
			CreatedQuiz.MentorId = UserService.Instance.CurrentUser.Id;

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}