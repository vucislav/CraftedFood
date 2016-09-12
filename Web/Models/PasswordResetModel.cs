using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PasswordResetModel
    {
        public Guid Guid { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordConfirm { get; set; }

        public PasswordResetModel()
        {

        }

        public PasswordResetModel(Guid guid)
        {
            Guid = guid;
        }
    }
}