namespace WebApi.DTOs
{
    public class MultipleProductsCreateDTO
    {
        public List<ProductCreateDTO> Products { get; set; }
        public ProductGroupCreateDTO ProductGroups { get; set; }
    }
}
