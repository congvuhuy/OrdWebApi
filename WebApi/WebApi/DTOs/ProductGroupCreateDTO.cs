namespace WebApi.DTOs
{
    public class ProductGroupCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
