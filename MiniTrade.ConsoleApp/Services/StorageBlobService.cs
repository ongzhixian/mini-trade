using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniTrade.ConsoleApp.Models;
using System.Text;

namespace MiniTrade.ConsoleApp.Services;

public class StorageBlobService : BackgroundService
{
    private readonly ILogger<StorageBlobService> logger;
    private readonly IOptions<AzureStorageSetting> options;

    public StorageBlobService(
        ILogger<StorageBlobService> logger, 
        IOptions<AzureStorageSetting> options)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation($"{nameof(StorageBlobService)} is starting.");

        await InitializeAsync(stoppingToken);

#pragma warning disable S125 // Sections of code should not be commented out
        //if (serviceClient == null)
        //    throw new InvalidOperationException();
#pragma warning restore S125 // Sections of code should not be commented out

        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("do nothing");

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task InitializeAsync(CancellationToken stopToken)
    {
#pragma warning disable S125 // Sections of code should not be commented out
        //const string tableName = "test";

        //TableServiceClient tableServiceClient = new(options.Value.ConnectionString);

        //Azure.Response<TableItem>? createTableResponse = await tableServiceClient.CreateTableIfNotExistsAsync(tableName, stopToken);

        //if (createTableResponse != null)
        //    logger.LogInformation("Create table {tableName}", tableName);

        //logger.LogInformation("Get table {tableName}", tableName);

        //tableClient = tableServiceClient.GetTableClient(tableName);

        //string partitionKey = "stationaries";
        //string rowKey = "k2";

        //var entity = new TableEntity(partitionKey, rowKey)
        //{
        //    { "Product", "Marker Set" },
        //    { "Price", 5.00 },
        //    { "Quantity", 21 }
        //};

        //await tableClient.AddEntityAsync(entity, stopToken);
#pragma warning restore S125 // Sections of code should not be commented out

        //////////////////////////////////////////

        // Create a BlobServiceClient object which will be used to create a container client
        BlobServiceClient blobServiceClient = new BlobServiceClient(options.Value.ConnectionString);

        //Create a unique name for the container
        string containerName = "notes";

        // Create the container and return a container client object
        // BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName)

#pragma warning disable S125 // Sections of code should not be commented out
        //if (await containerClient.ExistsAsync())
        //{
        //}

        //if (containerClient == null)
        //{
        //    Console.WriteLine("containerClient  is null");
        //} 
        //else
        //{
        //    Console.WriteLine("containerClient  is NOT null");
        //}
#pragma warning restore S125 // Sections of code should not be commented out

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient("my-in-memory.txt");

        //blobClient.Exists

        MemoryStream ms;
        using (ms = new MemoryStream())
        {
            Azure.Response? response = await blobClient.DownloadToAsync(ms, stopToken);

            logger.LogInformation("{response}", response);
        }

        Console.WriteLine("DOWNLOAD: {0}", Encoding.UTF8.GetString(ms.ToArray()));


#pragma warning disable S125 // Sections of code should not be commented out
        //using (MemoryStream ms = new MemoryStream())
        //{
        //    ms.Write(Encoding.UTF8.GetBytes("This is a sample text."));
        //    BlobClient blobClient = containerClient.GetBlobClient("my-in-memory.txt");
        //    ms.Seek(0, SeekOrigin.Begin);
        //    await blobClient.UploadAsync(ms, stopToken);
        //}

        // Create a local file in the ./data/ directory for uploading and downloading
        //string localPath = "./data/";
        //string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
        //string localFilePath = Path.Combine(localPath, fileName);

        //// Write text to the file
        //await File.WriteAllTextAsync(localFilePath, "Hello, World!");

        //// Get a reference to a blob


        //Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

        //// Upload data from the local file
        //await blobClient.UploadAsync(localFilePath, true);
#pragma warning restore S125 // Sections of code should not be commented out

    }

}