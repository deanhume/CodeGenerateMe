using System;
using System.Drawing;
using System.IO;
using CodeGenSite.Logic.Entity;
using ZXing;
using ZXing.Common;

namespace CodeGenSite.Logic
{
    public class Barcode
    {
        #region BarcodeType
        public BarcodeFormat CalculateType(string barcodeType)
        {
            switch (barcodeType)
            {
                case "code128a":
                    return BarcodeFormat.CODE_128;
                case "code128b":
                    return BarcodeFormat.CODE_128;
                case "code128c":
                    return BarcodeFormat.CODE_128;
                case "code39":
                    return BarcodeFormat.CODE_39;
                case "ean13":
                    return BarcodeFormat.EAN_13;
                case "ean8":
                    return BarcodeFormat.EAN_8;
                case "codabar":
                    return BarcodeFormat.CODABAR;
                case "code93":
                    return BarcodeFormat.CODE_93;
                case "maxicode":
                    return BarcodeFormat.MAXICODE;
                case "pdf417":
                    return BarcodeFormat.PDF_417;
                default:
                    throw new Exception("Type Required");
            }
        }
        #endregion

        /// <summary>
        /// Generates the barcode.
        /// </summary>
        /// <param name="barcodeType">Type of the barcode.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public BarcodeObject GenerateBarcode(string barcodeType, string value)
        {
            // Determine the barcode type
            BarcodeFormat codeType = CalculateType(barcodeType);

            try
            {
                BarcodeWriter writer = new BarcodeWriter();
                writer.Format = codeType;
                writer.Options = new EncodingOptions {Height = 100, Width = 300};
                BitMatrix bitMatrix = writer.Encode(value);
                Bitmap bitmap = writer.Write(value);

                // Convert to the image bytes
                return ConvertToBase64String(bitmap);

            }
            catch (Exception exception)
            {
                throw new Exception("GenerateBarcode is failing", exception);
            }
        }

        /// <summary>
        /// Converts to base64 string.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <returns></returns>
        public BarcodeObject ConvertToBase64String(Image img)
        {
            BarcodeObject barcodeObject = new BarcodeObject();

            using (var memoryStream = new MemoryStream())
            {
                using (var image = img)
                {
                    image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                }

                barcodeObject.Base64Image = Convert.ToBase64String(memoryStream.ToArray());
                barcodeObject.ImageBytes = memoryStream.ToArray();

            }

            return barcodeObject;
        }
    }
}
