using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository? productRepository = null;
        public ProductController() => productRepository = new ProductRepository();
        // GET: ProductController
        public IActionResult Index(string id)
        {
            if (productRepository == null)
            {
                return View();
            }
            var products = from p in productRepository.GetListProduct()
                           select p;
            if (!string.IsNullOrEmpty(id))
            {
                products = products.Where(pro => pro.ProductName!.ToUpper().Contains(id.ToUpper()) || pro.UnitPrice!.ToString().Contains(id)).ToList();
            }
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (productRepository == null)
            {
                return BadRequest();
            }
            var product = productRepository.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            int id = 0;
            if (productRepository != null)
            {
                id = productRepository.GetListProduct().Count() + 1;

                while (productRepository.GetProductById(id) != null)
                {
                    id++;
                }

                var categories = new SelectList(productRepository.GetListProduct().Select(cate => cate.CategoryId).Distinct().ToList(), "CategoryId");
                ViewBag.CategoryId = categories;
            }
            ViewBag.ProductId = id;
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (productRepository != null)
                    {
                        productRepository.AddProduct(product);

                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(product);
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (productRepository == null)
            {
                return BadRequest();
            }
            var product = productRepository.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    if (productRepository != null) 
                        productRepository.UpdateProduct(product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(product);
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (productRepository == null)
            {
                return BadRequest();
            }
            var product = productRepository.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                if (productRepository != null)
                {
                    productRepository.RemoveProduct(id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
