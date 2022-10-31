using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eStore.Controllers
{
    public class OrderDetailController : Controller
    {
        IOrderDetailRepository? orderDetailRepository = null;
        IProductRepository productRepository = null;
        public OrderDetailController()
        {
            orderDetailRepository = new OrderDetailRepository();
            productRepository = new ProductRepository();
        }
        // GET: OrderDetailController
        public ActionResult Index(int? id)
        {
            if (orderDetailRepository != null)
            {
                var orders = orderDetailRepository.GetDetailList().Where(ord => ord.OrderId == id).ToList();
                foreach (var item in orders)
                {
                    item.Product = productRepository.GetProductById(item.ProductId);
                }
                return View(orders);
            }
            return View(null);
        }

        // GET: OrderDetailController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (orderDetailRepository == null)
            {
                ViewBag.Message = "Order Repository is null.";
                return View();
            }
            var order = orderDetailRepository.GetOrderDetailById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: OrderDetailController/Create
        public ActionResult Create()
        {
            var orders = orderDetailRepository.GetDetailList();
            ViewBag.ProductId = new SelectList(productRepository.GetListProduct().Select(pro => pro.ProductId).Distinct().ToList(), "ProductId");
            var id = orders.Count() + 1;
            while (orderDetailRepository.GetOrderDetailById(id) != null)
            {
                id++;
            }
            ViewBag.OrderId = id;
            return View();
        }

        // POST: OrderDetailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderDetail orderDetail)
        {
            try
            {
                if (ModelState.ContainsKey("Order"))
                {
                    ModelState.Remove("Order");
                }
                if (ModelState.ContainsKey("Product"))
                {
                    ModelState.Remove("Product");
                }
                if (ModelState.IsValid)
                {
                    if (orderDetailRepository != null)
                    {
                        orderDetailRepository.AddOrder(orderDetail);
                    }
                }
                return RedirectToAction("Index", "Order");
            }
            catch (Exception ex)
            {
                ViewBag.Message(ex.Message);
                return View(orderDetail);
            }
        }

        // GET: OrderDetailController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (orderDetailRepository == null)
            {
                ViewBag.Message = "Order Repository is null.";
                return View();
            }
            var order = orderDetailRepository.GetOrderDetailById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderDetailController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OrderDetail orderDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (orderDetailRepository != null)
                    {
                        orderDetailRepository.UpdateOrder(orderDetail);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(orderDetail);
            }
        }

        // GET: OrderDetailController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (orderDetailRepository == null)
            {
                ViewBag.Message = "Order Repository is null.";
                return View();
            }
            var order = orderDetailRepository.GetOrderDetailById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrderDetailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                if (orderDetailRepository != null)
                {
                    orderDetailRepository.DeleteOrder(id);
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
