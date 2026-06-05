using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace MyShop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        MyContext _context;
        public IndexModel(MyContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> Products { get; set; }
        public void OnGet()
        {
            Products = _context.Products;
        }

        public IActionResult OnPostDelete(int id)
        {
            Product product = new Product()
            {
                Id = id
            };

            _context.Remove(product);
            _context.SaveChanges();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", id + ".jpg");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return RedirectToPage("Index");
        }
    }
}
