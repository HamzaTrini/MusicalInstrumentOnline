using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentOnline.Models;
using System.Data;
using System.Data.SqlClient;

namespace MusicalInstrumentOnline.Controllers
{
    public class CategoryCRUD : Controller
    {
        private readonly IWebHostEnvironment? _webHostEnviroment;
        private readonly IConfiguration _configuration;
        public CategoryCRUD(IWebHostEnvironment? webHostEnviroment, IConfiguration configuration)
        {
            _webHostEnviroment = webHostEnviroment;
            _configuration = configuration;
        }


        // GET: CategoryCRUD
        public ActionResult Index()
        {
            Category category = new Category();

            List<Category> list = new List<Category>();

            string cs = _configuration.GetConnectionString("ConnectionName");
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from Category", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Category");
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                category = new Category();
                category.categoryId = Convert.ToInt32(dr["Categoryid"]);
                category.name = dr["name"].ToString();
                category.emagePath = dr["imagepath"].ToString();

                list.Add(category);
            }
            return View(list.ToList());
        }

        // GET: CategoryCRUD/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryCRUD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryCRUD/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("name,emagePath,emageFile")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.emageFile != null)
                {
                    string wwwrootpath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + " " + category.emageFile.FileName;
                    string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await category.emageFile.CopyToAsync(fileStream);
                    }
                    category.emagePath = fileName;
                    string cs = _configuration.GetConnectionString("ConnectionName");
                    SqlConnection con = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand("insert into Category values(\'" + category.name + "\',\'" + category.emagePath + "\')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(category);
            
        }

        // GET: CategoryCRUD/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoryCRUD/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(decimal id, [Bind("categoryId,name,emagePath,emageFile")] Category category)
        {
            //if (id != category.categoryId)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                if (category.emageFile != null)
                    {
                        string wwwrootpath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + category.emageFile.FileName;
                        string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await category.emageFile.CopyToAsync(fileStream);
                        }
                        category.emagePath = fileName;
                         string cs = _configuration.GetConnectionString("ConnectionName");
                         SqlConnection con = new SqlConnection(cs);
                         SqlCommand cmd = new SqlCommand("UPDATE Category SET name =\'"+ category.name+"\', imagepath =\'"+ category.emagePath+"\' WHERE Categoryid=\'"+category.categoryId+"\';", con);
                          con.Open();
                         cmd.ExecuteNonQuery();
                         con.Close();
                }  

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoryCRUD/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoryCRUD/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
