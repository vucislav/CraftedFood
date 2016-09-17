using Core.DTOs;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public static class KetteringLogic
    {
        public static void Create(KetteringDTO kettering, int userId)
        {
            Kettering ket = new Kettering
            {
                KetteringId = kettering.KetteringId,
                Name = kettering.Name,
                Description = kettering.Description,
                Phone = kettering.Phone,
                Address = kettering.Address
            };
            using (var dc = new CraftedFoodEntities())
            {
                dc.Kettering.Add(ket);
                try
                {
                    dc.SaveChanges();

                    KetteringUserLogic.Create(dc, new KetteringUserDTO
                    {
                        KetteringId = ket.KetteringId,
                        UserId = userId
                    }, Enumerations.RoleEnum.Admin);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static void Edit(KetteringDTO kettering)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var ket = GetKetteringById(kettering.KetteringId, dc);
                if (ket != null)
                {
                    ket.Name = kettering.Name;
                    ket.Description = kettering.Description;
                    ket.Address = kettering.Address;
                    ket.Phone = kettering.Phone;
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

        public static void Delete(int ketteringId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var ket = GetKetteringById(ketteringId, dc);
                if (ket != null)
                {
                    ket.DeleteDate = DateTime.Now;
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

        public static KetteringDTO GetKetteringById(int ketteringId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var ket = GetKetteringById(ketteringId, dc);
                return new KetteringDTO
                {
                    KetteringId = ket.KetteringId,
                    Name = ket.Name,
                    Description = ket.Description,
                    Address = ket.Address,
                    Phone = ket.Phone
                };
            }
        }

        private static Kettering GetKetteringById(int ketteringId, CraftedFoodEntities dc)
        {
            return (from c in dc.Kettering
                    where c.KetteringId == ketteringId && c.DeleteDate == null
                    select c).FirstOrDefault();
        }
		
		public static IEnumerable<MenuDTO> GetMenusForKettering(int ketteringId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                return (from m in dc.Menu
                        where m.KetteringId == ketteringId && m.DeleteDate == null
                        select new MenuDTO
                        {
                            MenuId = m.MenuId,
                            KetteringId = m.KetteringId,
                            Name = m.Name
                        }).ToList();
            }
        }
    }
}
