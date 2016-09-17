using Core.DTOs;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public static class CompanyLogic
    {
        public static void Create(CompanyDTO company, int userId)
        {
            Company com = new Company
            {
                CompanyId = company.CompanyId,
                Name = company.Name,
                Description = company.Description,
                Address = company.Address,
                Phone = company.Phone
            };
            using (var dc = new CraftedFoodEntities())
            {
                dc.Company.Add(com);
                try
                {
                    dc.SaveChanges();

                    CompanyUserLogic.Create(new CompanyUserDTO
                    {
                        CompanyId = com.CompanyId,
                        UserId = userId
                    }, Enumerations.RoleEnum.Admin, dc);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static void Edit(CompanyDTO company)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var com = GetCompanyById(company.CompanyId, dc);
                if (com != null)
                {
                    com.Name = company.Name;
                    com.Description = company.Description;
                    com.Address = company.Address;
                    com.Phone = company.Phone;
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

        public static void Delete(int companyId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var com = GetCompanyById(companyId, dc);
                if (com != null)
                {
                    com.DeleteDate = DateTime.Now;
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

        public static CompanyDTO GetCompanyById(int companyId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var com = GetCompanyById(companyId, dc);
                var company = new CompanyDTO
                {
                    CompanyId = com.CompanyId,
                    Name = com.Name,
                    Description = com.Description,
                    Address = com.Address,
                    Phone = com.Phone,
                    Members = com.CompanyUser.ToList().Select(x => new UserDTO
                    {
                        FirstName = x.User.FirstName,
                        MiddleName = x.User.MiddleName,
                        LastName = x.User.LastName,
                        Username = x.User.Username,
                        Email = x.User.Email,
                        Phone = x.User.Phone
                    }).OrderBy(x => x.FirstName)
                };
                return company;
            }
        }

        private static Company GetCompanyById(int companyId, CraftedFoodEntities dc)
        {
            return (from c in dc.Company.Include("CompanyUser.User")
                    where c.CompanyId == companyId && c.DeleteDate == null
                    select c).FirstOrDefault();
        }
    }
}
