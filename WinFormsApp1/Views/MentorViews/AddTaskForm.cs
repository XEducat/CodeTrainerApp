using CodeTrainerApp.Model;

namespace CodeTrainerApp.Views.MentorViews
{
	public partial class AddTaskForm : Form
	{
		public ProgrammingTask CreatedTask { get; private set; } = new ProgrammingTask();

		private List<TestCase> _tests = new();
		private int selectedTestIndex = -1;
		private bool _isEditing = false; // прапорець редагування

		public AddTaskForm(ProgrammingTask? task = null)
		{
			InitializeComponent();

			if (task != null)
			{
				_isEditing = true;
				CreatedTask = task; // зберігаємо посилання на оригінальний об'єкт

				txtTitle.Text = task.Title;
				txtDescription.Text = task.Description;
				txtCode.Text = task.CodeTemplate;

				_tests = new List<TestCase>(task.Tests); // копія для редагування
				foreach (var test in _tests)
				{
					lbTests.Items.Add($"{test.Call} -> {test.Expected}");
				}
			}
		}

		// Tab у редакторі коду
		private void TxtCode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Tab)
			{
				e.SuppressKeyPress = true;
				int pos = txtCode.SelectionStart;
				txtCode.Text = txtCode.Text.Insert(pos, "\t");
				txtCode.SelectionStart = pos + 1;
			}
		}

		// Додавання тесту
		private void btnAddTest_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtCall.Text) || string.IsNullOrWhiteSpace(txtExpected.Text))
			{
				MessageBox.Show("Введіть Call і Expected");
				return;
			}

			var test = new TestCase
			{
				Call = txtCall.Text,
				Expected = txtExpected.Text
			};

			_tests.Add(test);
			lbTests.Items.Add($"{test.Call} -> {test.Expected}");

			txtCall.Clear();
			txtExpected.Clear();
			selectedTestIndex = -1;
		}

		// Вибір тесту зі списку
		private void lbTests_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lbTests.SelectedIndex == -1) return;

			selectedTestIndex = lbTests.SelectedIndex;
			var test = _tests[selectedTestIndex];

			txtCall.Text = test.Call;
			txtExpected.Text = test.Expected;
		}

		// Оновлення тесту
		private void btnUpdateTest_Click(object sender, EventArgs e)
		{
			if (selectedTestIndex == -1)
			{
				MessageBox.Show("Оберіть тест зі списку");
				return;
			}

			if (string.IsNullOrWhiteSpace(txtCall.Text) || string.IsNullOrWhiteSpace(txtExpected.Text))
			{
				MessageBox.Show("Заповніть Call і Expected");
				return;
			}

			_tests[selectedTestIndex] = new TestCase
			{
				Call = txtCall.Text,
				Expected = txtExpected.Text
			};

			lbTests.Items[selectedTestIndex] = $"{txtCall.Text} -> {txtExpected.Text}";
		}

		// Створення або редагування задачі
		private void btnCreate_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtDescription.Text) || string.IsNullOrWhiteSpace(txtCode.Text))
			{
				MessageBox.Show("Заповніть всі поля");
				return;
			}

			if (_tests.Count == 0)
			{
				MessageBox.Show("Додайте хоча б один тест");
				return;
			}

			if (_isEditing)
			{
				// Оновлюємо існуючий об'єкт
				CreatedTask.Title = txtTitle.Text;
				CreatedTask.Description = txtDescription.Text;
				CreatedTask.CodeTemplate = txtCode.Text;
				CreatedTask.Tests = new List<TestCase>(_tests);
			}
			else
			{
				// Створюємо новий
				CreatedTask = new ProgrammingTask
				{
					Title = txtTitle.Text,
					Description = txtDescription.Text,
					CodeTemplate = txtCode.Text,
					Tests = new List<TestCase>(_tests)
				};
			}

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}