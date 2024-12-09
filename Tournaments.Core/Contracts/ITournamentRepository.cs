using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.Entities;

namespace Tournaments.Core.Contracts
{
    public interface ITournamentRepository : IRepositoryBase<Tournament>
    {
        Task<Tournament> GetTournamentWithGamesAsync(int id);
    }
}
