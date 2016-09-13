using Core.DTOs;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public static class RatingLogic
    {
        public static void Create(RatingDTO rating)
        {
            Rating r = new Rating
            {
                RatingId = rating.RatingId,
                CompanyUserId = rating.CompanyUserId,
                MealId = rating.MealId,
                Mark = rating.MealId,
                Comment = rating.Comment
            };
            using (var dc = new CraftedFoodEntities())
            {
                dc.Rating.Add(r);
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

        public static void Edit(RatingDTO rating)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var r = GetRatingById(rating.RatingId, dc);
                if (r != null)
                {
                    r.CompanyUserId = rating.CompanyUserId;
                    r.MealId = rating.MealId;
                    r.Mark = rating.MealId;
                    r.Comment = rating.Comment;
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

        public static void Delete(int ratingId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var r = GetRatingById(ratingId, dc);
                if (r != null)
                {
                    r.DeleteDate = DateTime.Now;
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

        private static Rating GetRatingById(int ratingId, CraftedFoodEntities dc)
        {
            return (from c in dc.Rating
                    where c.RatingId == ratingId && c.DeleteDate == null
                    select c).FirstOrDefault();
        }
    }
}
