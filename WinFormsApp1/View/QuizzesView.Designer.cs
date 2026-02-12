namespace CodeTrainerApp.View
{
	partial class QuizzesView
	{
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListBox QuizListBox;
		private System.Windows.Forms.Button StartQuizButton;
		private System.Windows.Forms.Button LoginButton;
		private System.Windows.Forms.Button BackButton;
		private System.Windows.Forms.Label UserInfoLabel;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.QuizListBox = new System.Windows.Forms.ListBox();
			this.StartQuizButton = new System.Windows.Forms.Button();
			this.LoginButton = new System.Windows.Forms.Button();
			this.BackButton = new System.Windows.Forms.Button();
			this.UserInfoLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();

			// QuizListBox
			this.QuizListBox.FormattingEnabled = true;
			this.QuizListBox.ItemHeight = 16;
			this.QuizListBox.Location = new System.Drawing.Point(30, 60);
			this.QuizListBox.Size = new System.Drawing.Size(400, 260);
			this.QuizListBox.DoubleClick += new System.EventHandler(this.QuizListBox_DoubleClick);

			// StartQuizButton
			this.StartQuizButton.Location = new System.Drawing.Point(450, 60);
			this.StartQuizButton.Size = new System.Drawing.Size(150, 40);
			this.StartQuizButton.Text = "Почати тест";
			this.StartQuizButton.Click += new System.EventHandler(this.StartQuizButton_Click);

			// LoginButton
			this.LoginButton.Location = new System.Drawing.Point(650, 20);
			this.LoginButton.Size = new System.Drawing.Size(120, 35);
			this.LoginButton.Text = "Увійти";
			this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);

			// BackButton
			this.BackButton.Location = new System.Drawing.Point(450, 120);
			this.BackButton.Size = new System.Drawing.Size(150, 40);
			this.BackButton.Text = "Назад";
			this.BackButton.Click += new System.EventHandler(this.BackButton_Click);

			// UserInfoLabel
			this.UserInfoLabel.Location = new System.Drawing.Point(30, 20);
			this.UserInfoLabel.Size = new System.Drawing.Size(400, 30);
			this.UserInfoLabel.Text = "Гість";

			// QuizzesView
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 400);
			this.Controls.Add(this.QuizListBox);
			this.Controls.Add(this.StartQuizButton);
			this.Controls.Add(this.LoginButton);
			this.Controls.Add(this.BackButton);
			this.Controls.Add(this.UserInfoLabel);
			this.Text = "CodeTrainer - Тести";
			this.ResumeLayout(false);
		}
	}
}
