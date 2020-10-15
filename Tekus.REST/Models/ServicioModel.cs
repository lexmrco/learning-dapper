using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tekus.REST.Models
{
    public class ServicioModel
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public decimal ValorHora { get; set; }
        [Required]
        public int ClienteID { get; set; }
        [Required]
        public int PaisID { get; set; }
    }
}
