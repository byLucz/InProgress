using System;
using System.Linq;
using XKANBAN.Models.User;
using XKANBAN.Services.Interfaces;
using XKANBAN.Contxet;
using XKANBAN.DTOs.Users;

namespace XKANBAN.Services
{
    public class UserService : IUserService
    {
        KanBanContext _context;

        public UserService(KanBanContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public int GetUserIdByEmail(string email)
        {
            return _context.Users.First(u => u.Email == email).UserId;
        }

        public bool IsEmailExist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public bool IsUsernameExist(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }

        public User LoginUser(LoginViewModel model)
        {
            string login = model.Login.ToLower();
            string pass = model.Password;

            return _context.Users.SingleOrDefault(u => u.Username == login && u.Password == pass);
        }
    }
}