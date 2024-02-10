using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            
            var response = await _httpClient.PostAsync($"{_config["CommandService"]}/api/c/platforms" , httpContent);

            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to Command Service was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to Command Service was NOT OK!");
            }
        }
    }
}