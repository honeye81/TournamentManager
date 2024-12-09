using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.DTOs;
using Tournaments.Core.RequestFeatures;
using System.Text.Json;

namespace Tournament.Presentation.Controllers
{
    [ApiController]
    [Route("api/tournaments")]
    public class TournamentsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TournamentsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetTournaments([FromQuery] RequestParameters parameters)
        {
            var (tournaments, metaData) = await _serviceManager.TournamentService
                .GetAllTournamentsAsync(parameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(tournaments);
        }

        [HttpGet("{id}", Name = "TournamentById")]
        public async Task<IActionResult> GetTournament(int id)
        {
            var tournament = await _serviceManager.TournamentService.GetTournamentByIdAsync(id);
            return Ok(tournament);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTournament([FromBody] TournamentForCreationDto tournament)
        {
            var createdTournament = await _serviceManager.TournamentService.CreateTournamentAsync(tournament);
            return CreatedAtRoute("TournamentById", new { id = createdTournament.Id }, createdTournament);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTournament(int id, [FromBody] TournamentForUpdateDto tournament)
        {
            await _serviceManager.TournamentService.UpdateTournamentAsync(id, tournament);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            await _serviceManager.TournamentService.DeleteTournamentAsync(id);
            return NoContent();
        }
    }
}
