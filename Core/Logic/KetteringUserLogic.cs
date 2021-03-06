﻿using Core.DTOs;
using Core.Enumerations;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public static class KetteringUserLogic
    {
        public static void Create(KetteringUserDTO ketteringUser, RoleEnum role)
        {
            Create(ketteringUser, role, null);
        }
        
        public static void Create(CraftedFoodEntities context, KetteringUserDTO ketteringUser, RoleEnum role)
        {
            Create(ketteringUser, role, context);
        }

        private static void Create(KetteringUserDTO ketteringUser, RoleEnum role, CraftedFoodEntities context)
        {
            KetteringUser ketUser = new KetteringUser
            {
                UserId = ketteringUser.UserId,
                KetteringId = ketteringUser.KetteringId,
                RoleId = (int)role 
            };

            using (var dc = context ?? new CraftedFoodEntities())
            {
                dc.KetteringUser.Add(ketUser);
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

        public static void Delete(int ketteringUserId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var ketUser = GetKetteringUserById(ketteringUserId, dc);
                if (ketUser != null)
                {
                    ketUser.DeleteDate = DateTime.Now;
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

        private static KetteringUser GetKetteringUserById(int ketteringUserId, CraftedFoodEntities dc)
        {
            return (from c in dc.KetteringUser
                    where c.KetteringUserId == ketteringUserId && c.DeleteDate == null
                    select c).FirstOrDefault();
        }
    }
}
