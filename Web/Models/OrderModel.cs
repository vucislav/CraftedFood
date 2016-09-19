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
        public int UserId { get; set; }
        public int CompanyUserId { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public int MealId { get; set; }
        public string MealTitle { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string Comment { get; set; }
        public int Price { get; set; }

        public IEnumerable<CompanyModel> Companies { get; set; }

        public OrderModel()
        {

        }

        public OrderModel(int mealId, int companyUserId, int userId)
        {
            MealId = mealId;
            UserId = userId;
            CompanyUserId = companyUserId;
            Companies = CompanyLogic.GetCompaniesForUser(companyUserId).Select(x => new CompanyModel
            {
                CompanyId = x.CompanyId,
                Name = x.Name,
                Address = x.Address,
                Description = x.Description,
                Phone = x.Phone
            });
        }

        public OrderModel(int companyUserId)
        {

        }

        public static IEnumerable<OrderModel> GetOrdersForCompanyUser(int userId)
        {
            return OrderLogic.GetOrdersForCompanyUser(userId).Select(x => new OrderModel
            {
                OrderId = x.OrderId,
                CompanyUserId = x.CompanyUserId,
                CompanyName = x.CompanyName,
                CompanyId = x.CompanyId,
                Comment = x.Comment,
                Date = x.Date,
                MealTitle = x.MealTitle,
                Note = x.Note,
                Price = x.Price
            });
        }

        public static IEnumerable<OrderModel> GetOrdersForKettering(int ketteringId)
        {
            return OrderLogic.GetOrdersForKettering(ketteringId).Select(x => new OrderModel
            {
                OrderId = x.OrderId,
                CompanyUserId = x.CompanyUserId,
                CompanyId = x.CompanyId,
                Comment = x.Comment,
                Date = x.Date,
                MealTitle = x.MealTitle,
                Note = x.Note,
                Price = x.Price
            });
        }

        public static IEnumerable<OrderModel> GetOrdersForCompany(int companyId)
        {
            return OrderLogic.GetOrdersForCompany(companyId).Select(x => new OrderModel
            {
                OrderId = x.OrderId,
                CompanyUserId = x.CompanyUserId,
                CompanyId = x.CompanyId,
                Comment = x.Comment,
                Date = x.Date,
                MealTitle = x.MealTitle,
                Note = x.Note,
                Price = x.Price
            });
        }
    }
}