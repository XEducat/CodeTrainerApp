namespace WinFormsApp1.View
{
	partial class LoginView
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label EmailLabel;
		private System.Windows.Forms.TextBox EmailTextBox;
		private System.Windows.Forms.Label PasswordLabel;
		private System.Windows.Forms.TextBox PasswordTextBox;
		private System.Windows.Forms.Button LoginButton;
		private System.Windows.Forms.Button RegisterButton;
		private System.Windows.Forms.Label RoleLabel;
		private System.Windows.Forms.ComboBox RoleComboBox;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.EmailLabel = new System.Windows.Forms.Label();
			this.EmailTextBox = new System.Windows.Forms.TextBox();
			this.PasswordLabel = new System.Windows.Forms.Label();
			this.PasswordTextBox = new System.Windows.Forms.TextBox();
			this.LoginButton = new System.Windows.Forms.Button();
			this.RegisterButton = new System.Windows.Forms.Button();
			this.RoleLabel = new System.Windows.Forms.Label();
			this.RoleComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// EmailLabel
			// 
			this.EmailLabel.AutoSize = true;
			this.EmailLabel.Location = new System.Drawing.Point(25, 20);
			this.EmailLabel.Name = "EmailLabel";
			this.EmailLabel.Size = new System.Drawing.Size(39, 17);
			this.EmailLabel.TabIndex = 0;
			this.EmailLabel.Text = "Email";
			// 
			// EmailTextBox
			// 
			this.EmailTextBox.Location = new System.Drawing.Point(25, 40);
			this.EmailTextBox.Name = "EmailTextBox";
			this.EmailTextBox.Size = new System.Drawing.Size(250, 22);
			this.EmailTextBox.TabIndex = 1;
			// 
			// PasswordLabel
			// 
			this.PasswordLabel.AutoSize = true;
			this.PasswordLabel.Location = new System.Drawing.Point(25, 75);
			this.PasswordLabel.Name = "PasswordLabel";
			this.PasswordLabel.Size = new System.Drawing.Size(69, 17);
			this.PasswordLabel.TabIndex = 2;
			this.PasswordLabel.Text = "Password";
			// 
			// PasswordTextBox
			// 
			this.PasswordTextBox.Location = new System.Drawing.Point(25, 95);
			this.PasswordTextBox.Name = "PasswordTextBox";
			this.PasswordTextBox.PasswordChar = '*';
			this.PasswordTextBox.Size = new System.Drawing.Size(250, 22);
			this.PasswordTextBox.TabIndex = 3;
			// 
			// RoleLabel
			// 
			this.RoleLabel.AutoSize = true;
			this.RoleLabel.Location = new System.Drawing.Point(25, 130);
			this.RoleLabel.Name = "RoleLabel";
			this.RoleLabel.Size = new System.Drawing.Size(37, 17);
			this.RoleLabel.TabIndex = 4;
			this.RoleLabel.Text = "Role";
			// 
			// RoleComboBox
			// 
			this.RoleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RoleComboBox.FormattingEnabled = true;
			this.RoleComboBox.Items.AddRange(new object[] {
			"Student",
			"Mentor"});
			this.RoleComboBox.Location = new System.Drawing.Point(25, 150);
			this.RoleComboBox.Name = "RoleComboBox";
			this.RoleComboBox.Size = new System.Drawing.Size(250, 24);
			this.RoleComboBox.TabIndex = 5;
			// 
			// LoginButton
			// 
			this.LoginButton.Location = new System.Drawing.Point(25, 190);
			this.LoginButton.Name = "LoginButton";
			this.LoginButton.Size = new System.Drawing.Size(120, 30);
			this.LoginButton.TabIndex = 6;
			this.LoginButton.Text = "Login";
			this.LoginButton.UseVisualStyleBackColor = true;
			// 
			// RegisterButton
			// 
			this.RegisterButton.Location = new System.Drawing.Point(155, 190);
			this.RegisterButton.Name = "RegisterButton";
			this.RegisterButton.Size = new System.Drawing.Size(120, 30);
			this.RegisterButton.TabIndex = 7;
			this.RegisterButton.Text = "Register";
			this.RegisterButton.UseVisualStyleBackColor = true;
			// 
			// LoginForm
			// 
			this.ClientSize = new System.Drawing.Size(300, 240);
			this.Controls.Add(this.RegisterButton);
			this.Controls.Add(this.LoginButton);
			this.Controls.Add(this.RoleComboBox);
			this.Controls.Add(this.RoleLabel);
			this.Controls.Add(this.PasswordTextBox);
			this.Controls.Add(this.PasswordLabel);
			this.Controls.Add(this.EmailTextBox);
			this.Controls.Add(this.EmailLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "LoginForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Login / Register";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		#endregion
	}
}