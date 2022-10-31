using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetDetailList();
        OrderDetail? GetOrderDetailById(int id);
        void AddOrder(OrderDetail order);
        void UpdateOrder(OrderDetail order);
        void DeleteOrder(int id);
    }
}
