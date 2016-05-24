using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace stazkainos.Models
{
    public class CompareModel
    {

        [Required(ErrorMessage = "Proszę podać okres inwestycji")]
        [DisplayName("Data początkowa")]
        public string Range { get; set; }

        [Required(ErrorMessage = "Proszę podać ilość środków")]
        [DisplayName("Ilość środków")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Money { get; set; }

        [Required(ErrorMessage = "Proszę podać oprocentowanie")]
        [Range(0.01, 100.00,
            ErrorMessage = "Oprocentowanie musi zawierać się między 0.01 i 100.00")]
        public float Percent { get; set; }
    }
}