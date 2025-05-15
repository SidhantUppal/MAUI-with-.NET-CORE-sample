using System;
using System.Text.Json.Serialization;

namespace WaterBillingApp.Model
{
    public class ConsumerListDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("occupations")]
        public string Occupations { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("status")]
        public Status? Status { get; set; }

        [JsonPropertyName("paymentType")]
        public PaymentType? PaymentType { get; set; }
    }

    public enum Status
    {
        Pending = 1,
        Active = 2,
        Disconnected = 3
    }

    public enum PaymentType
    {
        Online = 1,
        Offline = 2
    }
}
