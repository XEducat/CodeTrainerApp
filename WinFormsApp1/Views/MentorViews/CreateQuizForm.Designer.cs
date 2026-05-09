using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views.MentorViews
{
	partial class CreateQuizForm
	{
		private System.ComponentModel.IContainer components = null;

		private TextBox txtTitle;
		private TextBox txtDescription;

		private Button btnAddTask;
		private Button btnDeleteTask;
		private Button btnCreateQuiz;

		private ListBox lbTasks;
		private ErrorProvider errorProvider1;

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			txtTitle = new TextBox();
			txtDescription = new TextBox();

			btnAddTask = new Button();
			btnDeleteTask = new Button();
			btnCreateQuiz = new Button();

			lbTasks = new ListBox();
			errorProvider1 = new ErrorProvider(components);

			((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
			SuspendLayout();

			// Title
			txtTitle.Location = new Point(30, 25);
			txtTitle.Size = new Size(520, 25);
			txtTitle.PlaceholderText = "Назва квіза (напр. Основи C#)";
			txtTitle.TextChanged += ValidateForm;

			// Description
			txtDescription.Location = new Point(30, 65);
			txtDescription.Size = new Size(520, 25);
			txtDescription.PlaceholderText = "Короткий опис квіза...";
			txtDescription.TextChanged += ValidateForm;

			// Tasks list label
			Label lblTasks = new Label();
			lblTasks.Text = "Список задач:";
			lblTasks.Location = new Point(30, 105);
			lblTasks.AutoSize = true;

			// Tasks list
			lbTasks.Location = new Point(30, 130);
			lbTasks.Size = new Size(520, 220);
			lbTasks.DoubleClick += lbTasks_DoubleClick;
			lbTasks.SelectedIndexChanged += ValidateForm;

			// Add Task
			btnAddTask.Text = "➕ Додати задачу";
			btnAddTask.Location = new Point(30, 370);
			btnAddTask.Size = new Size(150, 35);
			btnAddTask.Click += btnAddTask_Click;

			// Delete Task
			btnDeleteTask.Text = "🗑 Видалити";
			btnDeleteTask.Location = new Point(190, 370);
			btnDeleteTask.Size = new Size(120, 35);
			btnDeleteTask.Click += btnDeleteTask_Click;

			// Create quiz
			btnCreateQuiz.Text = "💾 Зберегти квіз";
			btnCreateQuiz.Location = new Point(400, 370);
			btnCreateQuiz.Size = new Size(150, 35);
			btnCreateQuiz.Click += btnCreateQuiz_Click;

			Controls.Add(txtTitle);
			Controls.Add(txtDescription);
			Controls.Add(lblTasks);
			Controls.Add(lbTasks);
			Controls.Add(btnAddTask);
			Controls.Add(btnDeleteTask);
			Controls.Add(btnCreateQuiz);

			Text = "Редактор квізу";
			ClientSize = new Size(580, 430);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			StartPosition = FormStartPosition.CenterParent;
			MaximizeBox = false;

			((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
			ApplyModernStyles();
			ResumeLayout(false);
			PerformLayout();
		}

		private void ApplyModernStyles()
		{
			this.BackColor = Theme.Background;
			this.Font = new Font("Segoe UI", 9F);

			StyleHelper.ApplyPrimaryButton(btnAddTask);
			StyleHelper.ApplyDangerButton(btnDeleteTask);
			StyleHelper.ApplySuccessButton(btnCreateQuiz);

			lbTasks.BackColor = Color.White;
			lbTasks.BorderStyle = BorderStyle.FixedSingle;
		}
	}
}