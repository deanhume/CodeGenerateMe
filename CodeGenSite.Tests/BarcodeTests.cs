using CodeGenSite.Logic;
using CodeGenSite.Logic.Entity;
using NUnit.Framework;

namespace CodeGenSite.Tests
{
    [TestFixture]
    public class BarcodeTests
    {
        [Test]
        public void GenerateBarcode_WithCodaBar_ShouldReturnBase64String()
        {
            // Arrange
            const string codabarType = "codabar";
            const string value = "A123*";

            // Act
            Barcode barcode = new Barcode();
            BarcodeObject calculated = barcode.GenerateBarcode(codabarType, value); 

            // Assert
            Assert.That(calculated.ImageBytes.Length, Is.EqualTo(4907));
        }

        [Test]
        public void GenerateBarcode_WithPdf417_ShouldReturnBase64String()
        {
            // Arrange
            const string codabarType = "pdf417";
            const string value = "A123*";

            // Act
            Barcode barcode = new Barcode();
            BarcodeObject calculated = barcode.GenerateBarcode(codabarType, value);

            // Assert
            Assert.That(calculated.ImageBytes.Length, Is.EqualTo(2756));
        }

        [Test]
        public void GenerateBarcode_WithEan8_ShouldReturnBase64String()
        {
            // Arrange
            const string codabarType = "ean8";
            const string value = "12435689";

            // Act
            Barcode barcode = new Barcode();
            BarcodeObject calculated = barcode.GenerateBarcode(codabarType, value);

            // Assert
            Assert.That(calculated.ImageBytes.Length, Is.EqualTo(5962));
        }

        [Test]
        public void GenerateBarcode_WithCode39_ShouldReturnBase64String()
        {
            // Arrange
            const string codabarType = "code39";
            const string value = "A123*";

            // Act
            Barcode barcode = new Barcode();
            BarcodeObject calculated = barcode.GenerateBarcode(codabarType, value);

            // Assert
            Assert.That(calculated.ImageBytes.Length, Is.EqualTo(4307));
        }
    }
}
