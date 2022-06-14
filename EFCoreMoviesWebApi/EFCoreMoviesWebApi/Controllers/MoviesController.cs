using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMoviesWebApi.DTOs;
using EFCoreMoviesWebApi.Entities;
using EFCoreMoviesWebApi.Entities.Keyless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genres.OrderByDescending(g => g.Name).Where(g => !g.Name.Contains("m")))
                .Include(m => m.CinemaHalls)
                    .ThenInclude(ch => ch.Cinema)
                .Include(m => m.MoviesActors)
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie is null)
            {
                return NotFound();
            }
            var movieDTO = _mapper.Map<MovieDTO>(movie);
            return movieDTO;
        }

        [HttpGet("automapper/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetWithAutoMaooer(int id)
        {
            var movie = await _context.Movies
               .ProjectTo<MovieDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie is null)
            {
                return NotFound();
            }
            var movieDTO = _mapper.Map<MovieDTO>(movie);
            return movieDTO;
        }

        [HttpGet("selectloading/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetWithSelectLoading(int id)
        {
            var movieDTO = await _context.Movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                Genres = _mapper.Map<List<GenreDTO>>(m.Genres.OrderByDescending(g => g.Name))
            }).FirstOrDefaultAsync(m => m.Id == id);

            if (movieDTO is null)
            {
                return NotFound();
            }
            return movieDTO;
        }

        [HttpGet("explicitloading/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetExplicitLoading(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie is null)
            {
                return NotFound();
            }

            //await _context.Entry(movie).Collection(p => p.Genres).LoadAsync();
            var genresCount = await _context.Entry(movie).Collection(p => p.Genres).Query().CountAsync();


            var movieDTO = _mapper.Map<MovieDTO>(movie);

            //return movieDTO;

            return Ok(new
            {
                Id = movieDTO.Id,
                Title = movieDTO.Title,
                GenresCount = genresCount
            });
        }

        [HttpGet("lazyloadin/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetLazyLoading(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie is null)
            {
                return NotFound();
            }

            var movieDTO = _mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(x => x.Id).ToList();

            return movieDTO;
        }

        [HttpGet("groupdedByCinema")]
        public async Task<ActionResult> GetGroupByCinema()
        {
            var groupedMovies = await _context.Movies.GroupBy(m => m.InCinemas).Select(g => new
            {
                InCinemas = g.Key,
                Count = g.Count(),
                Movies = g.ToList()
            }).ToListAsync();

            return Ok(groupedMovies);
        }

        [HttpGet("groupByGenresCount")]
        public async Task<ActionResult> GetGroupedByGenresCount()
        {
            var groupedMovies = await _context.Movies.GroupBy(m => m.Genres.Count()).Select(g => new
            {
                Count = g.Key,
                Title = g.Select(m => m.Title),
                Genres = g.Select(m => m.Genres).SelectMany(a => a).Select(ge => ge.Name).Distinct()
            }).ToListAsync();

            return Ok(groupedMovies);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> Filter([FromQuery] MovieFilterDTO movieFilterDTO)
        {
            var moviesQueryable = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(movieFilterDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(x => x.Title.Contains(movieFilterDTO.Title));
            }

            if (movieFilterDTO.InCinemas)
            {
                moviesQueryable = moviesQueryable.Where(m => m.InCinemas);
            }

            if (movieFilterDTO.UpcomingReleases)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(m => m.ReleaseDate > today);
            }
            if (movieFilterDTO.GenreId != 0)
            {
                moviesQueryable = moviesQueryable.
                                    Where(m => m.Genres.Select(g => g.Id).Contains(movieFilterDTO.GenreId));
            }

            var movies = await moviesQueryable.Include(m => m.Genres).ToListAsync();
            return _mapper.Map<List<MovieDTO>>(movies);
        }

        [HttpPost]
        public async Task<ActionResult> Post(MovieCreationDTO movieCreationDTO)
        {
            var movie = _mapper.Map<Movie>(movieCreationDTO);

            movie.Genres.ForEach(g => _context.Entry(g).State = EntityState.Unchanged);
            movie.CinemaHalls.ForEach(ch => _context.Entry(ch).State = EntityState.Unchanged);

            if (movie.MoviesActors is not null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i + 1;
                }
            }


            _context.Add(movie);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpGet("withcounts")]
        public async Task<ActionResult<IEnumerable<MovieWithCounts>>> GetWithCounts()
        {
            return await _context.Set<MovieWithCounts>().ToListAsync();
        }
    }
}
