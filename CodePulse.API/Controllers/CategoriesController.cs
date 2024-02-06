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
        public async Task<IActionResult> CreateCategories([FromBody] CreateCategoiresRequestDTO request)
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


        // GET: https://localhost:7207/api/Categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoriesRepository.GetAllAsync();

            // Domain Model -> DTO's
            var respons = new List<CategoryDTO>();
            foreach (var item in categories)
            {
                respons.Add(new CategoryDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    urlHandle = item.urlHandle,
                });
            }

            return Ok(respons);
        }


        // GET: https://localhost:7207/api/Categories/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {

            var existingCategory = await categoriesRepository.GetById(id);

            if (existingCategory is null)
            {
                return NotFound();
            }

            var response = new CategoryDTO { Id = existingCategory.Id, Name = existingCategory.Name, urlHandle = existingCategory.urlHandle };
            return Ok(response);
        }
    }
}
