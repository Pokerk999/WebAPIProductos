﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIProductos.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Price { get; set; }

        public string? Photo { get; set; }
    }
}
