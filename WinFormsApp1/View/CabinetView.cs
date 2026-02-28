using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CodeTrainerApp.Model;
using CodeTrainerApp.Services;

namespace CodeTrainerApp.View
{
	public partial class CabinetView : Form
	{
		private readonly string _email;
		private readonly string _role;
		//private readonly UserHistoryService _historyService;

		public CabinetView(string email, string role)
		{
			InitializeComponent();

			_email = email;
			_role = role;

			//_historyService = new UserHistoryService();

			this.WindowState = FormWindowState.Maximized;

			//LoadHistory();
		}

		//private async void LoadHistory()
		//{
		//	var history = await _historyService.GetUserHistoryAsync(_userId);
		//	dgvHistory.DataSource = history;
		//}

		private void btnLogout_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}