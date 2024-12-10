using System.ComponentModel.DataAnnotations;
using WebApi.Model;

namespace WebApi.DTOs
{
    public class ProductGroupDTO
    {
        public int ProductGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public ICollection<ProductDTO> ProductDTOs { get; set; }
    }
}
