using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI.Model;
using TNAI.Model.Entities;
using TNAI.Repository.Abstract;

namespace TNAI.Repository.Concrete
{
    public class RentalRepository : BaseRepository, IRentalRepository
    {
        public RentalRepository(AppDbContext context=null) : base(context) { }

        public async Task<Rental> GetRentalAsync(int id)
        {
            return await Context.Rentals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Rental>> GetAllRentalsAsync()
        {
            return await Context.Rentals.ToListAsync();
        }

        public async Task<List<Rental>> GetAllNotReturnedAsync()
        {
            return await Context.Rentals.Where(x=> x.ReturnDate == null).ToListAsync();
        }

        public async Task<bool> SaveRentalAsync(Rental Rental)
        {
            if (Rental == null)
                return false;

            Context.Entry(Rental).State = Rental.Id == default(int) ? EntityState.Added : EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteRentalAsync(int id)
        {
            var Rental = await GetRentalAsync(id);
            if (Rental == null)
                return true;

            Context.Rentals.Remove(Rental);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
