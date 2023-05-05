using System.Data.Common;
using System.Text.Json.Serialization;

namespace DotNetExample.Models
{
    public class CreditCardForm
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}