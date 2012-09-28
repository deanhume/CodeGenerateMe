using System;
using System.Linq;
using CodeGenSite.Logic.Entity;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace CodeGenSite.Logic
{
    public class ApiKeyCreation
    {
        #region ctor
        private readonly CloudStorageAccount _storageAccount;

        public ApiKeyCreation()
        {
            _storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));
        }
        #endregion

        public void FirstRun(ApiKeyEntity apiKeyEntity)
        {
            // Create the table client
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist
            const string tableName = "keys";
            tableClient.CreateTableIfNotExist(tableName);

            // Get the data service context
            TableServiceContext serviceContext = tableClient.GetDataServiceContext();

            // Add the new customer to the people table
            serviceContext.AddObject("keys", apiKeyEntity);

            // Submit the operation to the table service
            serviceContext.SaveChangesWithRetries();

            ApiKeyEntity retrieveApiKey = RetrieveApiKey(apiKeyEntity.ApiKey);
        }

        public ApiKeyEntity RetrieveApiKey(string apiKey)
        {
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();

            // Get the data service context
            TableServiceContext serviceContext = tableClient.GetDataServiceContext();

            CloudTableQuery<ApiKeyEntity> partitionQuery = (from e in serviceContext.CreateQuery<ApiKeyEntity>("keys")
                                                            where e.PartitionKey == apiKey
                                                            select e).AsTableServiceQuery<ApiKeyEntity>();

            foreach (ApiKeyEntity apiKeyEntity in partitionQuery)
            {
                return apiKeyEntity;
            }

            return null;
        }

        /// <summary>
        /// Generates a unique API key.
        /// </summary>
        /// <returns></returns>
        public string GenerateKey()
        {
            Guid g = Guid.NewGuid();
            string guidString = Convert.ToBase64String(g.ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            guidString = guidString.Replace("/", "");
            guidString = guidString.Replace("*", "");
            guidString = guidString.Replace("&", "");

            return guidString;
        }
    }
}
