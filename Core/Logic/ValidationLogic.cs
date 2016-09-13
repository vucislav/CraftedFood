using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Logic
{
    public class ValidationLogic
    {
        private static string validateName(string name)
        {
            if (name == null || name == "")
            {
                return "You cannot leave this field blank";
            }
            return "";
        }

        private static string validateUsername(string username)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            if (username == null || username == "")
            {
                return "You cannot leave this field blank";
            }
            if (username.Length < 8)
            {
                return "Username must be at least 8 charcaters long";
            }
            if (!regexItem.IsMatch(username)) {
                return "Username can only contain letters and numbers";
            }
            using (var dc = new CraftedFoodEntities())
            {
                var user = (from u in dc.User
                            where u.Username == username
                            select u).Any();

                if (user) return "Username already exists";
            }
            return "";
        }

        private static string validateMail (string mail)
        {
            try { 
                if (!Regex.IsMatch(mail,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                {
                    return "E-mail is not in the valid form";
                }
            }
            catch (RegexMatchTimeoutException)
            {
                
            }

            using (var dc = new CraftedFoodEntities())
            {
                var user = (from u in dc.User
                            where u.Email == mail
                            select u).Any();

                if (user) return "E-mail is already taken";
            }
            return "";
        }

        private static string validatePassword(string password)
        {
            if (password.Length < 8)
            {
                return "Password must be at least 8 charcaters long";
            }
            return "";
        }

    }
}
