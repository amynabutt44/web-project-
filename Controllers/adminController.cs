using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class adminController : Controller
    {
        [Authorize(Policy = "adminonly")]
        public IActionResult Index()
        {

       

           

           
            return View();
        }
         [Authorize(Policy = "adminonly")]
        public IActionResult products()
        {

            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            IRepository<product> repo = new GenericRepository<product>(c);

            List<product> p = repo.GetAll();
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;
            return View(p);
        }

        public IActionResult allorders ()
        {

            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            IRepository<Orderr> repo = new GenericRepository<Orderr>(c);

            List<Orderr> p = repo.GetAll();
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;
            return View(p);
        }


    }
}
