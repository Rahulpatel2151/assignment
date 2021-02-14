using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.BAL
{
    public interface IUserManager
    {
        UserView findUser(string username);
        bool isAuthenticated(string email, string password);
        bool isExisting(UserRegistration m, string username);
        void addUser(UserRegistration m);
        void editUser(int id, UserRegistration m);
        void editPassword(int id, string password);
        UserView findUserById(int? userId);
    }
}
