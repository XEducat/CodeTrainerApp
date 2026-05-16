using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CodeTrainerApp.UI
{
	public static class StyleHelper
	{
		[DllImport("dwmapi.dll")]
		private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

		private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
		private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

		public static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
		{
			if (IsWindows10OrGreater(17763))
			{
				var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
				if (!IsWindows10OrGreater(18985))
				{
					attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
				}

				int useDarkMode = enabled ? 1 : 0;
				return DwmSetWindowAttribute(handle, attribute, ref useDarkMode, sizeof(int)) == 0;
			}

			return false;
		}

		private static bool IsWindows10OrGreater(int build = -1)
		{
			return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
		}

		public static void ApplyPrimaryButton(Button btn)
		{
			btn.FlatStyle = FlatStyle.Flat;
			btn.FlatAppearance.BorderSize = 0;
			btn.FlatAppearance.MouseOverBackColor = Theme.PrimaryHover;
			btn.FlatAppearance.MouseDownBackColor = Theme.Primary;

			btn.UseVisualStyleBackColor = false;
			btn.BackColor = Theme.Primary;
			btn.ForeColor = Color.White;

			btn.Font = new Font("Segoe UI", 10F);
			btn.TextAlign = ContentAlignment.MiddleCenter;
			btn.Padding = new Padding(0);
			btn.Cursor = Cursors.Hand;

			MakeRounded(btn, 5);
		}

		public static void ApplySuccessButton(Button btn)
		{
			btn.FlatStyle = FlatStyle.Flat;
			btn.FlatAppearance.BorderSize = 0;
			btn.FlatAppearance.MouseOverBackColor = Theme.SuccessHover;
			btn.FlatAppearance.MouseDownBackColor = Theme.Success;

			btn.UseVisualStyleBackColor = false;
			btn.BackColor = Theme.Success;
			btn.ForeColor = Color.White;

			btn.Font = new Font("Segoe UI", 10F);
			btn.TextAlign = ContentAlignment.MiddleCenter;
			btn.Padding = new Padding(0);
			btn.Cursor = Cursors.Hand;

			MakeRounded(btn, 5);
		}

		public static void ApplyDangerButton(Button btn)
		{
			btn.FlatStyle = FlatStyle.Flat;
			btn.FlatAppearance.BorderSize = 0;
			btn.FlatAppearance.MouseOverBackColor = Theme.DangerHover;
			btn.FlatAppearance.MouseDownBackColor = Theme.Danger;

			btn.UseVisualStyleBackColor = false;
			btn.BackColor = Theme.Danger;
			btn.ForeColor = Color.White;

			btn.Font = new Font("Segoe UI", 10F);
			btn.TextAlign = ContentAlignment.MiddleCenter;
			btn.Padding = new Padding(0);
			btn.Cursor = Cursors.Hand;

			MakeRounded(btn, 5);
		}

		public static void ApplyMenuButton(Button btn)
		{
			btn.FlatStyle = FlatStyle.Flat;
			btn.FlatAppearance.BorderSize = 0;
			btn.FlatAppearance.MouseOverBackColor = Theme.MenuHover;
			btn.FlatAppearance.MouseDownBackColor = Theme.MenuSelected;

			btn.UseVisualStyleBackColor = false;
			btn.BackColor = Color.Transparent;
			btn.ForeColor = Theme.TextSecondary;

			btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btn.TextAlign = ContentAlignment.MiddleCenter;
			btn.Padding = new Padding(0);
			btn.Cursor = Cursors.Hand;

			MakeRounded(btn, 5);
		}

		public static void ApplyFormStyle(Form form)
		{
			if (form == null || form.IsDisposed) return;

			form.BackColor = Theme.Background;
			form.ForeColor = Theme.TextPrimary;

			// Якщо Handle вже є — застосовуємо відразу
			if (form.IsHandleCreated)
			{
				UseImmersiveDarkMode(form.Handle, Theme.IsDark);
			}
			else
			{
				// Якщо Handle ще не створено (наприклад, у конструкторі), 
				// підписуємося на подію його створення
				form.HandleCreated += (s, e) => 
				{
					if (!form.IsDisposed)
						UseImmersiveDarkMode(form.Handle, Theme.IsDark);
				};
			}

			foreach (Control ctrl in form.Controls)
			{
				ApplyControlStyle(ctrl);
			}
			form.Invalidate(true);
		}

		private static void ApplyControlStyle(Control ctrl)
		{
			if (ctrl == null || ctrl.IsDisposed) return;

			if (ctrl is Panel p)
			{
				p.BorderStyle = BorderStyle.None;
				string name = p.Name.ToLower();
				// Основні контейнери (фон вікна)
				if (name.Contains("main") || name.Contains("content") || name.Contains("split") || name.Contains("background"))
					p.BackColor = Theme.Background;
				// Шапки, бічні панелі, картки та вкладені контейнери
				else if (name.Contains("header") || name.Contains("surface") || name.Contains("card") || 
						 name.Contains("container") || name.Contains("side") || name.Contains("pnl") || 
						 name.Contains("panel") || name.Contains("box"))
					p.BackColor = Theme.Surface;
				else
					p.BackColor = Theme.Background;
			}
			else if (ctrl is TableLayoutPanel or FlowLayoutPanel or SplitContainer)
			{
				ctrl.BackColor = Color.Transparent; 
			}
			else if (ctrl is GroupBox gb)
			{
				gb.ForeColor = Theme.TextPrimary;
				gb.BackColor = Color.Transparent;
				gb.FlatStyle = FlatStyle.Flat;
				// Прибираємо застарілу рамку, залишаючи тільки сучасну логіку
				gb.Paint += (s, e) =>
				{
					e.Graphics.Clear(gb.Parent?.BackColor ?? Theme.Background);
					using (var brush = new SolidBrush(Theme.TextPrimary))
					using (var font = new Font("Segoe UI Semibold", 9F))
					{
						e.Graphics.DrawString(gb.Text, font, brush, 0, 0);
					}
					using (var pen = new Pen(Theme.Border, 1))
					{
						// Тонка лінія під заголовком для розділення
						e.Graphics.DrawLine(pen, 0, 20, gb.Width, 20);
					}
				};
			}
			else if (ctrl is Label l)
			{
				if (!l.Parent?.Name.ToLower().Contains("toppanel") ?? true)
					l.ForeColor = Theme.TextPrimary;
			}
			else if (ctrl is ListBox lb)
			{
				lb.BackColor = Theme.Surface;
				lb.ForeColor = Theme.TextPrimary;
				lb.BorderStyle = BorderStyle.None;
			}
			else if (ctrl is TextBox tb)
			{
				tb.BackColor = Theme.Surface;
				tb.ForeColor = Theme.TextPrimary;
				tb.BorderStyle = BorderStyle.FixedSingle;
			}
			else if (ctrl is RichTextBox rtb)
			{
				// Спеціальна обробка для редактора коду, якщо він має таку назву
				if (rtb.Name.ToLower().Contains("code"))
				{
					rtb.BackColor = Theme.CodeBackground;
					rtb.ForeColor = Theme.CodeForeground;
				}
				else
				{
					rtb.BackColor = Theme.Surface;
					rtb.ForeColor = Theme.TextPrimary;
				}
				rtb.BorderStyle = BorderStyle.None;
			}
			else if (ctrl is ComboBox cb)
			{
				cb.BackColor = Theme.Surface;
				cb.ForeColor = Theme.TextPrimary;
				cb.FlatStyle = FlatStyle.Flat;
			}
			else if (ctrl is DateTimePicker dtp)
			{
				dtp.CalendarMonthBackground = Theme.Surface;
				dtp.CalendarTitleBackColor = Theme.Primary;
				dtp.CalendarForeColor = Theme.TextPrimary;
				dtp.CalendarTitleForeColor = Color.White;
				dtp.CalendarTrailingForeColor = Theme.TextMuted;
				
				// Намагаємось покращити відображення самого контролу
				dtp.BackColor = Theme.Surface; 
				dtp.ForeColor = Theme.TextPrimary;
			}
			else if (ctrl is DataGridView dgv)
			{
				dgv.BackgroundColor = Theme.Surface;
				dgv.GridColor = Theme.Border;
				dgv.BorderStyle = BorderStyle.None;
				dgv.EnableHeadersVisualStyles = false;
				dgv.RowHeadersVisible = false;
				dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				dgv.MultiSelect = false;

				// Сучасний плоский стиль: тільки горизонтальні лінії
				dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
				dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

				// 1. Base Styles
				var baseStyle = new DataGridViewCellStyle
				{
					BackColor = Theme.Surface,
					ForeColor = Theme.TextPrimary,
					SelectionBackColor = Theme.GridSelection,
					SelectionForeColor = Theme.GridSelectionText,
					Alignment = DataGridViewContentAlignment.MiddleLeft,
					Font = new Font("Segoe UI", 10F),
					Padding = new Padding(15, 0, 0, 0)
				};

				// 2. Alternating Row Style
				var altStyle = baseStyle.Clone();
				altStyle.BackColor = Theme.GridAlternate;

				// 3. Header Style
				var headerStyle = new DataGridViewCellStyle
				{
					BackColor = Theme.GridHeader,
					ForeColor = Theme.TextSecondary,
					SelectionBackColor = Theme.GridHeader,
					Font = new Font("Segoe UI Semibold", 10F),
					Alignment = DataGridViewContentAlignment.MiddleLeft,
					Padding = new Padding(15, 0, 0, 0),
					WrapMode = DataGridViewTriState.True
				};

				// Apply to grid layers
				dgv.DefaultCellStyle = baseStyle;
				dgv.RowsDefaultCellStyle = baseStyle;
				dgv.AlternatingRowsDefaultCellStyle = altStyle;
				dgv.ColumnHeadersDefaultCellStyle = headerStyle;
				dgv.RowHeadersDefaultCellStyle = headerStyle;
				
				dgv.RowTemplate.Height = 52;
				dgv.ColumnHeadersHeight = 48;

				// 4. Force columns to inherit correctly (preserve format)
				foreach (DataGridViewColumn col in dgv.Columns)
				{
					var fmt = col.DefaultCellStyle.Format;
					col.DefaultCellStyle = new DataGridViewCellStyle 
					{ 
						BackColor = Color.Empty, 
						ForeColor = Color.Empty,
						SelectionBackColor = Theme.GridSelection,
						SelectionForeColor = Theme.GridSelectionText,
						Format = fmt
					};
				}
				
				dgv.Invalidate();
			}

			foreach (Control child in ctrl.Controls)
			{
				ApplyControlStyle(child);
			}
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