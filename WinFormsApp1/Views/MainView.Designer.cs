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

			// Фон елемента
			Color bgColor = isSelected ? Color.FromArgb(200, 220, 240) : Color.White;
			using (var bgBrush = new SolidBrush(bgColor))
				e.Graphics.FillRectangle(bgBrush, e.Bounds);

			// Ліва вертикальна смужка кольору
			using (var stripeBrush = new SolidBrush(Color.FromArgb(0, 123, 255)))
				e.Graphics.FillRectangle(stripeBrush, e.Bounds.Left, e.Bounds.Top, 6, e.Bounds.Height);

			// Заголовок квізу
			using (var titleFont = new Font("Segoe UI", 12, FontStyle.Bold))
			using (var titleBrush = new SolidBrush(Color.FromArgb(33, 37, 41)))
				e.Graphics.DrawString(quiz.Title, titleFont, titleBrush, e.Bounds.Left + 15, e.Bounds.Top + 10);

			// Опис квізу
			using (var descFont = new Font("Segoe UI", 10, FontStyle.Regular))
			using (var descBrush = new SolidBrush(Color.FromArgb(55, 65, 75)))
				e.Graphics.DrawString(quiz.Description, descFont, descBrush, e.Bounds.Left + 15, e.Bounds.Top + 35);

			// Бейдж з кількістю задач — більший і заокруглений
			string questionsText = $"{quiz.Tasks.Count} питань";
			using (var countFont = new Font("Segoe UI", 10, FontStyle.Bold))
			using (var countBrush = new SolidBrush(Color.White))
			using (var badgeBrush = new SolidBrush(Color.FromArgb(40, 167, 69)))
			{
				SizeF textSize = e.Graphics.MeasureString(questionsText, countFont);
				float paddingH = 16;
				float paddingV = 8;
				RectangleF badgeRect = new RectangleF(
					e.Bounds.Right - textSize.Width - paddingH - 10,
					e.Bounds.Top + (e.Bounds.Height - textSize.Height - paddingV) / 2, // Центруємо по вертикалі
					textSize.Width + paddingH,
					textSize.Height + paddingV
				);

				// Заокруглені кути
				using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
				{
					float radius = badgeRect.Height / 2;
					path.AddArc(badgeRect.Left, badgeRect.Top, radius, radius, 180, 90);
					path.AddArc(badgeRect.Right - radius, badgeRect.Top, radius, radius, 270, 90);
					path.AddArc(badgeRect.Right - radius, badgeRect.Bottom - radius, radius, radius, 0, 90);
					path.AddArc(badgeRect.Left, badgeRect.Bottom - radius, radius, radius, 90, 90);
					path.CloseFigure();

					e.Graphics.FillPath(badgeBrush, path);
				}

				// Вирівнювання тексту по центру бейджу через StringFormat
				using (StringFormat sf = new StringFormat())
				{
					sf.Alignment = StringAlignment.Center;       // горизонталь
					sf.LineAlignment = StringAlignment.Center;   // вертикаль
					e.Graphics.DrawString(questionsText, countFont, countBrush, badgeRect, sf);
				}
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

			// QuizListBox
			QuizListBox.BackColor = Theme.Background;
			QuizListBox.Dock = DockStyle.Fill;
			QuizListBox.DrawMode = DrawMode.OwnerDrawFixed;
			QuizListBox.Font = new Font("Segoe UI", 11F);
			QuizListBox.ForeColor = Theme.TextPrimary;
			QuizListBox.ItemHeight = 80;
			QuizListBox.Location = new Point(20, 88);
			QuizListBox.Name = "QuizListBox";
			QuizListBox.Size = new Size(1240, 612);
			QuizListBox.DrawItem += QuizListBox_DrawItem;
			QuizListBox.DoubleClick += QuizListBox_DoubleClick;

			// ProfileButton
			ProfileButton.Location = new Point(145, 0);
			ProfileButton.Margin = new Padding(5, 0, 0, 0);
			ProfileButton.Name = "ProfileButton";
			ProfileButton.Size = new Size(140, 40);
			ProfileButton.Text = "🔐 Увійти";
			ProfileButton.Click += ProfileButton_Click;

			// CabinetButton
			CabinetButton.Location = new Point(0, 0);
			CabinetButton.Margin = new Padding(0);
			CabinetButton.Name = "CabinetButton";
			CabinetButton.Size = new Size(140, 40);
			CabinetButton.Text = "🏠 Кабінет";
			CabinetButton.Click += CabinetButton_Click;

			// MainPanel
			MainPanel.BackColor = Theme.Surface;
			MainPanel.Controls.Add(QuizListBox);
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

			// TitleLabel
			TitleLabel.Dock = DockStyle.Fill;
			TitleLabel.Font = new Font("Segoe UI Semibold", 18F);
			TitleLabel.ForeColor = Theme.TextPrimary;
			TitleLabel.Text = "Code Trainer — Квізи";
			TitleLabel.TextAlign = ContentAlignment.MiddleLeft;

			// ButtonPanel
			ButtonPanel.AutoSize = true;
			ButtonPanel.Controls.Add(CabinetButton);
			ButtonPanel.Controls.Add(ProfileButton);
			ButtonPanel.Dock = DockStyle.Right;
			ButtonPanel.Size = new Size(285, 48);
			ButtonPanel.WrapContents = false;

			// MainView
			BackColor = Theme.Background;
			ClientSize = new Size(1280, 720);
			Controls.Add(MainPanel);
			Name = "MainView";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Code Trainer";
			WindowState = FormWindowState.Maximized;
			Load += QuizzesView_Load;

			// ✨ застосування стилів
			StyleHelper.ApplySuccessButton(CabinetButton);
			StyleHelper.ApplyPrimaryButton(ProfileButton);

			MainPanel.ResumeLayout(false);
			HeaderPanel.ResumeLayout(false);
			HeaderPanel.PerformLayout();
			ButtonPanel.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}