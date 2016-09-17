using Core.DTOs;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public static class OrderLogic
    {
        public static void Create(OrderDTO order)
        {
            Order o = new Order
            {
                OrderId = order.OrderId,
                CompanyUserId = order.CompanyUserId,
                MealId = order.MealId,
                Date = order.Date,
                Note = order.Note,
                Comment = order.Comment
            };
            using (var dc = new CraftedFoodEntities())
            {
                dc.Order.Add(o);
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

        public static void Edit(OrderDTO order)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var o = GetOrderById(order.OrderId, dc);
                if (o != null)
                {
                    o.CompanyUserId = order.CompanyUserId;
                    o.MealId = order.MealId;
                    o.Date = order.Date;
                    o.Note = order.Note;
                    o.Comment = order.Comment;
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

        public static void Delete(int orderId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var o = GetOrderById(orderId, dc);
                if (o != null)
                {
                    o.DeleteDate = DateTime.Now;
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

        private static Order GetOrderById(int orderId, CraftedFoodEntities dc)
        {
            return (from c in dc.Order
                    where c.OrderId == orderId && c.DeleteDate == null
                    select c).FirstOrDefault();
        }

        public static IEnumerable<OrderDTO> GetOrdersForCompanyUser(int companyUserId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                return (from o in dc.Order
                        where o.CompanyUserId == companyUserId && o.DeleteDate == null
                        select new OrderDTO
                        {
                            OrderId = o.OrderId,
                            CompanyUserId = o.CompanyUserId,
                            Comment = o.Comment,
                            Date = o.Date,
                            MealId = o.MealId,
                            MealTitle = o.Meal.Title,
                            Note = o.Note
                        }).ToList();
            }
        }
    }
}
