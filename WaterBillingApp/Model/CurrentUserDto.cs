using System.Text.Json.Serialization;

namespace WaterBillingApp.Model
{
    public class CurrentUserDto
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("divisonId")]
        public int DivisonId { get; set; }

        [JsonPropertyName("division")]
        public string Division { get; set; }

        [JsonPropertyName("cityId")]
        public int CityId { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("roleId")]
        public int RoleId { get; set; }

        [JsonPropertyName("roleName")]
        public string RoleName { get; set; }

        [JsonPropertyName("consumerId")]
        public int? ConsumerId { get; set; }
    }
}
