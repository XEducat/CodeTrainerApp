namespace CodeTrainerApp
{
	partial class MainView
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
			CurrentTaskLabel = new Label();
			TaskDescriptionLabel = new Label();
			CodeTextBox = new TextBox();
			CheckButton = new Button();
			NextButton = new Button();
			ResultTextBox = new TextBox();
			label1 = new Label();
			label2 = new Label();
			SkipButton = new Button();
			LoginButton = new Button();
			MainPanel = new Panel();
			ButtonsPanel = new Panel();
			MainPanel.SuspendLayout();
			ButtonsPanel.SuspendLayout();
			SuspendLayout();
			// 
			// CurrentTaskLabel
			// 
			CurrentTaskLabel.AutoSize = true;
			CurrentTaskLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			CurrentTaskLabel.ForeColor = Color.FromArgb(33, 37, 41);
			CurrentTaskLabel.Location = new Point(24, 20);
			CurrentTaskLabel.Name = "CurrentTaskLabel";
			CurrentTaskLabel.Size = new Size(167, 37);
			CurrentTaskLabel.TabIndex = 0;
			CurrentTaskLabel.Text = "Завдання 1";
			// 
			// TaskDescriptionLabel
			// 
			TaskDescriptionLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TaskDescriptionLabel.Font = new Font("Segoe UI", 11F);
			TaskDescriptionLabel.ForeColor = Color.FromArgb(73, 80, 87);
			TaskDescriptionLabel.Location = new Point(24, 70);
			TaskDescriptionLabel.Name = "TaskDescriptionLabel";
			TaskDescriptionLabel.Size = new Size(1511, 80);
			TaskDescriptionLabel.TabIndex = 2;
			TaskDescriptionLabel.Text = "Опис завдання";
			// 
			// CodeTextBox
			// 
			CodeTextBox.AcceptsReturn = true;
			CodeTextBox.AcceptsTab = true;
			CodeTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			CodeTextBox.BackColor = Color.FromArgb(248, 249, 250);
			CodeTextBox.Font = new Font("Consolas", 11F);
			CodeTextBox.ForeColor = Color.FromArgb(33, 37, 41);
			CodeTextBox.Location = new Point(24, 185);
			CodeTextBox.Multiline = true;
			CodeTextBox.Name = "CodeTextBox";
			CodeTextBox.ScrollBars = ScrollBars.Vertical;
			CodeTextBox.Size = new Size(1511, 280);
			CodeTextBox.TabIndex = 4;
			// 
			// CheckButton
			// 
			CheckButton.BackColor = Color.FromArgb(40, 167, 69);
			CheckButton.FlatAppearance.BorderSize = 0;
			CheckButton.FlatStyle = FlatStyle.Flat;
			CheckButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			CheckButton.ForeColor = Color.White;
			CheckButton.Location = new Point(0, 5);
			CheckButton.Name = "CheckButton";
			CheckButton.Size = new Size(130, 40);
			CheckButton.TabIndex = 0;
			CheckButton.Text = "✅ Перевірити";
			CheckButton.UseVisualStyleBackColor = false;
			// 
			// NextButton
			// 
			NextButton.BackColor = Color.Gray;
			NextButton.Enabled = false;
			NextButton.FlatAppearance.BorderSize = 0;
			NextButton.FlatStyle = FlatStyle.Flat;
			NextButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			NextButton.ForeColor = Color.White;
			NextButton.Location = new Point(290, 5);
			NextButton.Name = "NextButton";
			NextButton.Size = new Size(120, 40);
			NextButton.TabIndex = 2;
			NextButton.Text = "➡ Наступне";
			NextButton.UseVisualStyleBackColor = false;
			// 
			// ResultTextBox
			// 
			ResultTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			ResultTextBox.BackColor = Color.FromArgb(233, 236, 239);
			ResultTextBox.Font = new Font("Consolas", 10F);
			ResultTextBox.ForeColor = Color.FromArgb(33, 37, 41);
			ResultTextBox.Location = new Point(24, 565);
			ResultTextBox.Multiline = true;
			ResultTextBox.Name = "ResultTextBox";
			ResultTextBox.ReadOnly = true;
			ResultTextBox.ScrollBars = ScrollBars.Vertical;
			ResultTextBox.Size = new Size(1511, 100);
			ResultTextBox.TabIndex = 7;
			// 
			// label1
			// 
			label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			label1.ForeColor = Color.FromArgb(52, 58, 64);
			label1.Location = new Point(24, 160);
			label1.Name = "label1";
			label1.Size = new Size(120, 23);
			label1.TabIndex = 3;
			label1.Text = "Ваш код (C#):";
			// 
			// label2
			// 
			label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			label2.ForeColor = Color.FromArgb(52, 58, 64);
			label2.Location = new Point(24, 540);
			label2.Name = "label2";
			label2.Size = new Size(100, 23);
			label2.TabIndex = 6;
			label2.Text = "Результат:";
			// 
			// SkipButton
			// 
			SkipButton.BackColor = Color.FromArgb(255, 193, 7);
			SkipButton.FlatAppearance.BorderSize = 0;
			SkipButton.FlatStyle = FlatStyle.Flat;
			SkipButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			SkipButton.ForeColor = Color.FromArgb(33, 37, 41);
			SkipButton.Location = new Point(140, 5);
			SkipButton.Name = "SkipButton";
			SkipButton.Size = new Size(140, 40);
			SkipButton.TabIndex = 1;
			SkipButton.Text = "⏭ Пропустити";
			SkipButton.UseVisualStyleBackColor = false;
			// 
			// LoginButton
			// 
			LoginButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			LoginButton.BackColor = Color.FromArgb(108, 117, 125);
			LoginButton.FlatAppearance.BorderSize = 0;
			LoginButton.FlatStyle = FlatStyle.Flat;
			LoginButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			LoginButton.ForeColor = Color.White;
			LoginButton.Location = new Point(635, 22);
			LoginButton.Name = "LoginButton";
			LoginButton.Size = new Size(120, 36);
			LoginButton.TabIndex = 1;
			LoginButton.Text = "👤 Увійти";
			LoginButton.UseVisualStyleBackColor = false;
			LoginButton.Click += LoginButton_Click;
			// 
			// MainPanel
			// 
			MainPanel.BackColor = Color.White;
			MainPanel.Controls.Add(CurrentTaskLabel);
			MainPanel.Controls.Add(LoginButton);
			MainPanel.Controls.Add(TaskDescriptionLabel);
			MainPanel.Controls.Add(label1);
			MainPanel.Controls.Add(CodeTextBox);
			MainPanel.Controls.Add(ButtonsPanel);
			MainPanel.Controls.Add(label2);
			MainPanel.Controls.Add(ResultTextBox);
			MainPanel.Dock = DockStyle.Fill;
			MainPanel.Location = new Point(0, 0);
			MainPanel.Name = "MainPanel";
			MainPanel.Padding = new Padding(24);
			MainPanel.Size = new Size(811, 590);
			MainPanel.TabIndex = 0;
			// 
			// ButtonsPanel
			// 
			ButtonsPanel.Controls.Add(CheckButton);
			ButtonsPanel.Controls.Add(SkipButton);
			ButtonsPanel.Controls.Add(NextButton);
			ButtonsPanel.Location = new Point(24, 480);
			ButtonsPanel.Name = "ButtonsPanel";
			ButtonsPanel.Size = new Size(420, 50);
			ButtonsPanel.TabIndex = 5;
			// 
			// MainView
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(245, 246, 250);
			ClientSize = new Size(811, 590);
			Controls.Add(MainPanel);
			Name = "MainView";
			Text = "Code Trainer App (C#)";
			WindowState = FormWindowState.Maximized;
			Load += MainView_Load;
			MainPanel.ResumeLayout(false);
			MainPanel.PerformLayout();
			ButtonsPanel.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private Label CurrentTaskLabel;
		private Label TaskDescriptionLabel;
		private TextBox CodeTextBox;
		private Button CheckButton;
		private Button NextButton;
		private TextBox ResultTextBox;
		private Label label1;
		private Label label2;
		private Button SkipButton;
		private Button LoginButton;
		private Panel MainPanel;
		private Panel ButtonsPanel;
	}
}
