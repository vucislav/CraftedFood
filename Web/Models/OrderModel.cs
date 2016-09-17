using Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int CompanyUserId { get; set; }
        public int MealId { get; set; }
        public string MealTitle { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string Comment { get; set; }

        public OrderModel()
        {

        }

        public OrderModel(int mealId, int companyUserId)
        {
            MealId = mealId;
            CompanyUserId = companyUserId;
        }

        public OrderModel(int companyUserId)
        {

        }

        public static IEnumerable<OrderModel> GetOrdersForCompanyUser(int companyUserId)
        {
            return OrderLogic.GetOrdersForCompanyUser(companyUserId).Select(x => new OrderModel
            {
                OrderId = x.OrderId,
                CompanyUserId = x.CompanyUserId,
                Comment = x.Comment,
                Date = x.Date,
                MealTitle = x.MealTitle,
                Note = x.Note
            });
        }
    }
}