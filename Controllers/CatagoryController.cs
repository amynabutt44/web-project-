using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace WebApplication2.Controllers
{
    public class CatagoryController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public CatagoryController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public ViewResult Index()
        {


            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            IRepository<catagory> repo = new GenericRepository<catagory>(c);

            List<catagory> p = repo.GetAll();


            
           
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;
            return View(p);
           
        }



        [HttpGet]
        public ViewResult delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult delete(int code)
        {
            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            IRepository<catagory> repo = new GenericRepository<catagory>(c);
            catagory cat = new catagory();
            cat.Id = code;
         
           
            repo.delete(cat);
            return RedirectToAction("Index", "Catagory");
        }






        [HttpGet]
        public IActionResult UpdateImage()
        {
            return View();
        }

        // POST: Catagory/UpdateImage
        [HttpPost]
        public IActionResult UpdateImage(int code, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                ModelState.AddModelError("image", "Please select a file");
                return View();
            }

            string wwwrootPath = _env.WebRootPath;
            string path = Path.Combine(wwwrootPath, "uploadedfiles");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            
            string fileName = $"{code}_{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            string filePath = Path.Combine(path, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            catagory catagory = new catagory();
            catagory.Id = code;
            catagory.image = fileName;
            // Update the category image path in the database
           CatagoryRepositery cr= new CatagoryRepositery();
            cr.update(catagory);

            return RedirectToAction("Index", "Catagory");
        }







        [HttpGet]
        public ViewResult add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult add(string name, IFormFile image)
        {
            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            IRepository<catagory> repo = new GenericRepository<catagory>(c);

            string wwwrootPath = _env.WebRootPath;
            string path = Path.Combine(wwwrootPath, "uploadedfiles");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileName(image.FileName);
            string filePath = Path.Combine(path, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            catagory catagory = new catagory();
            catagory.Name = name;
            catagory.image = fileName;


            repo.Add(catagory);


            return RedirectToAction("Index","Catagory");
        }

    }
}