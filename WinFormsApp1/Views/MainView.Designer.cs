using CodeTrainerApp.Model;
using CodeTrainerApp.UI;
using System.Drawing;
using System.Windows.Forms;

namespace CodeTrainerApp.Views
{
	partial class MainView
	{
		private System.ComponentModel.IContainer components = null;
		private ListBox QuizListBox;
		private Button ProfileButton;
		private Button CabinetButton;
		private Button ThemeButton;
		private Panel MainPanel;
		private Label TitleLabel;
		private Panel HeaderPanel;
		private FlowLayoutPanel ButtonPanel;
		private Panel SearchPanel;
		private TextBox SearchTextBox;
		private Label SearchIconLabel;

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

			// Фон елемента - робимо його схожим на картку з відступами
			int padding = 10;
			Rectangle cardRect = new Rectangle(e.Bounds.Left + padding, e.Bounds.Top + padding / 2, e.Bounds.Width - padding * 2, e.Bounds.Height - padding);
			
			Color bgColor = isSelected ? Theme.GridSelection : Theme.Surface;
			Color borderColor = isSelected ? Theme.Primary : Theme.Border;

			using (var bgBrush = new SolidBrush(bgColor))
				e.Graphics.FillRectangle(bgBrush, cardRect);

			using (var pen = new Pen(borderColor, isSelected ? 2 : 1))
				e.Graphics.DrawRectangle(pen, cardRect);

			// Ліва вертикальна смужка кольору (всередині картки)
			using (var stripeBrush = new SolidBrush(Theme.Primary))
				e.Graphics.FillRectangle(stripeBrush, cardRect.Left, cardRect.Top, 6, cardRect.Height);

			// Заголовок квізу
			using (var titleFont = new Font("Segoe UI", 13, FontStyle.Bold))
			using (var titleBrush = new SolidBrush(Theme.TextPrimary))
				e.Graphics.DrawString(quiz.Title, titleFont, titleBrush, cardRect.Left + 20, cardRect.Top + 12);

			// Опис квізу
			using (var descFont = new Font("Segoe UI", 10, FontStyle.Regular))
			using (var descBrush = new SolidBrush(Theme.TextSecondary))
			{
				string desc = quiz.Description;
				if (desc.Length > 100) desc = desc.Substring(0, 97) + "...";
				e.Graphics.DrawString(desc, descFont, descBrush, cardRect.Left + 20, cardRect.Top + 40);
			}

			// Бейдж з кількістю задач
			string questionsText = $"{quiz.Tasks.Count} питань";
			using (var countFont = new Font("Segoe UI", 10, FontStyle.Bold))
			using (var countBrush = new SolidBrush(Color.White))
			using (var badgeBrush = new SolidBrush(Theme.Success))
			{
				SizeF textSize = e.Graphics.MeasureString(questionsText, countFont);
				float bPaddingH = 16;
				float bPaddingV = 8;
				RectangleF badgeRect = new RectangleF(
					cardRect.Right - textSize.Width - bPaddingH - 15,
					cardRect.Top + (cardRect.Height - textSize.Height - bPaddingV) / 2,
					textSize.Width + bPaddingH,
					textSize.Height + bPaddingV
				);

				using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
				{
					float radius = badgeRect.Height / 2;
					path.AddArc(badgeRect.Left, badgeRect.Top, radius, radius, 180, 90);
					path.AddArc(badgeRect.Right - radius, badgeRect.Top, radius, radius, 270, 90);
					path.AddArc(badgeRect.Right - radius, badgeRect.Bottom - radius, radius, radius, 0, 90);
					path.AddArc(badgeRect.Left, badgeRect.Bottom - radius, radius, radius, 90, 90);
					path.CloseFigure();

					e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
					e.Graphics.FillPath(badgeBrush, path);
				}

				using (StringFormat sf = new StringFormat())
				{
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;
					e.Graphics.DrawString(questionsText, countFont, countBrush, badgeRect, sf);
				}
			}
		}

		private Button RefreshButton;

		private void InitializeComponent()
		{
			QuizListBox = new ListBox();
			ProfileButton = new Button();
			CabinetButton = new Button();
			ThemeButton = new Button();
			RefreshButton = new Button();
			MainPanel = new Panel();
			HeaderPanel = new Panel();
			TitleLabel = new Label();
			ButtonPanel = new FlowLayoutPanel();
			SearchPanel = new Panel();
			SearchTextBox = new TextBox();
			SearchIconLabel = new Label();

			MainPanel.SuspendLayout();
			HeaderPanel.SuspendLayout();
			ButtonPanel.SuspendLayout();
			SearchPanel.SuspendLayout();
			SuspendLayout();

			// QuizListBox
			QuizListBox.BackColor = Theme.Background;
			QuizListBox.Dock = DockStyle.Fill;
			QuizListBox.DrawMode = DrawMode.OwnerDrawFixed;
			QuizListBox.Font = new Font("Segoe UI", 11F);
			QuizListBox.ForeColor = Theme.TextPrimary;
			QuizListBox.ItemHeight = 90;
			QuizListBox.Location = new Point(20, 138);
			QuizListBox.Name = "QuizListBox";
			QuizListBox.Size = new Size(1240, 562);
			QuizListBox.BorderStyle = BorderStyle.None;
			QuizListBox.DrawItem += QuizListBox_DrawItem;
			QuizListBox.DoubleClick += QuizListBox_DoubleClick;

			// ProfileButton
			ProfileButton.Location = new Point(190, 0);
			ProfileButton.Margin = new Padding(5, 0, 0, 0);
			ProfileButton.Name = "ProfileButton";
			ProfileButton.Size = new Size(140, 40);
			ProfileButton.Text = "🔐 Увійти";
			ProfileButton.Click += ProfileButton_Click;

			// CabinetButton
			CabinetButton.Location = new Point(45, 0);
			CabinetButton.Margin = new Padding(5, 0, 0, 0);
			CabinetButton.Name = "CabinetButton";
			CabinetButton.Size = new Size(140, 40);
			CabinetButton.Text = "🏠 Кабінет";
			CabinetButton.Click += CabinetButton_Click;

			// RefreshButton
			RefreshButton.Location = new Point(0, 0);
			RefreshButton.Margin = new Padding(0);
			RefreshButton.Name = "RefreshButton";
			RefreshButton.Size = new Size(40, 40);
			RefreshButton.Text = "🔄";
			RefreshButton.Click += (s, e) => LoadQuizzesAsync();

			// ThemeButton
			ThemeButton.Location = new Point(335, 0);
			ThemeButton.Margin = new Padding(5, 0, 0, 0);
			ThemeButton.Name = "ThemeButton";
			ThemeButton.Size = new Size(40, 40);
			ThemeButton.Text = "🌙";
			ThemeButton.Click += ThemeButton_Click;

			// MainPanel
			MainPanel.BackColor = Theme.Surface;
			MainPanel.Controls.Add(QuizListBox);
			MainPanel.Controls.Add(SearchPanel);
			MainPanel.Controls.Add(HeaderPanel);
			MainPanel.Dock = DockStyle.Fill;
			MainPanel.Location = new Point(0, 0);
			MainPanel.Padding = new Padding(20);
			MainPanel.Size = new Size(1280, 720);

			// HeaderPanel
			HeaderPanel.BackColor = Theme.Surface;
			HeaderPanel.Controls.Add(TitleLabel);
			HeaderPanel.Controls.Add(ButtonPanel);
			HeaderPanel.Dock = DockStyle.Top;
			HeaderPanel.Location = new Point(20, 20);
			HeaderPanel.Padding = new Padding(20, 10, 20, 10);
			HeaderPanel.Size = new Size(1240, 68);
			HeaderPanel.Paint += (s, e) =>
			{
				using (var pen = new Pen(Theme.Border, 1))
				{
					e.Graphics.DrawLine(pen, 0, HeaderPanel.Height - 1, HeaderPanel.Width, HeaderPanel.Height - 1);
				}
			};

			// TitleLabel
			TitleLabel.Dock = DockStyle.Fill;
			TitleLabel.Font = new Font("Segoe UI Semibold", 20F);
			TitleLabel.ForeColor = Theme.TextPrimary;
			TitleLabel.Text = "Доступні квізи";
			TitleLabel.TextAlign = ContentAlignment.MiddleLeft;

			// ButtonPanel
			ButtonPanel.AutoSize = true;
			ButtonPanel.Controls.Add(RefreshButton);
			ButtonPanel.Controls.Add(CabinetButton);
			ButtonPanel.Controls.Add(ProfileButton);
			ButtonPanel.Controls.Add(ThemeButton);
			ButtonPanel.Dock = DockStyle.Right;
			ButtonPanel.Size = new Size(375, 48);
			ButtonPanel.WrapContents = false;

			// SearchPanel
			SearchPanel.Dock = DockStyle.Top;
			SearchPanel.Location = new Point(20, 88);
			SearchPanel.Name = "SearchPanel";
			SearchPanel.Padding = new Padding(20, 10, 20, 10);
			SearchPanel.Size = new Size(1240, 50);

			// SearchIconLabel
			SearchIconLabel.AutoSize = false;
			SearchIconLabel.Location = new Point(20, 12);
			SearchIconLabel.Size = new Size(30, 26);
			SearchIconLabel.Text = "🔍";
			SearchIconLabel.Font = new Font("Segoe UI", 12F);
			SearchIconLabel.TextAlign = ContentAlignment.MiddleCenter;
			SearchIconLabel.ForeColor = Theme.TextSecondary;

			// SearchTextBox
			SearchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			SearchTextBox.Font = new Font("Segoe UI", 11F);
			SearchTextBox.Location = new Point(55, 12);
			SearchTextBox.Name = "SearchTextBox";
			SearchTextBox.Size = new Size(1165, 27);
			SearchTextBox.PlaceholderText = "Пошук квізів за назвою або описом...";
			SearchTextBox.TextChanged += SearchTextBox_TextChanged;

			SearchPanel.Controls.Add(SearchTextBox);
			SearchPanel.Controls.Add(SearchIconLabel);

			// MainView
			BackColor = Theme.Background;
			ClientSize = new Size(1280, 720);
			Controls.Add(MainPanel);
			Name = "MainView";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "CodeTrainer";
			WindowState = FormWindowState.Maximized;
			Load += QuizzesView_Load;

			MainPanel.ResumeLayout(false);
			HeaderPanel.ResumeLayout(false);
			HeaderPanel.PerformLayout();
			ButtonPanel.ResumeLayout(false);
			SearchPanel.ResumeLayout(false);
			SearchPanel.PerformLayout();
			ApplyModernStyles();
			ResumeLayout(false);
		}

		private void ApplyModernStyles()
		{
			StyleHelper.ApplyFormStyle(this);

			StyleHelper.ApplySuccessButton(CabinetButton);
			StyleHelper.ApplyPrimaryButton(ProfileButton);
			
			ThemeButton.FlatStyle = FlatStyle.Flat;
			ThemeButton.FlatAppearance.BorderSize = 0;
			ThemeButton.BackColor = Color.Transparent;
			ThemeButton.ForeColor = Theme.TextPrimary;
			ThemeButton.Font = new Font("Segoe UI", 14F);
			ThemeButton.Cursor = Cursors.Hand;

			RefreshButton.FlatStyle = FlatStyle.Flat;
			RefreshButton.FlatAppearance.BorderSize = 0;
			RefreshButton.BackColor = Color.Transparent;
			RefreshButton.ForeColor = Theme.TextPrimary;
			RefreshButton.Font = new Font("Segoe UI", 14F);
			RefreshButton.Cursor = Cursors.Hand;

			SearchTextBox.BackColor = Theme.Surface;
			SearchTextBox.ForeColor = Theme.TextPrimary;
			SearchTextBox.BorderStyle = BorderStyle.FixedSingle;
		}
	}
}