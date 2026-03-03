using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodeTrainerApp.View
{
	public partial class ProfileView : Form
	{
		private readonly User _user;

		// Событие выхода, подписчики (например QuizzesView) будут реагировать на него
		public event EventHandler? LoggedOut;

		public ProfileView(User user)
		{
			_user = user;

			InitializeComponent();

			LoadUserData();
		}

		private void LoadUserData()
		{
			if (_user == null) return;

			lblEmailValue.Text = _user.Email;
			lblLoginValue.Text = _user.Login;
			lblBirthDateValue.Text = _user.BirthDate.ToShortDateString();
			lblRoleValue.Text = _user.Role;
		}

		private async void btnLogout_Click(object sender, EventArgs e)
		{
			btnLogout.Enabled = false;
			try
			{
				// Выполняем logout (очищает cookie и локальные данные)
				await UserService.Instance.LogoutAsync();
			}
			catch
			{
				// Игнорируем ошибки logout, но всё равно продолжаем локально выходить
			}
			finally
			{
				// Уведомляем подписчиков и закрываем форму
				LoggedOut?.Invoke(this, EventArgs.Empty);
				this.Close();
			}
		}
	}
}