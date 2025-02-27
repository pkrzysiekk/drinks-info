using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrinksInfo.Model
{
    public class Categories
    {
        [JsonPropertyName("drinks")]
        public List<Category> categoriesList { get; set; }

    }
}
