using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AddToCartController : Controller
    {
        private const string CartSessionKey = "Cart";
        private readonly IRepository<product> _productRepo;
        private readonly IRepository<Orderr> _orderRepo;

        public AddToCartController()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            _productRepo = new GenericRepository<product>(connectionString);
            _orderRepo = new GenericRepository<Orderr>(connectionString);
        }

        public IActionResult Index()
        {
            List<product> products = _productRepo.GetAll();
            return View(products);
        }

        [HttpPost]
        public IActionResult Cart(int productId)
        {
            var user = User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId != null)
                {
                    HttpContext.Session.SetString("UserId", userId);
                    TempData["userid"] = userId;

                    HttpContext.Session.SetInt32("Id", productId);

                    var product = _productRepo.GetAll().FirstOrDefault(p => p.Id == productId);
                    if (product != null)
                    {
                        var cart = GetCart();
                        var cartItem = cart.FirstOrDefault(ci => ci.ProductId == productId.ToString());

                        if (cartItem != null)
                        {
                            cartItem.Quantity += 1;
                        }
                        else
                        {
                            cart.Add(new CartItem
                            {
                                ProductId = productId.ToString(),
                                Quantity = 1,
                                Price = product.price.ToString(),
                                Name = product.name,
                                Image = product.image
                            });
                        }

                        SaveCart(cart);

                        return RedirectToAction("ViewCarts", "AddToCart");
                    }
                }
            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(ci => ci.ProductId == productId.ToString());

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCart(cart);
            }

            return RedirectToAction("ViewCarts");
        }

        [HttpPost]
        public IActionResult IncreaseQuantity(int productId)
        {
            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(ci => ci.ProductId == productId.ToString());

            if (cartItem != null)
            {
                cartItem.Quantity += 1;
                SaveCart(cart);
            }

            return RedirectToAction("ViewCarts");
        }

        [HttpPost]
        public IActionResult DecreaseQuantity(int productId)
        {
            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(ci => ci.ProductId == productId.ToString());

            if (cartItem != null && cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
                SaveCart(cart);
            }

            return RedirectToAction("ViewCarts");
        }

        public IActionResult ViewCarts()
        {
            var cart = GetCart();

            ViewBag.TotalAmount = cart.Sum(item => item.Quantity * int.Parse(item.Price));
            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;
            return View(cart);
        }

        private List<CartItem> GetCart()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }

            return JsonSerializer.Deserialize<List<CartItem>>(cartJson);
        }

        private void SaveCart(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartSessionKey, cartJson);
        }

        public IActionResult Checkout()
        {
            var cart = GetCart();
            var totalAmount = cart.Sum(item => item.Quantity * int.Parse(item.Price));

            ViewBag.TotalAmount = totalAmount;
            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;
            return View(cart);
        }

        [HttpPost]
        public IActionResult DeliveryInformation(DeliveryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("OrderSummary", GetCart());
            }

            var userId = HttpContext.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var cart = GetCart();
            var totalAmount = cart.Sum(item => item.Quantity * int.Parse(item.Price));

            var order = new Orderr
            {
                UserId = userId,
                ProductId = HttpContext.Session.GetInt32("Id") ?? 0,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                TotalAmount = totalAmount,
                OrderDate = DateTime.Now,
                Name = model.Name
            };

            _orderRepo.Add(order);

            SaveCart(new List<CartItem>());

            return RedirectToAction("OrderConfirmation", new { userId = userId });
        }

        public IActionResult OrderConfirmation(string userId)
        {
            OrderRepository or = new OrderRepository();
            List<Orderr> orders = or.GetOrders(userId);

            if (orders.Count == 0)
            {
                return NotFound();
            }

            Orderr mostRecentOrder = orders.OrderByDescending(o => o.OrderDate).FirstOrDefault();
            string c = string.Empty;

            c = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mydb2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            IRepository<catagory> rep = new GenericRepository<catagory>(c);
            List<catagory> products = rep.GetAll().ToList();
            ViewBag.MyList = products;
            return View(mostRecentOrder);
        }
    }
}
