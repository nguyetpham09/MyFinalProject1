using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using System.Linq;

namespace TeduShop.Data.Repositories
{
    public interface IOrderDetailRepository :  IRepository<OrderDetail>
    {
        OrderDetail GetOrderDetailByOrderId(int orderId);
        int DeleteOrderDetail(int orderId);
    }

    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public int DeleteOrderDetail(int orderId)
        {
            var orderDetail = GetOrderDetailByOrderId(orderId);
            DbContext.OrderDetails.Remove(orderDetail);
            DbContext.SaveChanges();
            return orderId;
        }

        public OrderDetail GetOrderDetailByOrderId(int orderId)
        {
            var query = from od in DbContext.OrderDetails
                        where od.OrderID == orderId
                        select od;

            return query.FirstOrDefault();
        }
    }
}