using System.Text.Json.Serialization;


namespace DrinksInfo.Model
{
    public class Categories
    {
        [JsonPropertyName("drinks")]
        public List<Category> categoriesList { get; set; }

    }
}
