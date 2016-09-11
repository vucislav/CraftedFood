using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logic
{
    public static class UserLogic
    {
        public static int AuthorizeUser(string login, string password)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var user = (from u in dc.User
                            where (u.Username == login || u.Email == login) && u.Password == password
                            select u).ToList();

                if (user.Any()) return user.First().UserId;
                return 0;
            }
        }
    }
}
