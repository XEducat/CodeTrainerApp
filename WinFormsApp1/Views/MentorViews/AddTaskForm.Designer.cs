using Font = System.Drawing.Font;

namespace CodeTrainerApp.Views.MentorViews
{
	partial class AddTaskForm
	{
		private System.ComponentModel.IContainer components = null;

		private TextBox txtTitle;
		private TextBox txtDescription;
		private RichTextBox txtCode;

		private Label lblExample;

		private Label lblCall;
		private Label lblExpected;

		private TextBox txtCall;
		private TextBox txtExpected;

		private Button btnAddTest;
		private Button btnUpdateTest;
		private Button btnCreate;

		private ListBox lbTests;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			txtTitle = new TextBox();
			txtDescription = new TextBox();
			txtCode = new RichTextBox();

			lblExample = new Label();

			lblCall = new Label();
			lblExpected = new Label();

			txtCall = new TextBox();
			txtExpected = new TextBox();

			btnAddTest = new Button();
			btnUpdateTest = new Button();
			btnCreate = new Button();

			lbTests = new ListBox();

			SuspendLayout();

			// Title
			txtTitle.Location = new Point(20, 20);
			txtTitle.Size = new Size(300, 23);
			txtTitle.PlaceholderText = "Назва задачі";

			// Description
			txtDescription.Location = new Point(20, 60);
			txtDescription.Size = new Size(300, 23);
			txtDescription.PlaceholderText = "Постановка задачі";

			// Code editor
			txtCode.Font = new Font("Consolas", 10);
			txtCode.Location = new Point(20, 120);
			txtCode.Size = new Size(400, 150);
			txtCode.Text =
@"public class Solution
{

}";

			// Example
			lblExample.Font = new Font("Consolas", 10);
			lblExample.Location = new Point(450, 120);
			lblExample.Size = new Size(300, 150);
			lblExample.Text =
"Приклад початкового коду:\n\n" +
"public class Solution\n" +
"{\n" +
"    public int Sum(int a, int b)\n" +
"    {\n" +
"        // ваш код\n" +
"    }\n" +
"}";

			// Call label
			lblCall.Location = new Point(450, 290);
			lblCall.Size = new Size(200, 20);
			lblCall.Text = "Виклик тесту";

			// Call textbox
			txtCall.Location = new Point(450, 310);
			txtCall.Size = new Size(250, 23);
			txtCall.PlaceholderText = "new Solution().Sum(5,10)";

			// Expected label
			lblExpected.Location = new Point(450, 340);
			lblExpected.Size = new Size(200, 20);
			lblExpected.Text = "Очікуваний результат";

			// Expected textbox
			txtExpected.Location = new Point(450, 360);
			txtExpected.Size = new Size(250, 23);
			txtExpected.PlaceholderText = "15";

			// Add test button
			btnAddTest.Location = new Point(450, 400);
			btnAddTest.Size = new Size(120, 30);
			btnAddTest.Text = "Додати тест";
			btnAddTest.Click += btnAddTest_Click;

			// Update test button
			btnUpdateTest.Location = new Point(580, 400);
			btnUpdateTest.Size = new Size(120, 30);
			btnUpdateTest.Text = "Оновити тест";
			btnUpdateTest.Click += btnUpdateTest_Click;

			// Tests list
			lbTests.Location = new Point(20, 290);
			lbTests.Size = new Size(400, 120);
			lbTests.SelectedIndexChanged += lbTests_SelectedIndexChanged;

			// Create button
			btnCreate.Location = new Point(20, 430);
			btnCreate.Size = new Size(150, 30);
			btnCreate.Text = "Завершити";
			btnCreate.Click += btnCreate_Click;

			// Form
			ClientSize = new Size(800, 480);

			Controls.Add(txtTitle);
			Controls.Add(txtDescription);
			Controls.Add(txtCode);
			Controls.Add(lblExample);

			Controls.Add(lblCall);
			Controls.Add(txtCall);
			Controls.Add(lblExpected);
			Controls.Add(txtExpected);

			Controls.Add(lbTests);
			Controls.Add(btnAddTest);
			Controls.Add(btnUpdateTest);
			Controls.Add(btnCreate);

			Text = "Add Task";

			ResumeLayout(false);
			PerformLayout();
		}
	}
}