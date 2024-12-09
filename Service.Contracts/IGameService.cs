using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.DTOs;
using Tournaments.Core.RequestFeatures;

namespace Service.Contracts
{
    public interface IGameService
    {
        Task<(IEnumerable<GameDto> games, MetaData metaData)> GetGamesByTournamentAsync(int tournamentId, RequestParameters parameters);
        Task<GameDto> GetGameByIdAsync(int tournamentId, int id);
        Task<GameDto> CreateGameAsync(int tournamentId, GameForCreationDto game);
        Task UpdateGameAsync(int tournamentId, int id, GameForUpdateDto game);
        Task DeleteGameAsync(int tournamentId, int id);
    }
}
