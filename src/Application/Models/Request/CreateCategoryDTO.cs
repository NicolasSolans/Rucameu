using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "El nombre de la categoria es obligatorio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripción de la categoria es obligatorio.")]
        public string Description { get; set; }
    }
}
