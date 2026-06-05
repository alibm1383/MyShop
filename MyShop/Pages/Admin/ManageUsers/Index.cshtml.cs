using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using Models.Models;

namespace MyShop.Pages.Admin.ManageUsers
{
    public class IndexModel : PageModel
    {
        private readonly DataLayer.Data.MyContext _context;

        public IndexModel(DataLayer.Data.MyContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
        }
    }
}
