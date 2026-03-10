namespace CodeTrainerApp.Views.MentorViews
{
	partial class CreateQuizForm
	{
		private System.ComponentModel.IContainer components = null;

		private TextBox txtTitle;
		private TextBox txtDescription;

		private Button btnAddTask;
		private Button btnCreateQuiz;

		private ListBox lbTasks;

		private void InitializeComponent()
		{
			txtTitle = new TextBox();
			txtDescription = new TextBox();

			btnAddTask = new Button();
			btnCreateQuiz = new Button();

			lbTasks = new ListBox();

			SuspendLayout();

			// Title
			txtTitle.Location = new Point(30, 30);
			txtTitle.Size = new Size(300, 25);
			txtTitle.PlaceholderText = "Назва квіза";

			// Description
			txtDescription.Location = new Point(30, 70);
			txtDescription.Size = new Size(300, 25);
			txtDescription.PlaceholderText = "Опис";

			// Tasks list
			lbTasks.Location = new Point(30, 120);
			lbTasks.Size = new Size(400, 200);
			lbTasks.DoubleClick += lbTasks_DoubleClick;

			// Add Task
			btnAddTask.Text = "Додати задачу";
			btnAddTask.Location = new Point(30, 340);
			btnAddTask.Size = new Size(120, 30);
			btnAddTask.Click += btnAddTask_Click;

			// Create quiz
			btnCreateQuiz.Text = "Створити";
			btnCreateQuiz.Location = new Point(180, 340);
			btnCreateQuiz.Size = new Size(120, 30);
			btnCreateQuiz.Click += btnCreateQuiz_Click;

			Controls.Add(txtTitle);
			Controls.Add(txtDescription);
			Controls.Add(lbTasks);
			Controls.Add(btnAddTask);
			Controls.Add(btnCreateQuiz);

			Text = "Create Quiz";
			ClientSize = new Size(500, 400);

			ResumeLayout(false);
			PerformLayout();
		}
	}
}