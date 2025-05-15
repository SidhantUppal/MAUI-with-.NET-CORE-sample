
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WaterBillingApp.Model;

namespace WaterBillingApp.Services
{
    public class ConsumerService
    {
        private readonly HttpClient _httpClient;

        public ConsumerService()
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });
        }


        public async Task<List<ConsumerListDto>> GetConsumersAsync()
        {
            var limit = 10;
            var offset = 0;
            var search = "test";

            var token = Preferences.Get("accessToken", string.Empty);

            if (string.IsNullOrEmpty(token))
                throw new Exception("User token not found. Please log in again.");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            string url = $"{ApiConstants.BaseUrl}/api/consumer?Limit={limit}&Offset={offset}&Search={search}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Read raw response content
                var jsonString = await response.Content.ReadAsStringAsync();

                // Parse JSON to extract only "Items"
                using var doc = JsonDocument.Parse(jsonString);
                var itemsElement = doc.RootElement.GetProperty("items");

                var items = JsonSerializer.Deserialize<List<ConsumerListDto>>(itemsElement.GetRawText());

                return items ?? new List<ConsumerListDto>();
            }

            throw new Exception("Failed to fetch consumers");
        }

    }
}
