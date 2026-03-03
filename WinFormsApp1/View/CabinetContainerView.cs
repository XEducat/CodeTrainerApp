using CodeTrainerApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CodeTrainerApp.View
{
	public partial class CabinetContainerView : Form
	{
		public CabinetContainerView()
		{
			InitializeComponent();

			btnCabinet.Click += (s, e) => ShowForm(new UserHistoryView());
			btnSecondView.Click += (s, e) => ShowForm(new SecondView());
		}

		private void CabinetContainerView_Load(object sender, EventArgs e)
		{
			ShowForm(new UserHistoryView());
		}

		// ================= ЛОГІКА ПЕРЕМИКАННЯ =================
		private void ShowForm(Form form)
		{
			if (currentForm != null)
			{
				currentForm.Close();
			}

			currentForm = form;
			form.TopLevel = false;
			form.FormBorderStyle = FormBorderStyle.None;
			form.Dock = DockStyle.Fill;

			contentPanel.Controls.Clear();
			contentPanel.Controls.Add(form);

			form.Show();
		}
	}
}
