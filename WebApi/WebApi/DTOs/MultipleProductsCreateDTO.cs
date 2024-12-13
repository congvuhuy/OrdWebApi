namespace WebApi.DTOs
{
    public class MultipleProductsCreateDTO
    {
        public List<ProductCreateDTO> Products { get; set; }
        public List<ProductGroupCreateDTO> ProductGroups { get; set; }
    }
}
