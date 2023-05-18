using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentOnline.Models;
using System.Data.SqlClient;
using System.Data;

namespace MusicalInstrumentOnline.Controllers
{
    public class TastimonalCRUD : Controller
    {
        private readonly IWebHostEnvironment? _webHostEnviroment;
        private readonly IConfiguration _configuration;
        public TastimonalCRUD(IWebHostEnvironment? webHostEnviroment, IConfiguration configuration)
        {
            _webHostEnviroment = webHostEnviroment;
            _configuration = configuration;
        }
        public List<Tastimonal> GetInfo()
        {
            Tastimonal tastimonal = new Tastimonal();

            List<Tastimonal> list = new List<Tastimonal>();

            string cs = _configuration.GetConnectionString("ConnectionName");
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from tastimonal", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("tastimonal");
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                tastimonal = new Tastimonal();
                tastimonal.Id = Convert.ToInt32(dr["id"]);
                tastimonal.name = dr["name"].ToString();
                tastimonal.Description = dr["descriptions"].ToString();
                tastimonal.imagepath = dr["imagepath"].ToString();

                list.Add(tastimonal);
            }
            ViewBag.TastimonalList = 0;
            return list;
        }
        // GET: Tastimonal
        public ActionResult Index()
        {
            return View(GetInfo().ToList());
        }

        // GET: Tastimonal/Details/5
        public ActionResult Details(int id)
        {
            return View(GetInfo().ToList().Where(x=>x.Id==id));
        }

        // GET: Tastimonal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tastimonal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("name,Description,imageFile")] Tastimonal tastimonal )
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                if (ModelState.IsValid)
                {
                    if (tastimonal.imageFile != null)
                    {
                        string wwwrootpath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + " " + tastimonal.imageFile.FileName;
                        string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await tastimonal.imageFile.CopyToAsync(fileStream);
                        }
                        tastimonal.imagepath = fileName;
                        string cs = _configuration.GetConnectionString("ConnectionName");
                        SqlConnection con = new SqlConnection(cs);
                        SqlCommand cmd = new SqlCommand("insert into tastimonal values(\'" + tastimonal.imagepath + "\',\'" + tastimonal.Description + "\' ,\'" + tastimonal.name + "\')", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                return RedirectToAction("Login","Register__Login");

            }

            return RedirectToAction("Index","Home");
			

		}

        // GET: Tastimonal/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tastimonal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,name,Description,imageFile")] Tastimonal tastimonal)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (tastimonal.imageFile != null)
                    {
                        string wwwrootpath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + tastimonal.imageFile.FileName;
                        string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await tastimonal.imageFile.CopyToAsync(fileStream);
                        }
                        tastimonal.imagepath = fileName;
                        string cs = _configuration.GetConnectionString("ConnectionName");
                        SqlConnection con = new SqlConnection(cs);
                        SqlCommand cmd = new SqlCommand("UPDATE tastimonal SET name =\'" + tastimonal.name + "\', imagepath =\'" + tastimonal.imagepath + "\' , descriptions=\'"+ tastimonal.Description+"\'  WHERE id=\'" + tastimonal.Id + "\';", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    return RedirectToAction(nameof(Index));
                }
                return View(tastimonal);
            }
            catch
            {
                return View();
            }
        }

        // GET: Tastimonal/Delete/5
        public ActionResult Reject(int id)
        {
            return View(GetInfo().ToList().Where(x => x.Id == id));
        }

        // POST: Tastimonal/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reject(int id, IFormCollection collection)
        {
            try
            {
                string cs = _configuration.GetConnectionString("ConnectionName");
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("delete from tastimonal where id=\'" + id + "\';", con);
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
