using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eStore.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository? orderRepository = null;
        IOrderDetailRepository? orderDetailRepository = null;
        IMemberRepository? memberRepository = null;
        public OrderController()
        {
            orderRepository = new OrderRepository();
            orderDetailRepository = new OrderDetailRepository();
            memberRepository = new MemberRepository();
        }
        // GET: OrderController
        public ActionResult Index(int? id)
        {
            if (orderRepository != null)
            {
                var orders = orderRepository.GetOrderList();
                if (id != null)
                {
                    orders = orders.Where(ord => ord.MemberId == id).ToList();
                }
                return View(orders);
            }
            return View();
        }

        public ActionResult FilterDate(IFormCollection form)
        {
            if (orderRepository != null)
            {
                var orders = orderRepository.GetOrderList();
                DateTime startDate = Convert.ToDateTime(form["startDate"]);
                DateTime endDate = Convert.ToDateTime(form["endDate"]);
                if (startDate.CompareTo(endDate) > 0)
                {
                    TempData["Error"] = "The start date must before the end date.";
                    return RedirectToAction("Index", "Order");
                }
                orders = orders.Where(ord => ord.OrderDate.CompareTo(startDate) >= 0 && ord.OrderDate.CompareTo(endDate) <= 0).OrderByDescending(ord => ord.OrderDate).ToList();

                return View(orders);
            }

            return View(nameof(Index));
            
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (orderRepository == null)
            {
                ViewBag.Message = "Order Repository is null.";
                return View();
            }
            var order = orderRepository.GetOrderById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "OrderDetail", new {@id=order.OrderId});
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            if (orderRepository != null)
            {
                var orders = orderRepository.GetOrderList();
                ViewBag.MemberIds = new SelectList(orders.Select(ord => ord.MemberId).Distinct().ToList(), "MemberId");
                var id = orders.Count() + 1;
                while (orderRepository.GetOrderById(id) != null)
                {
                    id++;
                }
                ViewBag.OrderId = id;
                return View();
            }
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                if (ModelState.ContainsKey("Member"))
                {
                    ModelState.Remove("Member");
                }
                if (ModelState.IsValid)
                {
                    if (orderRepository != null)
                    {
                        orderRepository.AddOrder(order);
                    }
                }
                return RedirectToAction("Create", "OrderDetail");
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
                return View(order);
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (orderRepository == null)
            {
                ViewBag.Message = "Order Repository is null.";
                return View();
            }
            var order = orderRepository.GetOrderById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                if (ModelState.ContainsKey("Member"))
                {
                    ModelState.Remove("Member");
                }
                if (ModelState.IsValid)
                {
                    if (orderRepository != null)
                    {
                        orderRepository.UpdateOrder(order);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(order);
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (orderRepository == null)
            {
                ViewBag.Message = "Order Repository is null.";
                return View();
            }
            var order = orderRepository.GetOrderById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                if (orderRepository != null)
                {
                    if (orderDetailRepository != null)
                    {
                        orderDetailRepository.DeleteOrder(id);
                    }
                    orderRepository.DeleteOrder(id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
