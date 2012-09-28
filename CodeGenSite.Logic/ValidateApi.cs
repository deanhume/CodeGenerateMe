namespace CodeGenSite.Logic
{
    using Entity;

    public class ValidateApi
    {
        /// <summary>
        /// Validates the API key and returns an appropriate message.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <returns></returns>
        public Validation ValidateApiKey(string apiKey)
        {
            // Check for null
            if (string.IsNullOrEmpty(apiKey))
            {
                return new Validation {ErrorMessage = "Api key cannot be null", IsValid = false};
            }

            return new Validation { IsValid = true};
        }
    }
}
