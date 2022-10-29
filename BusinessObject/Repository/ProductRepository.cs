using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public IEnumerable<Product> GetListProduct() => ProductDAO.Instance.GetProductList();
        public Product? GetProductById(int id) => ProductDAO.Instance.GetProductById(id);
        public void AddProduct(Product product) => ProductDAO.Instance.AddProduct(product);
        public void UpdateProduct(Product product) => ProductDAO.Instance.UpdateProduct(product);
        public void RemoveProduct(int id) => ProductDAO.Instance.RemoveProduct(id);
    }
}
