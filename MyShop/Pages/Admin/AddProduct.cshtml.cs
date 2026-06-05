using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;
using System.Runtime.CompilerServices;

namespace MyShop.Pages.Admin
{
    public class AddProductModel : PageModel
    {
        MyContext _context;
        public List<Category> Categories { get; set; }
        public AddProductModel(MyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel Pro { get; set; }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Categories = _context.Categories.ToList();
                return Page();
            }

            Product product = new Product()
            {
                Title = Pro.Name,
                Description = Pro.Description,
                Price = Pro.Price,
                Quantity = Pro.Quantity,
                                
            };
            _context.Add(product);
            _context.SaveChanges();

            foreach (var id in Pro.CategoriesId)
            {
                CategoryToProduct categoryToProduct = new CategoryToProduct()
                {
                    CategoryId = id,
                    ProductId = product.Id
                };
                _context.Add(categoryToProduct);
            }
            _context.SaveChanges();

            if (Pro.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", product.Id +Path.GetExtension(Pro.Picture.FileName));
                using (var stream = new FileStream(filePath,FileMode.Create))
                {
                    Pro.Picture.CopyTo(stream);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
