using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentOnline.Models;
using System.Data.SqlClient;
using System.Data;

namespace MusicalInstrumentOnline.Controllers
{
    public class BayProductCRUD : Controller
    {
        private readonly IWebHostEnvironment? _webHostEnviroment;
        private readonly IConfiguration _configuration;
        public BayProductCRUD(IWebHostEnvironment? webHostEnviroment, IConfiguration configuration)
        {
            _webHostEnviroment = webHostEnviroment;
            _configuration = configuration;
        }
        public List<BayProduct> GetInfo()
        {
            BayProduct bayProduct = new BayProduct();

            List<BayProduct> list = new List<BayProduct>();

            string cs = _configuration.GetConnectionString("ConnectionName");
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from BayProduct", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("BayProduct");
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                bayProduct = new BayProduct();
                bayProduct.Id = Convert.ToInt32(dr["id"]);
                bayProduct.name = dr["name"].ToString();
                bayProduct.imagePath = dr["imagepath"].ToString();
                bayProduct.price = dr["price"].ToString();
                bayProduct.productId= Convert.ToInt32(dr["productid"]);
                list.Add(bayProduct);
            }
            return list;
        }
        // GET: BayProductCRUD
        public ActionResult Index()
        {
            return View(GetInfo().ToList());
        }

        // GET: BayProductCRUD/Details/5
        public ActionResult Details(int id)
        {
            return View(GetInfo().ToList().Where(x => x.Id == id));
        }

        // GET: BayProductCRUD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BayProductCRUD/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("name,price,emageFile,productId")] BayProduct payproduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (payproduct.emageFile != null)
                    {
                        string wwwrootpath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + " " + payproduct.emageFile.FileName;
                        string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await payproduct.emageFile.CopyToAsync(fileStream);
                        }
                        payproduct.imagePath = fileName;
                        string cs = _configuration.GetConnectionString("ConnectionName");
                        SqlConnection con = new SqlConnection(cs);
                        SqlCommand cmd = new SqlCommand("insert into BayProduct values(\'" + payproduct.imagePath + "\',\'" + payproduct.price + "\',\'" + payproduct.productId + "\',\'" + payproduct.name + "\')", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return RedirectToAction(nameof(Index));
                    }
                }

                return View(payproduct);
            }
            catch
            {
                return View();
            }
        }

        // GET: BayProductCRUD/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BayProductCRUD/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id,name,price,emageFile,productId")] BayProduct payproduct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (payproduct.emageFile != null)
                    {
                        string wwwrootpath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + payproduct.emageFile.FileName;
                        string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await payproduct.emageFile.CopyToAsync(fileStream);
                        }
                        payproduct.imagePath = fileName;
                        string cs = _configuration.GetConnectionString("ConnectionName");
                        SqlConnection con = new SqlConnection(cs);
                        SqlCommand cmd = new SqlCommand("UPDATE BayProduct SET name =\'" + payproduct.name + "\', imagepath =\'" + payproduct.imagePath + "\', price=\'" + payproduct.price + "\',productid=\'" + payproduct.productId + "\' WHERE id=\'" + payproduct.Id + "\';", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    return RedirectToAction(nameof(Index));
                }
                return View(payproduct);
            }
            catch
            {
                return View();
            }
        }

        // GET: BayProductCRUD/Delete/5
        public ActionResult Delete(int id)
        {
            return View(GetInfo().ToList().Where(x => x.Id == id));
        }

        // POST: BayProductCRUD/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                string cs = _configuration.GetConnectionString("ConnectionName");
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("delete from BayProduct where id=\'" + id + "\';", con);
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
