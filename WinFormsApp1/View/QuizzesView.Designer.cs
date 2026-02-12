using CodeTrainerApp.Model;
using System.Drawing;
using System.Windows.Forms;

namespace CodeTrainerApp.View
{
	partial class QuizzesView
	{
		private System.ComponentModel.IContainer components = null;
		private ListBox QuizListBox;
		private Button LoginButton;
		private Panel MainPanel;
		private Label TitleLabel;
		private TableLayoutPanel HeaderPanel;
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

			Color bgColor = isSelected
				? Color.FromArgb(233, 236, 239)
				: Color.White;

			using (var bgBrush = new SolidBrush(bgColor))
			{
				e.Graphics.FillRectangle(bgBrush, e.Bounds);
			}

			// Назва
			using (var titleFont = new Font("Segoe UI", 12, FontStyle.Bold))
			using (var titleBrush = new SolidBrush(Color.FromArgb(33, 37, 41)))
			{
				e.Graphics.DrawString(
					quiz.Title,
					titleFont,
					titleBrush,
					e.Bounds.Left + 15,
					e.Bounds.Top + 10);
			}

			// Опис
			using (var descFont = new Font("Segoe UI", 9))
			using (var descBrush = new SolidBrush(Color.FromArgb(108, 117, 125)))
			{
				e.Graphics.DrawString(
					quiz.Description,
					descFont,
					descBrush,
					e.Bounds.Left + 15,
					e.Bounds.Top + 40);
			}

			// Кількість запитань справа
			string questionsText = $"Питань: {quiz.Tasks.Count}";

			using (var countFont = new Font("Segoe UI", 11, FontStyle.Bold))
			using (var countBrush = new SolidBrush(Color.FromArgb(40, 167, 69)))
			{
				SizeF textSize = e.Graphics.MeasureString(questionsText, countFont);

				float x = e.Bounds.Right - textSize.Width - 20;
				float y = e.Bounds.Top + (e.Bounds.Height - textSize.Height) / 2;

				e.Graphics.DrawString(
					questionsText,
					countFont,
					countBrush,
					x,
					y);
			}

			e.DrawFocusRectangle();
		}

		private void InitializeComponent()
		{
			QuizListBox = new ListBox();
			LoginButton = new Button();
			MainPanel = new Panel();
			HeaderPanel = new TableLayoutPanel();
			ButtonPanel = new FlowLayoutPanel();
			TitleLabel = new Label();
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
			// LoginButton
			// 
			LoginButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			LoginButton.BackColor = Color.FromArgb(108, 117, 125);
			LoginButton.FlatAppearance.BorderSize = 0;
			LoginButton.FlatStyle = FlatStyle.Flat;
			LoginButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			LoginButton.ForeColor = Color.White;
			LoginButton.Location = new Point(0, 0);
			LoginButton.Margin = new Padding(0);
			LoginButton.Name = "LoginButton";
			LoginButton.RightToLeft = RightToLeft.No;
			LoginButton.Size = new Size(120, 40);
			LoginButton.TabIndex = 0;
			LoginButton.Text = "👤 Увійти";
			LoginButton.UseVisualStyleBackColor = false;
			LoginButton.Click += LoginButton_Click;
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
			HeaderPanel.ColumnCount = 2;
			HeaderPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			HeaderPanel.ColumnStyles.Add(new ColumnStyle());
			HeaderPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
			HeaderPanel.Controls.Add(ButtonPanel, 1, 0);
			HeaderPanel.Controls.Add(TitleLabel, 0, 0);
			HeaderPanel.Dock = DockStyle.Top;
			HeaderPanel.Location = new Point(20, 20);
			HeaderPanel.Margin = new Padding(0);
			HeaderPanel.Name = "HeaderPanel";
			HeaderPanel.Padding = new Padding(0, 10, 0, 10);
			HeaderPanel.RowCount = 1;
			HeaderPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			HeaderPanel.Size = new Size(1240, 68);
			HeaderPanel.TabIndex = 1;
			// 
			// ButtonPanel
			// 
			ButtonPanel.AutoSize = true;
			ButtonPanel.Controls.Add(LoginButton);
			ButtonPanel.Dock = DockStyle.Fill;
			ButtonPanel.FlowDirection = FlowDirection.RightToLeft;
			ButtonPanel.Location = new Point(1117, 13);
			ButtonPanel.Name = "ButtonPanel";
			ButtonPanel.Size = new Size(120, 42);
			ButtonPanel.TabIndex = 1;
			// 
			// TitleLabel
			// 
			TitleLabel.Anchor = AnchorStyles.Left;
			TitleLabel.AutoSize = true;
			TitleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			TitleLabel.ForeColor = Color.FromArgb(33, 37, 41);
			TitleLabel.Location = new Point(3, 19);
			TitleLabel.Name = "TitleLabel";
			TitleLabel.Size = new Size(223, 30);
			TitleLabel.TabIndex = 0;
			TitleLabel.Text = "Code Trainer - Квізи";
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
