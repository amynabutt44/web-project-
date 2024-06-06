using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using WebApplication2.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    public class productsController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public productsController(IWebHostEnvironment env)
        {
            _env = env;
        }


        //[Authorize (Policy = "pakistanonly")]
        public IActionResult Index()
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
        
        public IActionResult allproduct( string name)
        {
            HttpContext.Response.Cookies.Delete("first_request");
            CatagoryRepositery cr = new CatagoryRepositery();
            int id=cr.getid(name);

            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            IRepository<product> repo = new GenericRepository<product>(c);

            List<product> p = repo.GetById(id);
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;
            return View(p);
        }

     

        [HttpGet]
        public ViewResult add()
        {


            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            IRepository<catagory> repo = new GenericRepository<catagory>(c);

            List<catagory> p = repo.GetAll();
            return View(p);

        }



        public IActionResult add(string name, int price, string description, IFormFile image, int category)
        {

            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            IRepository<product> repo = new GenericRepository<product>(c);
        

            string fileName = Path.GetFileName(image.FileName);
            string fileExtension = Path.GetExtension(fileName);

            product p = new product();
            p.name = name;
            p.price = price;
            p.c_id = category;
            p.description = description;
            p.image = fileName; 

           
            repo.Add(p);
        
         

            string wwwrootPath = _env.WebRootPath;
            string path = Path.Combine(wwwrootPath, "uploadedfiles");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = Path.Combine(path, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return RedirectToAction("products", "admin");
        }


        //[HttpGet]
        //public ViewResult delete()
        //{
        //    return View();
        //}

        [HttpPost]
        public ActionResult delete(int id)
        {
            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            IRepository<product> repo = new GenericRepository<product>(c);

            product p = new product();
            p.Id = id;
            
            repo.delete(p);
            return RedirectToAction("products", "admin");
        }


      


        public ViewResult update()
        {
            return View();
        }
        [HttpPost]
        public IActionResult update(int code, int price  , string name)
        {


           
            product p = new product();
            p.price = price;
            p.Id = code;
            p.name=name;
            productRepository pr = new productRepository();
            pr.update_price(p);
            return RedirectToAction("products", "admin");
        }






    }
}
