using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TNAI_2022_Framework.Models.InputModels
{
    public class RentalInputModel
    {
       
        [Required]
        public string RenterName { get; set; }

        [Required]
        public ICollection<int> ProductIds { get; set; }
    }
}