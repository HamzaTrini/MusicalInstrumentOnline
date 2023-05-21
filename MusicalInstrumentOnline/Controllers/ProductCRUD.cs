using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentOnline.Models;
using System.Data.SqlClient;
using System.Data;

namespace MusicalInstrumentOnline.Controllers
{
    public class ProductCRUD : Controller
    {
        private readonly IWebHostEnvironment? _webHostEnviroment;
        private readonly IConfiguration _configuration;
        public ProductCRUD(IWebHostEnvironment? webHostEnviroment, IConfiguration configuration)
        {
            _webHostEnviroment = webHostEnviroment;
            _configuration = configuration;
        }
        public List<Product> GetInfo()
        {
            Product product = new Product();
            List<Product> list = new List<Product>();

            string cs = _configuration.GetConnectionString("ConnectionName");
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from Product", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Product");
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                product = new Product();
                product.categoryId = Convert.ToInt32(dr["Categoryid"]);
                product.name = dr["name"].ToString();
                product.imagePath = dr["imagepath"].ToString();
                product.price = dr["price"].ToString();
                product.productId = Convert.ToInt32(dr["Productid"]);


                list.Add(product);
            }
            return list;
        }
        // GET: ProductCRUD
        public ActionResult Index()
        {
            return View(GetInfo().ToList());
        }

        // GET: ProductCRUD/Details/5
        public ActionResult Details(int id)
        {
            return View(GetInfo().ToList().Where(x => x.productId == id));
        }

        // GET: ProductCRUD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCRUD/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("name,price,emageFile,categoryId")] Product product)
        {
            try
            {     if (ModelState.IsValid)
                    {
                        if (product.emageFile != null)
                        {
                            string wwwrootpath = _webHostEnviroment.WebRootPath;
                            string fileName = Guid.NewGuid().ToString() + " " + product.emageFile.FileName;
                            string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await product.emageFile.CopyToAsync(fileStream);
                            }
                            product.imagePath = fileName;
                            string cs = _configuration.GetConnectionString("ConnectionName");
                            SqlConnection con = new SqlConnection(cs);
                            SqlCommand cmd = new SqlCommand("insert into Product values(\'" + product.name + "\',\'" + product.imagePath + "\',\'"+ product.price+"\',\'"+ product.categoryId+"\')", con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            return RedirectToAction(nameof(Index));
                        }
                    }

                    return View(product);
                }
            catch
            {
                return View();
            }
        }

        // GET: ProductCRUD/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Id=id;
            return View();
        }

        // POST: ProductCRUD/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Edit(int id, [Bind("productId,name,price,emageFile,categoryId")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (product.emageFile != null)
                    {
                        string wwwrootpath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + product.emageFile.FileName;
                        string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await product.emageFile.CopyToAsync(fileStream);
                        }
                        product.imagePath = fileName;
                        string cs = _configuration.GetConnectionString("ConnectionName");
                        SqlConnection con = new SqlConnection(cs);
                        SqlCommand cmd = new SqlCommand("UPDATE Product SET name =\'" + product.name + "\', imagepath =\'" + product.imagePath + "\', price=\'"+ product.price+ "\',Categoryid=\'"+ product.categoryId+"\' WHERE Productid=\'" + product.productId + "\';", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductCRUD/Delete/5
        public ActionResult Delete(int id)
        {
            return View(GetInfo().ToList().Where(x => x.productId == id));
        }

        // POST: ProductCRUD/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                string cs = _configuration.GetConnectionString("ConnectionName");
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("delete from Product where Productid=\'" + id + "\';", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
