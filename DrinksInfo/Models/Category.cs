using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DrinksInfo.Model
{
    public class Category
    {
        [JsonPropertyName("strCategory")]
        public string CategoryName { get; set; }
    }
}
