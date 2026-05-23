using CodeTrainerApp.UI;
using System.Drawing;
using Xunit;

namespace CodeTrainer.Tests.App
{
    public class ThemeTests
    {
        [Fact]
        public void Theme_ToggleMode_ShouldChangeColors()
        {
            // Arrange
            Theme.CurrentMode = ThemeMode.Light;
            Color lightPrimary = Theme.Primary;

            // Act
            Theme.CurrentMode = ThemeMode.Dark;
            Color darkPrimary = Theme.Primary;

            // Assert
            Assert.NotEqual(lightPrimary, darkPrimary);
            Assert.True(Theme.IsDark);
        }

        [Fact]
        public void Theme_DefaultMode_ShouldBeCorrect()
        {
            // Провірка чи завантажується початковий стан
            Assert.NotNull(Theme.Primary);
            Assert.NotNull(Theme.Background);
        }

        [Fact]
        public void Theme_Event_ShouldFireOnChanged()
        {
            // Arrange
            bool eventFired = false;
            Theme.ThemeChanged += () => eventFired = true;

            // Act
            Theme.CurrentMode = Theme.IsDark ? ThemeMode.Light : ThemeMode.Dark;

            // Assert
            Assert.True(eventFired);
        }
    }
}
