using System;
using System.Drawing;
using System.Windows.Forms;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.View
{
	public partial class CabinetView : Form
	{
		private Panel mainPanel;
		private Panel headerPanel;
		private Label lblTitle;
		private Label lblEmail;
		private Label lblRole;
		private Panel footerPanel;
		private Button btnLogout;
		private DataGridView dgvHistory;

		private void InitializeComponent()
		{
			mainPanel = new Panel();
			headerPanel = new Panel();
			lblTitle = new Label();
			lblEmail = new Label();
			lblRole = new Label();
			footerPanel = new Panel();
			btnLogout = new Button();
			dgvHistory = new DataGridView();
			mainPanel.SuspendLayout();
			headerPanel.SuspendLayout();
			footerPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
			SuspendLayout();
			// 
			// mainPanel
			// 
			mainPanel.BackColor = Color.White;
			mainPanel.Controls.Add(dgvHistory);
			mainPanel.Controls.Add(footerPanel);
			mainPanel.Controls.Add(headerPanel);
			mainPanel.Dock = DockStyle.Fill;
			mainPanel.Location = new Point(0, 0);
			mainPanel.Name = "mainPanel";
			mainPanel.Padding = new Padding(20);
			mainPanel.TabIndex = 0;
			// 
			// headerPanel
			// 
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Height = 100;
			headerPanel.BackColor = Color.Transparent;
			headerPanel.Controls.Add(lblTitle);
			headerPanel.Controls.Add(lblEmail);
			headerPanel.Controls.Add(lblRole);
			headerPanel.Padding = new Padding(0, 0, 0, 10);
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
			lblTitle.ForeColor = Color.FromArgb(33, 37, 41);
			lblTitle.Location = new Point(0, 0);
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
			lblEmail.Location = new Point(0, 42);
			lblEmail.Name = "lblEmail";
			lblEmail.Size = new Size(0, 21);
			lblEmail.TabIndex = 1;
			// 
			// lblRole
			// 
			lblRole.AutoSize = true;
			lblRole.Font = new Font("Segoe UI", 12F);
			lblRole.ForeColor = Color.FromArgb(33, 37, 41);
			lblRole.Location = new Point(0, 65);
			lblRole.Name = "lblRole";
			lblRole.Size = new Size(0, 21);
			lblRole.TabIndex = 2;
			// 
			// footerPanel
			// 
			footerPanel.Dock = DockStyle.Bottom;
			footerPanel.Height = 64;
			footerPanel.Padding = new Padding(0, 10, 0, 0);
			footerPanel.BackColor = Color.Transparent;
			footerPanel.Controls.Add(btnLogout);
			// 
			// btnLogout
			// 
			btnLogout.BackColor = Color.FromArgb(220, 53, 69);
			btnLogout.FlatAppearance.BorderSize = 0;
			btnLogout.FlatStyle = FlatStyle.Flat;
			btnLogout.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnLogout.ForeColor = Color.White;
			btnLogout.Anchor = AnchorStyles.Right | AnchorStyles.Top;
			btnLogout.Location = new Point(0, 10);
			btnLogout.Name = "btnLogout";
			btnLogout.Size = new Size(120, 40);
			btnLogout.TabIndex = 4;
			btnLogout.Text = "Вийти";
			btnLogout.UseVisualStyleBackColor = false;
			btnLogout.Click += BtnLogout_Click;
			// align logout to right inside footer
			btnLogout.Location = new Point(footerPanel.ClientSize.Width - btnLogout.Width - 10, 10);
			footerPanel.Resize += (s, e) =>
			{
				btnLogout.Location = new Point(Math.Max(10, footerPanel.ClientSize.Width - btnLogout.Width - 10), 10);
			};
			// 
			// dgvHistory
			// 
			dgvHistory.Dock = DockStyle.Fill;
			dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvHistory.BackgroundColor = Color.FromArgb(248, 249, 250);
			dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvHistory.Location = new Point(20, 120);
			dgvHistory.Name = "dgvHistory";
			dgvHistory.TabIndex = 3;
			dgvHistory.ReadOnly = true;
			dgvHistory.AllowUserToAddRows = false;
			dgvHistory.AllowUserToDeleteRows = false;
			dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvHistory.RowHeadersVisible = false;
			dgvHistory.AutoGenerateColumns = true;
			dgvHistory.Margin = new Padding(0);
			// ensure vertical scrollbar visible when needed and rows wrap
			dgvHistory.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
			dgvHistory.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
			dgvHistory.ScrollBars = ScrollBars.Vertical;
			// set minimal height to avoid tiny grid on small screens
			dgvHistory.MinimumSize = new Size(200, 120);
			// 
			// CabinetView
			// 
			BackColor = Color.FromArgb(245, 246, 250);
			ClientSize = new Size(900, 600);
			Controls.Add(mainPanel);
			Name = "CabinetView";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Кабінет користувача";
			WindowState = FormWindowState.Maximized;
			mainPanel.ResumeLayout(false);
			headerPanel.ResumeLayout(false);
			headerPanel.PerformLayout();
			footerPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
			ResumeLayout(false);
		}

		private void BtnLogout_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}