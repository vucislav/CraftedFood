using Core.DTOs;
using Core.Enumerations;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                            where (u.Username == login || u.Email == login) && u.Password == password && u.DeleteDate == null
                            select u).ToList();

                if (user.Any()) return user.First().UserId;
                return 0;
            }
        }

        public static void ResetPasswordMail(string login)
        {
            string recipient = null;
            Guid guid = new Guid();
            using (var dc = new CraftedFoodEntities())
            {
                var user = (from u in dc.User
                            where (u.Username == login || u.Email == login) && u.DeleteDate == null
                            select u).ToList();
                if (user.Any())
                {
                    recipient = user.First().Email;
                    guid = Guid.NewGuid();
                    user.First().PasswordResetGuid = guid;
                    dc.SaveChanges();

                    // URADITI: ZASTO MI NE DOZVOLJAVA DA URADIM OVO???? (inner exception: transaction nesto, thread nesto)
                    /*try
                    {
                        dc.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }*/
                }
            }

            if (recipient != null)
            {
                string email = "mazomon@gmail.com";
                string password = "018521800v+";

                var loginInfo = new NetworkCredential(email, password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                msg.From = new MailAddress("craftedfood@level.com", "CraftedFood by LeVeL");
                msg.To.Add(new MailAddress(recipient));
                msg.Subject = "Password Recovery";
                msg.IsBodyHtml = true;
                msg.Body = "<h2>Password Recovery</h2> <div>Follow <a href='http://localhost:62094/Home/PasswordReset?token=" + guid + "'>this</a> link in order to recover your password.</div>";

                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
        }

        public static void ResetPassword(Guid guid, string newPassword)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var user = from u in dc.User
                           where u.PasswordResetGuid == guid && u.DeleteDate == null
                           select u;
                if (user.Any())
                {
                    user.First().PasswordResetGuid = null;
                    user.First().Password = newPassword;

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
        }

        public static void CreateUser(UserDTO user)
        {
            User u = new User()
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password
            };

            using (var dc = new CraftedFoodEntities())
            {
                dc.User.Add(u);

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

        public static IEnumerable<CompanyDTO> GetCompaniesForUser(int UserId)
        {
            List<CompanyDTO> companies = new List<CompanyDTO>();
            using (var dc = new CraftedFoodEntities())
            {
                var user = (from u in dc.User
                            where u.UserId == UserId && u.DeleteDate == null
                            select u).FirstOrDefault();

                if (user != null)
                {
                    foreach (var ket in user.KetteringUser.Where(x => x.Kettering.DeleteDate == null))
                    {
                        companies.Add(new CompanyDTO
                        {
                            CompanyId = ket.Kettering.KetteringId,
                            Name = ket.Kettering.Name,
                            Description = ket.Kettering.Description,
                            Address = ket.Kettering.Address,
                            Phone = ket.Kettering.Phone,
                            IsKettering = true
                        });
                    }
                    foreach (var com in user.CompanyUser.Where(x => x.Company.DeleteDate == null))
                    {
                        companies.Add(new CompanyDTO
                        {
                            CompanyId = com.Company.CompanyId,
                            Name = com.Company.Name,
                            Description = com.Company.Description,
                            Address = com.Company.Address,
                            Phone = com.Company.Phone,
                            IsKettering = false
                        });
                    }
                }
            }
            return companies;
        }

        public static UserDTO GetUserById(int userId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                return (from u in dc.User
                        where u.UserId == userId && u.DeleteDate == null
                        select new UserDTO
                        {
                            FirstName = u.FirstName,
                            MiddleName = u.MiddleName,
                            LastName = u.LastName,
                            Email = u.Email,
                            Username = u.Username,
                            Phone = u.Phone
                        }).FirstOrDefault();
            }
        }

        public static void EditProfile(UserDTO newUser)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var user = (from u in dc.User
                         where u.UserId == newUser.UserId
                         select u).FirstOrDefault();

                if (user != null)
                {
                    user.FirstName = newUser.FirstName;
                    user.MiddleName = newUser.MiddleName;
                    user.LastName = newUser.LastName;
                    user.Email = newUser.Email;
                    user.Username = newUser.Username;
                    user.Phone = newUser.Phone;
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

        public static void ChangePassword(int userId, string oldPassword, string newPassword)
        {
            using (var dc = new CraftedFoodEntities())
            {
                var user = (from u in dc.User
                            where u.UserId == userId && u.Password == oldPassword
                            select u).FirstOrDefault();
                if (user != null)
                {
                    user.Password = newPassword;
                }

                try
                {
                    dc.SaveChanges();
                }
                catch(Exception e)
                {
                    throw e;
                }       
            }
        }

        public static int? GetIdByUsername(string username)
        {
            using (var dc = new CraftedFoodEntities())
            {
                return (from u in dc.User
                        where u.Username == username
                        select u.UserId).FirstOrDefault();
            }
        }

        public static int GetCompanyUserId(int userId, int companyId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                return (from u in dc.CompanyUser
                        where u.UserId == userId && u.CompanyId == companyId
                        select u.CompanyUserId).ToList().LastOrDefault();
            }
        }

        public static int GetKetteringUserId(int userId, int ketteringId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                return (from u in dc.KetteringUser
                        where u.UserId == userId && u.KetteringId == ketteringId
                        select u.KetteringUserId).ToList().LastOrDefault();
            }
        }

        public static int GetCompanyId(int userId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                return (from u in dc.CompanyUser
                        where u.UserId == userId
                        select u.CompanyId).FirstOrDefault();
            }
        }

        public static bool IsAdmin(int userId, int companyId)
        {
            using (var dc = new CraftedFoodEntities())
            {
                return (from u in dc.CompanyUser
                        where u.UserId == userId && u.CompanyId == companyId
                        select u.RoleId).FirstOrDefault() == (int)RoleEnum.Admin;
            }
        }
    }
}
