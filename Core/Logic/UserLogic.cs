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
                            where (u.Username == login || u.Email == login) && u.Password == password
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
                            where (u.Username == login || u.Email == login)
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
                           where u.PasswordResetGuid == guid
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
    }
}
