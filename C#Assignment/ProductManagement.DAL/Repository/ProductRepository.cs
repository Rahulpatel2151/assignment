using AutoMapper;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ProductsdbEntities db = new ProductsdbEntities();
        private readonly IMapper mapper;
        public ProductRepository()
        {
            AutoMapperConfig.init();
            mapper = AutoMapperConfig.Mapper;
        }
        public ProductsModel getProduct(int id)
        {
            Products p = db.Products.Find(id);
            
            ProductsModel pm = mapper.Map<Products, ProductsModel>(p);
            return pm;
        }

        public IEnumerable<ProductsModel> getProducts()
        {
            IEnumerable<Products> plist = db.Products;
            
            IEnumerable<ProductsModel> productslist = plist.Select(x => mapper.Map<Products, ProductsModel>(x)).ToList();

            return productslist ;
        }

        public void putProducts(int id, ProductsModel products)
        {
            Products p = db.Products.Find(id);
            p.Name = products.Name;
            p.Price = products.Price;
            p.Quantity = products.Quantity;
            p.Category = products.Category;
            p.ShortDescription = products.ShortDescription;
            p.LongDescription = products.LongDescription;
            p.SmallImage = products.SmallImage;
            p.LargeImage = products.LargeImage;
            db.Entry(p).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;    
            }
        }
        public bool ProductsExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }

        public void addProduct(ProductsModel products)
        {
            Products p = mapper.Map<ProductsModel, Products>(products);
            try
            {
                db.Products.Add(p);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public void deleteProduct(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
        }
    }
}
