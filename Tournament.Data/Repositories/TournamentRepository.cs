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
    public class TournamentRepository : RepositoryBase<Tournaments.Core.Entities.Tournament>, ITournamentRepository
    {
        public TournamentRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<Tournaments.Core.Entities.Tournament> GetTournamentWithGamesAsync(int id)
        {
            return await Context.Tournaments
                .Include(t => t.Games)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
