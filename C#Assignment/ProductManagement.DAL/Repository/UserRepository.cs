using AutoMapper;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private ProductsdbEntities db = new ProductsdbEntities();
        private readonly IMapper mapper;
        public UserRepository()
        {
            AutoMapperConfig.init();
            mapper = AutoMapperConfig.Mapper;
        }
        public UserView findUser(string username)
        {
            var model = db.Users.Where(e => e.Email == username).ToList();//find user
            Users u = model[0];
            
            UserView _user = mapper.Map<Users, UserView>(u);
            return _user;
        }
        public bool isAuthenticated(string email,string password)
        {
            bool isValid = db.Users.Any(x => x.Email == email && x.Password == password);
            return isValid;

        }
        public bool isExisting(UserRegistration m,string username)
        {
            return db.Users.Any(x => x.Email == m.Email && x.Email != username);

        }
        public void addUser(UserRegistration m)
        {
            Users u = new Users()
            {
                Password = HashSHA1(m.Password),
                Email = m.Email,
                MobileNo = m.MobileNo,
                Name = m.Name,
            };
            db.Users.Add(u);
            db.SaveChanges();
        }
        public void editUser(int id, UserRegistration m)
        {
            var u = db.Users.Find(id);
            u.Email = m.Email;
            u.MobileNo = m.MobileNo;
            u.Name = m.Name;
            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void editPassword(int id, string password)
        {
            var u = db.Users.Find(id);
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

    }
}
