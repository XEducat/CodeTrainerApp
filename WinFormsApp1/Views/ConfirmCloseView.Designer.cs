using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views
{
    partial class ConfirmCloseView
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
		private System.Windows.Forms.Label labelTitle;
		private System.Windows.Forms.Panel mainPanel;
		private System.Windows.Forms.Panel buttonPanel;

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
			labelTitle = new Label();
			labelMessage = new Label();
			btnYes = new Button();
			btnNo = new Button();
			mainPanel = new Panel();
			buttonPanel = new Panel();
			mainPanel.SuspendLayout();
			buttonPanel.SuspendLayout();
			SuspendLayout();
			// 
			// labelTitle
			// 
			labelTitle.Dock = DockStyle.Top;
			labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			labelTitle.Location = new Point(20, 20);
			labelTitle.Name = "labelTitle";
			labelTitle.Size = new Size(360, 40);
			labelTitle.TabIndex = 3;
			labelTitle.Text = "Завершити тест?";
			labelTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// labelMessage
			// 
			labelMessage.Dock = DockStyle.Top;
			labelMessage.Font = new Font("Segoe UI", 10F);
			labelMessage.ForeColor = Color.FromArgb(107, 114, 128);
			labelMessage.Location = new Point(20, 60);
			labelMessage.Name = "labelMessage";
			labelMessage.Size = new Size(360, 45);
			labelMessage.TabIndex = 0;
			labelMessage.Text = "Ваш прогрес у цьому тесті не буде збережено. Ви дійсно бажаєте вийти?";
			labelMessage.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnYes
			// 
			btnYes.DialogResult = DialogResult.Yes;
			btnYes.Location = new Point(204, 10);
			btnYes.Name = "btnYes";
			btnYes.Size = new Size(130, 40);
			btnYes.TabIndex = 1;
			btnYes.Text = "Так, вийти";
			btnYes.UseVisualStyleBackColor = true;
			// 
			// btnNo
			// 
			btnNo.DialogResult = DialogResult.No;
			btnNo.Location = new Point(28, 10);
			btnNo.Name = "btnNo";
			btnNo.Size = new Size(130, 40);
			btnNo.TabIndex = 2;
			btnNo.Text = "Продовжити";
			btnNo.UseVisualStyleBackColor = true;
			// 
			// mainPanel
			// 
			mainPanel.BackColor = Color.White;
			mainPanel.Controls.Add(buttonPanel);
			mainPanel.Controls.Add(labelMessage);
			mainPanel.Controls.Add(labelTitle);
			mainPanel.Dock = DockStyle.Fill;
			mainPanel.Location = new Point(0, 0);
			mainPanel.Name = "mainPanel";
			mainPanel.Padding = new Padding(20);
			mainPanel.Size = new Size(400, 200);
			mainPanel.TabIndex = 3;
			// 
			// buttonPanel
			// 
			buttonPanel.Controls.Add(btnNo);
			buttonPanel.Controls.Add(btnYes);
			buttonPanel.Dock = DockStyle.Bottom;
			buttonPanel.Location = new Point(20, 120);
			buttonPanel.Name = "buttonPanel";
			buttonPanel.Size = new Size(360, 60);
			buttonPanel.TabIndex = 4;
			// 
			// ConfirmCloseView
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(400, 200);
			Controls.Add(mainPanel);
			FormBorderStyle = FormBorderStyle.None;
			Name = "ConfirmCloseView";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Підтвердження";
			mainPanel.ResumeLayout(false);
			buttonPanel.ResumeLayout(false);
			ApplyModernStyles();
			ResumeLayout(false);
		}

		private void ApplyModernStyles()
		{
			mainPanel.BackColor = Theme.Surface;
			labelTitle.ForeColor = Theme.TextPrimary;
			labelMessage.ForeColor = Theme.TextSecondary;

			StyleHelper.ApplyPrimaryButton(btnNo);
			
			// Налаштовуємо кнопку "Вийти" (Danger style)
			btnYes.FlatStyle = FlatStyle.Flat;
			btnYes.FlatAppearance.BorderSize = 0;
			btnYes.BackColor = Color.FromArgb(254, 226, 226); // Light red
			btnYes.ForeColor = Color.FromArgb(220, 38, 38);   // Dark red
			btnYes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnYes.Cursor = Cursors.Hand;

			// Малюємо рівномірну сіру рамку прямо на mainPanel
			mainPanel.Paint += (s, e) =>
			{
				// Виразна сіра рамка (2 пікселі) для кращого контрасту
				using (var pen = new Pen(Color.FromArgb(160, 160, 160), 2))
				{
					e.Graphics.DrawRectangle(pen, 1, 1, mainPanel.Width - 2, mainPanel.Height - 2);
				}
			};
		}
	}
}
