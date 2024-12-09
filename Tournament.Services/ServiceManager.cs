using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Contracts;
using AutoMapper;
using Tournaments.Core.Contracts;

namespace Tournament.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITournamentService> _tournamentService;
        private readonly Lazy<IGameService> _gameService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tournamentService = new Lazy<ITournamentService>(() =>
                new TournamentService(unitOfWork, mapper));

            _gameService = new Lazy<IGameService>(() =>
                new GameService(unitOfWork, mapper));
        }

        public ITournamentService TournamentService => _tournamentService.Value;
        public IGameService GameService => _gameService.Value;
    }
}
