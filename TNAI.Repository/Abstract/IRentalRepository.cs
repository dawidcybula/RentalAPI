using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TNAI.Model.Entities;

namespace TNAI.Repository.Abstract
{
    public interface IRentalRepository
    {
        Task<Rental> GetRentalAsync(int id);
        Task<List<Rental>> GetAllRentalsAsync();
        Task<bool> SaveRentalAsync(Rental rental);  
        Task<bool> DeleteRentalAsync(int id);
        Task<List<Rental>> GetAllNotReturnedAsync();


    }
}
