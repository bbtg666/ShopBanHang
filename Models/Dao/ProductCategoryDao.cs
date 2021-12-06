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
            return context.ProductCategories.Where(x => x.Status == true && x.ParentID != null).OrderBy(x => x.DisplayOrder).ToList();
        }
        public List<ProductCategory> ListParentProductCategory()
        {
            return context.ProductCategories.Where(x => x.Status == true && x.ParentID == null).OrderBy(x => x.DisplayOrder).ToList();
        }
        public ProductCategory GetProductCategoryByID(long id)
        {
            return context.ProductCategories.Find(id);
        }
        public List<long> ListCategoryIDChild(long id)
        {
            return context.ProductCategories.Where(x => x.ParentID == id).Select(x => x.ID).ToList();
        }
    }
}
