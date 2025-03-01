using System.Text.Json.Serialization;

namespace DrinksInfo.Model
{
    public class Category
    {
        [JsonPropertyName("strCategory")]
        public string CategoryName { get; set; }
    }
}
