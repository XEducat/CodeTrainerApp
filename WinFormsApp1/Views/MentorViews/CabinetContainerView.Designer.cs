using CodeTrainerApp.Services;
using System;
using System.Drawing;
using System.Windows.Forms;
using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views.MentorViews
{
	public partial class CabinetContainerView : Form
	{
		private Panel headerPanel;
		private Button btnCabinet;
		private Button btnSecondView;
		private Panel contentPanel;

		private void InitializeComponent()
		{
			headerPanel = new Panel();
			btnCabinet = new Button();
			btnSecondView = new Button();
			contentPanel = new Panel();
			headerPanel.SuspendLayout();
			SuspendLayout();
			// 
			// headerPanel
			// 
			headerPanel.BackColor = Theme.Surface;
			headerPanel.Controls.Add(btnCabinet);
			headerPanel.Controls.Add(btnSecondView);
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Location = new Point(0, 0);
			headerPanel.Name = "headerPanel";
			headerPanel.Size = new Size(1400, 60);
			headerPanel.TabIndex = 1;
			// 
			// btnCabinet
			// 
			btnCabinet.BackColor = Theme.Primary;
			btnCabinet.FlatAppearance.BorderSize = 0;
			btnCabinet.FlatStyle = FlatStyle.Flat;
			btnCabinet.ForeColor = Color.White;
			btnCabinet.Location = new Point(20, 10);
			btnCabinet.Name = "btnCabinet";
			btnCabinet.Size = new Size(160, 40);
			btnCabinet.TabIndex = 0;
			btnCabinet.Text = "Історія";
			btnCabinet.UseVisualStyleBackColor = false;
			// 
			// btnSecondView
			// 
			btnSecondView.BackColor = Theme.TextSecondary;
			btnSecondView.FlatAppearance.BorderSize = 0;
			btnSecondView.FlatStyle = FlatStyle.Flat;
			btnSecondView.ForeColor = Color.White;
			btnSecondView.Location = new Point(190, 10);
			btnSecondView.Name = "btnSecondView";
			btnSecondView.Size = new Size(160, 40);
			btnSecondView.TabIndex = 1;
			btnSecondView.Text = "Мої квізи";
			btnSecondView.UseVisualStyleBackColor = false;
			// 
			// contentPanel
			// 
			contentPanel.BackColor = Theme.Background;
			contentPanel.Dock = DockStyle.Fill;
			contentPanel.Location = new Point(0, 60);
			contentPanel.Name = "contentPanel";
			contentPanel.Size = new Size(1400, 740);
			contentPanel.TabIndex = 0;
			// 
			// CabinetContainerView
			// 
			ClientSize = new Size(1400, 800);
			Controls.Add(contentPanel);
			Controls.Add(headerPanel);
			FormBorderStyle = FormBorderStyle.Sizable;
			Name = "CabinetContainerView";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "CodeTrainer - Кабінет";
			WindowState = FormWindowState.Maximized;
			Load += CabinetContainerView_Load;
			headerPanel.ResumeLayout(false);
			ApplyModernStyles();
			ResumeLayout(false);
		}

		private void ApplyModernStyles()
		{
			StyleHelper.ApplyFormStyle(this);

			headerPanel.BackColor = Theme.Surface;
			headerPanel.Paint += (s, e) => 
			{
				using (var pen = new Pen(Theme.Border))
				{
					e.Graphics.DrawLine(pen, 0, headerPanel.Height - 1, headerPanel.Width, headerPanel.Height - 1);
				}
			};

			StyleHelper.ApplyMenuButton(btnCabinet);
			StyleHelper.ApplyMenuButton(btnSecondView);
		}
	}
}