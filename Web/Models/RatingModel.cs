using Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class RatingModel
    {
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int MealId { get; set; }
        public int Mark { get; set; }
        public string Comment { get; set; }

        public RatingModel()
        {

        }

        public RatingModel(int mealId, int userId)
        {
            MealId = mealId;
            UserId = userId;
        }

        public static IEnumerable<RatingModel> GetRatingsForMeal(int mealId)
        {
            return MealLogic.GetRatingsForMeal(mealId).Select(x => new RatingModel
            {
                RatingId = x.RatingId,
                MealId = x.MealId,
                UserId = x.UserId,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                Comment = x.Comment,
                Mark = x.Mark,
        });
        }
    }
}