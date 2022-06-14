using AutoMapper;
using EFCoreMoviesWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PeopleController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await _context.Persons
                .Include(p => p.ReceivedMessages)
                .Include(p => p.SentMessages)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (person == null)
                return NotFound();
            return person;
        }
    }
}
