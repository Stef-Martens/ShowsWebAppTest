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
    public class SeasonsController : ControllerBase
    {
        private readonly ISeasonService _seasonService;

        public SeasonsController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        // GET: api/Seasons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeasonDTO>>> GetSeasons()
        {
            var seasons = await _seasonService.GetAllAsync();
            return Ok(seasons);
        }

        // GET: api/Seasons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonDTO>> GetSeasonDTO(int id)
        {
            var season = await _seasonService.GetByIdAsync(id);
            return Ok(season);
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

            var season = await _seasonService.GetByIdAsync(id);
            if (season == null)
                return NotFound();


            try
            {
                await _seasonService.UpdateAsync(season);
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
            await _seasonService.AddAsync(seasonDTO);
            return CreatedAtAction("GetSeasonDTO", new { id = seasonDTO.Id }, seasonDTO);
        }

        // DELETE: api/SeasonDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeasonDTO(int id)
        {
            var season = await _seasonService.GetByIdAsync(id);
            if (season == null)
            {
                return NotFound();
            }
            await _seasonService.DeleteAsync(id);
            return NoContent();
        }

        private bool SeasonExists(int id)
        {
            return _seasonService.GetAllAsync().Result.Any(e => e.Id == id);
        }
    }
}
