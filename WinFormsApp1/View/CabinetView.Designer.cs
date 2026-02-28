using System;
using System.Drawing;
using System.Windows.Forms;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.View
{
	public partial class CabinetView : Form
	{
		private Panel mainPanel;
		private Label lblTitle;
		private Label lblEmail;
		private Label lblRole;
		private DataGridView dgvHistory;
		private Button btnLogout;

		private void InitializeComponent()
		{
			mainPanel = new Panel();
			lblTitle = new Label();
			lblEmail = new Label();
			lblRole = new Label();
			dgvHistory = new DataGridView();
			btnLogout = new Button();
			mainPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
			SuspendLayout();
			// 
			// mainPanel
			// 
			mainPanel.BackColor = Color.White;
			mainPanel.Controls.Add(lblTitle);
			mainPanel.Controls.Add(lblEmail);
			mainPanel.Controls.Add(lblRole);
			mainPanel.Controls.Add(dgvHistory);
			mainPanel.Controls.Add(btnLogout);
			mainPanel.Dock = DockStyle.Fill;
			mainPanel.Location = new Point(0, 0);
			mainPanel.Name = "mainPanel";
			mainPanel.Padding = new Padding(20);
			mainPanel.Size = new Size(284, 261);
			mainPanel.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Dock = DockStyle.Top;
			lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
			lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
			lblTitle.Location = new Point(20, 20);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(259, 32);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Кабінет користувача";
			// 
			// lblEmail
			// 
			lblEmail.AutoSize = true;
			lblEmail.Font = new Font("Segoe UI", 12F);
			lblEmail.ForeColor = Color.FromArgb(33, 37, 41);
			lblEmail.Location = new Point(20, 50);
			lblEmail.Name = "lblEmail";
			lblEmail.Size = new Size(0, 21);
			lblEmail.TabIndex = 1;
			// 
			// lblRole
			// 
			lblRole.AutoSize = true;
			lblRole.Font = new Font("Segoe UI", 12F);
			lblRole.ForeColor = Color.FromArgb(33, 37, 41);
			lblRole.Location = new Point(20, 80);
			lblRole.Name = "lblRole";
			lblRole.Size = new Size(0, 21);
			lblRole.TabIndex = 2;
			// 
			// dgvHistory
			// 
			dgvHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvHistory.BackgroundColor = Color.FromArgb(248, 249, 250);
			dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvHistory.Location = new Point(20, 120);
			dgvHistory.Name = "dgvHistory";
			dgvHistory.Size = new Size(1084, 561);
			dgvHistory.TabIndex = 3;
			// 
			// btnLogout
			// 
			btnLogout.BackColor = Color.FromArgb(220, 53, 69);
			btnLogout.FlatAppearance.BorderSize = 0;
			btnLogout.FlatStyle = FlatStyle.Flat;
			btnLogout.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnLogout.ForeColor = Color.White;
			btnLogout.Location = new Point(20, 550);
			btnLogout.Name = "btnLogout";
			btnLogout.Size = new Size(120, 40);
			btnLogout.TabIndex = 4;
			btnLogout.Text = "Вийти";
			btnLogout.UseVisualStyleBackColor = false;
			btnLogout.Click += BtnLogout_Click;
			// 
			// CabinetView
			// 
			BackColor = Color.FromArgb(245, 246, 250);
			ClientSize = new Size(284, 261);
			Controls.Add(mainPanel);
			Name = "CabinetView";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Кабінет користувача";
			WindowState = FormWindowState.Maximized;
			mainPanel.ResumeLayout(false);
			mainPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
			ResumeLayout(false);
		}

		private void BtnLogout_Click(object sender, EventArgs e)
		{
			// Можна тут робити UserService.Instance.Logout();
			this.Close();
		}
	}
}