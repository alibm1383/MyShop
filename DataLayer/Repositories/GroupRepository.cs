using DataLayer.Data;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IGroupRepository
    {
        IEnumerable<Category> GetAllCategories();
        IEnumerable<ShowGroupsViewModel> GetGroupForShow();
    }

    public class GroupRepository : IGroupRepository
    {
        MyContext _context;

        public GroupRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public IEnumerable<ShowGroupsViewModel> GetGroupForShow()
        {
            return _context.Categories.Select(c => new ShowGroupsViewModel
            {
                GroupId = c.Id,
                GroupTitle = c.Title,
                ProductsCount = _context.CategoryToProducts.Count(x => x.CategoryId == c.Id)
            }).ToList();
        }
    }
}
