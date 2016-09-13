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


        public static IEnumerable<CompanyModel> GetCompaniesForUser(int UserId)
        {
            return UserLogic.GetCompaniesForUser(UserId).Select(x => new CompanyModel
            {
                CompanyId = x.CompanyId,
                Name = x.Name,
                Address = x.Address,
                Description = x.Description,
                Phone = x.Phone
            });
        }
    }
}