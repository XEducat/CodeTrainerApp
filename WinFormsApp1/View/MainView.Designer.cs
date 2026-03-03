using CodeTrainerApp.Model;
using System.Drawing;
using System.Windows.Forms;

namespace CodeTrainerApp.View
{
	partial class MainView
	{
		private System.ComponentModel.IContainer components = null;
		private ListBox QuizListBox;
		private Button ProfileButton;
		private Button CabinetButton;
		private Panel MainPanel;
		private Label TitleLabel;
		private Panel HeaderPanel;
		private FlowLayoutPanel ButtonPanel;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();
			base.Dispose(disposing);
		}

		private void QuizListBox_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index < 0) return;
			var quiz = (Quiz)QuizListBox.Items[e.Index];
			e.DrawBackground();

			bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
			Color bgColor = isSelected ? Color.FromArgb(233, 236, 239) : Color.White;

			using (var bgBrush = new SolidBrush(bgColor))
				e.Graphics.FillRectangle(bgBrush, e.Bounds);

			using (var titleFont = new Font("Segoe UI", 12, FontStyle.Bold))
			using (var titleBrush = new SolidBrush(Color.FromArgb(33, 37, 41)))
				e.Graphics.DrawString(quiz.Title, titleFont, titleBrush, e.Bounds.Left + 15, e.Bounds.Top + 10);

			using (var descFont = new Font("Segoe UI", 9))
			using (var descBrush = new SolidBrush(Color.FromArgb(108, 117, 125)))
				e.Graphics.DrawString(quiz.Description, descFont, descBrush, e.Bounds.Left + 15, e.Bounds.Top + 40);

			string questionsText = $"Питань: {quiz.Tasks.Count}";
			using (var countFont = new Font("Segoe UI", 11, FontStyle.Bold))
			using (var countBrush = new SolidBrush(Color.FromArgb(40, 167, 69)))
			{
				SizeF textSize = e.Graphics.MeasureString(questionsText, countFont);
				float x = e.Bounds.Right - textSize.Width - 20;
				float y = e.Bounds.Top + (e.Bounds.Height - textSize.Height) / 2;
				e.Graphics.DrawString(questionsText, countFont, countBrush, x, y);
			}

			e.DrawFocusRectangle();
		}

		private void InitializeComponent()
		{
			QuizListBox = new ListBox();
			ProfileButton = new Button();
			CabinetButton = new Button();
			MainPanel = new Panel();
			HeaderPanel = new Panel();
			TitleLabel = new Label();
			ButtonPanel = new FlowLayoutPanel();
			MainPanel.SuspendLayout();
			HeaderPanel.SuspendLayout();
			ButtonPanel.SuspendLayout();
			SuspendLayout();
			// 
			// QuizListBox
			// 
			QuizListBox.BackColor = Color.FromArgb(248, 249, 250);
			QuizListBox.Dock = DockStyle.Fill;
			QuizListBox.DrawMode = DrawMode.OwnerDrawFixed;
			QuizListBox.Font = new Font("Segoe UI", 11F);
			QuizListBox.ForeColor = Color.FromArgb(33, 37, 41);
			QuizListBox.ItemHeight = 80;
			QuizListBox.Location = new Point(20, 88);
			QuizListBox.Margin = new Padding(0, 10, 0, 10);
			QuizListBox.Name = "QuizListBox";
			QuizListBox.Size = new Size(1240, 612);
			QuizListBox.TabIndex = 0;
			QuizListBox.DrawItem += QuizListBox_DrawItem;
			QuizListBox.DoubleClick += QuizListBox_DoubleClick;
			// 
			// ProfileButton
			// 
			ProfileButton.BackColor = Color.FromArgb(13, 110, 253);
			ProfileButton.FlatAppearance.BorderSize = 0;
			ProfileButton.FlatStyle = FlatStyle.Flat;
			ProfileButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			ProfileButton.ForeColor = Color.White;
			ProfileButton.Location = new Point(145, 0);
			ProfileButton.Margin = new Padding(5, 0, 0, 0);
			ProfileButton.Name = "ProfileButton";
			ProfileButton.Size = new Size(140, 40);
			ProfileButton.TabIndex = 1;
			ProfileButton.Text = "🔐 Увійти";
			ProfileButton.UseVisualStyleBackColor = false;
			ProfileButton.Click += ProfileButton_Click;
			// 
			// CabinetButton
			// 
			CabinetButton.BackColor = Color.FromArgb(25, 135, 84);
			CabinetButton.FlatAppearance.BorderSize = 0;
			CabinetButton.FlatStyle = FlatStyle.Flat;
			CabinetButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			CabinetButton.ForeColor = Color.White;
			CabinetButton.Location = new Point(0, 0);
			CabinetButton.Margin = new Padding(0);
			CabinetButton.Name = "CabinetButton";
			CabinetButton.Size = new Size(140, 40);
			CabinetButton.TabIndex = 0;
			CabinetButton.Text = "🏠 Кабінет";
			CabinetButton.UseVisualStyleBackColor = false;
			CabinetButton.Click += CabinetButton_Click;
			// 
			// MainPanel
			// 
			MainPanel.BackColor = Color.White;
			MainPanel.Controls.Add(QuizListBox);
			MainPanel.Controls.Add(HeaderPanel);
			MainPanel.Dock = DockStyle.Fill;
			MainPanel.Location = new Point(0, 0);
			MainPanel.Name = "MainPanel";
			MainPanel.Padding = new Padding(20);
			MainPanel.Size = new Size(1280, 720);
			MainPanel.TabIndex = 0;
			// 
			// HeaderPanel
			// 
			HeaderPanel.BackColor = Color.FromArgb(245, 246, 250);
			HeaderPanel.Controls.Add(TitleLabel);
			HeaderPanel.Controls.Add(ButtonPanel);
			HeaderPanel.Dock = DockStyle.Top;
			HeaderPanel.Location = new Point(20, 20);
			HeaderPanel.Name = "HeaderPanel";
			HeaderPanel.Padding = new Padding(20, 10, 20, 10);
			HeaderPanel.Size = new Size(1240, 68);
			HeaderPanel.TabIndex = 1;
			// 
			// TitleLabel
			// 
			TitleLabel.Dock = DockStyle.Fill;
			TitleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			TitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
			TitleLabel.Location = new Point(20, 10);
			TitleLabel.Name = "TitleLabel";
			TitleLabel.Size = new Size(915, 48);
			TitleLabel.TabIndex = 0;
			TitleLabel.Text = "Code Trainer - Квізи";
			TitleLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// ButtonPanel
			// 
			ButtonPanel.AutoSize = true;
			ButtonPanel.Controls.Add(CabinetButton);
			ButtonPanel.Controls.Add(ProfileButton);
			ButtonPanel.Dock = DockStyle.Right;
			ButtonPanel.Location = new Point(935, 10);
			ButtonPanel.Name = "ButtonPanel";
			ButtonPanel.Size = new Size(285, 48);
			ButtonPanel.TabIndex = 1;
			ButtonPanel.WrapContents = false;
			// 
			// QuizzesView
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(245, 246, 250);
			ClientSize = new Size(1280, 720);
			Controls.Add(MainPanel);
			Name = "QuizzesView";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Code Trainer - Квізи";
			WindowState = FormWindowState.Maximized;
			Load += QuizzesView_Load;
			MainPanel.ResumeLayout(false);
			HeaderPanel.ResumeLayout(false);
			HeaderPanel.PerformLayout();
			ButtonPanel.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}