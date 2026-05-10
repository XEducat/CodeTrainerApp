using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views.MentorViews
{
	public partial class AddTaskForm : Form
	{
		public ProgrammingTask CreatedTask { get; private set; } = new ProgrammingTask();

		private List<TestCase> _tests = new();
		private int selectedTestIndex = -1;
		private bool _isEditing = false;
		private bool _isVerified = false;

		private const string DefaultStub = @"public class Solution
{

}";

		public AddTaskForm(ProgrammingTask? task = null)
		{
			InitializeComponent();

			Theme.ThemeChanged += OnThemeChanged;
			this.FormClosed += (s, e) => Theme.ThemeChanged -= OnThemeChanged;
			OnThemeChanged();

			if (task != null)
// ... (rest of constructor)
			{
				_isEditing = true;
				CreatedTask = task; 

				txtTitle.Text = task.Title;
				txtDescription.Text = task.Description;
				txtCode.Text = task.CodeTemplate;

				_tests = new List<TestCase>(task.Tests); 
				_isVerified = true; 
			}
			RefreshTestsList();
			ValidateForm(this, EventArgs.Empty);
			ValidateTestInputs(this, EventArgs.Empty);

			txtCode.TextChanged += (s, e) => { _isVerified = false; ValidateForm(s, e); };
		}

		private void OnThemeChanged()
		{
			StyleHelper.ApplyFormStyle(this);
			ApplyModernStyles();
		}

		private void RefreshTestsList()
		{
			lbTests.Items.Clear();
			foreach (var test in _tests)
			{
				lbTests.Items.Add($"{test.Call} -> {test.Expected}");
			}
		}

		private void ValidateForm(object? sender, EventArgs e)
		{
			bool isTitleValid = !string.IsNullOrWhiteSpace(txtTitle.Text) && txtTitle.Text.Length > 3;
			bool isDescriptionValid = !string.IsNullOrWhiteSpace(txtDescription.Text) && txtDescription.Text.Length > 5;
			
			string currentCode = txtCode.Text.Trim().Replace("\r\n", "\n");
			string stub = DefaultStub.Trim().Replace("\r\n", "\n");
			
			bool isNotDefault = !currentCode.Equals(stub, StringComparison.OrdinalIgnoreCase);
			bool isCodeValid = !string.IsNullOrWhiteSpace(txtCode.Text) && txtCode.Text.Contains("class Solution") && isNotDefault;
			bool hasTests = _tests.Count > 0;

			if (!isTitleValid) errorProvider1.SetError(txtTitle, "Назва занадто коротка або порожня");
			else errorProvider1.SetError(txtTitle, "");

			if (!isDescriptionValid) errorProvider1.SetError(txtDescription, "Опис занадто короткий або порожній");
			else errorProvider1.SetError(txtDescription, "");

			if (!isCodeValid) 
			{
				if (!isNotDefault) errorProvider1.SetError(txtCode, "Ви не змінили шаблон коду!");
				else errorProvider1.SetError(txtCode, "Код повинен містити 'class Solution' і не бути порожнім");
			}
			else errorProvider1.SetError(txtCode, "");

			if (!hasTests) errorProvider1.SetError(lbTests, "Додайте хоча б один тест");
			else errorProvider1.SetError(lbTests, "");

			btnVerify.Enabled = isTitleValid && isDescriptionValid && isCodeValid && hasTests;
			btnCreate.Enabled = btnVerify.Enabled && _isVerified;
			
			if (_isVerified)
			{
				lblStatus.Text = "✅ Перевірено";
				lblStatus.ForeColor = Theme.Success;
			}
			else
			{
				lblStatus.Text = "⚠️ Потрібна перевірка";
				lblStatus.ForeColor = Theme.Warning;
			}
		}

		private void ValidateTestInputs(object? sender, EventArgs e)
		{
			bool isCallValid = !string.IsNullOrWhiteSpace(txtCall.Text) && txtCall.Text.Contains("new Solution().");
			bool isExpectedValid = !string.IsNullOrWhiteSpace(txtExpected.Text);

			if (!string.IsNullOrWhiteSpace(txtCall.Text) && !isCallValid)
				errorProvider1.SetError(txtCall, "Виклик повинен починатися з 'new Solution().'");
			else
				errorProvider1.SetError(txtCall, "");

			btnAddTest.Enabled = isCallValid && isExpectedValid;
			btnUpdateTest.Enabled = isCallValid && isExpectedValid && selectedTestIndex != -1;
			btnDeleteTest.Enabled = lbTests.SelectedIndex != -1;
		}

		// Tab у редакторі коду
		private void TxtCode_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Tab)
			{
				e.SuppressKeyPress = true;
				int pos = txtCode.SelectionStart;
				txtCode.Text = txtCode.Text.Insert(pos, "    "); // 4 пробіли замість \t
				txtCode.SelectionStart = pos + 4;
			}
		}

		// Додавання тесту
		private void btnAddTest_Click(object sender, EventArgs e)
		{
			var test = new TestCase
			{
				Call = txtCall.Text,
				Expected = txtExpected.Text
			};

			_tests.Add(test);
			_isVerified = false;
			RefreshTestsList();

			btnClearTest_Click(this, EventArgs.Empty);
			ValidateForm(this, EventArgs.Empty);
		}

		// Вибір тесту зі списку
		private void lbTests_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lbTests.SelectedIndex == -1)
			{
				selectedTestIndex = -1;
				ValidateTestInputs(this, EventArgs.Empty);
				return;
			}

			selectedTestIndex = lbTests.SelectedIndex;
			var test = _tests[selectedTestIndex];

			txtCall.Text = test.Call;
			txtExpected.Text = test.Expected;
			ValidateTestInputs(this, EventArgs.Empty);
		}

		// Оновлення тесту
		private void btnUpdateTest_Click(object sender, EventArgs e)
		{
			if (selectedTestIndex == -1) return;

			_tests[selectedTestIndex] = new TestCase
			{
				Call = txtCall.Text,
				Expected = txtExpected.Text
			};

			_isVerified = false;
			RefreshTestsList();
			ValidateForm(this, EventArgs.Empty);
		}

		private void btnDeleteTest_Click(object sender, EventArgs e)
		{
			if (lbTests.SelectedIndex == -1) return;

			int index = lbTests.SelectedIndex;
			_tests.RemoveAt(index);
			_isVerified = false;
			RefreshTestsList();
			
			btnClearTest_Click(this, EventArgs.Empty);
			ValidateForm(this, EventArgs.Empty);
		}

		private void btnClearTest_Click(object sender, EventArgs e)
		{
			txtCall.Clear();
			txtExpected.Clear();
			lbTests.SelectedIndex = -1;
			selectedTestIndex = -1;
			ValidateTestInputs(this, EventArgs.Empty);
		}

		private async void btnVerify_Click(object sender, EventArgs e)
		{
			btnVerify.Enabled = false;
			lblStatus.Text = "⌛ Перевірка...";
			lblStatus.ForeColor = Theme.Primary;

			var task = new ProgrammingTask
			{
				CodeTemplate = txtCode.Text,
				Tests = _tests
			};

			var result = await CodeCompiler.RunCode(task, txtCode.Text);

			if (result.success)
			{
				_isVerified = true;
				MessageBox.Show("Всі тести успішно пройдено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				_isVerified = false;
				string errorMsg = string.IsNullOrEmpty(result.errorMessage) ? result.output : result.errorMessage;
				MessageBox.Show($"Помилка перевірки:\n\n{errorMsg}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			btnVerify.Enabled = true;
			ValidateForm(this, EventArgs.Empty);
		}

		// Створення або редагування задачі
		private void btnCreate_Click(object sender, EventArgs e)
		{
			if (!_isVerified)
			{
				MessageBox.Show("Будь ласка, спочатку перевірте код!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (_isEditing)
			{
				CreatedTask.Title = txtTitle.Text;
				CreatedTask.Description = txtDescription.Text;
				CreatedTask.CodeTemplate = txtCode.Text;
				CreatedTask.Tests = new List<TestCase>(_tests);
			}
			else
			{
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