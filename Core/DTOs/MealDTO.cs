using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Core.DTOs
{
    public class MealDTO
    {
        public int MealId { get; set; }
        public int MenuId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Byte[] Image { get; set; }
        public int Quantitiy { get; set; }
        public int UnitOfMeasureId { get; set; }
        public int MealCategoryId { get; set; }
    }
}
