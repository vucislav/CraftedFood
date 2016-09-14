using Core.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }

        public UserModel()
        {

        }

        public UserModel(int userId)
        {
            var user = UserLogic.GetUserById(userId);
            UserId = userId;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            Username = user.Username;
            Email = user.Email;
            Phone = user.Phone;
        }
    }
}