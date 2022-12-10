using Microsoft.AspNetCore.Mvc;
using MusicMarket.Services.Products.DbStuff.DbModels;
using MusicMarket.Services.Products.Repositories.IRepositories;

namespace MusicMarket.Services.Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll() 
        {
            return await _productRepository.GetAllAsync();
        }
        
    }
}