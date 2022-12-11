using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MusicMarket.Services.Products.DbStuff.DbModels;
using MusicMarket.Services.Products.Repositories.IRepositories;
using MusicMarket.Services.Products.ViewModels;
using System.Collections.Generic;

namespace MusicMarket.Services.Products.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoriesController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryRepository repository, 
            IMapper mapper)
        {
            _categoryRepository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryViewModel>> GetAll() 
        {
            return _mapper
                .Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.GetAllAsync());
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(long categoryId) 
        {
            try
            {
                await _categoryRepository.DeleteByIdAsync(categoryId);
            }
            catch(Exception) 
            {
                return new NotFoundResult();
            }

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateNewCategory(string categoryName) 
        {
            var category = new Category()
            {
                Name = categoryName
            };


            try
            {
                await _categoryRepository.SaveAsync(category);
            }
            catch (Exception) 
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<CategoryViewModel>(category));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid) 
            {
                var dbModel = _mapper.Map<Category>(categoryViewModel);
                try
                {
                    await _categoryRepository.SaveAsync(dbModel);
                }
                catch (Exception)
                {
                    return BadRequest();
                }

                return Ok(_mapper.Map<CategoryViewModel>(dbModel));
            }


            return BadRequest();
        }
    }
}
