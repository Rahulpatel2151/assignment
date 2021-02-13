using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Repository
{
    public interface IProductRepository
    {
        IEnumerable<ProductsModel> getProducts();
        ProductsModel getProduct(int id);
        void putProducts(int id, ProductsModel products);
        bool ProductsExists(int id);
        void addProduct(ProductsModel products);
        void deleteProduct(int id);
    }
}
