using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views.MentorViews
{
    public partial class GrantAttemptsForm : Form
    {
        public int Count => (int)_numCount.Value;

        public GrantAttemptsForm(string userEmail, string userLogin, string quizTitle, int currentTotal)
        {
            InitializeComponent();
            
            // Заповнюємо дані, що прийшли
            _lblInfo.Text = $"Користувач: {userLogin}\nТест: {quizTitle}\n\nВсього дозволено спроб (мінімум 2):";
            
            _numCount.Maximum = currentTotal + 100;
            _numCount.Value = currentTotal;

            // Стилізація
            StyleHelper.ApplyFormStyle(this);
            StyleHelper.ApplyPrimaryButton(_btnOk);
            StyleHelper.ApplySecondaryButton(_btnCancel);
            
            _lblInfo.ForeColor = Theme.TextPrimary;
        }
    }
}