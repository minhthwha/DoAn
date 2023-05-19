using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models
{
    [Table("OrderItems")]
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }

        [Required]
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        // An item belongs to one order.
        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        // An item belongs to one drink.
        [ForeignKey("DrinkId")]
        public Guid DrinkId { get; set; }
        public Drink? Drink { set; get; }
    }
}
