using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO? instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------------------------
        public IEnumerable<OrderDetail> GetListOrderDetail()
        {
            var orderDetails = new List<OrderDetail>();
            try
            {
                using var context = new MyFStoreContext();
                orderDetails = context.OrderDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }
        public OrderDetail? GetOrderDetailById(int id)
        {
            OrderDetail? order = null;
            try
            {
                using var context = new MyFStoreContext();
                order = context.OrderDetails.Where(ord => ord.OrderId == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }
        public void AddOrder(OrderDetail order)
        {
            try
            {
                var _order = GetOrderDetailById(order.OrderId);
                if (_order == null)
                {
                    using var context = new MyFStoreContext();
                    context.OrderDetails.Add(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This order is already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateOrder(OrderDetail order)
        {
            try
            {
                var _order = GetOrderDetailById(order.OrderId);
                if (_order == null)
                {
                    using var context = new MyFStoreContext();
                    context.OrderDetails.Update(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This order is already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteOrder(int id)
        {
            try
            {
                var order = GetOrderDetailById(id);
                if (order != null)
                {
                    using var context = new MyFStoreContext();
                    context.OrderDetails.Remove(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This order doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
