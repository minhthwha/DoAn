using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafeteria.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DisplayName("Mã người dùng")]
        public Guid UserId { get; set; }

        [Required]
        [DisplayName("Tên người dùng")]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        [DisplayName("Email")]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        [DisplayName("Số điện thoại")]
        [MaxLength(10)]
        [Phone]
        public string UserPhone { get; set; } = string.Empty;

        [Required]
        [DisplayName("Địa chỉ")]
        public string UserAddress { get; set; } = string.Empty;

        [Required]
        [DisplayName("Chức vụ")]
        public string Position { get; set; } = string.Empty;

        [Required]
        [DisplayName("Mật khẩu")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DisplayName("Ngày đăng ký")]
        [DataType(DataType.DateTime)]
        public DateTime RegisteredAt { get; set; } = DateTime.Now;

        [Required]
        [DisplayName("Vai trò quản trị")]
        public bool IsAdmin { get; set; } = false;

        [Required]
        [DisplayName("Trạng thái hoạt động")]
        public bool IsActive { get; set; } = true;

        // An account contains many orders.
        public virtual ICollection<Order> UserOrders { get; set; } = new List<Order>();
    }
}
