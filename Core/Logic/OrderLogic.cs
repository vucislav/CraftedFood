﻿using Core.DTOs;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Core.Logic
{
    public static class OrderLogic
    {
        public static void Create(OrderDTO order)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var companyUserId = (from c in dc.CompanyUser
                                     where c.UserId == order.UserId && c.CompanyId == order.CompanyId
                                     select c.CompanyUserId).ToList().FirstOrDefault();

                Order o = new Order
                {
                    OrderId = order.OrderId,
                    CompanyUserId = companyUserId,
                    MealId = order.MealId,
                    Date = order.Date,
                    Note = order.Note,
                };

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

        public static IEnumerable<OrderDTO> GetOrdersForCompanyUser(int userId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                return (from o in dc.Order
                        where o.CompanyUser.UserId == userId && o.DeleteDate == null
                        select new OrderDTO
                        {
                            OrderId = o.OrderId,
                            CompanyUserId = o.CompanyUserId,
                            CompanyId = o.CompanyUser.CompanyId,
                            CompanyName = o.CompanyUser.Company.Name,
                            Comment = o.Comment,
                            Date = o.Date,
                            MealId = o.MealId,
                            MealTitle = o.Meal.Title,
                            Note = o.Note,
                            Price = (int)o.Meal.Price
                        }).ToList();
            }
        }

        public static IEnumerable<OrderDTO> GetOrdersForKettering(int ketteringId)
        {
            var currentDate = DateTime.Now.AddDays(-1);

            using (var dc = new CraftedFoodEntities())
            {
                return (from o in dc.Order
                        where o.Meal.Menu.KetteringId == ketteringId && o.Date >= currentDate && o.DeleteDate == null
                        select new OrderDTO
                        {
                            OrderId = o.OrderId,
                            CompanyUserId = o.CompanyUserId,
                            CompanyId = o.CompanyUser.CompanyId,
                            Comment = o.Comment,
                            Date = o.Date,
                            MealId = o.MealId,
                            MealTitle = o.Meal.Title,
                            Note = o.Note,
                            Price = (int)o.Meal.Price
                        }).ToList();
            }
        }

        public static IEnumerable<OrderDTO> GetOrdersForCompany(int companyId)
        {
            var currentDate = DateTime.Today;

            using (var dc = new CraftedFoodEntities())
            {
                return (from o in dc.Order
                        where o.CompanyUser.CompanyId == companyId && o.Date == currentDate && o.DeleteDate == null
                        select new OrderDTO
                        {
                            OrderId = o.OrderId,
                            CompanyUserId = o.CompanyUserId,
                            CompanyId = o.CompanyUser.CompanyId,
                            Comment = o.Comment,
                            Date = o.Date,
                            MealId = o.MealId,
                            MealTitle = o.Meal.Title,
                            Note = o.Note,
                            Price = (int)o.Meal.Price
                        }).ToList();
            }
        }
    }
}
