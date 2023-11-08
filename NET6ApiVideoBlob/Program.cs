using Azure.Storage.Blobs;

namespace NET6ApiVideoBlob
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton(provider => {
                IConfigurationRoot configuration = new ConfigurationBuilder().
                AddJsonFile("appsettings.json").
                Build();
                return configuration;
            });

            IConfigurationRoot configuration = new ConfigurationBuilder().
                AddJsonFile("appsettings.json").
                Build();

            string connectionString = configuration["StorageConnectionString"];

            builder.Services.AddSingleton(provider => {
                BlobServiceClient serviceClient = new BlobServiceClient(connectionString);

                //var serviceProperties = await blobServiceClient.GetPropertiesAsync();
                //serviceProperties.Value.DefaultServiceVersion = "2021-06-08";
                //await blobServiceClient.SetPropertiesAsync(serviceProperties.Value);

                return serviceClient;
            });

            /*builder.Services.AddSingleton<BlobServiceClient>((serviceProvider) => {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var storageAccountUri = config["StorageAccount:Uri"];
                var accountUri = new Uri(storageAccountUri);
                var azureCredentialOptions = new DefaultAzureCredentialOptions();
                azureCredentialOptions.VisualStudioTenantId = config["VisualStudioTenantId"];
                var credential = new DefaultAzureCredential(azureCredentialOptions);
                var blobServiceClient = new BlobServiceClient(accountUri, credential);
                return blobServiceClient;
            });*/

            ////change request to Accept-Ranges HTTP
            /*var connectionString = "YOUR-STORAGE-ACCOUNT-CONNECTION-STRING";
            var blobServiceClient = new BlobServiceClient(connectionString);
            var serviceProperties = await blobServiceClient.GetPropertiesAsync();
            serviceProperties.Value.DefaultServiceVersion = "2021-06-08";
            await blobServiceClient.SetPropertiesAsync(serviceProperties.Value);*/


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}