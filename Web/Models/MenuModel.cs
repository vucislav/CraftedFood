using Core.Enumerations;
using Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class MenuModel
    {
        public int MenuId { get; set; }
        public int KetteringId { get; set; }
        public string Name { get; set; }
        public IEnumerable<MealModel> Meals { get; set; }

        public MenuModel()
        {

        }

        public MenuModel(int menuId)
        {
            var menu = MenuLogic.GetMenuById(menuId);
            MenuId = menu.MenuId;
            KetteringId = menu.KetteringId;
            Name = menu.Name;
            Meals = menu.Meals.Select(x => new MealModel()
            {
                MealId = x.MealId,
                Title = x.Title,
                Description = x.Description,
                Quantity = x.Quantity,
                UnitOfMeasure = ((UnitOfMeasureEnum)x.UnitOfMeasureId).ToString(),
                MealCategory = ((MealCategoryEnum)x.MealCategoryId).ToString(),
                DisplayImage = x.Image
            });
        }

        public MenuModel(int? ketteringId)
        {
            KetteringId = (int)ketteringId;
        }

        public static IEnumerable<MenuModel> GetMenusForKettering(int ketteringId)
        {
            return KetteringLogic.GetMenusForKettering(ketteringId).Select(x => new MenuModel
            {
                MenuId = x.MenuId,
                Name = x.Name,
                KetteringId = x.KetteringId,
                /*Meals = x.Meals.Select(y => new MealModel
                {
                    MealId = y.MealId,
                    MenuId = y.MenuId,
                    MealCategories = (MealCategoryEnum)y.MealCategoryId,
                    UnitOfMeasures = (UnitOfMeasureEnum)y.UnitOfMeasureId,
                    Title = y.Title,
                    Description = y.Description,
                    Quantity = y.Quantity,
                    DisplayImage = y.Image
                })*/
            });
        }
    }
}