using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.Contracts;

namespace Tournament.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryContext _context;
        private ITournamentRepository _tournamentRepository;
        private IGameRepository _gameRepository;

        public UnitOfWork(RepositoryContext context)
        {
            _context = context;
        }

        public ITournamentRepository TournamentRepository =>
            _tournamentRepository ??= new TournamentRepository(_context);

        public IGameRepository GameRepository =>
            _gameRepository ??= new GameRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
