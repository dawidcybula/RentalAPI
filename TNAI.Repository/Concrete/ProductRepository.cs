﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI.Model;
using TNAI.Model.Entities;
using TNAI.Repository.Abstract;

namespace TNAI.Repository.Concrete
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context=null) : base(context) { }

        public async Task<Product> GetProductAsync(int id)
        {
            return await Context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await Context.Products.ToListAsync();
        }
        public async Task<List<Product>> GetAllNotRented()
        {
            return await Context.Products.Where(x => x.Rentals.All(y => y.ReturnDate != null)).ToListAsync();
        }
        public async Task<List<Product>> GetAllRented()
        {
            return await Context.Products.Where(x => x.Rentals.Any(y => y.ReturnDate == null)).ToListAsync();
        }



        public async Task<bool> SaveProductAsync(Product Product)
        {
            if (Product == null)
                return false;
            
            Context.Entry(Product).State = Product.Id == default(int) ? EntityState.Added : EntityState.Modified;
            

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var Product = await GetProductAsync(id);
            if (Product == null)
                return true;

            Context.Products.Remove(Product);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

       
    }
}
