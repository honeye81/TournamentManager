using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.Contracts;
using Tournaments.Core.Entities;

namespace Tournament.Data.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<int> CountGamesInTournamentAsync(int tournamentId)
        {
            return await Context.Games.CountAsync(g => g.TournamentId == tournamentId);
        }

        public async Task<IEnumerable<Game>> GetGamesByTournamentAsync(int tournamentId, int pageNumber, int pageSize)
        {
            return await Context.Games
                .Where(g => g.TournamentId == tournamentId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
