using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class MenuDTO
    {
        public int MenuId { get; set; }
        public int KetteringId { get; set; }
        public string Name { get; set; }
		public IEnumerable<MealDTO> Meals { get; set; }
    }
}
