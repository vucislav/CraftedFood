using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class KetteringUserModel
    {
        public int UserId { get; set; }
        public int KetteringId { get; set; }

        public KetteringUserModel (int id)
        {
            KetteringId = id;
        }
    }
}