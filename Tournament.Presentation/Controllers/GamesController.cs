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
    [Route("api/tournaments/{tournamentId}/games")]
    public class GamesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public GamesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames(int tournamentId, [FromQuery] RequestParameters parameters)
        {
            var (games, metaData) = await _serviceManager.GameService
                .GetGamesByTournamentAsync(tournamentId, parameters);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metaData));

            return Ok(games);
        }

        [HttpGet("{id}", Name = "GetGameForTournament")]
        public async Task<IActionResult> GetGame(int tournamentId, int id)
        {
            var game = await _serviceManager.GameService.GetGameByIdAsync(tournamentId, id);
            return Ok(game);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame(int tournamentId, [FromBody] GameForCreationDto game)
        {
            var createdGame = await _serviceManager.GameService.CreateGameAsync(tournamentId, game);

            return CreatedAtRoute(
                "GetGameForTournament",
                new { tournamentId, id = createdGame.Id },
                createdGame);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int tournamentId, int id, [FromBody] GameForUpdateDto game)
        {
            await _serviceManager.GameService.UpdateGameAsync(tournamentId, id, game);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int tournamentId, int id)
        {
            await _serviceManager.GameService.DeleteGameAsync(tournamentId, id);
            return NoContent();
        }
    }
}
