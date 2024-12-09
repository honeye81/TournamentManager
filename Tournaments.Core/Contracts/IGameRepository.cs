using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.Entities;

namespace Tournaments.Core.Contracts
{
    public interface IGameRepository : IRepositoryBase<Game>
    {
        Task<int> CountGamesInTournamentAsync(int tournamentId);
        Task<IEnumerable<Game>> GetGamesByTournamentAsync(int tournamentId, int pageNumber, int pageSize);
    }
}
