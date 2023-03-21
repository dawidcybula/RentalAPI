using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TNAI.Model.Entities;

namespace TNAI_2022_Framework.Models.OutputModels
{
    public class RentalOutputModel
    {
        public int Id { get; set; }
        public string RenterName { get; set; }
        public DateTime? RentingDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public virtual ICollection<ProductOutputModel> Products { get; set; }
    }
}