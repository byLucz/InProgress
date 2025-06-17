using XKANBAN.Models.User;
using XKANBAN.DTOs.Users;

namespace XKANBAN.Services.Interfaces
{
    public interface IUserService
    {
        bool IsUsernameExist(string username);
        bool IsEmailExist(string email);
        void AddUser(User user);
        User LoginUser(LoginViewModel model);
        int GetUserIdByEmail(string email);
    }
}