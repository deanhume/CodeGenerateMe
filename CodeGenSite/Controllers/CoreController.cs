using System.Collections.Generic;
using System.Web.Mvc;

namespace CodeGenSite.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;
    using Extensions;
    using Logic;
    using Models;
    using Validation = Logic.Entity.Validation;

    public class CoreController : ApiController
    {
        #region Create Barcode
        [HttpGet]
        public HttpResponseMessage Create(string apiKey, string type, string value, string format = "png", bool returnAsBase64 = false)
        {
            // Validate the api key
            ValidateApi validateApi = new ValidateApi();
            Validation validateApiKey = validateApi.ValidateApiKey(apiKey);

            // Api key is invalid.
            if (!validateApiKey.IsValid)
            {
                return BuildErrorResponse(validateApiKey);    
            }

            // Validate the values first
            ValidateInput validation = new ValidateInput();
            Validation validationInput = validation.ValidateBarcodeValue(type, value);
            
            // Result is Valid
            if (validationInput.IsValid)
            {
                return CreateBarcode(type, value, format, returnAsBase64);
            }

            // Result is not valid
            return BuildErrorResponse(validationInput);
        }

        /// <summary>
        /// Creates the barcode.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <param name="returnAsBase64">if set to <c>true</c> [return as base64].</param>
        public HttpResponseMessage CreateBarcode(string type, string value, string format = "png", bool returnAsBase64 = false)
        {
            // Next create the barcode
            Barcode barcode = new Barcode();
            var generate128 = barcode.GenerateBarcode(type, value);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            // Should we return as a base 64 string?
            if (!returnAsBase64)
            {
                //result.Content = new ByteArrayContent(generate128.ImageBytes);

                // Then decide the format
                switch (format)
                {
                    case "jpg":
                        format = string.Format("image/{0}", format);
                        break;
                    case "gif":
                        format = string.Format("image/{0}", format);
                        break;
                    default:
                        format = "image/png";
                        break;
                }

                result.Content.Headers.ContentType = new MediaTypeHeaderValue(format);
            }
            else
            {
                result.Content = new StringContent("");//new StringContent(new {generate128.Base64Image, DateCreated = DateTime.Now.ToShortDateString()}.ToJSON());
            }

            return result;
        }

        #endregion

        #region CreateBatch

        [HttpPost]
        public HttpResponseMessage CreateBatch(ImageBatch imageBatch)
        {
            // Our response object
            ImageBatchResponse batchResult = new ImageBatchResponse();

            // Validate the api key
            ValidateApi validateApi = new ValidateApi();
            Validation validateApiKey = validateApi.ValidateApiKey(imageBatch.ApiKey);

            // Api key is invalid.
            if (!validateApiKey.IsValid)
            {
                return BuildErrorResponse(validateApiKey);
            }

            // Loop through the values
            List<ImageResponse> imageList = new List<ImageResponse>();
            foreach (ImageDetails detail in imageBatch.ImageDetails)
            {
                // Validate the values first
                ValidateInput validation = new ValidateInput();
                Validation validationInput = validation.ValidateBarcodeValue(detail.Type, detail.Value);

                // Result is Valid
                ImageResponse imageResponse = new ImageResponse();
                if (validationInput.IsValid)
                {
                    imageResponse.ImageUrl = string.Format(
                        "http://www.codegenerate.me/Code/Barcode?type={0}&value={1}", detail.Type, detail.Value);
                    imageResponse.Result = "Success";
                    batchResult.SuccessfulCount++;
                }
                else
                {
                    imageResponse.Result = validationInput.ErrorMessage;
                }

                imageList.Add(imageResponse);
            }

            // Add the images
            batchResult.Images = imageList.ToArray();

            // Build the object to return
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
                                             {Content = new StringContent(batchResult.ToJSON())};

            return result;
        }

        #endregion

        /// <summary>
        /// Returns an appropriate error message - this method is reused.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <returns>A Http Reponse Message.</returns>
        public HttpResponseMessage BuildErrorResponse(Validation validationResult)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
            responseMessage.Content = new StringContent(new ErrorDetails { Error = validationResult.ErrorMessage }.ToJSON());
            responseMessage.ReasonPhrase = validationResult.ErrorMessage;

            return responseMessage;
        }
    }
}
