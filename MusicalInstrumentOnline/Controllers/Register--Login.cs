﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MusicalInstrumentOnline.Models;
using System.Data;
using System.Data.SqlClient;

namespace MusicalInstrumentOnline.Controllers
{
    public class Register__Login : Controller
    {
        private readonly IWebHostEnvironment? _webHostEnviroment;
        private readonly IConfiguration _configuration;
        public Register__Login(IWebHostEnvironment? webHostEnviroment, IConfiguration configuration)
        {
            _webHostEnviroment = webHostEnviroment;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind("Fullname,userName,Password,Email")] RegisterClass register)
        {
          string cs = _configuration.GetConnectionString("ConnectionName");
          SqlConnection con = new SqlConnection(cs);
          SqlCommand cmd = new SqlCommand("insert into Register values(\'" + register.Fullname + "\',\'" + register.userName + "\',\'" + register.Password + "\',\'" + register.Email + "\')", con);
          con.Open();
          cmd.ExecuteNonQuery();
          User_ user = new User_();
          user.userName = register.userName;
          user.Password = register.Password;
          user.Rollid = "2";
          SqlCommand cmd2 = new SqlCommand("insert into user_ values(\'" + user.userName + "\',\'" + user.Password + "\',\'" + user.Rollid + "\')", con);
          cmd2.ExecuteNonQuery();
          con.Close();
          return  RedirectToAction(nameof(Login));
        
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

            [HttpPost]
        public IActionResult Login([FromForm] string userName,string Password)
        {
            User_ login = new User_();

            List<User_> list = new List<User_>();

            string cs = _configuration.GetConnectionString("ConnectionName");
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from User_", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("User_");
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
               login = new User_();
               login.userName = dr["username"].ToString();
               login.Password = dr["Password"].ToString();
               login.Rollid = dr["Rollid"].ToString();

                list.Add(login);
            }
            var user = list.Where(x => x.userName == userName && x.Password == Password).FirstOrDefault();
            if (user != null)
            {
                switch (user.Rollid)
                {
                    case "1":
                        HttpContext.Session.SetInt32("id", (int)user.id);
                        return RedirectToAction("Index", "Admin");

                    case "2":
                        HttpContext.Session.SetInt32("id", (int)user.id);
                        return RedirectToAction("Index", "Home");
                }

            }
            return View();
        }
    }
}
