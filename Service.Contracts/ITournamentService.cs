using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.DTOs;
using Tournaments.Core.RequestFeatures;

namespace Service.Contracts
{
    public interface ITournamentService
    {
        Task<(IEnumerable<TournamentDto> tournaments, MetaData metaData)> GetAllTournamentsAsync(RequestParameters parameters);
        Task<TournamentDto> GetTournamentByIdAsync(int id);
        Task<TournamentDto> CreateTournamentAsync(TournamentForCreationDto tournament);
        Task UpdateTournamentAsync(int id, TournamentForUpdateDto tournament);
        Task DeleteTournamentAsync(int id);
    }
}
