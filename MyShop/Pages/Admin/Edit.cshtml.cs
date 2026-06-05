using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;

namespace MyShop.Pages.Admin
{
    public class EditModel : PageModel
    {
        MyContext _context;
        public List<Category> Categories { get; set; }
        public EditModel(MyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel Product { get; set; }

        public void OnGet(int id)
        {
            Categories = _context.Categories.ToList();
            Product = _context.Products.Where(p => p.Id == id).Select(p => new AddEditProductViewModel()
            {
                Id = p.Id,
                Description = p.Description,
                Name = p.Title,
                Price = p.Price,
                Quantity = p.Quantity,
                CategoriesId = _context.CategoryToProducts.Where(c => c.ProductId == id).Select(c => c.CategoryId).ToList()
            }).FirstOrDefault()!;
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
                Id = Product.Id,
                Title = Product.Name,
                Description = Product.Description,
                Price = Product.Price,
                Quantity = Product.Quantity
            };

            var categories = _context.CategoryToProducts
            .Where(c => c.ProductId == Product.Id);
            _context.CategoryToProducts.RemoveRange(categories);

            foreach (var id in Product.CategoriesId)
            {
                CategoryToProduct categoryToProduct = new CategoryToProduct()
                {
                    CategoryId = id,
                    ProductId = product.Id
                };
                _context.Add(categoryToProduct);
            }

            _context.Update(product);
            _context.SaveChanges();


            if (Product.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", product.Id + ".jpg");
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
