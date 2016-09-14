using Core.Enumerations;
using Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                Title = x.Title,
                MealCategory = ((MealCategoryEnum)x.MealCategoryId).ToString(),
            });
        }

        public static IEnumerable<MenuModel> GetMenusForKettering(int ketteringId)
        {
            return KetteringLogic.GetMenusForKettering(ketteringId).Select(x => new MenuModel
            {
                MenuId = x.MenuId,
                Name = x.Name,
            });
        }
    }
}