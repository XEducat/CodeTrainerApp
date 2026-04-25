using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CodeTrainerApp.UI
{
	public static class StyleHelper
	{
		public static void ApplyPrimaryButton(Button btn)
		{
			btn.FlatStyle = FlatStyle.Flat;
			btn.FlatAppearance.BorderSize = 0;

			btn.UseVisualStyleBackColor = false;

			btn.BackColor = Theme.Primary;
			btn.ForeColor = Color.White;

			btn.Font = new Font("Segoe UI", 10F);
			btn.Cursor = Cursors.Hand;

			MakeRounded(btn, 5);

			btn.MouseEnter += (s, e) => btn.BackColor = Theme.PrimaryHover;
			btn.MouseLeave += (s, e) => btn.BackColor = Theme.Primary;
		}

		public static void ApplySuccessButton(Button btn)
		{
			btn.FlatStyle = FlatStyle.Flat;
			btn.FlatAppearance.BorderSize = 0;

			btn.UseVisualStyleBackColor = false;

			btn.BackColor = Theme.Success;
			btn.ForeColor = Color.White;

			btn.Font = new Font("Segoe UI", 10F);
			btn.Cursor = Cursors.Hand;

			MakeRounded(btn, 5);
		}

		private static void MakeRounded(Button button, int radius)
		{
			void UpdateRegion()
			{
				GraphicsPath path = new GraphicsPath();

				int d = radius * 2;

				path.AddArc(0, 0, d, d, 180, 90);
				path.AddArc(button.Width - d, 0, d, d, 270, 90);
				path.AddArc(button.Width - d, button.Height - d, d, d, 0, 90);
				path.AddArc(0, button.Height - d, d, d, 90, 90);

				path.CloseFigure();

				button.Region = new Region(path);
			}

			button.Resize += (s, e) => UpdateRegion();
			UpdateRegion();
		}
	}
}