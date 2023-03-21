using AutoMapper;

using Swashbuckle.Swagger.Annotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using TNAI.Model.Entities;
using TNAI.Repository.Abstract;
using TNAI.Repository.Concrete;
using TNAI_2022_Framework.Models.InputModels;
using TNAI_2022_Framework.Models.OutputModels;

namespace TNAI_2022_Framework.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository,ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Pobierz wybraną kategorię
        /// </summary>
        /// <param name="id">Identyfikator produktu</param>
        /// <returns></returns>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Description = "Pobrano produkt")]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Nie znaleziono produktu")]
        
        public async Task<IHttpActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid id");

            var product = await _productRepository.GetProductAsync(id);
            if (product == null)
                return NotFound();

            var result = _mapper.Map<ProductOutputModel>(product);

            return Ok(result);
        }
        [Route("api/ProductsNotRented")]
        public async Task<IHttpActionResult> GetAllNotRented()
        {

            var product = await _productRepository.GetAllNotRented();
            if (product == null)
                return NotFound();

            var result = _mapper.Map<List<ProductOutputModel>>(product);

            return Ok(result);
        }
        [Route("api/ProductsRented")]
        public async Task<IHttpActionResult> GetAllRented()
        {

            var product = await _productRepository.GetAllRented();
            if (product == null)
                return NotFound();

            var result = _mapper.Map<List<ProductOutputModel>>(product);

            return Ok(result);
        }

        public async Task<IHttpActionResult> GetAll()
        {
            var products = await _productRepository.GetAllProductsAsync();
            if (products == null)
                return NotFound();

            var result = _mapper.Map<List<ProductOutputModel>>(products);

            return Ok(result);
        }


        public async Task<IHttpActionResult> Post(ProductInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest("");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            var category = await _categoryRepository.GetCategoryAsync(inputModel.CategoryId);
            if (category == null) return BadRequest($"No category with id {inputModel.CategoryId}");

            var product = new Product()
            {
                Name = inputModel.Name,
                CategoryId = inputModel.CategoryId,
                
            };

            var result = await _productRepository.SaveProductAsync(product);

            if (!result)
                return BadRequest($"Blad zapisu");

            var productOutputModel = _mapper.Map<ProductOutputModel>(product);

            return Ok(productOutputModel);
        }

        public async Task<IHttpActionResult> Put([FromUri] int id, [FromBody] ProductInputModel inputModel)
        {
            if (inputModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productRepository.GetProductAsync(id);
            if(product == null)
                return NotFound();

            
            var category = await _categoryRepository.GetCategoryAsync(inputModel.CategoryId);
            if (category == null) return BadRequest($"No category with id {inputModel.CategoryId}");

            product.Name = inputModel.Name;
            product.CategoryId = inputModel.CategoryId;

            var result = await _productRepository.SaveProductAsync(product);
            if (!result)
                return InternalServerError();

            var productOutputModel = _mapper.Map<ProductOutputModel>(product);

            return Ok(productOutputModel);
        }

        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            if (id <= 0)
                return BadRequest();

            var result = await _productRepository.DeleteProductAsync(id);

            return Ok(result);
        }
    }
}
