using System;
using System.Drawing;
using System.IO;

namespace CodeTrainerApp.UI
{
	public enum ThemeMode { Light, Dark }

	public static class Theme
	{
		public static event Action ThemeChanged;
		private static ThemeMode _currentMode = ThemeMode.Light;
		private static readonly string SettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "theme.txt");

		static Theme()
		{
			LoadTheme();
		}

		public static ThemeMode CurrentMode
		{
			get => _currentMode;
			set
			{
				if (_currentMode != value)
				{
					_currentMode = value;
					SaveTheme();
					ThemeChanged?.Invoke();
				}
			}
		}

		private static void LoadTheme()
		{
			try
			{
				if (File.Exists(SettingsPath))
				{
					string content = File.ReadAllText(SettingsPath).Trim();
					if (Enum.TryParse(content, out ThemeMode mode))
					{
						_currentMode = mode;
					}
				}
			}
			catch { /* Ignore errors on load */ }
		}

		private static void SaveTheme()
		{
			try
			{
				File.WriteAllText(SettingsPath, _currentMode.ToString());
			}
			catch { /* Ignore errors on save */ }
		}

		public static bool IsDark => CurrentMode == ThemeMode.Dark;

		// Основні кольори
		public static Color Primary => IsDark ? Color.FromArgb(129, 140, 248) : Color.FromArgb(79, 70, 229);
		public static Color PrimaryHover => IsDark ? Color.FromArgb(165, 180, 252) : Color.FromArgb(67, 56, 202);

		public static Color Success => Color.FromArgb(16, 185, 129);
		public static Color SuccessHover => Color.FromArgb(5, 150, 105);
		public static Color Danger => Color.FromArgb(239, 68, 68);
		public static Color DangerHover => Color.FromArgb(220, 38, 38);
		public static Color Warning => Color.FromArgb(245, 158, 11);
		public static Color WarningHover => Color.FromArgb(217, 119, 6);


		// Фон
		public static Color Background => IsDark ? Color.FromArgb(17, 24, 39) : Color.FromArgb(243, 244, 246); // Gray 100
		public static Color Sidebar => IsDark ? Color.FromArgb(31, 41, 55) : Color.FromArgb(249, 250, 251); // Gray 50
		public static Color Surface => IsDark ? Color.FromArgb(31, 41, 55) : Color.White;

		// Текст
		public static Color TextPrimary => IsDark ? Color.FromArgb(249, 250, 251) : Color.FromArgb(15, 23, 42); // Slate 900
		public static Color TextSecondary => IsDark ? Color.FromArgb(209, 213, 219) : Color.FromArgb(71, 85, 105); // Slate 600
		public static Color TextMuted => IsDark ? Color.FromArgb(107, 114, 128) : Color.FromArgb(148, 163, 184); // Slate 400

		// Елементи
		public static Color Border => IsDark ? Color.FromArgb(55, 65, 81) : Color.FromArgb(226, 232, 240); // Slate 200
		public static Color MenuHover => IsDark ? Color.FromArgb(55, 65, 81) : Color.FromArgb(226, 232, 240); // Slate 200
		public static Color MenuSelected => IsDark ? Color.FromArgb(75, 85, 99) : Color.FromArgb(203, 213, 225); // Slate 300
		public static Color CodeBackground => Color.FromArgb(30, 30, 30);
		public static Color CodeForeground => Color.FromArgb(220, 220, 220);
		public static Color CodeHeader => IsDark ? Color.FromArgb(17, 24, 39) : Color.FromArgb(241, 245, 249); // Slate 100

		// Таблиці та списки
		public static Color GridHeader => IsDark ? Color.FromArgb(45, 55, 72) : Color.FromArgb(241, 245, 249); // Slate 100
		public static Color GridAlternate => IsDark ? Color.FromArgb(37, 47, 63) : Color.FromArgb(248, 250, 252); // Slate 50
		public static Color GridSelection => IsDark ? Color.FromArgb(63, 72, 87) : Color.FromArgb(224, 231, 255); // Light Indigo
		public static Color GridSelectionText => IsDark ? Theme.Primary : Color.FromArgb(67, 56, 202); // Darker Indigo in Light

		// Бейджі
		public static Color BadgeBackground => IsDark ? Color.FromArgb(55, 65, 81) : Color.FromArgb(230, 240, 255);
	}
}