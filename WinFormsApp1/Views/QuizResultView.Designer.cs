using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views
{
	partial class QuizResultView
	{
		private System.ComponentModel.IContainer components = null;
		private Panel MainPanel;
		private Label TitleLabel;
		private Label ScoreLabel;
		private Label MessageLabel;
		private Button FinishButton;
		private Label ResultIconLabel;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			MainPanel = new Panel();
			TitleLabel = new Label();
			ScoreLabel = new Label();
			MessageLabel = new Label();
			FinishButton = new Button();
			ResultIconLabel = new Label();

			MainPanel.SuspendLayout();
			SuspendLayout();

			// MainPanel
			MainPanel.BackColor = Theme.Surface;
			MainPanel.Controls.Add(ResultIconLabel);
			MainPanel.Controls.Add(TitleLabel);
			MainPanel.Controls.Add(ScoreLabel);
			MainPanel.Controls.Add(MessageLabel);
			MainPanel.Controls.Add(FinishButton);
			MainPanel.Dock = DockStyle.Fill;
			MainPanel.Location = new Point(0, 0);
			MainPanel.Name = "MainPanel";
			MainPanel.Padding = new Padding(30);
			MainPanel.Size = new Size(450, 400);

			// ResultIconLabel
			ResultIconLabel.Font = new Font("Segoe UI", 48F);
			ResultIconLabel.Location = new Point(30, 40);
			ResultIconLabel.Name = "ResultIconLabel";
			ResultIconLabel.Size = new Size(390, 80);
			ResultIconLabel.Text = "🎉";
			ResultIconLabel.TextAlign = ContentAlignment.MiddleCenter;

			// TitleLabel
			TitleLabel.Font = new Font("Segoe UI Semibold", 20F);
			TitleLabel.ForeColor = Theme.TextPrimary;
			TitleLabel.Location = new Point(30, 130);
			TitleLabel.Name = "TitleLabel";
			TitleLabel.Size = new Size(390, 45);
			TitleLabel.Text = "Квіз завершено!";
			TitleLabel.TextAlign = ContentAlignment.MiddleCenter;

			// ScoreLabel
			ScoreLabel.Font = new Font("Segoe UI Bold", 36F);
			ScoreLabel.ForeColor = Theme.Primary;
			ScoreLabel.Location = new Point(30, 185);
			ScoreLabel.Name = "ScoreLabel";
			ScoreLabel.Size = new Size(390, 70);
			ScoreLabel.Text = "10 / 10";
			ScoreLabel.TextAlign = ContentAlignment.MiddleCenter;

			// MessageLabel
			MessageLabel.Font = new Font("Segoe UI", 12F);
			MessageLabel.ForeColor = Theme.TextSecondary;
			MessageLabel.Location = new Point(30, 265);
			MessageLabel.Name = "MessageLabel";
			MessageLabel.Size = new Size(390, 60);
			MessageLabel.Text = "Чудовий результат! Ви справжній майстер!";
			MessageLabel.TextAlign = ContentAlignment.TopCenter;

			// FinishButton
			FinishButton.Location = new Point(125, 335);
			FinishButton.Name = "FinishButton";
			FinishButton.Size = new Size(200, 45);
			FinishButton.Text = "Завершити";
			FinishButton.Click += FinishButton_Click;

			// QuizResultView
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(450, 410);
			Controls.Add(MainPanel);
			FormBorderStyle = FormBorderStyle.None;
			Name = "QuizResultView";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Результат квізу";
			MainPanel.ResumeLayout(false);
			ApplyModernStyles();
			ResumeLayout(false);
		}

		private void ApplyModernStyles()
		{
			MainPanel.BackColor = Theme.Surface;
			TitleLabel.ForeColor = Theme.TextPrimary;
			ScoreLabel.ForeColor = Theme.Primary;
			MessageLabel.ForeColor = Theme.TextSecondary;
			ResultIconLabel.ForeColor = Theme.Primary;

			StyleHelper.ApplyPrimaryButton(FinishButton);

			// Сучасна рамка
			MainPanel.Paint += (s, e) =>
			{
				using (var pen = new Pen(Theme.Border, 2))
				{
					e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
					e.Graphics.DrawRectangle(pen, 1, 1, MainPanel.Width - 2, MainPanel.Height - 2);
				}
			};
		}
	}
}