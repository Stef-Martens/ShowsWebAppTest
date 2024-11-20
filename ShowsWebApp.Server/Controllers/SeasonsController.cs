using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShowsWebApp.Server.DTOs;
using ShowsWebApp.Server.Data;
using AutoMapper;
using ShowsWebApp.Server.Models;

namespace ShowsWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        private readonly ShowsWebAppServerContext _context;
        private readonly IMapper _mapper;

        public SeasonsController(ShowsWebAppServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Seasons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeasonDTO>>> GetSeasons()
        {
            var seasons = await _context.Seasons
                .Include(s => s.Episodes) // Include related data if needed
                .ToListAsync();

            // Map entities to DTOs
            var seasonDtos = _mapper.Map<IEnumerable<SeasonDTO>>(seasons);

            return Ok(seasonDtos);
        }

        // GET: api/Seasons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonDTO>> GetSeasonDTO(int id)
        {
            var season = await _context.Seasons.FindAsync(id);

            if (season == null)
            {
                return NotFound();
            }

            // Map entity to DTO
            var seasonDTO = _mapper.Map<SeasonDTO>(season);

            return Ok(seasonDTO);
        }

        // PUT: api/Seasons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeasonDTO(int id, SeasonDTO seasonDTO)
        {
            if (id != seasonDTO.Id)
            {
                return BadRequest();
            }

            var season = await _context.Seasons.FindAsync(id);
            if (season == null)
                return NotFound();

            _mapper.Map(seasonDTO, season);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeasonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SeasonDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SeasonDTO>> PostSeasonDTO(SeasonDTO seasonDTO)
        {
            var season = _mapper.Map<Season>(seasonDTO);

            season.Show = await _context.Shows.FindAsync(season.ShowId);

            _context.Seasons.Add(season);
            await _context.SaveChangesAsync();

            var createdSeasonDTO = _mapper.Map<SeasonDTO>(season);

            return CreatedAtAction("GetSeasonDTO", new { id = createdSeasonDTO.Id }, createdSeasonDTO);
        }

        // DELETE: api/SeasonDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeasonDTO(int id)
        {
            var seasonDTO = await _context.Seasons.FindAsync(id);
            if (seasonDTO == null)
            {
                return NotFound();
            }

            _context.Seasons.Remove(seasonDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeasonExists(int id)
        {
            return _context.Seasons.Any(e => e.Id == id);
        }
    }
}
