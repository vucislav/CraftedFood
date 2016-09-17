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
            MenuId = id;
        }

        public MealModel(int? id)
        {
            var m = MealLogic.GetMealById((int)id);
            MealId = (int)id;
            Title = m.Title;
            Description = m.Description;
            DisplayImage = m.Image;
            Quantity = m.Quantity;
            UnitOfMeasure = ((UnitOfMeasureEnum)m.UnitOfMeasureId).ToString();
            MealCategory = ((MealCategoryEnum)m.MealCategoryId).ToString();
        }
    }
}