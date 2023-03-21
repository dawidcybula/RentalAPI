using AutoMapper;

using Swashbuckle.Swagger.Annotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using TNAI.Model;
using TNAI.Model.Entities;
using TNAI.Repository.Abstract;
using TNAI.Repository.Concrete;
using TNAI_2022_Framework.Models.InputModels;
using TNAI_2022_Framework.Models.OutputModels;

namespace TNAI_2022_Framework.Controllers
{
    public class RentalController : ApiController
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        
        public RentalController(IRentalRepository rentalRepository,IProductRepository productRepository , IMapper mapper)
        {
            AppDbContext context = AppDbContext.Create();
            _rentalRepository = new RentalRepository(context);
            _productRepository = new ProductRepository(context);
            _mapper = mapper;
        }

        /// <summary>
        /// Pobierz RENTAL
        /// </summary>
        /// <param name="id">Identyfikator kategorii</param>
        /// <returns></returns>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Description = "Pobrano wynajmowany sprzęt")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Nie znaleziono skrzętu")]

        
        public async Task<IHttpActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid id");

            var rental = await _rentalRepository.GetRentalAsync(id);
            if (rental == null)
                return NotFound();

            var result = _mapper.Map<RentalOutputModel>(rental);

            return Ok(result);
        }
        public async Task<IHttpActionResult> GetAll()
        {
            var rentals = await _rentalRepository.GetAllRentalsAsync();
            if (rentals == null)
                return NotFound();

            var result = _mapper.Map<List<RentalOutputModel>>(rentals);

            return Ok(result);
        }
        [Route("api/RentalsNotReturned")]
        public async Task<IHttpActionResult> GetAllNotReturned()
        {
            var rentals = await _rentalRepository.GetAllNotReturnedAsync();
            if (rentals == null)
                return NotFound();

            var result = _mapper.Map<List<RentalOutputModel>>(rentals);

            return Ok(result);
        }


        public async Task<IHttpActionResult> Put([FromUri] int id)//oddanie przedmiotow
        {
    
            var rental = await _rentalRepository.GetRentalAsync(id);
            if (rental == null)
                return NotFound();

            rental.ReturnDate = DateTime.Now;
            

            var result = await _rentalRepository.SaveRentalAsync(rental);
            if (!result)
                return InternalServerError();

            var rentaloutput = _mapper.Map<RentalOutputModel>(rental);

            return Ok(rentaloutput);
        }
        public async Task<IHttpActionResult> Delete([FromUri] int id)//usuwanie
        {
            if (id <= 0)
                return BadRequest();

            var result = await _rentalRepository.DeleteRentalAsync(id);

            return Ok(result);
        }
        public async Task<IHttpActionResult> Post(RentalInputModel inputModel) //post na kilka przedmiotow
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            
            var rental = new Rental()
            {
                RenterName = inputModel.RenterName,
                RentingDate = DateTime.Now,
            };

            foreach (var id in inputModel.ProductIds)
            {
                Product product = await _productRepository.GetProductAsync(id);
                if (product == null) return BadRequest($"No product with id {id}");
                if (product.Rentals.Any(x => x.ReturnDate == null)) return BadRequest($"Product {id} already rented");
                rental.Products.Add(product);
            }

            var saverental = await _rentalRepository.SaveRentalAsync(rental);
            if (!saverental)
                return InternalServerError();

            var rentaloutputmodel = _mapper.Map<RentalOutputModel>(rental);
            return Ok(rentaloutputmodel);
        }

    }
}