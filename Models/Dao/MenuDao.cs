using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class MenuDao
    {
        ShopOnlineDBConText context;
        public MenuDao()
        {
            context = new ShopOnlineDBConText();
        }
        public List<Menu> ListByGroupID(int groupID)
        {
            return context.Menus.Where(x => x.TypeID == groupID).OrderBy(x=>x.DisplayOrder).ToList();
        }
    }
}
