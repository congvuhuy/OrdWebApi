namespace WebApi.DTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public int ProductGroupId { get; set; }
    }
}
