using System.Drawing;
using System.Windows.Forms;

namespace CodeTrainerApp.View
{
	partial class QuizzesView
	{
		private System.ComponentModel.IContainer components = null;
		private ListBox QuizListBox;
		private Button StartQuizButton;
		private Button LoginButton;
		private Panel MainPanel;
		private Label TitleLabel;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			QuizListBox = new ListBox();
			StartQuizButton = new Button();
			LoginButton = new Button();
			MainPanel = new Panel();
			TitleLabel = new Label();
			MainPanel.SuspendLayout();
			SuspendLayout();
			// 
			// QuizListBox
			// 
			QuizListBox.BackColor = Color.FromArgb(248, 249, 250);
			QuizListBox.DrawMode = DrawMode.OwnerDrawFixed;
			QuizListBox.Font = new Font("Segoe UI", 11F);
			QuizListBox.ForeColor = Color.FromArgb(33, 37, 41);
			QuizListBox.ItemHeight = 80;
			QuizListBox.Location = new Point(24, 80);
			QuizListBox.Name = "QuizListBox";
			QuizListBox.Size = new Size(740, 164);
			QuizListBox.TabIndex = 2;
			QuizListBox.DrawItem += QuizListBox_DrawItem;
			QuizListBox.DoubleClick += QuizListBox_DoubleClick;
			// 
			// StartQuizButton
			// 
			StartQuizButton.BackColor = Color.FromArgb(40, 167, 69);
			StartQuizButton.FlatAppearance.BorderSize = 0;
			StartQuizButton.FlatStyle = FlatStyle.Flat;
			StartQuizButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			StartQuizButton.ForeColor = Color.White;
			StartQuizButton.Location = new Point(24, 340);
			StartQuizButton.Name = "StartQuizButton";
			StartQuizButton.Size = new Size(200, 45);
			StartQuizButton.TabIndex = 3;
			StartQuizButton.Text = "🚀 Почати тест";
			StartQuizButton.UseVisualStyleBackColor = false;
			StartQuizButton.Click += StartQuizButton_Click;
			// 
			// LoginButton
			// 
			LoginButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			LoginButton.BackColor = Color.FromArgb(108, 117, 125);
			LoginButton.FlatAppearance.BorderSize = 0;
			LoginButton.FlatStyle = FlatStyle.Flat;
			LoginButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			LoginButton.ForeColor = Color.White;
			LoginButton.Location = new Point(1210, 25);
			LoginButton.Name = "LoginButton";
			LoginButton.Size = new Size(160, 36);
			LoginButton.TabIndex = 1;
			LoginButton.Text = "👤 Увійти";
			LoginButton.UseVisualStyleBackColor = false;
			LoginButton.Click += LoginButton_Click;
			// 
			// MainPanel
			// 
			MainPanel.BackColor = Color.White;
			MainPanel.Controls.Add(TitleLabel);
			MainPanel.Controls.Add(LoginButton);
			MainPanel.Controls.Add(QuizListBox);
			MainPanel.Controls.Add(StartQuizButton);
			MainPanel.Dock = DockStyle.Fill;
			MainPanel.Location = new Point(0, 0);
			MainPanel.Name = "MainPanel";
			MainPanel.Padding = new Padding(24);
			MainPanel.Size = new Size(800, 420);
			MainPanel.TabIndex = 0;
			// 
			// TitleLabel
			// 
			TitleLabel.AutoSize = true;
			TitleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			TitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
			TitleLabel.Location = new Point(24, 20);
			TitleLabel.Name = "TitleLabel";
			TitleLabel.Size = new Size(213, 37);
			TitleLabel.TabIndex = 0;
			TitleLabel.Text = "Доступні квізи";
			// 
			// QuizzesView
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(245, 246, 250);
			ClientSize = new Size(800, 420);
			Controls.Add(MainPanel);
			Name = "QuizzesView";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Code Trainer - Квізи";
			MainPanel.ResumeLayout(false);
			MainPanel.PerformLayout();
			ResumeLayout(false);
		}
	}
}
