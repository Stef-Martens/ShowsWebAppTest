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
using ShowsWebApp.Server.Services;

namespace ShowsWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;

        public EpisodesController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        // GET: api/Episodes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EpisodeDTO>>> GetEpisodes()
        {
            var episodes = await _episodeService.GetAllAsync();
            return Ok(episodes);
        }

        // GET: api/Episodes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EpisodeDTO>> GetEpisodeDTO(int id)
        {
            var episode = await _episodeService.GetByIdAsync(id);
            return Ok(episode);
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

            var episode = await _episodeService.GetByIdAsync(id);
            if (episode == null)
            {
                return NotFound();
            }

            try
            {
                await _episodeService.UpdateAsync(episode);
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
            await _episodeService.AddAsync(episodeDTO);
            return CreatedAtAction("GetEpisodeDTO", new { id = episodeDTO.Id }, episodeDTO);
        }

        // DELETE: api/EpisodeDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEpisodeDTO(int id)
        {
            var episode = await _episodeService.GetByIdAsync(id);
            if (episode == null)
            {
                return NotFound();
            }

            await _episodeService.DeleteAsync(id);

            return NoContent();
        }

        private bool EpisodeExists(int id)
        {
            return _episodeService.GetByIdAsync(id) != null;
        }
    }
}
