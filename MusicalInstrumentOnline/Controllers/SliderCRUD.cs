using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentOnline.Models;
using System.Data.SqlClient;
using System.Data;

namespace MusicalInstrumentOnline.Controllers
{
    public class SliderCRUD : Controller
    {
        private readonly IWebHostEnvironment? _webHostEnviroment;
        private readonly IConfiguration _configuration;
        public SliderCRUD(IWebHostEnvironment? webHostEnviroment, IConfiguration configuration)
        {
            _webHostEnviroment = webHostEnviroment;
            _configuration = configuration;
        }

        public List<Slider> GetInfo()
        {
            Slider slider = new Slider();

            List<Slider> list = new List<Slider>();

            string? cs = _configuration.GetConnectionString("ConnectionName");
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from Slider", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Slider");
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                slider = new Slider();
                slider.Id = Convert.ToInt32(dr["id"]);
                slider.Description1 = dr["descriptions1"].ToString();
                slider.Description2 = dr["descriptions2"].ToString();
                slider.Description3 = dr["descriptions3"].ToString();

                slider.imagepath = dr["imagepath"].ToString();

                list.Add(slider);
            }
            return list;
        }
        // GET: Slider
        public ActionResult Index()
        {
            return View(GetInfo().ToList());
        }

        // GET: Slider/Details/5
        public ActionResult Details(int id)
        {
            return View(GetInfo().ToList().Where(x => x.Id == id));
        }

        // GET: Slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create([Bind("Description1,Description2,Description3,imageFile")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                if (slider.imageFile != null)
                {
                    string wwwrootpath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + " " + slider.imageFile.FileName;
                    string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await slider.imageFile.CopyToAsync(fileStream);
                    }
                    slider.imagepath = fileName;
                    string cs = _configuration.GetConnectionString("ConnectionName");
                    SqlConnection con = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand("insert into Slider values(\'" + slider.imagepath + "\',\'" + slider.Description1 + "\' ,\'" + slider.Description2 + "\',\'"+slider.Description3+"\')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(slider);
        }

        // GET: Slider/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        // POST: Slider/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(int id, [Bind("Id,Description1,Description2,Description3,imageFile")] Slider slider)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (slider.imageFile != null)
                    {
                        string? wwwrootpath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + slider.imageFile.FileName;
                        string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await slider.imageFile.CopyToAsync(fileStream);
                        }
                        slider.imagepath = fileName;
                        string? cs = _configuration.GetConnectionString("ConnectionName");
                        SqlConnection con = new SqlConnection(cs);
                        SqlCommand cmd = new SqlCommand("UPDATE Slider SET imagepath =\'" + slider.imagepath + "\', descriptions1 =\'" + slider.Description1 + "\' , descriptions2=\'" + slider.Description2 + "\' ,descriptions3=\'"+slider.Description3+"\'  WHERE id=\'" + slider.Id + "\';", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    return RedirectToAction(nameof(Index));
                }
                return View(slider);
            }
            catch
            {
                return View();
            }
        }

        // GET: Slider/Delete/5
        public ActionResult Delete(int id)
        {
            return View(GetInfo().ToList().Where(x => x.Id == id));
        }

        // POST: Slider/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                string cs = _configuration.GetConnectionString("ConnectionName");
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("delete from Slider where Id=\'" + id + "\';", con);
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
