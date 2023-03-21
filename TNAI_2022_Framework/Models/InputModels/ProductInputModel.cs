using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace TNAI_2022_Framework.Models.InputModels
{
    public class ProductInputModel
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
