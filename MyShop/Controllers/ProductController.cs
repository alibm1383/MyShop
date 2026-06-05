using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyShop.Controllers
{
    public class ProductController : Controller
    {
        MyContext _context;

        public ProductController(MyContext context)
        {
            _context = context;
        }

        [Route("Group/{id}/{name}")]
        public IActionResult ShowProductByGroupId(int id, string name)
        {
            ViewBag.Titre = name;

            var products = _context.CategoryToProducts.
                Where(c => c.CategoryId == id).
                Include(c => c.Product).Select(c => c.Product).ToList();

            return View("Views/Home/Index.cshtml",products);
        }
    }
}
