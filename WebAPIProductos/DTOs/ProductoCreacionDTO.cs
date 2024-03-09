using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebAPIProductos.Validaciones;

namespace WebAPIProductos.DTOs
{
    public class ProductoCreacionDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener más de {1} carácteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }

        [Required]
        public string? Model { get; set; }

        [Required]
        [MaxLength(120)]
        public string? Description { get; set; }


        [Required]
        public decimal? Price { get; set; }

        [Required]
        [DisplayName("upload Image")]
        public string? Photo { get; set; }
    }
}
