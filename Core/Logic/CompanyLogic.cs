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
                    Members = com.CompanyUser.Where(x => x.DeleteDate == null && x.User.DeleteDate == null)
                    .Select(x => new UserDTO
                    {
                        UserId = x.User.UserId,
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

        public static IEnumerable<CompanyDTO> GetCompaniesForUser(int companyUserId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var userId = (from c in dc.CompanyUser
                             where c.CompanyUserId == companyUserId
                             select c.UserId).FirstOrDefault();
                var companies = (from c in dc.CompanyUser
                        where c.UserId == userId
                            select new CompanyDTO
                            {
                                CompanyId = c.CompanyId,
                                Name = c.Company.Name,
                                Description = c.Company.Description,
                                Address = c.Company.Address,
                                Phone = c.Company.Phone
                            }).ToList();
                return companies;
            }
        }

        public static IEnumerable<CompanyDTO> FilterCompanies(string term)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var result1 = (from c in dc.Company
                              where c.Name.Contains(term)
                              select c);
                var result2 = (from k in dc.Kettering
                               where k.Name.Contains(term)
                               select k);

                var result = result1.Select(x => new CompanyDTO
                {
                    CompanyId = x.CompanyId,
                    Name = x.Name,
                    Description = x.Description,
                    Address = x.Address,
                    Phone = x.Phone,
                    IsKettering = false
                }).ToList();
                result.AddRange(result2.Select(x => new CompanyDTO
                {
                    CompanyId = x.KetteringId,
                    Name = x.Name,
                    Address = x.Address,
                    Description = x.Description,
                    Phone = x.Phone,
                    IsKettering = true
                }));
                return result;
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
