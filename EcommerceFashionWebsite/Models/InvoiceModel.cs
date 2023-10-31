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

        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatusType? OrderStatus { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DeliveryMethodType DeliveryMethod { get; set; }

        [ValidateNever]
        public ICollection<AdditionalServiceModel> AdditionalServiceModels { get; set; }
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
