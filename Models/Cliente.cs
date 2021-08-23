using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Valkimia.Models
{
    public class Cliente
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }
        [Required]
        [MaxLength(50)]
        public string Domicilio { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        public int CiudadId { get; set; }
        [Required]
        public bool Habilitado { get; set; }
        public Ciudad Ciudad { get; set; }
    }
}
