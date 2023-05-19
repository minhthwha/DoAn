using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models
{
    [Table("DrinkItems")]
    public class DrinkItem
    {
        [Key]
        public Guid DrinkItemId { get; set; }

        [Required]
        [DisplayName("Số lượng")]
        public float Quantity { get; set; }

        [Required]
        [MaxLength(10)]
        [DisplayName("Đơn vị tính")]
        public string CalculationUnit { get; set; } = string.Empty;

        // An item belongs to one drink.
        [ForeignKey("DrinkId")]
        public Drink? Drink { set; get; }

        // An item belongs to one ingredient.
        [ForeignKey("IngredientId")]
        public Ingredient? Ingredient { get; set; }
    }
}
