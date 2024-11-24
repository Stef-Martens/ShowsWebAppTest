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
using Microsoft.AspNetCore.Authorization;

namespace ShowsWebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {

        private readonly IShowService _showService;

        public ShowsController(IShowService showService)
        {
            _showService = showService;
        }


        // GET: api/ShowDTOes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShowDTO>>> GetShowDTO()
        {
            var showDTOs = await _showService.GetAllAsync();
            return Ok(showDTOs);
        }

        // GET: api/ShowsFull
        [HttpGet("ShowsFull")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Show>>> GetShowsFull()
        {
            var shows = await _showService.GetShowsFull();
            return Ok(shows);
        }

        // GET: api/ShowDTOes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShowDTO>> GetShowDTO(int id)
        {
            var showDTO = await _showService.GetByIdAsync(id);
            return Ok(showDTO);
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

            try
            {
                await _showService.UpdateAsync(showDTO);
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
            await _showService.AddAsync(showDTO);
            return Ok(showDTO);
        }

        // DELETE: api/ShowDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShowDTO(int id)
        {
            var showDTO = await _showService.GetByIdAsync(id);
            if (showDTO == null)
            {
                return NotFound();
            }
            await _showService.DeleteAsync(id);
            return NoContent();
        }

        private bool ShowExists(int id)
        {
            return _showService.GetAllAsync().Result.Any(e => e.Id == id);
        }
    }
}
