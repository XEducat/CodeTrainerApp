using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views
{
	partial class QuizView
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			TopPanel = new Panel();
			QuizTitleLabel = new Label();
			LoginButton = new Button();
			MainSplit = new SplitContainer();
			SidePanel = new Panel();
			ProgressLabel = new Label();
			QuizProgressBar = new ProgressBar();
			TaskDescriptionLabel = new Label();
			CurrentTaskLabel = new Label();
			EditorPanel = new Panel();
			ButtonsPanel = new Panel();
			CheckButton = new Button();
			SkipButton = new Button();
			NextButton = new Button();
			ResultPanel = new Panel();
			ResultTextBox = new RichTextBox();
			ResultHeaderLabel = new Label();
			CodePanel = new Panel();
			CodeTextBox = new RichTextBox();
			CodeHeaderLabel = new Label();
			TopPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)MainSplit).BeginInit();
			MainSplit.Panel1.SuspendLayout();
			MainSplit.Panel2.SuspendLayout();
			MainSplit.SuspendLayout();
			SidePanel.SuspendLayout();
			EditorPanel.SuspendLayout();
			ButtonsPanel.SuspendLayout();
			ResultPanel.SuspendLayout();
			CodePanel.SuspendLayout();
			SuspendLayout();
			// 
			// TopPanel
			// 
			TopPanel.BackColor = Theme.Primary;
			TopPanel.Controls.Add(QuizTitleLabel);
			TopPanel.Controls.Add(LoginButton);
			TopPanel.Dock = DockStyle.Top;
			TopPanel.Location = new Point(0, 0);
			TopPanel.Name = "TopPanel";
			TopPanel.Size = new Size(1100, 60);
			TopPanel.TabIndex = 0;
			// 
			// QuizTitleLabel
			// 
			QuizTitleLabel.AutoSize = true;
			QuizTitleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			QuizTitleLabel.ForeColor = Color.White;
			QuizTitleLabel.Location = new Point(20, 17);
			QuizTitleLabel.Name = "QuizTitleLabel";
			QuizTitleLabel.Size = new Size(251, 25);
			QuizTitleLabel.TabIndex = 0;
			QuizTitleLabel.Text = "Code Trainer - Назва Квізу";
			// 
			// LoginButton
			// 
			LoginButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			LoginButton.BackColor = Theme.PrimaryHover;
			LoginButton.FlatAppearance.BorderSize = 0;
			LoginButton.FlatStyle = FlatStyle.Flat;
			LoginButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			LoginButton.ForeColor = Color.White;
			LoginButton.Location = new Point(970, 12);
			LoginButton.Name = "LoginButton";
			LoginButton.Size = new Size(110, 35);
			LoginButton.TabIndex = 1;
			LoginButton.Text = "👤 Увійти";
			LoginButton.UseVisualStyleBackColor = false;
			LoginButton.Click += LoginButton_Click;
			// 
			// MainSplit
			// 
			MainSplit.Dock = DockStyle.Fill;
			MainSplit.Location = new Point(0, 60);
			MainSplit.Name = "MainSplit";
			// 
			// MainSplit.Panel1
			// 
			MainSplit.Panel1.Controls.Add(SidePanel);
			MainSplit.Panel1MinSize = 300;
			// 
			// MainSplit.Panel2
			// 
			MainSplit.Panel2.Controls.Add(EditorPanel);
			MainSplit.Size = new Size(1100, 640);
			MainSplit.SplitterDistance = 350;
			MainSplit.TabIndex = 1;
			// 
			// SidePanel
			// 
			SidePanel.BackColor = Theme.Sidebar;
			SidePanel.Controls.Add(ProgressLabel);
			SidePanel.Controls.Add(QuizProgressBar);
			SidePanel.Controls.Add(TaskDescriptionLabel);
			SidePanel.Controls.Add(CurrentTaskLabel);
			SidePanel.Dock = DockStyle.Fill;
			SidePanel.Location = new Point(0, 0);
			SidePanel.Name = "SidePanel";
			SidePanel.Padding = new Padding(25);
			SidePanel.Size = new Size(350, 640);
			SidePanel.TabIndex = 0;
			// 
			// ProgressLabel
			// 
			ProgressLabel.AutoSize = true;
			ProgressLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			ProgressLabel.ForeColor = Theme.TextSecondary;
			ProgressLabel.Location = new Point(25, 65);
			ProgressLabel.Name = "ProgressLabel";
			ProgressLabel.Size = new Size(83, 15);
			ProgressLabel.TabIndex = 4;
			ProgressLabel.Text = "ПРОГРЕС: 0/0";
			// 
			// QuizProgressBar
			// 
			QuizProgressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			QuizProgressBar.Location = new Point(25, 85);
			QuizProgressBar.Name = "QuizProgressBar";
			QuizProgressBar.Size = new Size(300, 8);
			QuizProgressBar.TabIndex = 3;
			// 
			// TaskDescriptionLabel
			// 
			TaskDescriptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			TaskDescriptionLabel.Font = new Font("Segoe UI", 11F);
			TaskDescriptionLabel.ForeColor = Theme.TextPrimary;
			TaskDescriptionLabel.Location = new Point(25, 120);
			TaskDescriptionLabel.Name = "TaskDescriptionLabel";
			TaskDescriptionLabel.Size = new Size(300, 495);
			TaskDescriptionLabel.TabIndex = 2;
			TaskDescriptionLabel.Text = "Тут буде опис завдання...";
			// 
			// CurrentTaskLabel
			// 
			CurrentTaskLabel.AutoSize = true;
			CurrentTaskLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			CurrentTaskLabel.ForeColor = Theme.TextPrimary;
			CurrentTaskLabel.Location = new Point(25, 25);
			CurrentTaskLabel.Name = "CurrentTaskLabel";
			CurrentTaskLabel.Size = new Size(134, 30);
			CurrentTaskLabel.TabIndex = 0;
			CurrentTaskLabel.Text = "Завдання 1";
			// 
			// EditorPanel
			// 
			EditorPanel.BackColor = Theme.Background;
			EditorPanel.Controls.Add(ButtonsPanel);
			EditorPanel.Controls.Add(ResultPanel);
			EditorPanel.Controls.Add(CodePanel);
			EditorPanel.Dock = DockStyle.Fill;
			EditorPanel.Location = new Point(0, 0);
			EditorPanel.Name = "EditorPanel";
			EditorPanel.Padding = new Padding(20);
			EditorPanel.Size = new Size(746, 640);
			EditorPanel.TabIndex = 0;
			// 
			// ButtonsPanel
			// 
			ButtonsPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			ButtonsPanel.Controls.Add(CheckButton);
			ButtonsPanel.Controls.Add(SkipButton);
			ButtonsPanel.Controls.Add(NextButton);
			ButtonsPanel.Location = new Point(20, 560);
			ButtonsPanel.Name = "ButtonsPanel";
			ButtonsPanel.Size = new Size(706, 60);
			ButtonsPanel.TabIndex = 2;
			// 
			// CheckButton
			// 
			CheckButton.BackColor = Theme.Success;
			CheckButton.FlatAppearance.BorderSize = 0;
			CheckButton.FlatStyle = FlatStyle.Flat;
			CheckButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			CheckButton.ForeColor = Color.White;
			CheckButton.Location = new Point(0, 5);
			CheckButton.Name = "CheckButton";
			CheckButton.Size = new Size(160, 45);
			CheckButton.TabIndex = 0;
			CheckButton.Text = "✅ Перевірити";
			CheckButton.UseVisualStyleBackColor = false;
			CheckButton.Click += CheckButton_Click;
			// 
			// SkipButton
			// 
			SkipButton.BackColor = Theme.Warning;
			SkipButton.FlatAppearance.BorderSize = 0;
			SkipButton.FlatStyle = FlatStyle.Flat;
			SkipButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			SkipButton.ForeColor = Color.White;
			SkipButton.Location = new Point(175, 5);
			SkipButton.Name = "SkipButton";
			SkipButton.Size = new Size(160, 45);
			SkipButton.TabIndex = 1;
			SkipButton.Text = "⏭ Пропустити";
			SkipButton.UseVisualStyleBackColor = false;
			SkipButton.Click += SkipButton_Click;
			// 
			// NextButton
			// 
			NextButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			NextButton.BackColor = Theme.Primary;
			NextButton.FlatAppearance.BorderSize = 0;
			NextButton.FlatStyle = FlatStyle.Flat;
			NextButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			NextButton.ForeColor = Color.White;
			NextButton.Location = new Point(546, 5);
			NextButton.Name = "NextButton";
			NextButton.Size = new Size(160, 45);
			NextButton.TabIndex = 2;
			NextButton.Text = "Наступне ➡";
			NextButton.UseVisualStyleBackColor = false;
			NextButton.Click += NextButton_Click;
			// 
			// ResultPanel
			// 
			ResultPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			ResultPanel.BackColor = Theme.Surface;
			ResultPanel.Controls.Add(ResultTextBox);
			ResultPanel.Controls.Add(ResultHeaderLabel);
			ResultPanel.Location = new Point(20, 400);
			ResultPanel.Name = "ResultPanel";
			ResultPanel.Padding = new Padding(1);
			ResultPanel.Size = new Size(706, 140);
			ResultPanel.TabIndex = 1;
			// 
			// ResultTextBox
			// 
			ResultTextBox.BackColor = Theme.Surface;
			ResultTextBox.BorderStyle = BorderStyle.None;
			ResultTextBox.Dock = DockStyle.Fill;
			ResultTextBox.Font = new Font("Consolas", 10F);
			ResultTextBox.ForeColor = Theme.TextPrimary;
			ResultTextBox.Location = new Point(1, 31);
			ResultTextBox.Name = "ResultTextBox";
			ResultTextBox.ReadOnly = true;
			ResultTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
			ResultTextBox.Size = new Size(704, 108);
			ResultTextBox.TabIndex = 1;
			ResultTextBox.Text = "";
			// 
			// ResultHeaderLabel
			// 
			ResultHeaderLabel.BackColor = Theme.MenuHover;
			ResultHeaderLabel.Dock = DockStyle.Top;
			ResultHeaderLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			ResultHeaderLabel.ForeColor = Theme.TextSecondary;
			ResultHeaderLabel.Location = new Point(1, 1);
			ResultHeaderLabel.Name = "ResultHeaderLabel";
			ResultHeaderLabel.Padding = new Padding(10, 0, 0, 0);
			ResultHeaderLabel.Size = new Size(704, 30);
			ResultHeaderLabel.TabIndex = 0;
			ResultHeaderLabel.Text = "КОНСОЛЬ ВИВОДУ";
			ResultHeaderLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// CodePanel
			// 
			CodePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			CodePanel.BackColor = Theme.CodeBackground;
			CodePanel.Controls.Add(CodeTextBox);
			CodePanel.Controls.Add(CodeHeaderLabel);
			CodePanel.Location = new Point(20, 20);
			CodePanel.Name = "CodePanel";
			CodePanel.Padding = new Padding(1);
			CodePanel.Size = new Size(706, 360);
			CodePanel.TabIndex = 0;
			// 
			// CodeTextBox
			// 
			CodeTextBox.BackColor = Theme.CodeBackground;
			CodeTextBox.BorderStyle = BorderStyle.None;
			CodeTextBox.Dock = DockStyle.Fill;
			CodeTextBox.Font = new Font("Consolas", 12F);
			CodeTextBox.ForeColor = Theme.CodeForeground;
			CodeTextBox.Location = new Point(1, 31);
			CodeTextBox.Name = "CodeTextBox";
			CodeTextBox.Size = new Size(704, 328);
			CodeTextBox.TabIndex = 1;
			CodeTextBox.Text = "";
			CodeTextBox.WordWrap = false;
			// 
			// CodeHeaderLabel
			// 
			CodeHeaderLabel.BackColor = Theme.CodeHeader;
			CodeHeaderLabel.Dock = DockStyle.Top;
			CodeHeaderLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			CodeHeaderLabel.ForeColor = Theme.TextSecondary;
			CodeHeaderLabel.Location = new Point(1, 1);
			CodeHeaderLabel.Name = "CodeHeaderLabel";
			CodeHeaderLabel.Padding = new Padding(10, 0, 0, 0);
			CodeHeaderLabel.Size = new Size(704, 30);
			CodeHeaderLabel.TabIndex = 0;
			CodeHeaderLabel.Text = "РЕДАКТОР КОДУ (C#)";
			CodeHeaderLabel.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// QuizView
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Theme.Background;
			ClientSize = new Size(1100, 700);
			Controls.Add(MainSplit);
			Controls.Add(TopPanel);
			Name = "QuizView";
			Text = "CodeTrainer";
			WindowState = FormWindowState.Maximized;
			FormClosing += QuizView_FormClosing;
			TopPanel.ResumeLayout(false);
			TopPanel.PerformLayout();
			MainSplit.Panel1.ResumeLayout(false);
			MainSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)MainSplit).EndInit();
			MainSplit.ResumeLayout(false);
			SidePanel.ResumeLayout(false);
			SidePanel.PerformLayout();
			EditorPanel.ResumeLayout(false);
			ButtonsPanel.ResumeLayout(false);
			ResultPanel.ResumeLayout(false);
			CodePanel.ResumeLayout(false);
			ApplyModernStyles();
			ResumeLayout(false);
		}

		private void ApplyModernStyles()
		{
			StyleHelper.ApplyFormStyle(this);

			// Кнопки
			StyleHelper.ApplySuccessButton(CheckButton);
			StyleHelper.ApplyPrimaryButton(NextButton);

			StyleHelper.ApplyMenuButton(SkipButton);
			SkipButton.BackColor = Theme.Warning;
			SkipButton.ForeColor = Color.White;
			SkipButton.FlatAppearance.MouseOverBackColor = Theme.WarningHover;

			// Налаштування LoginButton
			StyleHelper.ApplyPrimaryButton(LoginButton);
			LoginButton.BackColor = Theme.IsDark ? Color.FromArgb(67, 56, 202) : Theme.PrimaryHover;
			LoginButton.FlatAppearance.MouseOverBackColor = Theme.IsDark ? Color.FromArgb(79, 70, 229) : Color.FromArgb(55, 48, 163);

			// Панелі
			TopPanel.BackColor = Theme.IsDark ? Theme.Surface : Theme.Primary;
			SidePanel.BackColor = Theme.Sidebar;
			EditorPanel.BackColor = Theme.Background;
			ButtonsPanel.BackColor = Theme.Background;

			CodePanel.BackColor = Theme.CodeBackground;
			CodeTextBox.BackColor = Theme.CodeBackground;
			CodeTextBox.ForeColor = Theme.CodeForeground;
			CodeHeaderLabel.BackColor = Theme.CodeHeader;

			ResultPanel.BackColor = Theme.Surface;

			ResultTextBox.BackColor = Theme.Surface;
			ResultTextBox.ForeColor = Theme.TextPrimary;
			ResultHeaderLabel.BackColor = Theme.MenuHover;
			ResultHeaderLabel.ForeColor = Theme.TextSecondary;

			QuizTitleLabel.ForeColor = Color.White;
			CurrentTaskLabel.ForeColor = Theme.TextPrimary;
			ProgressLabel.ForeColor = Theme.TextSecondary;
			TaskDescriptionLabel.ForeColor = Theme.TextPrimary;
		}

		#endregion

		private Panel TopPanel;
		private Label QuizTitleLabel;
		private Button LoginButton;
		private SplitContainer MainSplit;
		private Panel SidePanel;
		private Label CurrentTaskLabel;
		private Label TaskDescriptionLabel;
		private ProgressBar QuizProgressBar;
		private Label ProgressLabel;
		private Panel EditorPanel;
		private Panel CodePanel;
		private Label CodeHeaderLabel;
		private RichTextBox CodeTextBox;
		private Panel ResultPanel;
		private Label ResultHeaderLabel;
		private RichTextBox ResultTextBox;
		private Panel ButtonsPanel;
		private Button CheckButton;
		private Button SkipButton;
		private Button NextButton;
	}
}