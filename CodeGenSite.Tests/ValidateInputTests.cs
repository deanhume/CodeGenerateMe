using CodeGenSite.Logic;
using CodeGenSite.Logic.Entity;
using NUnit.Framework;

namespace CodeGenSite.Tests
{
    [TestFixture]
    public class ValidateInputTests
    {
        [Test]
        public void ValidateCodaBar_WithCorrectParams_ShouldReturnTrue()
        {
            // Arrange
            const string codabarType = "codabar";
            const string value = "A123*";

            // Act
            ValidateInput validateInput = new ValidateInput();
            Validation calculated = validateInput.ValidateBarcodeValue(codabarType, value);

            // Assert
            Assert.That(calculated.IsValid, Is.True);
        }

        [Test]
        public void ValidateCodaBar_WithIncorrectStartingParams_ShouldReturnFalse()
        {
            // Arrange
            const string codabarType = "codabar";
            const string value = "123T";

            // Act
            ValidateInput validateInput = new ValidateInput();
            Validation calculated = validateInput.ValidateBarcodeValue(codabarType, value);

            // Assert
            Assert.That(calculated.IsValid, Is.False);
        }

        [Test]
        public void ValidateCodaBar_WithIncorrectEndingParams_ShouldReturnFalse()
        {
            // Arrange
            const string codabarType = "codabar";
            const string value = "D123";

            // Act
            ValidateInput validateInput = new ValidateInput();
            Validation calculated = validateInput.ValidateBarcodeValue(codabarType, value);

            // Assert
            Assert.That(calculated.IsValid, Is.False);
        }

        [Test]
        public void ValidateEan8_WithCorrectParams_ShouldReturnTrue()
        {
            // Arrange
            const string codabarType = "ean8";
            const string value = "12345678";

            // Act
            ValidateInput validateInput = new ValidateInput();
            Validation calculated = validateInput.ValidateBarcodeValue(codabarType, value);

            // Assert
            Assert.That(calculated.IsValid, Is.True);
        }

        [Test]
        public void ValidateEan8_WithInCorrectParams_ShouldReturnTrue()
        {
            // Arrange
            const string codabarType = "ean8";
            const string value = "12345";

            // Act
            ValidateInput validateInput = new ValidateInput();
            Validation calculated = validateInput.ValidateBarcodeValue(codabarType, value);

            // Assert
            Assert.That(calculated.IsValid, Is.False);
        }

        [Test]
        public void ValidateCode39_WithCorrectParams_ShouldReturnTrue()
        {
            // Arrange
            const string codabarType = "code39";
            const string value = "12345A";

            // Act
            ValidateInput validateInput = new ValidateInput();
            Validation calculated = validateInput.ValidateBarcodeValue(codabarType, value);

            // Assert
            Assert.That(calculated.IsValid, Is.True);
        }
    }
}
