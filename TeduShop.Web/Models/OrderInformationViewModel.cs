using System;

namespace TeduShop.Web.Models
{
    [Serializable]
    public class OrderInformationViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerAddress { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool Status { get; set; }
        public string PaymentStatus { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string CustomerId { get; set; }
        public string ProductImage { set; get; }
        public string AddressDistrict { get; set; }
        public string AddressWard { get; set; }
        public string AddressCity { get; set; }
        public string ShipmentId { get; set; }
        public string ShipmentStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public int Total { get; set; }
    }
}