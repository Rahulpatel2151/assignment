using ProductManagement.DAL.Repository;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.BAL
{
    public class ProductManager : IProductManager
    {
        private IProductRepository _productRepository;
        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void addProduct(ProductsModel products)
        {
            _productRepository.addProduct(products);
        }

        public void deleteProduct(int id)
        {
            _productRepository.deleteProduct(id);
        }

        public ProductsModel getProduct(int id)
        {
            return _productRepository.getProduct(id);

        }

        public IEnumerable<ProductsModel> getProducts()
        {
            return _productRepository.getProducts();
        }

        public bool ProductsExists(int id)
        {
            return _productRepository.ProductsExists(id);
        }

        public void putProducts(int id, ProductsModel products)
        {
            _productRepository.putProducts(id,products);
        }
    }
}
