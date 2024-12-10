using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Model
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

        public Decimal Price { get; set; }
        public int Quantity {  get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        [ForeignKey("ProductGroupId")]
        public int ProductGroupId {  get; set; }
        //[JsonIgnore]
        public ProductGroup ProductGroup { get; set; }

    }
}
