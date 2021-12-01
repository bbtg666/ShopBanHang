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
            context.SaveChanges();
            return user.ID;
        }
        public User GetUser(string userName)
        {
            return context.Users.Where(x => x.UserName == userName).SingleOrDefault();
        }
        public bool Login(string userName, string password)
        {
            var rs = context.Users.Count(x => x.UserName == userName && x.Password == password);
            if (rs > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
