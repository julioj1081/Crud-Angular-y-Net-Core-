using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TarjetasBacked.Models
{
    public class TarjetaCredito
    {
        public int Id { get; set; }
        [Required]
        //[Display(Name = "Titular")]
        public string Titular { get; set; }
        [Required]
        //[Display(Name = "NumeroTarjeta")]
        public string NumeroTarjeta { get; set; }
        [Required]
        //[Display(Name = "FechaExpiracion")]
        public string FechaExpiracion{ get; set; }
        [Required]
        //[Display(Name = "CVV")]
        public string CVV{ get; set; }
    }
}
