using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MusicalInstrumentOnline.Models;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipelines;
using System.Net.Mail;
using System.Net;
using System.Net.Mail;
using MimeKit.Text;
using MailKit.Security;

namespace MusicalInstrumentOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
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
                product.productId = Convert.ToInt32(dr["Productid"]);
                product.categoryId = Convert.ToInt32(dr["Categoryid"]);
                product.name = dr["name"].ToString();
                product.imagePath = dr["imagepath"].ToString();
                product.price = dr["price"].ToString();
                product.productId = Convert.ToInt32(dr["Productid"]);

                list.Add(product);
            }
            return list;
        }
        public List<User_> GetInfoUser()
        {
            User_ user = new User_();
            List<User_> list = new List<User_>();

            string cs = _configuration.GetConnectionString("ConnectionName");
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from User_", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("User_");
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
               user = new User_();
               user.id = Convert.ToInt32(dr["id"]);
               user.userName = dr["username"].ToString();
               user.Password = dr["Password"].ToString();
               list.Add(user);
            }
            return list;
        }
        public List<CartProduct> GetInfoCart()
        {
            CartProduct cart = new CartProduct();
            List<CartProduct> list = new List<CartProduct>();

            string cs = _configuration.GetConnectionString("ConnectionName");
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from Cart", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Cart");
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cart = new CartProduct();
                cart.Id = Convert.ToInt32(dr["id"]);
                cart.price = dr["price"].ToString();
                cart.name = dr["name"].ToString();
                cart.imagePath = dr["imagepath"].ToString();
                cart.Quantity = Convert.ToInt32(dr["Quantity"]);
                cart.totalPrice = Convert.ToDouble(dr["totalprice"]);
                cart.Productid = Convert.ToInt32(dr["Productid"]);
                cart.usertId = Convert.ToInt32(dr["userid"]);

                list.Add(cart);
            }
            return list;
        }
        public List<CartProduct> GetInfoCartByUser(int id)
        {
            CartProduct cart = new CartProduct();
            List<CartProduct> list = new List<CartProduct>();

            string cs = _configuration.GetConnectionString("ConnectionName");
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from Cart where userid=\'"+id+"\'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Cart");
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cart = new CartProduct();
                cart.Id = Convert.ToInt32(dr["id"]);
                cart.price = dr["price"].ToString();
                cart.name = dr["name"].ToString();
                cart.imagePath = dr["imagepath"].ToString();
                cart.Quantity = Convert.ToInt32(dr["Quantity"]);
                cart.totalPrice = Convert.ToDouble(dr["totalprice"]);
                cart.Productid = Convert.ToInt32(dr["Productid"]);
                cart.usertId = Convert.ToInt32(dr["userid"]);

                list.Add(cart);
            }
            return list;
        }
        public List<VisaCard> GetInfoCartPayment()
        {
            VisaCard visa = new VisaCard();
            List<VisaCard> list = new List<VisaCard>();

            string cs = _configuration.GetConnectionString("ConnectionName");
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("select * from VisaCard", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("VisaCard");
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                visa = new VisaCard();
                visa.id = Convert.ToInt32(dr["id"]);
                visa.usertname = dr["username"].ToString();
                visa.password = dr["password"].ToString();
                visa.expirydate = dr["expirydate"].ToString();
                visa.cvv = Convert.ToInt32(dr["cvv"]);
                visa.crdit = Convert.ToInt32(dr["crdit"]);
                visa.cardnumber = Convert.ToInt32(dr["cardnumber"]);
                list.Add(visa);
            }
            return list;
        }
        [HttpGet]
        public IActionResult Index()
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
                category.categoryId=Convert.ToInt32(dr["Categoryid"]);
                category.name = dr["name"].ToString();
                category.emagePath = dr["imagepath"].ToString();

                list.Add(category);
            }

            TeamMember team_Member = new TeamMember();
            List<TeamMember> list2 = new List<TeamMember>();
            SqlCommand cmd2 = new SqlCommand("select * from Teammember", con);
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable("Teammember");
            da2.Fill(dt2);
            foreach (DataRow dr in dt2.Rows)
            {
                team_Member = new TeamMember();
                team_Member.description = dr["descriptions"].ToString();
                team_Member.name = dr["name"].ToString();
                team_Member.emagePath = dr["imagepath"].ToString();

                list2.Add(team_Member);
            }
            Tastimonal tastimonal = new Tastimonal();
            List<Tastimonal> list3 = new List<Tastimonal>();
            SqlCommand cmd3 = new SqlCommand("select * from tastimonal", con);
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable("tastimonal");
            da3.Fill(dt3);
            foreach (DataRow dr in dt3.Rows)
            {
                tastimonal = new Tastimonal();
                tastimonal.Description = dr["descriptions"].ToString();
                tastimonal.name = dr["name"].ToString();
                tastimonal.imagepath = dr["imagepath"].ToString();

                list3.Add(tastimonal);
            }

            Slider slider = new Slider();
            List<Slider> list4 = new List<Slider>();
            SqlCommand cmd4 = new SqlCommand("select * from Slider", con);
            SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
            DataTable dt4 = new DataTable("Slider");
            da4.Fill(dt4);
            foreach (DataRow dr in dt4.Rows)
            {
                slider = new Slider();
                slider.Description1 = dr["descriptions1"].ToString();
                slider.Description2 = dr["descriptions2"].ToString();
                slider.Description3 = dr["descriptions3"].ToString();
                slider.imagepath = dr["imagepath"].ToString();

                list4.Add(slider);
            }
            var home = Tuple.Create<IEnumerable<Category>, IEnumerable<TeamMember>,IEnumerable<Tastimonal>,IEnumerable<Slider>>(list,list2,list3,list4);

            return View(home);
        }

        [HttpPost]
        public IActionResult Index([Bind("name,email,spicalNote")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                string cs = _configuration.GetConnectionString("ConnectionName");
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("insert into Contact values(\'" + contact.name + "\',\'" + contact.email + "\',\'" + contact.spicalNote + "\')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();                                
            }
            return View();
        }
        [HttpGet]
        public IActionResult Product(int id)
        {
            if (id != 0)
            {
                return View(GetInfo().ToList().Where(x => x.categoryId == id));
            }
            else
            {
                ViewBag.Product = "All Product";
                return View(GetInfo().ToList());
            }
        }
        
        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BayProd(int id)
        {
            // var userid =0;
            if (HttpContext.Session.GetInt32("id") != null)
            {
                //userid =(int)HttpContext.Session.GetInt32("id");
                //var user = GetInfoUser().ToList();
                //var home = Tuple.Create<IEnumerable<User_>, IEnumerable<Product>>(user, product);
                var product = GetInfo().ToList().Where(x => x.productId == id);
                return View(product);
            }
            else
            {
                return RedirectToAction("Login","Register__Login");
            }
        }

        [HttpPost]
        public IActionResult BayProd([Bind("Productid,name,price,imagePath,Quantity")] CartProduct cart,int Productid)
        {
            var userid = 0;
            if (HttpContext.Session.GetInt32("id") != null)
            {
              userid = (int)HttpContext.Session.GetInt32("id");
            if (ModelState.IsValid)
            {
                string price = "";
                if (cart.price.Length == 7)
                {
                    price = cart.price.Substring(1, 6);
                }
                else if (cart.price.Length == 6)
                {
                    price = cart.price.Substring(1, 5);
                }

                cart.totalPrice = cart.Quantity * Convert.ToDouble(price);
                string cs = _configuration.GetConnectionString("ConnectionName");
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("insert into cart values(\'" + cart.name + "\',\'" + cart.price + "\',\'" + cart.imagePath + "\',\'" + cart.Quantity + "\',\'" + cart.totalPrice + "\',\'" + cart.Productid + "\',\'"+ userid+"\')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
             List<CartProduct> list = new List<CartProduct>();
                return RedirectToAction("Cart");
            }
            else
            {
                return RedirectToAction("Login", "Register__Login");
            }
        }
        [HttpGet]
        public IActionResult Cart()
        {
            var userid = 0;
            if (HttpContext.Session.GetInt32("id") != null)
            {
                List<CartProduct> list = new List<CartProduct>();
                userid = (int)HttpContext.Session.GetInt32("id");
                var cart = GetInfoCartByUser(userid);
                foreach (var item in cart)
                {
                    if (item.price.Length == 7)
                    {
                        item.price = item.price.Substring(1, item.price.Length-1);
                    }
                    else if (item.price.Length == 6)
                    {
                        item.price = item.price.Substring(1, item.price.Length-1);
                    }
                    list.Add(item);
                    
                }
                ViewBag.TotalSale = list.Sum(x => x.Quantity * Convert.ToDouble(x.price));
                return View(GetInfoCart().Where(x=> x.usertId== userid));
            }
            else
            {
                return RedirectToAction("Login", "Register__Login");
            }
        }
        [HttpPost]
        public IActionResult Cart(int id)
        {
            return RedirectToAction("Payment");
        }

        [HttpGet]
        public IActionResult Payment()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Payment([Bind("id,crdit,cvv,cardnumber,usertname,expirydate,password")] VisaCard visa)
        {
            var userid = 0;
            if (HttpContext.Session.GetInt32("id") != null)
            {
                List<CartProduct> list = new List<CartProduct>();
                userid = (int)HttpContext.Session.GetInt32("id");
                var Info = GetInfoCartPayment();
                foreach (var item in Info)
                {
                    if (visa.usertname == item.usertname && visa.password == item.password)
                    {
                        var cart = GetInfoCartByUser(userid);
                        
                        foreach (var item2 in cart)
                        {
                            if (item2.price.Length == 7)
                            {
                               item2.price = item2.price.Substring(1, item2.price.Length-1);
                            }
                            else if (item2.price.Length == 6)
                            {
                                item2.price = item2.price.Substring(1, item2.price.Length-1);
                            }
                            list.Add(item2);

                        }
                        double totalsale = list.Sum(x => x.Quantity * Convert.ToDouble(x.price));
                        string cs = _configuration.GetConnectionString("ConnectionName");
                        var temp = item.crdit - totalsale;
                        SqlConnection con = new SqlConnection(cs);
                        SqlCommand cmd = new SqlCommand("Update VisaCard set crdit=\'"+temp+"\' where username=\'"+item.usertname+"\'", con);
                        SqlCommand cmd2 = new SqlCommand("delete cart where  userid=\'" + userid + "\'",con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        con.Close();
                        var email = new MimeMessage();
                        email.From.Add(MailboxAddress.Parse("jessy.kozey84@ethereal.email"));
                        email.To.Add(MailboxAddress.Parse("jessy.kozey84@ethereal.email"));
                        email.Subject = "Eshop Hamza Altrini";
                        email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Thank You "+visa.usertname+ " For Coming</h1> <br/> <h1> Hamza Altrini</h1>" };
                        using var smtp = new MailKit.Net.Smtp.SmtpClient();
                        smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTlsWhenAvailable);//gmail.com.email
                        smtp.Authenticate("jessy.kozey84@ethereal.email", "pRv6dcbJFee4cByG1j");//write password
                        smtp.Send(email);
                        smtp.Disconnect(true);
                    }
                   

                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Register__Login");
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("id");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}