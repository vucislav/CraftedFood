//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class KetteringUser
    {
        public int KetteringUserId { get; set; }
        public int UserId { get; set; }
        public int KetteringId { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
    
        public virtual Kettering Kettering { get; set; }
        public virtual User User { get; set; }
    }
}
