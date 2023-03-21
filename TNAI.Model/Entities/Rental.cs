using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNAI.Model.Entities
{
    public class Rental
    {
        public Rental()
        {
            Products = new List<Product>();
        }

        public int Id { get; set; }
        public string RenterName { get; set; }
        public DateTime? RentingDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
