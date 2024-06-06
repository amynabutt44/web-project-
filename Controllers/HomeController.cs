using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;
using WebApplication2.Models.viewmodel;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            IRepository<product> repo = new GenericRepository<product>(c);

            List<product> p = repo.GetAll();

            IRepository<catagory> r = new GenericRepository<catagory>(c);

            List<catagory> cat= r.GetAll();

            Catagoryproduct cp = new Catagoryproduct { c = cat, product = p };

            //object o = HttpContext.Session.GetString("mydata");
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;

            return View(cp);
        }

        
        public ViewResult aboutUs()
        {
            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;

            return View();
        }
        public ViewResult refundpolicy()
        {
            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;
            return View();
        }

        public ViewResult contactUs()
        {
            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;
            return View();
        }
        // [Authorize(Policy = "businessHourOnly")]

        public ViewResult FAQ()
        {
            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;
            return View();
        }

    }
}
