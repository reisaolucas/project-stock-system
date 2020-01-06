using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProStock.Domain;
using ProStock.Repository;

namespace ProStock.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProStockRepository _repository;
        public ProductController(IProStockRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllProductsAsync();
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha na comunicação com o banco de dados");
            }            
        }

        [HttpGet("{ProductId}")]
        public async Task<IActionResult> Get(int ProductId)
        {
            try
            {
                var results = await _repository.GetProductByIdAsync(ProductId);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha na comunicação com o banco de dados");
            }  
        }
        [HttpPost]
        public async Task<IActionResult> Post(Product model)
        {
            try
            {
                _repository.Add(model);
                
                if(await _repository.SaveChangesAsync()){
                    return Created($"/api/product/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha na comunicação com o banco de dados");
            } 

            return BadRequest(); 
        }
        [HttpPut]
        public async Task<IActionResult> Put(int ProductId, Product model)
        {
            try
            {
                var product = await _repository.GetProductByIdAsync(ProductId);

                if(product == null) return NotFound();

                _repository.Update(model);
                
                if(await _repository.SaveChangesAsync()){
                    return Created($"/api/product/{model.Id}", model);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha na comunicação com o banco de dados");
            } 

            return BadRequest(); 
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int ProductId)
        {
            try
            {
                var product = await _repository.GetProductByIdAsync(ProductId);
                
                if(product == null) return NotFound();

                _repository.Delete(product);
                
                if(await _repository.SaveChangesAsync()){
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha na comunicação com o banco de dados");
            } 

            return BadRequest(); 
        }

    }
}