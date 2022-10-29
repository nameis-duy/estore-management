using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO? instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        //---------------------------------------------------------------
        public IEnumerable<Product> GetProductList()
        {
            var products = new List<Product>();
            try
            {
                using var context = new MyFStoreContext();
                products = context.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }
        public Product? GetProductById(int id)
        {
            Product? product = null;
            try
            {
                using var context = new MyFStoreContext();
                product = context.Products.SingleOrDefault(pro => pro.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }
        
        public void AddProduct(Product product)
        {
            try
            {
                var _product = GetProductById(product.ProductId);
                if (_product == null)
                {
                    using var context = new MyFStoreContext();
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This product is already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateProduct(Product product)
        {
            try
            {
                var _product = GetProductById(product.ProductId);
                if (_product != null)
                {
                    using var context = new MyFStoreContext();
                    context.Products.Update(product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void RemoveProduct(int id)
        {
            try
            {
                var product = GetProductById(id);
                if (product != null)
                {
                    using var context = new MyFStoreContext();
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This product doesn't exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
