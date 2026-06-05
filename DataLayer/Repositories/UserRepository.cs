using DataLayer.Data;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        bool isExistUserByEmail(string email);
        User GetUserByEmail(string email, string password);
    }

    public class UserRepository : IUserRepository
    {
        private MyContext _context;

        public UserRepository(MyContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByEmail(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public bool isExistUserByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
