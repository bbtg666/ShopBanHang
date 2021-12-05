using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class SlideDao
    {
        ShopOnlineDBConText context;
        public SlideDao()
        {
            context = new ShopOnlineDBConText();
        }
        public List<Slide> ListSlide()
        {
            return context.Slides.Where(x=>x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
