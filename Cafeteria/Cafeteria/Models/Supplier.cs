using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        [DisplayName("Mã nhà cung cấp")]
        public Guid SupplierId { get; set; }

        [Required]
        [DisplayName("Tên nhà cung cấp")]
        public string SupplierName { get; set; } = string.Empty;

        [Required, EmailAddress]
        [DisplayName("Email")]
        public string SupplierEmail { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        [Phone]
        [DisplayName("Số điện thoại")]
        public string SupplierPhone { get; set; } = string.Empty;

        [Required]
        [DisplayName("Địa chỉ")]
        public string SupplierAddress { get; set; } = string.Empty;

        // A supplier contains many ingredients.
        public virtual ICollection<Ingredient> SupplierIngredients { get; set; } = new List<Ingredient>();
    }
}
