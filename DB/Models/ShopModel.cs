﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeProjectOnPharmacies.DB.Models
{
    public class ShopModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Addres { get; set; }
        public string Phone { get; set; }
    }
}
