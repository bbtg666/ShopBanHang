using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ProductDao
    {
        private ShopOnlineDBConText context;
        public ProductDao()
        {
            context = new ShopOnlineDBConText();
        }
        public List<Product> ListNewProduct(int top)
        {
            return context.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<Product> ListFeatureProduct(int top)
        {
            return context.Products.Where(x => x.TopHot!= null && x.TopHot > DateTime.Now).OrderByDescending(x => x.TopHot).Take(top).ToList();
        }
        public List<Product> ListRalatedProduct(int top, Product proId)
        {
            return context.Products.Where(x => x.ID != proId.ID && x.CategoryID == proId.CategoryID).Take(top).ToList();
        }
        public Product GetProductById(long id)
        {
            return context.Products.Find(id);
        }
        public List<Product> GetListProductByCategoryIdPagination(long id, ref long totalRecords, int page, int pageSize)
        {
            totalRecords = context.Products.Where(x => x.Status == true &&  x.CategoryID == id).Count();
            return context.Products.Where(x => x.Status == true && x.CategoryID == id).OrderBy(x => x.CreateDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public List<Product> GetListProductByListChildCategoryIdPagination(long id, ref long totalRecords, int page, int pageSize, List<long> listID)
        {
            totalRecords = context.Products.Where(x => x.Status == true && listID.Contains(x.ID)).Count();
            return context.Products.Where(x => x.Status == true && listID.Contains(x.ID)).OrderBy(x => x.CreateDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
