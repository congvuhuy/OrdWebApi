using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebApi.Model;
using System.Text.Json.Serialization;

namespace WebApi.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public int ProductGroupId { get; set; }
        [JsonIgnore]
        public ProductGroupDTO ProductGroupDTOs { get; set; }
    }
}
