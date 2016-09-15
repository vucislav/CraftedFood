using Core.Enumerations;
using Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public UnitOfMeasureEnum UnitOfMeasures { get; set; }
        public MealCategoryEnum MealCategories { get; set; }

        public int UnitOfMeasureId { get; set; }
        public int MealCategoryId { get; set; }

        public MealModel()
        {

        }

        public MealModel (int id)
        {
            var m = MealLogic.GetMealById(id);
            MenuId = m.MenuId;
            Title = m.Title;
            Description = m.Description;
            //Image = m.Image;
            Quantity = m.Quantity;
            UnitOfMeasureId = m.UnitOfMeasureId;
            MealCategoryId = m.MealCategoryId;
        }
    }
}