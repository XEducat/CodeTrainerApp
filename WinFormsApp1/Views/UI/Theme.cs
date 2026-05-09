using System.Drawing;

namespace CodeTrainerApp.UI
{
	public static class Theme
	{
		// Основні кольори (Modern Blue & Emerald)
		public static Color Primary = Color.FromArgb(79, 70, 229); // Indigo
		public static Color PrimaryHover = Color.FromArgb(67, 56, 202);

		public static Color Success = Color.FromArgb(16, 185, 129); // Emerald 500
		public static Color SuccessHover = Color.FromArgb(5, 150, 105); // Emerald 600
		public static Color Danger = Color.FromArgb(239, 68, 68);  // Rose 500
		public static Color DangerHover = Color.FromArgb(220, 38, 38);  // Rose 600
		public static Color Warning = Color.FromArgb(245, 158, 11); // Amber 500
		public static Color WarningHover = Color.FromArgb(217, 119, 6); // Amber 600


		// Фон
		public static Color Background = Color.FromArgb(243, 244, 246); // Gray 100
		public static Color Sidebar = Color.White;
		public static Color Surface = Color.White;

		// Текст
		public static Color TextPrimary = Color.FromArgb(17, 24, 39); // Gray 900
		public static Color TextSecondary = Color.FromArgb(75, 85, 99); // Gray 600
		public static Color TextMuted = Color.FromArgb(156, 163, 175); // Gray 400

		// Елементи
		public static Color Border = Color.FromArgb(229, 231, 235); // Gray 200
		public static Color MenuHover = Color.FromArgb(243, 244, 246); // Gray 100
		public static Color MenuSelected = Color.FromArgb(229, 231, 235); // Gray 200
		public static Color CodeBackground = Color.FromArgb(30, 30, 30); // Dark Mode for code
		public static Color CodeForeground = Color.FromArgb(220, 220, 220);
	}
}