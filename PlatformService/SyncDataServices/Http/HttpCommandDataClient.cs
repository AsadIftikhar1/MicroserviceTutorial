using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RestSharp;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;

        // Constructor injection of RestClient and IConfiguration
        public HttpCommandDataClient(RestClient restClient, IConfiguration configuration)
        {
            _restClient = restClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var jsonContent = JsonSerializer.Serialize(plat);
            var request = new RestRequest(_configuration["CommandService"], Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddJsonBody(jsonContent);  // Pass the serialized JSON here
            try
            {
                // Execute the request asynchronously
                var response = await _restClient.ExecuteAsync(request);

                // Check if the response was successful
                if (response.IsSuccessful)
                {
                    Console.WriteLine("--> Sync POST to CommandService was OK!");
                }
                else
                {
                    Console.WriteLine("--> Sync POST to CommandService was NOT OK!");
                    Console.WriteLine($"Response Status: {response.StatusCode}");
                    Console.WriteLine($"Response Content: {response.Content}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: ", ex.Message);
            }
        }
    }
}
