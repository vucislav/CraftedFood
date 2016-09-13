using Core.DTOs;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public static class UserRoleLogic
    {
        public static void Create(UserRoleDTO userRole)
        {
            UserRole uR = new UserRole
            {
                UserRoleId = userRole.UserRoleId,
                UserId = userRole.UserId,
                RoleId = userRole.RoleId

            };
            using (var dc = new CraftedFoodEntities())
            {
                dc.UserRole.Add(uR);
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

        public static void Delete(int userRoleId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var userRole = GetUserRoleById(userRoleId, dc);
                if (userRole != null)
                {
                    userRole.DeleteDate = DateTime.Now;
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

        private static UserRole GetUserRoleById(int userRoleId, CraftedFoodEntities dc)
        {
            return (from c in dc.UserRole
                    where c.UserRoleId == userRoleId && c.DeleteDate == null
                    select c).FirstOrDefault();
        }
    }
}
