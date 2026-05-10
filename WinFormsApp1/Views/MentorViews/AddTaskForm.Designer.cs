using CodeTrainerApp.UI;
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

		private GroupBox gbInfo;
		private GroupBox gbCode;
		private GroupBox gbTests;
		private Panel pnlBottom;

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			
			gbInfo = new GroupBox();
			gbCode = new GroupBox();
			gbTests = new GroupBox();
			pnlBottom = new Panel();

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

			// gbInfo
			gbInfo.Text = "Основна інформація";
			gbInfo.Location = new Point(20, 10);
			gbInfo.Size = new Size(760, 100);
			gbInfo.Controls.Add(txtTitle);
			gbInfo.Controls.Add(txtDescription);

			txtTitle.Location = new Point(15, 25);
			txtTitle.Size = new Size(730, 23);
			txtTitle.PlaceholderText = "Назва задачі (напр. Сума двох чисел)";
			txtTitle.TextChanged += ValidateForm;

			txtDescription.Location = new Point(15, 60);
			txtDescription.Size = new Size(730, 23);
			txtDescription.PlaceholderText = "Постановка задачі та умови...";
			txtDescription.TextChanged += ValidateForm;

			// gbCode
			gbCode.Text = "Редактор коду";
			gbCode.Location = new Point(20, 120);
			gbCode.Size = new Size(760, 220);
			gbCode.Controls.Add(txtCode);
			gbCode.Controls.Add(lblExample);

			txtCode.Font = new Font("Consolas", 10);
			txtCode.Location = new Point(15, 25);
			txtCode.Size = new Size(450, 180);
			txtCode.Text = "public class Solution\n{\n\n}";
			txtCode.KeyDown += TxtCode_KeyDown;

			lblExample.Font = new Font("Segoe UI", 8F);
			lblExample.ForeColor = Theme.TextSecondary;
			lblExample.Location = new Point(480, 25);
			lblExample.Size = new Size(265, 180);
			lblExample.Text = "Приклад структури:\n\npublic class Solution\n{\n    public int Sum(int a, int b)\n    {\n        return a + b;\n    }\n}\n\n* Використовуйте class Solution обов'язково.";

			// gbTests
			gbTests.Text = "Тестові випадки";
			gbTests.Location = new Point(20, 350);
			gbTests.Size = new Size(760, 160);
			gbTests.Controls.Add(lbTests);
			gbTests.Controls.Add(lblCall);
			gbTests.Controls.Add(txtCall);
			gbTests.Controls.Add(lblExpected);
			gbTests.Controls.Add(txtExpected);
			gbTests.Controls.Add(btnAddTest);
			gbTests.Controls.Add(btnUpdateTest);
			gbTests.Controls.Add(btnDeleteTest);
			gbTests.Controls.Add(btnClearTest);

			lbTests.Location = new Point(15, 25);
			lbTests.Size = new Size(300, 120);
			lbTests.SelectedIndexChanged += lbTests_SelectedIndexChanged;

			lblCall.Location = new Point(330, 20);
			lblCall.Size = new Size(100, 20);
			lblCall.Text = "Виклик:";
			
			txtCall.Location = new Point(330, 40);
			txtCall.Size = new Size(200, 23);
			txtCall.PlaceholderText = "new Solution().Sum(2, 2)";
			txtCall.TextChanged += ValidateTestInputs;

			lblExpected.Location = new Point(540, 20);
			lblExpected.Size = new Size(100, 20);
			lblExpected.Text = "Очікуємо:";

			txtExpected.Location = new Point(540, 40);
			txtExpected.Size = new Size(150, 23);
			txtExpected.PlaceholderText = "4";
			txtExpected.TextChanged += ValidateTestInputs;

			btnAddTest.Location = new Point(330, 75);
			btnAddTest.Size = new Size(100, 30);
			btnAddTest.Text = "Додати";
			btnAddTest.Click += btnAddTest_Click;

			btnUpdateTest.Location = new Point(440, 75);
			btnUpdateTest.Size = new Size(100, 30);
			btnUpdateTest.Text = "Оновити";
			btnUpdateTest.Click += btnUpdateTest_Click;

			btnDeleteTest.Location = new Point(550, 75);
			btnDeleteTest.Size = new Size(100, 30);
			btnDeleteTest.Text = "Видалити";
			btnDeleteTest.Click += btnDeleteTest_Click;

			btnClearTest.Location = new Point(330, 115);
			btnClearTest.Size = new Size(100, 30);
			btnClearTest.Text = "Очистити";
			btnClearTest.Click += btnClearTest_Click;

			// pnlBottom
			pnlBottom.Dock = DockStyle.Bottom;
			pnlBottom.Height = 60;
			pnlBottom.BackColor = Theme.Surface;
			pnlBottom.Controls.Add(btnCreate);
			pnlBottom.Controls.Add(btnVerify);
			pnlBottom.Controls.Add(lblStatus);

			btnCreate.Location = new Point(20, 15);
			btnCreate.Size = new Size(150, 30);
			btnCreate.Text = "Зберегти задачу";
			btnCreate.Click += btnCreate_Click;

			btnVerify.Location = new Point(180, 15);
			btnVerify.Size = new Size(150, 30);
			btnVerify.Text = "Перевірити код";
			btnVerify.Click += btnVerify_Click;

			lblStatus.Location = new Point(340, 20);
			lblStatus.Size = new Size(300, 20);
			lblStatus.Text = "⚠️ Потрібна перевірка";
			lblStatus.TextAlign = ContentAlignment.MiddleLeft;

			// Form
			ClientSize = new Size(800, 580);
			Text = "Конструктор задачі";
			FormBorderStyle = FormBorderStyle.FixedDialog;
			StartPosition = FormStartPosition.CenterParent;
			MaximizeBox = false;

			Controls.Add(gbInfo);
			Controls.Add(gbCode);
			Controls.Add(gbTests);
			Controls.Add(pnlBottom);

			((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
			ApplyModernStyles();
			ResumeLayout(false);
		}

		private void ApplyModernStyles()
		{
			this.Font = new Font("Segoe UI", 9F);

			StyleHelper.ApplyPrimaryButton(btnVerify);
			StyleHelper.ApplySuccessButton(btnCreate);

			StyleHelper.ApplyPrimaryButton(btnAddTest);
			StyleHelper.ApplyPrimaryButton(btnUpdateTest);
			StyleHelper.ApplyDangerButton(btnDeleteTest);

			StyleHelper.ApplyMenuButton(btnClearTest);
			btnClearTest.FlatStyle = FlatStyle.Flat;
			btnClearTest.FlatAppearance.BorderSize = 1;
			btnClearTest.FlatAppearance.BorderColor = Theme.Border;
			btnClearTest.BackColor = Theme.Surface;
			btnClearTest.ForeColor = Theme.TextPrimary;
			btnClearTest.Cursor = Cursors.Hand;

			txtCode.BackColor = Theme.CodeBackground;
			txtCode.ForeColor = Theme.CodeForeground;

			lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			
			StyleHelper.ApplyFormStyle(this);
		}
	}
}