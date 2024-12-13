namespace WebApi.DTOs
{
    public class MultiProductDTO
    {
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
