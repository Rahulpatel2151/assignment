using AutoMapper;
using DealerManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private DealerManagementEntities db = new DealerManagementEntities();
        private readonly IMapper mapper;
        public UserRepository()
        {
            AutoMapperConfig.init();
            mapper = AutoMapperConfig.Mapper;
        }
        public UserView findUser(string username)
        {
            var model = db.Customers.Where(e => e.Email == username).ToList();//find user
            try
            {
                Customers u = model[0];
                UserView _user = mapper.Map<Customers, UserView>(u);
                return _user;
            }
            catch 
            {
                return null;
            }
        }
        public bool isAuthenticated(string email, string password)
        {
            bool isValid = db.Customers.Any(x => x.Email == email && x.Password == password);
            return isValid;

        }
        public bool isExisting(UserRegistration m, string username)
        {
            return db.Customers.Any(x => x.Email == m.Email && x.Email != username);

        }
        public void addUser(UserRegistration m)
        {
            Customers c = mapper.Map<UserRegistration, Customers>(m);
            db.Customers.Add(c);
            db.SaveChanges();
        }
        public void editUser(int id, UserRegistration m)
        {
            var u = db.Customers.Find(id);
            u.Email = m.Email;
            u.Phone = m.Phone;
            u.Name = m.Name;
            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void editPassword(int id, string password)
        {
            var u = db.Customers.Find(id);
            u.Password = password;
            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();
        }
        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public UserView findUserById(int? userId)
        {
            Customers u = db.Customers.Find(userId);//find user
            try
            {
                UserView _user = mapper.Map<Customers, UserView>(u);
                return _user;
            }
            catch
            {
                return null;
            }
        }
    }
}
