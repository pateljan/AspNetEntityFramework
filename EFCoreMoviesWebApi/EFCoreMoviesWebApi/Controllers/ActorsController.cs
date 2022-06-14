using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMoviesWebApi.DTOs;
using EFCoreMoviesWebApi.Entities;
using EFCoreMoviesWebApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ActorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get(int page = 1, int recordsToTake = 2)
        {
            //Readonly queries with no tracking boosts read operation
            return await _context.Actors.AsNoTracking()
                .OrderBy(g => g.Name)
                .ProjectTo<ActorDTO>(_mapper.ConfigurationProvider)
                //use above automapper instead of manual select
                //.Select(a => new ActorDTO { Id = a.Id, Name = a.Name, DateOfBirth = a.DataOfBirth })
                .Paginate(page, recordsToTake)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActorCreationDTO actorCreationDTO)
        {
            var actor = _mapper.Map<Actor>(actorCreationDTO);
            _context.Add(actor);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(ActorCreationDTO actorCreationDTO, int id)
        {
            var actorDb = await _context.Actors.FirstOrDefaultAsync(p => p.Id == id);
            if (actorDb is null)
            {
                return NotFound();
            }

            actorDb = _mapper.Map(actorCreationDTO, actorDb);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("disconnected/{id:int}")]
        public async Task<ActionResult> DicsconnectedUpdate(ActorCreationDTO actorCreationDTO, int id)
        {
            var existsActor = await _context.Actors.AnyAsync(p => p.Id == id);
            if (!existsActor)
            {
                return NotFound();
            }

            var actor = _mapper.Map<Actor>(actorCreationDTO);
            actor.Id = id;
            _context.Update(actor);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
