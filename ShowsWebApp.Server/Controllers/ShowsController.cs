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
    public class ShowsController : ControllerBase
    {
        private readonly ShowsWebAppServerContext _context;
        private readonly IMapper _mapper;

        public ShowsController(ShowsWebAppServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ShowDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowDTO>>> GetShowDTO()
        {
            var shows = await _context.Shows
                .Include(s => s.Seasons) // Include related data if needed
                .ToListAsync();

            // Map entities to DTOs
            var showDtos = _mapper.Map<IEnumerable<ShowDTO>>(shows);

            return Ok(showDtos);
        }

        // GET: api/ShowsFull
        [HttpGet("ShowsFull")]
        public async Task<ActionResult<IEnumerable<Show>>> GetShowsFull()
        {
            var shows = await _context.Shows
                .Include(s => s.Seasons)
                .ThenInclude(s => s.Episodes)
                .Select(s => new Show
                {
                    Id = s.Id,
                    Title = s.Title,
                    Genre = s.Genre,
                    Language = s.Language,
                    Description = s.Description,
                    Seasons = s.Seasons.Select(season => new Season
                    {
                        Id = season.Id,
                        Title = season.Title,
                        ShowId = season.ShowId,
                        Episodes = season.Episodes.Select(episode => new Episode
                        {
                            Id = episode.Id,
                            Title = episode.Title,
                            SeasonId = episode.SeasonId,
                        }).ToList()
                    }).ToList()
                })
                .ToListAsync();
            return Ok(shows);
        }

        // GET: api/ShowDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShowDTO>> GetShowDTO(int id)
        {
            var show = await _context.Shows.FindAsync(id);

            if (show == null)
            {
                return NotFound();
            }

            // Map entity to DTO
            var showDTO = _mapper.Map<ShowDTO>(show);

            return showDTO;
        }

        // PUT: api/ShowDTOes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShowDTO(int id, ShowDTO showDTO)
        {
            if (id != showDTO.Id)
            {
                return BadRequest();
            }

            var show = await _context.Shows.FindAsync(id);
            if (show == null)
                return NotFound();

            _mapper.Map(showDTO, show);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowExists(id))
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

        // POST: api/ShowDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ShowDTO>> PostShowDTO(ShowDTO showDTO)
        {
            var show = _mapper.Map<Show>(showDTO);
            _context.Shows.Add(show);
            await _context.SaveChangesAsync();
            var createdShowDTO = _mapper.Map<ShowDTO>(show);
            return Ok(createdShowDTO);
        }

        // DELETE: api/ShowDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowDTO(int id)
        {
            var showDTO = await _context.Shows.FindAsync(id);
            if (showDTO == null)
            {
                return NotFound();
            }

            _context.Shows.Remove(showDTO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShowExists(int id)
        {
            return _context.Shows.Any(e => e.Id == id);
        }
    }
}
