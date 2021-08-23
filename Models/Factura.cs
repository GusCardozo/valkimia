using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Valkimia.Models
{
    public class Factura
    {
        [Required]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        [MaxLength(200)]
        public string Detalle { get; set; }
        [Required]
        public decimal Importe { get; set; }
        public Cliente Cliente { get; set; }
    }
}
