using System;
using System.Drawing;
using System.Windows.Forms;
using CodeTrainerApp.Model;

namespace CodeTrainerApp.View
{
	public partial class ProfileView : Form
	{
		private readonly User _user;

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

		private void btnLogout_Click(object sender, EventArgs e)
		{
			// @TODO: Додайте код для очищення даних користувача
			this.Close();
		}
	}
}