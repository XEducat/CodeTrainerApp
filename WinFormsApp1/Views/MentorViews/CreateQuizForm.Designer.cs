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
			txtTitle.Location = new Point(30, 30);
			txtTitle.Size = new Size(300, 25);
			txtTitle.PlaceholderText = "Назва квіза";
			txtTitle.TextChanged += ValidateForm;

			// Description
			txtDescription.Location = new Point(30, 70);
			txtDescription.Size = new Size(300, 25);
			txtDescription.PlaceholderText = "Опис";
			txtDescription.TextChanged += ValidateForm;

			// Tasks list
			lbTasks.Location = new Point(30, 120);
			lbTasks.Size = new Size(400, 200);
			lbTasks.DoubleClick += lbTasks_DoubleClick;
			lbTasks.SelectedIndexChanged += ValidateForm;

			// Add Task
			btnAddTask.Text = "Додати задачу";
			btnAddTask.Location = new Point(30, 340);
			btnAddTask.Size = new Size(120, 30);
			btnAddTask.Click += btnAddTask_Click;

			// Delete Task
			btnDeleteTask.Text = "Видалити задачу";
			btnDeleteTask.Location = new Point(160, 340);
			btnDeleteTask.Size = new Size(120, 30);
			btnDeleteTask.Click += btnDeleteTask_Click;

			// Create quiz
			btnCreateQuiz.Text = "Створити";
			btnCreateQuiz.Location = new Point(310, 340);
			btnCreateQuiz.Size = new Size(120, 30);
			btnCreateQuiz.Click += btnCreateQuiz_Click;

			Controls.Add(txtTitle);
			Controls.Add(txtDescription);
			Controls.Add(lbTasks);
			Controls.Add(btnAddTask);
			Controls.Add(btnDeleteTask);
			Controls.Add(btnCreateQuiz);

			Text = "Створення квізу";
			ClientSize = new Size(500, 400);

			((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}