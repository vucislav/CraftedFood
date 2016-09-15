using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class MealModel
    {
        public int MealId { get; set; }
        public int MenuId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string DisplayImage { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public string MealCategory { get; set; }

        public int UnitOfMeasureId { get; set; }
        public int MealCategoryId { get; set; }

        public MealModel()
        {

        }

        public MealModel(int menuId)
        {
            MenuId = menuId;
        }
    }
}