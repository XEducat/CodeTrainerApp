using CodeTrainerApp.Model;
using CodeTrainerApp.Services;
using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views.RegisteredUserViews
{
	public partial class ProfileView : Form
	{
		private readonly User _user;
		public event EventHandler? LoggedOut;

		public ProfileView(User user)
		{
			_user = user;

			InitializeComponent();

			Theme.ThemeChanged += OnThemeChanged;
			this.Disposed += (s, e) => Theme.ThemeChanged -= OnThemeChanged;
			OnThemeChanged();

			LoadUserData();
		}

		private void OnThemeChanged()
		{
			StyleHelper.ApplyFormStyle(this);
			ApplyModernStyles();
		}

		private void LoadUserData()
		{
			if (_user == null) return;

			lblEmailValue.Text = _user.Email;
			lblLoginValue.Text = _user.Login;
			lblBirthDateValue.Text = _user.BirthDate.ToShortDateString();
			lblRoleValue.Text = TranslateRole(_user.Role);
		}

		private async void btnLogout_Click(object sender, EventArgs e)
		{
			btnLogout.Enabled = false;
			try
			{
				// Виконуємо logout (очищає cookie та локальні дані)
				await UserService.Instance.LogoutAsync();
			}
			catch
			{
				// Ігноруємо помилки logout, але все одно продовжуємо локально виходити
			}
			finally
			{
				// Повідомляємо підписників і закриваємо форму
				LoggedOut?.Invoke(this, EventArgs.Empty);
				this.Close();
			}
		}

		private string TranslateRole(string role)
		{
			return role switch
			{
				"Student" => "Студент",
				"Mentor" => "Ментор",
				"Admin" => "Адміністратор",
				_ => role
			};
		}
	}
}