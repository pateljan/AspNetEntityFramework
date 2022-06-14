using EFCoreMoviesWebApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCoreMoviesWebApi.Utilities;
using AutoMapper;
using EFCoreMoviesWebApi.DTOs;

namespace EFCoreMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenresController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> Get(int page =1 , int recordsToTake = 2)
        {
            _context.Logs.Add(new Log { Messsage = "Executing Get from GenresController" });
            await _context.SaveChangesAsync();
            //Readonly queries with no tracking boosts read operation
            return await _context.Genres.AsNoTracking()
                
                //.Where(p=> !p.IsDeleted) // using query filter in Genre COnfig
                //.OrderBy(g => g.Name)
                .OrderByDescending( g=> EF.Property<DateTime>(g, "CreatedDate")) // to access shallow field
                .Paginate(page, recordsToTake)
                .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            if (genre == null)
                return NotFound();

            var createdDate = _context.Entry(genre).Property<DateTime>("CreatedDate").CurrentValue;
            return Ok(new
            {
                id = genre.Id,
                name = genre.Name,
                createddate = createdDate
            });
        }

        [HttpGet("first")]
        public async Task<ActionResult<Genre>> GetFirst()
        {
            //Readonly queries with no tracking boosts read operation
            var genre =  await _context.Genres.FirstOrDefaultAsync(g => g.Name.Contains("z"));
            if (genre is null)
            {
                return NotFound();
            }
            return genre;
        }

        [HttpGet("filter")]
        public async Task<IEnumerable<Genre>> Filter(string name)
        {
            return await _context.Genres.Where(g => g.Name.Contains(name)).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(GenreCreationDTO genreCreationDTO)
        {
            var genreExists = await _context.Genres.AnyAsync(p => p.Name == genreCreationDTO.Name);
            if (genreExists)
            {
                return BadRequest($"The genre with name {genreCreationDTO.Name} already exist..");
            }
            var genre = _mapper.Map<Genre>(genreCreationDTO);
            _context.Add(genre); // marking genre as added status
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPost("severalinsert")]
        public async Task<ActionResult> SeveralPost(GenreCreationDTO[] genresDTO)
        {
            var genres = _mapper.Map<Genre[]>(genresDTO);
            //foreach (var item in genres)
            //{
            //    _context.Add(item);
            //}

            _context.AddRange(genres);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("add2")]
        public async Task<ActionResult> Add2(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(p => p.Id == id);

            if (genre is null)
            {
                return NotFound();
            }

            genre.Name += " 2";
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete( int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(p => p.Id == id);
            if (genre is null)
            {
                return NotFound();
            }
            _context.Remove(genre);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("softdelete/{id:int}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(p => p.Id == id);
            if (genre is null)
            {
                return NotFound();
            }
            genre.IsDeleted = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("restordelete/{id:int}")]
        public async Task<ActionResult> RestoreDelete(int id)
        {

            var genre = await _context.Genres.IgnoreQueryFilters().FirstOrDefaultAsync(p => p.Id == id);
            if (genre is null)
            {
                return NotFound();
            }
            genre.IsDeleted = false;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
