using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProductManagement.BAL;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    [RoutePrefix("api")]
    public class ProductAPIController : ApiController
    {
        private IProductManager _productManager;
        public ProductAPIController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        // GET: api/ProductAPI
        [Route("ProductAPI")]
        public IEnumerable<ProductsModel> GetProducts()
        {
            return _productManager.getProducts();
        }

        // GET: api/ProductAPI/5
        [Route("ProductAPI/{id}")]
        [ResponseType(typeof(ProductsModel))]
        public IHttpActionResult GetProducts(int id)
        {
            ProductsModel products = _productManager.getProduct(id);
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // PUT: api/ProductAPI/5
        [ResponseType(typeof(void))]
        [Route("ProductAPI/{id}")]
        public IHttpActionResult PutProducts(int id, ProductsModel products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != products.Id)
            {
                return BadRequest();
            }
            if (_productManager.ProductsExists(id))
            {
                try
                {
                    _productManager.putProducts(id, products);
                    return StatusCode(HttpStatusCode.NoContent);

                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }

        }

        // POST: api/ProductAPI

        [Route("ProductAPI")]
        [ResponseType(typeof(ProductsModel))]
        public IHttpActionResult PostProducts(ProductsModel products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //try
            //{
                _productManager.addProduct(products);
            //}
            //catch
            //{
            //    return BadRequest();
            //}

            return Content(HttpStatusCode.Created,"Created");
        }

        // DELETE: api/ProductAPI/5
        [Route("ProductAPI/{id}")]
        [ResponseType(typeof(ProductsModel))]
        public IHttpActionResult DeleteProducts(int id)
        {
            ProductsModel products = _productManager.getProduct(id);
            if (products == null)
            {
                return NotFound();
            }
            _productManager.deleteProduct(id);
            
            return Ok(products);
        }

    
       
    }
}