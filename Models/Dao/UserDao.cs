using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class UserDao
    {
        ShopOnlineDBConText context;
        public UserDao()
        {
            context = new ShopOnlineDBConText();
        }
        public long Insert(User user)
        {
            context.Users.Add(user);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 10);
            context.SaveChanges();
            return user.ID;
        }
        public User GetUser(string userName)
        {
            return context.Users.Where(x => x.UserName == userName).SingleOrDefault();
        }
        public int Login(string userName, string password)
        {
            User user = GetUser(userName);
            if (user == null)
                return 0;
            else if (user.Status == false)
                return -1;
            else
            {
                bool checkPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (checkPassword)
                    return 1;
                else
                    return 0;
            }
        }
    }
}
