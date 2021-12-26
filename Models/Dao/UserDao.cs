using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

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
        public bool Update(User user)
        {
            try
            {

                var userEntity = context.Users.Find(user.ID);
                if(!String.IsNullOrEmpty(user.Password))
                {
                    userEntity.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }
                userEntity.Name = user.Name;
                userEntity.Address = user.Address;
                userEntity.Email = user.Email;
                userEntity.Phone = user.Phone;
                userEntity.ModifiedBy = user.ModifiedBy;
                userEntity.ModifiedDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var user = context.Users.Find(id);
                context.Users.Remove(user);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public bool ChangeStatus(long id)
        {
            var user = context.Users.Find(id);
            user.Status = !user.Status;
            context.SaveChanges();
            if (user.Status)
                return true;
            else return false;
        }
        public User GetUser(string userName)
        {
            return context.Users.Where(x => x.UserName == userName).FirstOrDefault();
        }
        public User GetUserByID(long id)
        {
            return context.Users.Find(id);
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
        public IEnumerable<User> padgination(string searchString, int page, int pageSize)
        {
            IQueryable<User> modelUser = context.Users;
            if (!String.IsNullOrEmpty(searchString))
            {
                modelUser = modelUser.Where(x => x.Name.Contains(searchString) || x.UserName.Contains(searchString)
                                                || x.Address.Contains(searchString) || x.Email.Contains(searchString) 
                                                || x.Phone.Contains(searchString)
                                            );
            }
            return modelUser.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public bool CheckUserName(string userName)
        {
            return context.Users.Where(x => x.UserName == userName).FirstOrDefault() != null;  
        }
        public bool CheckEmail(string email)
        {
            return context.Users.Where(x => x.Email == email).FirstOrDefault() != null;
        }
    }
}
