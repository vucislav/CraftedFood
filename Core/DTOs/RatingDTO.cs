using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class RatingDTO
    {
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public int MealId { get; set; }
        public int Mark { get; set; }
        public string Comment { get; set; }
    }
}
