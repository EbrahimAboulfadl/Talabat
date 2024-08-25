using System.Text.Json.Serialization;

namespace Talabat.Core.Entities
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; set; } = new();

    }
}