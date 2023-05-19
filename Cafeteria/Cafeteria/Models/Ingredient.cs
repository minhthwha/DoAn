using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models
{
    [Table("Ingredients")]
    public class Ingredient
    {
        [Key]
        [DisplayName("Mã nguyên vật liệu")]
        public Guid IngredientId { get; set; }

        [Required]
        [DisplayName("Tên nguyên vật liệu")]
        public string IngredientName { get; set; } = string.Empty;

        [Required]
        [DisplayName("Số lượng")]
        public float Quantity { get; set; }

        [Required]
        [MaxLength(10)]
        [DisplayName("Đơn vị tính")]
        public string CalculationUnit { get; set; } = string.Empty;

        [Required]
        [DisplayName("Giá nhập")]
        public double Price { get; set; }

        [Required]
        [DisplayName("Ngày nhập kho")]
        [DataType(DataType.DateTime)]
        public DateTime ImportedAt { get; set; }

        [Required]
        [DisplayName("Ngày xuất kho")]
        [DataType(DataType.DateTime)]
        public DateTime ExportedAt { get; set; }

        [Required]
        [DisplayName("Hạn sử dụng")]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [DisplayName("Trạng thái hoạt động")]
        public bool IsActive { get; set; } = true;

        // An ingredient belongs to many drink items.
        public virtual ICollection<DrinkItem> DrinkItems { get; set; } = new List<DrinkItem>();

        // An ingredients belongs to one suppplier.
        [ForeignKey("SupplierId")]
        public Guid SupplierId { get; set; }
    }
}
