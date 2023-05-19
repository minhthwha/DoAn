using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using static Cafeteria.Models.Enum;

namespace Cafeteria.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DisplayName("Mã đơn hàng")]
        public Guid OrderId { get; set; }

        [Required]
        [DisplayName("Tên khách hàng")]
        public string GuestName { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        [Phone]
        [DisplayName("Số điện thoại")]
        public string GuestPhone { get; set; } = string.Empty;

        [Required]
        [DisplayName("Địa chỉ")]
        public string GuestAddress { get; set; } = string.Empty;

        [Required]
        [DisplayName("Ngày tạo")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [AllowNull]
        [DisplayName("Ghi chú")]
        public string Note { get; set; } = string.Empty;

        /*[Required]
        [DisplayName("Trạng thái giao hàng")]
        public bool IsDelivered { get; set; } = false;

        [Required]
        [DisplayName("Trạng thái hoàn thành")]
        public bool IsCompleted { get; set; } = false;*/

        public Status Status { get; set; } = Status.Ordered;

        // An order belongs to an account.
        [ForeignKey("UserId")]
        public User? User { get; set; }

        // An order contains many order items.
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
