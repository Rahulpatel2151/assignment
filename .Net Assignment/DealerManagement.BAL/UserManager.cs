using DealerManagement.DAL.Repository;
using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.BAL
{
    public class UserManager : IUserManager
    {
        IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void addUser(UserRegistration m)
        {
            _userRepository.addUser(m);
        }

        public void editPassword(int id, string password)
        {
            _userRepository.editPassword(id, password);
        }

        public void editUser(int id, UserRegistration m)
        {
            _userRepository.editUser(id, m);
        }

        public UserView findUser(string username)
        {
            return _userRepository.findUser(username);
        }

        public UserView findUserById(int? userId)
        {
            return _userRepository.findUserById(userId);
        }

        public bool isAuthenticated(string email, string password)
        {
            return _userRepository.isAuthenticated(email, password);

        }

        public bool isExisting(UserRegistration m, string username)
        {
            return _userRepository.isExisting(m, username);
        }
        
    }
}
