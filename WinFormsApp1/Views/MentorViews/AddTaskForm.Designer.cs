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
		private Button btnDeleteTest;
		private Button btnClearTest;
		private Button btnVerify;
		private Button btnCreate;
		private Label lblStatus;

		private ListBox lbTests;
		private ErrorProvider errorProvider1;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
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
			btnDeleteTest = new Button();
			btnClearTest = new Button();
			btnVerify = new Button();
			btnCreate = new Button();
			lblStatus = new Label();

			lbTests = new ListBox();
			errorProvider1 = new ErrorProvider(components);

			((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
			SuspendLayout();

			// Title
			txtTitle.Location = new Point(20, 20);
			txtTitle.Size = new Size(300, 23);
			txtTitle.PlaceholderText = "Назва задачі";
			txtTitle.TextChanged += ValidateForm;

			// Description
			txtDescription.Location = new Point(20, 60);
			txtDescription.Size = new Size(300, 23);
			txtDescription.PlaceholderText = "Постановка задачі";
			txtDescription.TextChanged += ValidateForm;

			// Code editor
			txtCode.Font = new Font("Consolas", 10);
			txtCode.Location = new Point(20, 120);
			txtCode.Size = new Size(400, 150);
			txtCode.Text =
@"public class Solution
{

}";
			txtCode.TextChanged += ValidateForm;
			txtCode.KeyDown += TxtCode_KeyDown;

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
			txtCall.TextChanged += ValidateTestInputs;

			// Expected label
			lblExpected.Location = new Point(450, 340);
			lblExpected.Size = new Size(200, 20);
			lblExpected.Text = "Очікуваний результат";

			// Expected textbox
			txtExpected.Location = new Point(450, 360);
			txtExpected.Size = new Size(250, 23);
			txtExpected.PlaceholderText = "15";
			txtExpected.TextChanged += ValidateTestInputs;

			// Add test button
			btnAddTest.Location = new Point(450, 400);
			btnAddTest.Size = new Size(100, 30);
			btnAddTest.Text = "Додати";
			btnAddTest.Click += btnAddTest_Click;

			// Update test button
			btnUpdateTest.Location = new Point(560, 400);
			btnUpdateTest.Size = new Size(100, 30);
			btnUpdateTest.Text = "Оновити";
			btnUpdateTest.Click += btnUpdateTest_Click;

			// Delete test button
			btnDeleteTest.Location = new Point(670, 400);
			btnDeleteTest.Size = new Size(100, 30);
			btnDeleteTest.Text = "Видалити";
			btnDeleteTest.Click += btnDeleteTest_Click;

			// Clear test button
			btnClearTest.Location = new Point(450, 435);
			btnClearTest.Size = new Size(100, 30);
			btnClearTest.Text = "Очистити";
			btnClearTest.Click += btnClearTest_Click;

			// Tests list
			lbTests.Location = new Point(20, 290);
			lbTests.Size = new Size(400, 120);
			lbTests.SelectedIndexChanged += lbTests_SelectedIndexChanged;

			// Create button
			btnCreate.Location = new Point(20, 430);
			btnCreate.Size = new Size(150, 30);
			btnCreate.Text = "Зберегти задачу";
			btnCreate.Click += btnCreate_Click;

			// Verify button
			btnVerify.Location = new Point(180, 430);
			btnVerify.Size = new Size(150, 30);
			btnVerify.Text = "Перевірити код";
			btnVerify.Click += btnVerify_Click;

			// Status label
			lblStatus.Location = new Point(340, 435);
			lblStatus.Size = new Size(150, 20);
			lblStatus.Text = "⚠️ Потрібна перевірка";

			// Form
			ClientSize = new Size(800, 480);
			Text = "Додавання задачі";

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
			Controls.Add(btnDeleteTest);
			Controls.Add(btnClearTest);
			Controls.Add(btnVerify);
			Controls.Add(btnCreate);
			Controls.Add(lblStatus);

			((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}