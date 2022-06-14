using EFCoreMoviesWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController:ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GET()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("merch")]
        public async Task<ActionResult<IEnumerable<Merchandising>>> GetMerchandising()
        {
            return await _context.Set<Merchandising>().ToListAsync();
        }

        [HttpGet("rentablemovie")]
        public async Task<ActionResult<IEnumerable<RentableMovie>>> GetRentableMovies()
        {
            return await _context.Set<RentableMovie>().ToListAsync();
        }
    }
}
