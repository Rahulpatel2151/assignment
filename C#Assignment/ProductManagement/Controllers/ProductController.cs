using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using log4net;
using Newtonsoft.Json;
using PagedList;
using ProductManagement;
using ProductManagement.Models;

namespace ProductManagement.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProductController));

        string Baseurl = "https://localhost:44332/"; //BaseUrl to call API

        // GET: Product
        public async Task<ActionResult> Index(int? Page_No)
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ProductsModel> productlist = new List<ProductsModel>();
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ProductAPI");
                Log.Info(Res.StatusCode);
                if (Res.IsSuccessStatusCode)
                {
                    var p = Res.Content.ReadAsStringAsync().Result;
                    productlist = JsonConvert.DeserializeObject<IEnumerable<ProductsModel>>(p);
                }

                int Size_Of_Page = 5;
                int No_Of_Page = (Page_No ?? 1);
                if (TempData["isPositive"] != null)
                {
                    ViewBag.PositiveStatus = TempData["PositiveStatus"];
                    ViewBag.isPositive = TempData["isPositive"];
                }
                else
                {
                    ViewBag.isPositive = false;
                }
                if (TempData["isNegative"] != null)
                {
                    ViewBag.NegativeStatus = TempData["NegativeStatus"];
                    ViewBag.isNegative = TempData["isNegative"];
                }
                else
                {
                    ViewBag.isNegative = false;
                }
                return View(productlist.ToPagedList(No_Of_Page, Size_Of_Page));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Index(string sort_order, string search, int? Page_No)
        {
            using (var client = new HttpClient())
            {
                IEnumerable<ProductsModel> productlist = new List<ProductsModel>();
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ProductAPI");
                Log.Info(Res.StatusCode);
                if (Res.IsSuccessStatusCode)
                {
                    var p = Res.Content.ReadAsStringAsync().Result;
                    productlist = JsonConvert.DeserializeObject<IEnumerable<ProductsModel>>(p);
                }
                if (search != null)//Searching
                {
                    var product = productlist;
                    {
                        product = product.Where(x => x.Name.ToUpper().Contains(search.ToUpper())
                            || x.Category.ToUpper().Contains(search.ToUpper()) || x.ShortDescription.ToUpper().Contains(search.ToUpper()));
                    }
                    productlist = product;
                }
                switch (sort_order)//Sorting
                {
                    case "Name-ascending":
                        productlist = productlist.OrderBy(x => x.Name, StringComparer.OrdinalIgnoreCase);
                        break;
                    case "Name-descending":
                        productlist = productlist.OrderByDescending(x => x.Name, StringComparer.OrdinalIgnoreCase);
                        break;
                    case "Category-ascending":
                        productlist = productlist.OrderBy(x => x.Category, StringComparer.OrdinalIgnoreCase);
                        break;
                    case "Category-descending":
                        productlist = productlist.OrderByDescending(x => x.Category, StringComparer.OrdinalIgnoreCase);
                        break;
                    case "Price-ascending":
                        productlist = productlist.OrderBy(x => x.Price);
                        break;
                    case "Price-descending":
                        productlist = productlist.OrderByDescending(x => x.Price);
                        break;
                }
                //for pagination
                int Size_Of_Page = 5;
                int No_Of_Page = (Page_No ?? 1);

                if (TempData["isPositive"] != null)
                {
                    ViewBag.PositiveStatus = TempData["PositiveStatus"];
                    ViewBag.isPositive = TempData["isPositive"];
                }
                else
                {
                    ViewBag.isPositive = false;
                }
                if (TempData["isNegative"] != null)
                {
                    ViewBag.NegativeStatus = TempData["NegativeStatus"];
                    ViewBag.isNegative = TempData["isNegative"];
                }
                else
                {
                    ViewBag.isNegative = false;
                }
                return View(productlist.ToPagedList(No_Of_Page, Size_Of_Page));
            }
        }

        // GET: Product/Details/5
        [OutputCache(Duration = 20)]    
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                ProductsModel product = new ProductsModel();

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ProductAPI/" + id);
                if (Res.StatusCode == HttpStatusCode.OK)
                {
                    var p = Res.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<ProductsModel>(p);
                    ViewBag.isValid = true;
                    return View(product);
                }
                if (Res.StatusCode == HttpStatusCode.NotFound)
                {
                    ViewBag.isValid = false;
                }
                return View();
            }
        }

        // GET: Product/Create
        [OutputCache(Duration = 20)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Category,Price,Quantity,ShortDescription,LongDescription,SmallImage,LargeImage")] ProductView products)
        {
            ProductsModel dbproducts = new ProductsModel();
            try
            {
                //default image path
                //if will not upload largeimage it will store default image path
                string smallfilepath = "/UploadedFiles/SmallImages/default.jpg";
                string largefilepath = "/UploadedFiles/LargeImages/default.jpg";
                string timestamp = DateTime.Now.ToString("yyyymmddHHmmssfff");
                if (products.SmallImage != null)
                {
                    
                    string smallpath = Path.Combine(Server.MapPath("~/UploadedFiles/SmallImages/"),timestamp +products.SmallImage.FileName);
                    smallfilepath = "/UploadedFiles/SmallImages/" + timestamp +products.SmallImage.FileName;
                    Debug.WriteLine(smallpath);
                    products.SmallImage.SaveAs(smallpath);
                }
                if (products.LargeImage != null)
                {
                    string largepath = Path.Combine(Server.MapPath("~/UploadedFiles/LargeImages"), timestamp + products.LargeImage.FileName);
                    largefilepath = "/UploadedFiles/LargeImages/" + timestamp +products.LargeImage.FileName;
                    products.LargeImage.SaveAs(largepath);
                }
                dbproducts.Name = products.Name;
                dbproducts.Price = products.Price;
                dbproducts.Quantity = products.Quantity;
                dbproducts.Category = products.Category;
                dbproducts.ShortDescription = products.ShortDescription;
                dbproducts.LongDescription = products.LongDescription;
                dbproducts.SmallImage = smallfilepath;
                dbproducts.LargeImage = largefilepath;

            }
            catch (Exception e)
            {
                Log.Error(e);
                Debug.WriteLine(e);
                ViewBag.status = "Error while file uploading.";
                return View(products);
            }
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                string data = JsonConvert.SerializeObject(dbproducts);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsync("api/ProductAPI/", content);
                Log.Info(Res.StatusCode);
                Debug.WriteLine(Res.StatusCode);
                if (Res.StatusCode == HttpStatusCode.Created)
                {
                    //If Product Added
                    TempData["isPositive"] = true;
                    TempData["PositiveStatus"] = "Added Successfully";
                }
                else
                {
                    //Failure
                    TempData["isNegative"] = true;
                    TempData["NegativeStatus"] = "Failed to Upload";
                }
                return RedirectToAction("Index");
            }
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                ProductsModel product = new ProductsModel();

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ProductAPI/" + id);
                if (Res.StatusCode == HttpStatusCode.OK)
                {
                    var p = Res.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<ProductsModel>(p);//Deserializing 
                    ViewBag.isValid = true;
                    if (product == null)
                    {
                        return HttpNotFound();
                    }
                    ProductView dbproducts = new ProductView();
                    dbproducts.Id = product.Id;
                    dbproducts.Name = product.Name;
                    dbproducts.Price = product.Price;
                    dbproducts.Quantity = product.Quantity;
                    dbproducts.Category = product.Category;
                    dbproducts.ShortDescription = product.ShortDescription;
                    dbproducts.LongDescription = product.LongDescription;
                    return View(dbproducts);
                }
                else
                {
                    return HttpNotFound();
                }
            }

        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, [Bind(Include = "Id,Name,Category,Price,Quantity,ShortDescription,LongDescription,SmallImage,LargeImage")] ProductView products)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsModel dbproducts = new ProductsModel();
            try
            {
                string smallfilepath = dbproducts.SmallImage;
                string largefilepath = dbproducts.LargeImage;
                string timestamp = DateTime.Now.ToString("yyyymmddHHmmssfff");
                if (products.SmallImage != null)
                {

                    string smallpath = Path.Combine(Server.MapPath("~/UploadedFiles/SmallImages/"), timestamp + products.SmallImage.FileName);
                    smallfilepath = "/UploadedFiles/SmallImages/" + timestamp + products.SmallImage.FileName;
                    Debug.WriteLine(smallpath);
                    products.SmallImage.SaveAs(smallpath);
                }
                if (products.LargeImage != null)
                {
                    string largepath = Path.Combine(Server.MapPath("~/UploadedFiles/LargeImages"), timestamp + products.LargeImage.FileName);
                    largefilepath = "/UploadedFiles/LargeImages/" + timestamp + products.LargeImage.FileName;
                    products.LargeImage.SaveAs(largepath);
                }
                dbproducts.Id = products.Id;
                dbproducts.Name = products.Name;
                dbproducts.Price = products.Price;
                dbproducts.Quantity = products.Quantity;
                dbproducts.Category = products.Category;
                dbproducts.ShortDescription = products.ShortDescription;
                dbproducts.LongDescription = products.LongDescription;
                dbproducts.SmallImage = smallfilepath;
                dbproducts.LargeImage = largefilepath;

            }
            catch (Exception)
            {
                ViewBag.status = "Error while file uploading.";
                return View(products);
            }
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                string data = JsonConvert.SerializeObject(dbproducts);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PutAsync("api/ProductAPI/" + id, content);
                Log.Info(Res.StatusCode);
                if (Res.IsSuccessStatusCode)
                {
                    TempData["isPositive"] = true;
                    TempData["PositiveStatus"] = "Updated Successfully";
                }
                else
                {
                    TempData["isNegative"] = true;
                    TempData["NegativeStatus"] = "Failed to Update";
                }
                return View("Index");
            }
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                ProductsModel product = new ProductsModel();

                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ProductAPI/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var p = Res.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<ProductsModel>(p);
                    ViewBag.isValid = true;
                    if (product == null)
                    {
                        return HttpNotFound();
                    }

                    return View(product);
                }
                else
                {
                    return HttpNotFound();
                }

            }
        }
        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.DeleteAsync("api/ProductAPI/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    TempData["isPositive"] = true;
                    TempData["PositiveStatus"] = "Deleted Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["isNegative"] = true;
                    TempData["NegativeStatus"] = "Failed to Delete";
                    return RedirectToAction("Index");
                }
            }
        }
        public async Task<ActionResult> DeleteMultiple(int[] del)
        {
            if (del == null)
            {
                return RedirectToAction("Index");
            }
            int successcount = 0, failcount = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                for (int i = 0; i < del.Length; i++)
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.DeleteAsync("api/ProductAPI/" + del[i]);
                    if (Res.IsSuccessStatusCode)
                    {
                        TempData["isPositive"] = true;
                        successcount++;
                    }
                    else
                    {
                        TempData["isNegative"] = true;
                        failcount++;
                    }
                }
                TempData["PositiveStatus"] = successcount + " products deleted successfully";
                TempData["NegativeStatus"] = failcount + " products not deleted";

                return RedirectToAction("Index");
            }
        }
        
    }
}
