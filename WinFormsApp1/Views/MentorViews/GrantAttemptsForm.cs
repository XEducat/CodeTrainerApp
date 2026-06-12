using CodeTrainerApp.UI;

namespace CodeTrainerApp.Views.MentorViews
{
    public class GrantAttemptsForm : Form
    {
        private NumericUpDown _numCount;
        private Button _btnOk;
        private Button _btnCancel;
        private Label _lblInfo;

        public int Count => (int)_numCount.Value;

        public GrantAttemptsForm(string userEmail, string userLogin, string quizTitle, int currentTotal)
        {
            InitializeComponents(userLogin, quizTitle, currentTotal);
            
            StyleHelper.ApplyFormStyle(this);
            StyleHelper.ApplyPrimaryButton(_btnOk);
            
            _lblInfo.ForeColor = Theme.TextPrimary;
            _numCount.BackColor = Theme.Surface;
            _numCount.ForeColor = Theme.TextPrimary;
        }

        private void InitializeComponents(string userLogin, string quizTitle, int currentTotal)
        {
            this.Text = "Керування спробами";
            this.Size = new Size(320, 240);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            _lblInfo = new Label
            {
                Text = $"Користувач: {userLogin}\nТест: {quizTitle}\n\nВсього дозволено спроб (мінімум 2):",
                Location = new Point(20, 20),
                Size = new Size(260, 80),
                TextAlign = ContentAlignment.TopLeft
            };

            _numCount = new NumericUpDown
            {
                Location = new Point(20, 110),
                Size = new Size(260, 30),
                Minimum = 2,
                Maximum = currentTotal + 100, // Обмеження: не більше ніж поточне + 100
                Value = currentTotal,
                Font = new Font("Segoe UI", 12)
            };

            _btnOk = new Button
            {
                Text = "ОК",
                Location = new Point(20, 150),
                Size = new Size(125, 35),
                DialogResult = DialogResult.OK
            };

            _btnCancel = new Button
            {
                Text = "Скасувати",
                Location = new Point(155, 150),
                Size = new Size(125, 35),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.Add(_lblInfo);
            this.Controls.Add(_numCount);
            this.Controls.Add(_btnOk);
            this.Controls.Add(_btnCancel);

            this.AcceptButton = _btnOk;
            this.CancelButton = _btnCancel;
        }
    }
}
