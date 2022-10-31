using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public IEnumerable<OrderDetail> GetDetailList() => OrderDetailDAO.Instance.GetListOrderDetail();
        public OrderDetail? GetOrderDetailById(int id) => OrderDetailDAO.Instance.GetOrderDetailById(id);
        public void AddOrder(OrderDetail order) => OrderDetailDAO.Instance.AddOrder(order);
        public void UpdateOrder(OrderDetail order) => OrderDetailDAO.Instance.UpdateOrder(order);
        public void DeleteOrder(int id) => OrderDetailDAO.Instance.DeleteOrder(id);
    }
}
