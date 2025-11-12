using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripción del producto es obligatorio")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "El precio tiene que ser mayor que 0")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El stock inicial tiene que ser de al menos 1.")]
        public int Stock { get; set; } = 1;
        public string ImgUrl { get; set; }

        [Required(ErrorMessage = "La categoria es obligatoria.")]
        public int CategoryId { get; set; }
        //public bool Enable { get; set; } = true;
    }
}
