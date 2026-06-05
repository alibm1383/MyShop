using System.Diagnostics;
using System.Security.Claims;
using DataLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace MyShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


    public IActionResult Index()
    {

        ViewBag.Titre = "خوش آمدید";
        var products = _context.Products.ToList();
        return View(products);
    }

    public IActionResult Detail(int id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }

        var categories = _context.Products.Where(p => p.Id == id)
            .SelectMany(c => c.CategoryToProducts).Select(c => c.Category).ToList();

        var vm = new DetailsViewModel()
        {
            Product = product,
            Categories = categories
        };

        return View(vm);
    }


    [Authorize]
    public IActionResult AddToCart(int productId)
    {
        var product = _context.Products.SingleOrDefault(p => p.Id == productId);
        if (product != null)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _context.Orders.FirstOrDefault(o => o.UserId == userId && o.IsFinally == false);
            if (order != null)
            {
                var orderDetail = _context.OrderDetails.FirstOrDefault(od => od.OrderId == order.OrderId && od.ProductId == product.Id);
                if (orderDetail != null)
                {
                    orderDetail.Count += 1;
                }
                else
                {
                    orderDetail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = product.Id,
                        Price = product.Price,
                        Count = 1
                    };

                    _context.Add(orderDetail);
                }
            }
            else
            {
                order = new Order()
                {
                    UserId = userId,
                    CreateDate = DateTime.Now,
                    IsFinally = false
                };
                _context.Add(order);

                OrderDetail orderDetail = new OrderDetail()
                {
                    Order = order,
                    ProductId = product.Id,
                    Price = product.Price,
                    Count = 1
                };

                _context.Add(orderDetail);
            }

            _context.SaveChanges();
        }

        return RedirectToAction("ShowCart");
    }

    [Authorize]
    public IActionResult RemoveCart(int orderDetailId)
    {
        var od = _context.OrderDetails.Find(orderDetailId);
        _context.Remove(od);
        _context.SaveChanges();

        return RedirectToAction("ShowCart");
    }

    [Authorize]
    public IActionResult ShowCart()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());

        var order = _context.Orders.
            Where(o => o.UserId == userId && o.IsFinally == false).
            Include(o => o.OrderDetails).
            ThenInclude(p => p.Product).
            FirstOrDefault();

        return View(order);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("ContactUs")]
    public IActionResult ContactUs()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
