using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProductos.DTOs;
using WebAPIProductos.Entidades;

namespace WebAPIProductos.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductoCreacionDTO, Producto>();
            CreateMap<Producto, ProductoDTO>();

        }
    }
}
