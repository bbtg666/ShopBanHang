using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class CategoryDao
    {
        private ShopOnlineDBConText context;
        public CategoryDao()
        {
            context = new ShopOnlineDBConText();
        }
        public List<Category> ListCategory()
        {
            return context.Categories.Where(x => x.Status == true).ToList();
        }
    }
}
