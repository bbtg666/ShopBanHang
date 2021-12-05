using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ProductCategoryDao
    {
        private ShopOnlineDBConText context;
        public ProductCategoryDao()
        {
            context = new ShopOnlineDBConText();
        }
        public List<ProductCategory> ListProductCategory()
        {
            return context.ProductCategories.Where(x => x.Status == true).OrderBy(x=>x.DisplayOrder).ToList();
        }
    }
}
