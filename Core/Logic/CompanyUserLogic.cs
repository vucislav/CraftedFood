using Core.DTOs;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public static class CompanyUserLogic
    {
        public static void Create(CompanyUserDTO companyUser)
        {
            CompanyUser comUser = new CompanyUser
            {
                CompanyUserId = companyUser.CompanyUserId,
                UserId = companyUser.UserId,
                CompanyId = companyUser.CompanyId
            };
            using (var dc = new CraftedFoodEntities())
            {
                dc.CompanyUser.Add(comUser);
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

        public static void Delete(int companyUserId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var comUser = GetCompanyUserById(companyUserId, dc);
                if (comUser != null)
                {
                    comUser.DeleteDate = DateTime.Now;
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

        private static CompanyUser GetCompanyUserById(int companyUserId, CraftedFoodEntities dc)
        {
            return (from c in dc.CompanyUser
                    where c.CompanyUserId == companyUserId && c.DeleteDate == null
                    select c).FirstOrDefault();
        }
    }
}
