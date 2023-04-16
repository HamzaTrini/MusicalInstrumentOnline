using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentOnline.Models;
using System.Data;
using System.Data.SqlClient;

namespace MusicalInstrumentOnline.Controllers
{
    public class Team_Member : Controller
    {
        private readonly IWebHostEnvironment? _webHostEnviroment;
        private readonly IConfiguration _configuration;
        public Team_Member(IWebHostEnvironment? webHostEnviroment, IConfiguration configuration)
        {
            _webHostEnviroment = webHostEnviroment;
            _configuration = configuration;
        }
        // GET: Team_Member
        public ActionResult Index()
        {
            TeamMember team_Member = new TeamMember();

            List<TeamMember> list = new List<TeamMember>();

            string cs = _configuration.GetConnectionString("ConnectionName");
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from Teammember", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Teammember");
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                team_Member = new TeamMember();
                team_Member.description = dr["descriptions"].ToString();
                team_Member.name = dr["name"].ToString();
                team_Member.emagePath = dr["imagepath"].ToString();

                list.Add(team_Member);
            }
            return View(list.ToList());
        }

        // GET: Team_Member/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Team_Member/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team_Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("name,emageFile,description")] TeamMember team_Member)
        {
            if (ModelState.IsValid)
            {
                if (team_Member.emageFile != null)
                {
                    string wwwrootpath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + " " + team_Member.emageFile.FileName;
                    string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await team_Member.emageFile.CopyToAsync(fileStream);
                    }
                    team_Member.emagePath = fileName;
                    string cs = _configuration.GetConnectionString("ConnectionName");
                    SqlConnection con = new SqlConnection(cs);
                    SqlCommand cmd = new SqlCommand("insert into Teammember values(\'" + team_Member.name + "\',\'" + team_Member.emagePath + "\',\'"+ team_Member.description+"\')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(team_Member);
        }

        // GET: Team_Member/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Team_Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,name,emageFile,description")] TeamMember team_Member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (team_Member.emageFile != null)
                    {
                        string wwwrootpath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + team_Member.emageFile.FileName;
                        string path = Path.Combine(wwwrootpath + "/Home/img/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await team_Member.emageFile.CopyToAsync(fileStream);
                        }
                        team_Member.emagePath = fileName;
                        string cs = _configuration.GetConnectionString("ConnectionName");
                        SqlConnection con = new SqlConnection(cs);
                        SqlCommand cmd = new SqlCommand("UPDATE Teammember SET name =\'" + team_Member.name + "\', imagepath =\'" + team_Member.emagePath +"\',descriptions =\'" + team_Member.description +"\' WHERE id=\'" + team_Member.Id + "\';", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Team_Member/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Team_Member/Delete/5
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
