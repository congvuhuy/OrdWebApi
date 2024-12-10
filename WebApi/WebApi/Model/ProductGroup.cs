using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi.Model
{
    [Table("ProductGroup")]

    public class ProductGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductGroupId {  get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set;}
        public Boolean IsDeleted { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set;}

       
    }
}
