using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        private static OrderDAO? instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }
        //--------------------------------------------------------
        public IEnumerable<Order> GetListOrder()
        {
            var orders = new List<Order>();
            try
            {
                using var context = new MyFStoreContext();
                orders = context.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }
        public Order? GetOrderById(int id)
        {
            Order? order = null;
            try
            {
                using var context = new MyFStoreContext();
                order = context.Orders.SingleOrDefault(ord => ord.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }
        public void AddOrder(Order order)
        {
            try
            {
                var _order = GetOrderById(order.OrderId);
                if (_order == null)
                {
                    using var context = new MyFStoreContext();
                    context.Orders.Add(order);
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
        public void UpdateOrder(Order order)
        {
            try
            {
                var _order = GetOrderById(order.OrderId);
                if (_order != null)
                {
                    using var context = new MyFStoreContext();
                    context.Orders.Update(order);
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
                var order = GetOrderById(id);
                if (order != null)
                {
                    using var context = new MyFStoreContext();
                    context.Orders.Remove(order);
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
