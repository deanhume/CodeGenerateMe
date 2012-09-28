using Microsoft.WindowsAzure.StorageClient;

namespace CodeGenSite.Logic.Entity
{
    public class ApiKeyEntity : TableServiceEntity
    {
        private readonly ApiKeyCreation _apiKeyCreation = new ApiKeyCreation();

        public ApiKeyEntity(string companyName)
        {
            this.PartitionKey = companyName;
            this.RowKey = _apiKeyCreation.GenerateKey();
        }

        public string CompanyName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactName { get; set; }
        public string ApiKey { get; set; }
    }
}
