using Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class KetteringModel
    {
        public int KetteringId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public IEnumerable<UserModel> Members { get; set; }

        public KetteringModel()
        {

        }

        public KetteringModel(int ketteringId)
        {
            var ket = KetteringLogic.GetKetteringById(ketteringId);
            KetteringId = ket.KetteringId;
            Name = ket.Name;
            Description = ket.Description;
            Address = ket.Address;
            Phone = ket.Phone;
            Members = ket.Members.Select(x => new UserModel
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
    }
}