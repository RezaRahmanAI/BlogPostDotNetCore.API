using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CodePulse.API.Models.DTO;
using CodePulse.API.Models.Domain;
using CodePulse.API.Data;
using CodePulse.API.Repository.Interface;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICategoriesRepository categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategories(CreateCategoiresRequestDTO request)
        {

            // Map DTO -> Model
            var category = new Category
            {
                Name = request.Name,
                urlHandle = request.urlHandle,
            };

           await categoriesRepository.CreateAsync(category);

            // Map Model -> DTO
            var response = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                urlHandle = category.urlHandle,
            };


            return Ok(response);
        }
    }
}
