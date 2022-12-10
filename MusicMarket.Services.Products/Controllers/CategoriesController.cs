using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicMarket.Services.Products.DbStuff.DbModels;
using MusicMarket.Services.Products.Repositories.IRepositories;

namespace MusicMarket.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll() 
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}
