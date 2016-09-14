using Core.DTOs;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public static class MenuLogic
    {
        public static void Create(MenuDTO menu)
        {
            Menu m = new Menu
            {
				KetteringId = menu.KetteringId,
				Name = menu.Name
            };
            using (var dc = new CraftedFoodEntities())
            {
                dc.Menu.Add(m);
                try
                {
                    dc.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static void Edit(MenuDTO menu)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var m = GetMenuById(menu.MenuId, dc);
                if (m != null)
                {
                    m.MenuId = menu.MenuId;
					m.KetteringId = menu.KetteringId;
					m.Name = menu.Name;
                }
                try
                {
                    dc.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static void Delete(int menuId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var m = GetMenuById(menuId, dc);
                if (m != null)
                {
                    m.DeleteDate = DateTime.Now;
                }
                try
                {
                    dc.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static MenuDTO GetMenuById(int menuId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var menu = GetMenuById(menuId, dc);
                return new MenuDTO
                {
                    MenuId = menu.MenuId,
                    KetteringId = menu.KetteringId,
                    Name = menu.Name,
                    Meals = menu.Meal.Select(x => new MealDTO
                    {
                        MealId = x.MealId,
                        MenuId = x.MenuId,
                        Title = x.Title,
                        Description = x.Description,
                        Image = x.Image,
                        Quantity = x.Quantity,
                        MealCategoryId = x.MealCategoryId,
                        UnitOfMeasureId = x.UnitOfMeasureId
                    })
                };
            }
                
        }

        private static Menu GetMenuById(int menuId, CraftedFoodEntities dc)
        {
            return (from c in dc.Menu
                    where c.MenuId == menuId && c.DeleteDate == null
                    select c).FirstOrDefault();
        }
    }
}
