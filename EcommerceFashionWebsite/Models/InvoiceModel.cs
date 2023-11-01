using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceFashionWebsite.Models
{
    public class InvoiceModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserModel")]
        public string ApplicationUserId { get; set; }

        [ValidateNever]
        public UserModel ApplicationUser { get; set; }

        public ICollection<InvoiceSliceModel>? InvoiceSliceModels { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatusType? OrderStatus { get; set; } = OrderStatusType.Processing;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DeliveryMethodType DeliveryMethod { get; set; }
        public double Discount { get; set; } = 0.3;
    }

    public enum OrderStatusType
    {
        Processing,        // Đơn hàng đang được xử lý
        Shipped,           // Đơn hàng đã được gửi đi
        Delivered,         // Đơn hàng đã được giao thành công
        Completed,         // Đơn hàng đã hoàn thành (tất cả các công việc liên quan đều đã kết thúc)
        Canceled      // Đơn hàng đã bị hủy
    }

    public enum DeliveryMethodType
    {
        Standard,
        Express
    }
}
