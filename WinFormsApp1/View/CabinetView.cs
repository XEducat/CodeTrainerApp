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

		public CabinetView(User user)
		{
			InitializeComponent();

			_email = user.Email;
			_role = user.Role;

			LoadUserInfo();
		}

		private void LoadUserInfo()
		{
			lblEmail.Text = $"Email: {_email}";
			lblRole.Text = $"Роль: {_role}";

			// Тут пізніше можна підвантажити історію користувача
			// dgvHistory.DataSource = await _historyService.GetUserHistoryAsync(user.Id);
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