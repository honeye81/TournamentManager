using AutoMapper;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.Contracts;
using Tournaments.Core.DTOs;
using Tournaments.Core.Entities;
using Tournaments.Core.Exceptions;
using Tournaments.Core.RequestFeatures;


namespace Tournament.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const int MaxGamesPerTournament = 10;

        public GameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<GameDto> games, MetaData metaData)> GetGamesByTournamentAsync(int tournamentId, RequestParameters parameters)
        {
            var games = await _unitOfWork.GameRepository.GetGamesByTournamentAsync(tournamentId, parameters.PageNumber, parameters.PageSize);
            var count = await _unitOfWork.GameRepository.CountGamesInTournamentAsync(tournamentId);

            var metadata = new MetaData
            {
                TotalCount = count,
                PageSize = parameters.PageSize,
                CurrentPage = parameters.PageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)parameters.PageSize)
            };

            return (_mapper.Map<IEnumerable<GameDto>>(games), metadata);
        }

        public async Task<GameDto> GetGameByIdAsync(int tournamentId, int id)
        {
            var game = await _unitOfWork.GameRepository.GetByIdAsync(id);

            if (game == null || game.TournamentId != tournamentId)
                throw new NotFoundException($"Game with id: {id} not found in tournament: {tournamentId}");

            return _mapper.Map<GameDto>(game);
        }

        public async Task<GameDto> CreateGameAsync(int tournamentId, GameForCreationDto gameDto)
        {
            var gamesCount = await _unitOfWork.GameRepository.CountGamesInTournamentAsync(tournamentId);
            if (gamesCount >= MaxGamesPerTournament)
                throw new BusinessRuleViolationException("Maximum number of games (10) reached for this tournament");

            var game = _mapper.Map<Game>(gameDto);
            game.TournamentId = tournamentId;

            _unitOfWork.GameRepository.Create(game);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<GameDto>(game);
        }

        public async Task UpdateGameAsync(int tournamentId, int id, GameForUpdateDto gameDto)
        {
            var game = await _unitOfWork.GameRepository.GetByIdAsync(id);

            if (game == null || game.TournamentId != tournamentId)
                throw new NotFoundException($"Game with id: {id} not found in tournament: {tournamentId}");

            _mapper.Map(gameDto, game);
            _unitOfWork.GameRepository.Update(game);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteGameAsync(int tournamentId, int id)
        {
            var game = await _unitOfWork.GameRepository.GetByIdAsync(id);

            if (game == null || game.TournamentId != tournamentId)
                throw new NotFoundException($"Game with id: {id} not found in tournament: {tournamentId}");

            _unitOfWork.GameRepository.Delete(game);
            await _unitOfWork.SaveAsync();
        }
    }
}