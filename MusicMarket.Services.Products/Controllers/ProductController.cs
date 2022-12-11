using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicMarket.Services.Products.DbStuff.DbModels;
using MusicMarket.Services.Products.Repositories.IRepositories;
using MusicMarket.Services.Products.ViewModels;
using System.Collections.Generic;

namespace MusicMarket.Services.Products.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRepository;
        ICategoryRepository _categoryRepository;
        IMapper _mapper;
        public ProductController(IProductRepository productRepository,
            IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAll() 
        {
            return _mapper
                .Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAllAsync()); 
        }

        [HttpGet]
        public async Task<ProductViewModel> GetById(long id)
        {
            return _mapper
                .Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));
        }



        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)  //trying to save category entity that already exists
        {
            if (ModelState.IsValid) 
            {
                var dbModel = _mapper.Map<Product>(productViewModel);

                var type = dbModel.Category.GetType().Name;

                if (!await _categoryRepository.IsExistAsync(_mapper.Map<Category>(dbModel.Category)))
                {
                    return BadRequest();
                }

                try
                {

                    await _productRepository.SaveAsync(dbModel);
                }
                catch (Exception) 
                {
                    return BadRequest();
                }

                return Ok(_mapper.Map<ProductViewModel>(dbModel));

            }

            return BadRequest();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(long productId) 
        {
            try
            {
                await _productRepository.DeleteByIdAsync(productId);
            }
            catch (Exception) 
            {
                return BadRequest();
            }

            return Ok(); 
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(ProductViewModel productViewModel) 
        {
            if (ModelState.IsValid) 
            {
                var dbModel = _mapper.Map<Product>(productViewModel);
                var type = dbModel.Category.GetType().Name;

                if (!await _categoryRepository.IsExistAsync(_mapper.Map<Category>(dbModel.Category))) 
                {
                    return BadRequest();
                }

                try
                {
                    await _productRepository.SaveAsync(dbModel);
                }
                catch (Exception) 
                {
                    return BadRequest();
                }

                return Ok(_mapper.Map<ProductViewModel>(dbModel));
            }

            return BadRequest();

        }
        
    }
}