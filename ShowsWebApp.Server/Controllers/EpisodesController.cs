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
    public class EpisodesController : ControllerBase
    {
        private readonly ShowsWebAppServerContext _context;
        private readonly IMapper _mapper;

        public EpisodesController(ShowsWebAppServerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Episodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EpisodeDTO>>> GetEpisodes()
        {
            var episodes = await _context.Episodes
                .Include(e => e.Season) // Include related data if needed
                .ToListAsync();

            // Map entities to DTOs
            var episodeDtos = _mapper.Map<IEnumerable<EpisodeDTO>>(episodes);


            return Ok(episodeDtos);
        }

        // GET: api/Episodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EpisodeDTO>> GetEpisodeDTO(int id)
        {
            var episode = await _context.Episodes.FindAsync(id);

            if (episode == null)
            {
                return NotFound();
            }

            // Map entity to DTO
            var episodeDTO = _mapper.Map<EpisodeDTO>(episode);

            return episodeDTO;
        }

        // PUT: api/Episodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEpisodeDTO(int id, EpisodeDTO episodeDTO)
        {
            if (id != episodeDTO.Id)
            {
                return BadRequest();
            }

            var episode = await _context.Episodes.FindAsync(id);
            if (episode == null)
            {
                return NotFound();
            }

            // Map updated fields from DTO to entity
            _mapper.Map(episodeDTO, episode);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpisodeExists(id))
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

        // POST: api/EpisodeDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EpisodeDTO>> PostEpisodeDTO(EpisodeDTO episodeDTO)
        {
            // Map DTO to entity
            var episode = _mapper.Map<Episode>(episodeDTO);

            episode.Season = await _context.Seasons.FindAsync(episode.SeasonId);

            _context.Episodes.Add(episode);
            await _context.SaveChangesAsync();

            // Map entity back to DTO (with the generated Id)
            var createdDTO = _mapper.Map<EpisodeDTO>(episode);

            return CreatedAtAction("GetEpisodeDTO", new { id = createdDTO.Id }, episodeDTO);
        }

        // DELETE: api/EpisodeDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpisodeDTO(int id)
        {
            var episode = await _context.Episodes.FindAsync(id);
            if (episode == null)
            {
                return NotFound();
            }

            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EpisodeExists(int id)
        {
            return _context.Episodes.Any(e => e.Id == id);
        }
    }
}
