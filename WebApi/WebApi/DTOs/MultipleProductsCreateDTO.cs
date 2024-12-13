namespace WebApi.DTOs
{
    public class MultipleProductsCreateDTO
    {
        public List<MultiProductDTO> Products { get; set; }
        public ProductGroupCreateDTO ProductGroups { get; set; }
    }
}
