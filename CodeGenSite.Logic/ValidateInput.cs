using System;
using System.Text.RegularExpressions;
using CodeGenSite.Logic.Entity;

namespace CodeGenSite.Logic
{
    public class ValidateInput
    {
        /// <summary>
        /// Validates the barcode value.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public Validation ValidateBarcodeValue(string type, string value)
        {
            Validation validation = new Validation();

            // Check for null values
            if (type == null || value == null)
            {
                validation.IsValid = false;
                validation.ErrorMessage = "Please supply a value and type";
                return validation;
            }

            switch (type)
            {
                case "code128a":
                    {
                        // Should only be numeric
                        validation = IsNumeric(value);
                        break;
                    }
                case "code128b":
                    {
                        // Should only be numeric
                        validation = IsNumeric(value);
                        break;
                    }
                case "code128c":
                    {
                        // Should only be numeric
                        validation = IsNumeric(value);
                        break;
                    }
                case "bookland":
                    {
                        validation = ValidateBookland(value);
                        break;
                    }
                case "ean13":
                    {
                        validation = ValidateEan13(value);
                        break;
                    }
                case "code11":
                    {
                        validation = IsNumeric(value);
                        break;
                    }
                case "code39":
                    {
                        validation.IsValid = true;
                        break;
                    }
                    // Done
                case "codabar":
                    {
                        validation = ValidateCodaBar(value);
                        break;
                    }
                // Done
                case "pdf417":
                    {
                        validation.IsValid = true;
                        break;
                    }
                    //done
                case "ean8":
                    {
                        validation = ValidateEan8(value);
                        break;
                    }
                case "qrcode":
                    {
                        validation.IsValid = true;
                        return validation;
                    }
                default:
                    {
                        validation = new Validation {ErrorMessage = "Code type not recognized.", IsValid = false};
                        break;
                    }
            }

            return validation;
        }

        private Validation ValidateCodaBar(string value)
        {
            Validation validation = new Validation();

            // Codabar should start with one of the following: A, B, C, D
            if (value.StartsWith("A") || value.StartsWith("B") || value.StartsWith("C") || value.StartsWith("D"))
            {
                // Codabar should end with one of the following: T, N, *, E
                if (value.EndsWith("T") || value.EndsWith("N") || value.EndsWith("*") || value.EndsWith("E"))
                {
                    return new Validation {ErrorMessage = string.Empty, IsValid = true};
                }
                else
                {
                    return new Validation {ErrorMessage = "Codabar should end with one of the following: T, N, *, E", IsValid = false};
                }
            }

            return new Validation {ErrorMessage = "Codabar should start with one of the following: A, B, C, D", IsValid = false};
        }

        /// <summary>
        /// Validates the ean8 value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private Validation ValidateEan8(string value)
        {
            Validation validation = IsNumeric(value);

            // Length should only be max 8 numbers
            if (value.Length != 8)
            {
                validation.IsValid = false;
                validation.ErrorMessage = "The value must be 8 digits long";

                return validation;
            }

            return validation;
        }

        /// <summary>
        /// Validates the bookland barcode.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private Validation ValidateBookland(string value)
        {
            Validation isNumeric = IsNumeric(value);

            if (!isNumeric.IsValid)
            {
                return isNumeric;
            }

            if (value.Substring(0,3) != "978")
            {
                return new Validation { ErrorMessage = "The value must start with 978", IsValid = false };
            }

            if (value.Length < 9)
            {
                return new Validation { ErrorMessage = "Please supply a minimum of 9 numbers", IsValid = false };
            }

            if (value.Length == 11)
            {
                return new Validation { ErrorMessage = "ISBN codes need to be 9, 10, 12, or 13 digits long", IsValid = false };
            }

            return new Validation { ErrorMessage = string.Empty, IsValid = true };
        }


        /// <summary>
        /// Validates the ean13 barcode.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private Validation ValidateEan13(string value)
        {
            Regex reNum = new Regex(@"^\d+$");
            if (reNum.Match(value).Success == false)
            {
                return new Validation { ErrorMessage = "The value must be numeric", IsValid = false };
            }

            if (value.Length != 12)
            {
                return new Validation { ErrorMessage = "The value must be 12 digits long", IsValid = false };
            }

            return new Validation { ErrorMessage = string.Empty, IsValid = true };
        }  

        /// <summary>
        /// Determines whether the specified value is numeric.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is numeric; otherwise, <c>false</c>.
        /// </returns>
        private Validation IsNumeric(string value)
        {
            long parsedInteger;
            bool isNumeric = long.TryParse(value, out parsedInteger);

            if (!isNumeric)
            {
                return new Validation { ErrorMessage = "Please supply numeric values only", IsValid = false };
            }

            return new Validation {ErrorMessage = string.Empty, IsValid = true};
        }
    }
}
