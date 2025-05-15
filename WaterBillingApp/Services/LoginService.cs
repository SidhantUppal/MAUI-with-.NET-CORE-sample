using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using WaterBillingApp.Model;
using System.Diagnostics;


namespace WaterBillingApp.Services
{
    public class LoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService()
        {
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });
        }
        public async Task<List<string>> FetchLoginTypesAsync()
        {
            try
            {
                var limit = 10;
                var offset = 0;
                var search = "test";
                string url = $"{ApiConstants.BaseUrl}/api/account/getRoles?Limit={limit}&Offset={offset}&Search={search}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(jsonString);

                    var items = jsonDocument.RootElement.GetProperty("items")
                        .EnumerateArray()
                        .Select(role => role.GetProperty("name").GetString())
                        .ToList();

                    return items;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Connection Error: {ex.Message}");
            }

            return new List<string>();
        }
        public async Task<List<string>> FetchLoginDivisionsAsync()
        {
            try
            {
                var limit = 10;
                var offset = 0;
                var search = "test";
                string url = $"{ApiConstants.BaseUrl}/api/division?Limit={limit}&Offset={offset}&Search={search}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var jsonDocument = JsonDocument.Parse(jsonString);

                    var items = jsonDocument.RootElement.GetProperty("items")
                        .EnumerateArray()
                        .Select(div => div.GetProperty("officeName").GetString())
                        .ToList();

                    return items;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Connection Error: {ex.Message}");
            }

            return new List<string>();
        }
        public async Task<CurrentUserDto?> LoginAsync(string username, string password, string role, string division)
        {
            try
            {
                var loginData = new
                {
                    Identifier = username,
                    Password = password,
                    Role = role,
                    Division = division
                };

                var jsonContent = JsonSerializer.Serialize(loginData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{ApiConstants.BaseUrl}/api/account/login", content);

                if (!response.IsSuccessStatusCode) return null;

                var resultString = await response.Content.ReadAsStringAsync();
                var loginResult = JsonSerializer.Deserialize<LoginResult>(resultString);

                if (loginResult?.AccessToken == null) return null;

                Preferences.Set("accessToken", loginResult.AccessToken);

                // Call UserDetails API with accessToken
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", loginResult.AccessToken);

                var userDetailsResponse = await _httpClient.GetAsync($"{ApiConstants.BaseUrl}/api/account/loginUserDetails");

                if (!userDetailsResponse.IsSuccessStatusCode) return null;

                var userDetailsJson = await userDetailsResponse.Content.ReadAsStringAsync();
                var userDetails = JsonSerializer.Deserialize<CurrentUserDto>(userDetailsJson);

                return userDetails;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Login failed: {ex.Message}");
                return null;
            }
        }
    }
}
