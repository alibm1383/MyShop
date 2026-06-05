using DataLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;

namespace MyShop.Pages.Admin
{
    public class AddCategoryModel : PageModel
    {
        MyContext _context;

        [BindProperty]
        public Category Category { get; set; }
        public AddCategoryModel(MyContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Add(Category);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
