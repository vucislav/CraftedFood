﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int CompanyUserId { get; set; }
        public int MealId { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string Comment { get; set; }
    }
}