using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<Order> GetOrderList() => OrderDAO.Instance.GetListOrder();
        public Order? GetOrderById(int id) => OrderDAO.Instance.GetOrderById(id);
        public void AddOrder(Order order) => OrderDAO.Instance.AddOrder(order);
        public void UpdateOrder(Order order) => OrderDAO.Instance.UpdateOrder(order);
        public void DeleteOrder(int id) => OrderDAO.Instance.DeleteOrder(id);
    }
}
