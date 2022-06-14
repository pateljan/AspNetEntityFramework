using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreMoviesWebApi.DTOs;
using EFCoreMoviesWebApi.Entities;
using EFCoreMoviesWebApi.Entities.Keyless;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCoreMoviesWebApi.Controllers
{
    [ApiController]
    [Route("api/cinemas")]
    public class CinemasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CinemasController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CinemaDTO>> Get()
        {
            return await _context.Cinemas.ProjectTo<CinemaDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("closetome")]
        public async Task<ActionResult> GetCloseToMe(double latitude, double longitude)
        {
            //-69.940154, 18.483280
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var myLocation = geometryFactory.CreatePoint(new Coordinate(longitude, latitude));

            var maxDistanceInMeters = 2000; // 2 KMS
            var cinemas = await _context.Cinemas
                .OrderBy(c => c.Location.Distance(myLocation))
                .Where(c => c.Location.IsWithinDistance(myLocation, maxDistanceInMeters))
                .Select(c => new
                {
                    Name = c.Name,
                    Distance = Math.Round(c.Location.Distance(myLocation))
                }).ToListAsync();

            return Ok(cinemas);
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var geoMetryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var cinemaLocation = geoMetryFactory.CreatePoint(new Coordinate(-69.913539, 18.476256));

            var cinema = new Cinema()
            {
                Name = " My Cinema",
                Location = cinemaLocation,
                CinemaDetail = new CinemaDetail()
                {
                    History = "the history ....",
                    Missions = "the mission...."
                },
                CinemaOffer = new CinemaOffer()
                {
                    DiscountPercentage = 5,
                    Begin = DateTime.Today,
                    End = DateTime.Today.AddDays(7)
                },
                CinemaHalls = new HashSet<CinemaHall>()
                {
                    new CinemaHall()
                    {
                        Cost=200,
                        Currency = Currency.DominicaPeso,
                        CinemaHallType = CinemaHallType.TwoDimensions
                    },
                    new CinemaHall()
                    {
                        Cost=250,
                        Currency = Currency.USDollar,
                        CinemaHallType = CinemaHallType.ThreeDimensions
                    }
                }
            };
            _context.Add(cinema);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("withDTO")]
        public async Task<ActionResult> Post(CinemaCreationDTO cinemaCreationDTO)
        {
            var cinema = _mapper.Map<Cinema>(cinemaCreationDTO);
            _context.Add(cinema);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("cinemaoffer")]
        public async Task<ActionResult> PutCinemaOffer(CinemaOffer cinemaOffer)
        {
            _context.Update(cinemaOffer);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult> GETById(int id)
        {
            var cinemaDb = await _context.Cinemas
                .Include(c => c.CinemaHalls)
                .Include(c => c.CinemaOffer)
                .Include(c => c.CinemaDetail)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cinemaDb is null)
            {
                return NotFound();
            }
            cinemaDb.Location = null;
            return Ok(cinemaDb);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(CinemaCreationDTO cinemaCreationDTO, int id)
        {
            var cinemaDb = await _context.Cinemas
                .Include(c => c.CinemaHalls)
                .Include(c => c.CinemaOffer)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cinemaDb is null)
            {
                return NotFound();
            }

            cinemaDb = _mapper.Map(cinemaCreationDTO, cinemaDb);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("withoutlocation")]
        public async Task<IEnumerable<CinemaWithoutLocation>> GetWithoutLocation()
        {
            //return await _context.Set<CinemaWithoutLocation>().ToListAsync();
            return await _context.CinemaWithoutLocations.ToListAsync();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cinema = await _context.Cinemas.Include(p => p.CinemaHalls).FirstOrDefaultAsync(p => p.Id == id);
            if (cinema is null)
            {
                return NotFound();
            }

            _context.Remove(cinema);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
