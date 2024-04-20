﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string PictureUrl { get; set; }

        public int Quantiy { get; set; }
    }
}
