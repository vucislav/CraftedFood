using Core.DTOs;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public static class MealLogic
    {
        public static void Create(MealDTO meal)
        {
            Meal m = new Meal
            {
                MealId = meal.MealId,
                MenuId = meal.MenuId,
                Title = meal.Title,
                Description = meal.Description,
                Image = meal.Image,
                Quantity = meal.Quantity,
                UnitOfMeasureId = meal.UnitOfMeasureId,
                MealCategoryId = meal.MealCategoryId
            };
            using (var dc = new CraftedFoodEntities())
            {
                dc.Meal.Add(m);
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

        public static void Edit(MealDTO meal)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var m = GetMealById(meal.MealId, dc);
                if (m != null)
                {
                    m.MenuId = meal.MenuId;
                    m.Title = meal.Title;
                    m.Description = meal.Description;
                    m.Image = meal.Image;
                    m.Quantity = meal.Quantity;
                    m.UnitOfMeasureId = meal.UnitOfMeasureId;
                    m.MealCategoryId = meal.MealCategoryId;
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

        public static void Delete(int mealId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var m = GetMealById(mealId, dc);
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

        private static Meal GetMealById(int mealId, CraftedFoodEntities dc)
        {
            return (from c in dc.Meal
                    where c.MealId == mealId && c.DeleteDate == null
                    select c).FirstOrDefault();
        }
    }
}
