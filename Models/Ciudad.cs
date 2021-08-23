using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Valkimia.Models
{
    public class Ciudad
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
    }
}
