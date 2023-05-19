using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Cafeteria.Models
{
    [Table("Drinks")]
    public class Drink
    {
        [Key]
        [DisplayName("Mã đồ uống")]
        public Guid DrinkId { get; set; }

        [Required]
        [DisplayName("Tên đồ uống")]
        public string DrinkName { get; set; } = string.Empty;
        
        [Required]
        [DisplayName("Giá bán")]
        public double Price { get; set; }

        [Required]
        [MaxLength(10)]
        [DisplayName("Đơn vị tính")]
        public string CalculationUnit { get; set; } = string.Empty;

        [AllowNull]
        [DisplayName("Hình ảnh")]
        public string? Image { get; set; }

        [AllowNull]
        [DisplayName("Mô tả")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DisplayName("Ngày tạo")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [DisplayName("Trạng thái hoạt động")]
        public bool IsActive { get; set; } = true;

        // A drink belongs to many order items.
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // A drink contains many drink items.
        public virtual ICollection<DrinkItem> DrinkItems { get; set; } = new List<DrinkItem>();

        // An item belongs to one category.
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
    }
}
