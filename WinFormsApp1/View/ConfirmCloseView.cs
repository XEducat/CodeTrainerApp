using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CodeTrainerApp.View
{
	public partial class ConfirmCloseView : Form
	{
		public ConfirmCloseView()
		{
			InitializeComponent();

			// Кнопки "Так" та "Ні" як стандартні діалогові результати
			this.AcceptButton = btnYes;
			this.CancelButton = btnNo;
		}
	}
}
