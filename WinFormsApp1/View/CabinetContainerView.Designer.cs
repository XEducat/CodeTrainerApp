using CodeTrainerApp.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeTrainerApp.View
{
	public partial class CabinetContainerView : Form
	{
		private Panel headerPanel;
		private Button btnCabinet;
		private Button btnSecondView;

		private Panel contentPanel;

		private Form currentForm;

		private void InitializeComponent()
		{
			headerPanel = new Panel();
			btnCabinet = new Button();
			btnSecondView = new Button();
			contentPanel = new Panel();

			SuspendLayout();

			// ================= HEADER =================
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Height = 60;
			headerPanel.BackColor = Color.FromArgb(33, 37, 41);

			btnCabinet.Text = "Кабінет";
			btnCabinet.Width = 150;
			btnCabinet.Height = 40;
			btnCabinet.Location = new Point(20, 10);
			btnCabinet.BackColor = Color.FromArgb(52, 120, 246);
			btnCabinet.ForeColor = Color.White;
			btnCabinet.FlatStyle = FlatStyle.Flat;
			btnCabinet.FlatAppearance.BorderSize = 0;

			btnSecondView.Text = "Інша форма";
			btnSecondView.Width = 150;
			btnSecondView.Height = 40;
			btnSecondView.Location = new Point(190, 10);
			btnSecondView.BackColor = Color.FromArgb(108, 117, 125);
			btnSecondView.ForeColor = Color.White;
			btnSecondView.FlatStyle = FlatStyle.Flat;
			btnSecondView.FlatAppearance.BorderSize = 0;
			btnSecondView.Click += (s, e) => ShowForm(new SecondView());

			headerPanel.Controls.Add(btnCabinet);
			headerPanel.Controls.Add(btnSecondView);

			// ================= CONTENT =================
			contentPanel.Dock = DockStyle.Fill;
			contentPanel.BackColor = Color.FromArgb(245, 246, 250);

			// ================= FORM =================
			ClientSize = new Size(1400, 800);
			Controls.Add(contentPanel);
			Controls.Add(headerPanel);

			StartPosition = FormStartPosition.CenterScreen;
			WindowState = FormWindowState.Maximized;
			Text = "CodeTrainer Dashboard";

			ResumeLayout(false);
		}
	}
}