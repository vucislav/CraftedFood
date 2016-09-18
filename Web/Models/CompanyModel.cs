using Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CompanyModel
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsKettering { get; set; }
        public IEnumerable<UserModel> Members { get; set; }

        public CompanyModel()
        {

        }

        public CompanyModel(int companyId)
        {
            var com = CompanyLogic.GetCompanyById(companyId);
            CompanyId = com.CompanyId;
            Name = com.Name;
            Description = com.Description;
            Address = com.Address;
            Phone = com.Phone;
            IsKettering = com.IsKettering;
            Members = com.Members.Select(x => new UserModel
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                Email = x.Email,
                Username = x.Username,
                Phone = x.Phone
            });
        }

        public static IEnumerable<CompanyModel> GetCompaniesForUser(int UserId)
        {
            return UserLogic.GetCompaniesForUser(UserId).Select(x => new CompanyModel
            {
                CompanyId = x.CompanyId,
                Name = x.Name,
                Address = x.Address,
                Description = x.Description,
                Phone = x.Phone,
                IsKettering = x.IsKettering
            });
        }

        public static IEnumerable<CompanyModel> GetCompaniesSearch(string term)
        {
            var companies = CompanyLogic.FilterCompanies(term);
            return companies.Select(x => new CompanyModel
            {
                CompanyId = x.CompanyId,
                Name = x.Name,
                Address = x.Address,
                Phone = x.Phone,
                Description = x.Phone,
                IsKettering = x.IsKettering
            });
        }
    }
}