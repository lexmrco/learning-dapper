using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tekus.REST.Models
{
    public class ClienteModel
    {
        [Required]
        public string NIT { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string CorreoElectronico { get; set; }
    }
}
