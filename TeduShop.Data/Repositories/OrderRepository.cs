using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TeduShop.Common.ViewModels;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<OrderInformation> GetAllOrderInformation();
        IEnumerable<OrderInformation> GetAllOrderInformationByKeyword(int? orderId);
        OrderInformation GetOrderInformationByOrderId(int orderId);
        IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate);
        IEnumerable<OrderInformation> GetOrdersInformationByUserId(string userId, int page, int pageSize, out int totalRow);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<OrderInformation> GetAllOrderInformation()
        {
            var query = from p in DbContext.Products
                        join orderDetail in DbContext.OrderDetails
                        on p.ID equals orderDetail.ProductID
                        join order in DbContext.Orders
                        on orderDetail.OrderID equals order.ID
                        join customer in DbContext.Users
                        on order.CustomerId equals customer.Id
                        select new { p, orderDetail, order, customer };

            var result = query.Select(x => new OrderInformation() { 
                OrderId = x.order.ID,
                CreatedDate = x.order.CreatedDate,
                CustomerAddress = x.order.CustomerAddress,
                CustomerName = x.order.CustomerName, 
                PaymentMethod = x.order.PaymentMethod,
                CustomerPhoneNumber = x.customer.PhoneNumber,
                PaymentStatus = x.order.PaymentStatus,
                Price = x.orderDetail.Price,
                ProductName = x.p.Name,
                Quantity = x.orderDetail.Quantity,
                Status = x.order.Status,
                CustomerId = x.order.CustomerId,
                ProductImage = x.p.Image,
                AddressCity = x.order.AddressCity,
                AddressWard = x.order.AddressWard,
                AddressDistrict = x.order.AddressDistrict,
                ShipmentId = x.order.ShipmentId,
                ShipmentStatus = x.order.ShipmentStatus
            });

            return result;
        }

        public IEnumerable<OrderInformation> GetAllOrderInformationByKeyword(int? keyword)
        {
            var query = from p in DbContext.Products
                        join orderDetail in DbContext.OrderDetails
                        on p.ID equals orderDetail.ProductID
                        join order in DbContext.Orders
                        on orderDetail.OrderID equals order.ID
                        join customer in DbContext.Users
                        on order.CustomerId equals customer.Id
                        where order.ID == keyword
                        select new { p, orderDetail, order, customer };

            var result = query.Select(x => new OrderInformation()
            {
                OrderId = x.order.ID,
                CreatedDate = x.order.CreatedDate,
                CustomerAddress = x.order.CustomerAddress,
                CustomerName = x.order.CustomerName,
                PaymentMethod = x.order.PaymentMethod,
                CustomerPhoneNumber = x.customer.PhoneNumber,
                PaymentStatus = x.order.PaymentStatus,
                Price = x.orderDetail.Price,
                ProductName = x.p.Name,
                Quantity = x.orderDetail.Quantity,
                Status = x.order.Status,
                CustomerId = x.order.CustomerId,
                ProductImage = x.p.Image,
                AddressCity = x.order.AddressCity,
                AddressWard = x.order.AddressWard,
                AddressDistrict = x.order.AddressDistrict,
                ShipmentId = x.order.ShipmentId,
                ShipmentStatus = x.order.ShipmentStatus
            });

            return result;
        }

        public OrderInformation GetOrderInformationByOrderId(int orderId)
        {
            var query = from p in DbContext.Products
                        join orderDetail in DbContext.OrderDetails
                        on p.ID equals orderDetail.ProductID
                        join order in DbContext.Orders
                        on orderDetail.OrderID equals order.ID
                        join customer in DbContext.Users
                        on order.CustomerId equals customer.Id
                        where order.ID == orderId
                        select new OrderInformation() 
                        {
                            OrderId = order.ID,
                            CreatedDate = order.CreatedDate,
                            CustomerAddress = order.CustomerAddress,
                            CustomerName = order.CustomerName,
                            PaymentMethod = order.PaymentMethod,
                            CustomerPhoneNumber = customer.PhoneNumber,
                            PaymentStatus = order.PaymentStatus,
                            Price = orderDetail.Price,
                            ProductName = p.Name,
                            Quantity = orderDetail.Quantity,
                            Status = order.Status,
                            CustomerId = order.CustomerId,
                            CustomerAddressCity = order.CustomerAddressCity,
                            CustomerAddressDistrict = order.CustomerAddressDistrict,
                            CustomerAddressWard = order.CustomerAddressWard,
                            ShipmentId = order.ShipmentId,
                            ShipmentStatus = order.ShipmentStatus,
                            RateId = order.RateId,
                            Weight = order.Weight,
                            ProductImage = p.Image,
                            AddressCity = order.AddressCity,
                            AddressWard = order.AddressWard,
                            AddressDistrict = order.AddressDistrict,
                        };

            return query.First();
        }

        public IEnumerable<OrderInformation> GetOrdersInformationByUserId(string userId, int page, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Products
                        join orderDetail in DbContext.OrderDetails
                        on p.ID equals orderDetail.ProductID
                        join order in DbContext.Orders
                        on orderDetail.OrderID equals order.ID
                        join customer in DbContext.Users
                        on order.CustomerId equals customer.Id
                        where order.CustomerId == userId
                        orderby order.CreatedDate descending
                        select new { p, orderDetail, order, customer };

            var result = query.Select(x => new OrderInformation()
            {
                OrderId = x.order.ID,
                CreatedDate = x.order.CreatedDate,
                CustomerAddress = x.order.CustomerAddress,
                CustomerName = x.order.CustomerName,
                PaymentMethod = x.order.PaymentMethod,
                CustomerPhoneNumber = x.customer.PhoneNumber,
                PaymentStatus = x.order.PaymentStatus,
                Price = x.orderDetail.Price,
                ProductName = x.p.Name,
                ProductImage = x.p.Image,
                Quantity = x.orderDetail.Quantity,
                Status = x.order.Status,
                CustomerId = x.order.CustomerId,
                CustomerAddressCity = x.order.CustomerAddressCity,
                CustomerAddressDistrict = x.order.CustomerAddressDistrict,
                CustomerAddressWard = x.order.CustomerAddressWard,
                ShipmentId = x.order.ShipmentId,
                ShipmentStatus = x.order.ShipmentStatus,
                RateId = x.order.RateId,
                Weight = x.order.Weight,
                AddressCity = x.order.AddressCity,
                AddressWard = x.order.AddressWard,
                AddressDistrict = x.order.AddressDistrict,
            }); ;
            totalRow = result.Count();

            return result.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<RevenueStatisticViewModel> GetRevenueStatistic(string fromDate, string toDate)
        {
            var parameters = new SqlParameter[]{
                new SqlParameter("@fromDate",fromDate),
                new SqlParameter("@toDate",toDate)
            };
            return DbContext.Database.SqlQuery<RevenueStatisticViewModel>("GetRevenueStatistic @fromDate,@toDate", parameters);
        }
    }
}