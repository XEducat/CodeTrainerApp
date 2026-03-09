using System.Drawing;
using System.Windows.Forms;

namespace CodeTrainerApp.Views.MentorViews
{
	partial class MentorQuizzesView
	{
		private System.ComponentModel.IContainer components = null;

		private FlowLayoutPanel _quizPanel;
		private Button _addQuizButton;

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
			_quizPanel = new FlowLayoutPanel();
			_addQuizButton = new Button();
			SuspendLayout();
			// 
			// _quizPanel
			// 
			_quizPanel.AutoScroll = true;
			_quizPanel.Dock = DockStyle.Fill;
			_quizPanel.FlowDirection = FlowDirection.TopDown;
			_quizPanel.Location = new Point(0, 40);
			_quizPanel.Name = "_quizPanel";
			_quizPanel.Padding = new Padding(10);
			_quizPanel.Size = new Size(800, 560);
			_quizPanel.TabIndex = 1;
			_quizPanel.WrapContents = false;
			// 
			// _addQuizButton
			// 
			_addQuizButton.BackColor = Color.MediumSeaGreen;
			_addQuizButton.Dock = DockStyle.Top;
			_addQuizButton.FlatStyle = FlatStyle.Flat;
			_addQuizButton.ForeColor = Color.White;
			_addQuizButton.Location = new Point(0, 0);
			_addQuizButton.Name = "_addQuizButton";
			_addQuizButton.Size = new Size(800, 40);
			_addQuizButton.TabIndex = 0;
			_addQuizButton.Text = "Додати квіз";
			_addQuizButton.UseVisualStyleBackColor = false;
			_addQuizButton.Click += AddQuizButton_Click;
			// 
			// MentorQuizzesView
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 600);
			Controls.Add(_quizPanel);
			Controls.Add(_addQuizButton);
			Name = "MentorQuizzesView";
			Text = "Квізи ментора";
			ResumeLayout(false);
		}
	}
}