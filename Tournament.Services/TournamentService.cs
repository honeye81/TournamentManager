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
using Tournaments.Core.RequestFeatures;
using Tournaments.Core.Exceptions;


namespace Tournament.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TournamentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<TournamentDto> tournaments, MetaData metaData)> GetAllTournamentsAsync(RequestParameters parameters)
        {
            var tournaments = await _unitOfWork.TournamentRepository
                .GetAllAsync(parameters.PageNumber, parameters.PageSize);

            var count = await _unitOfWork.TournamentRepository.CountAsync();

            var metadata = new MetaData
            {
                TotalCount = count,
                PageSize = parameters.PageSize,
                CurrentPage = parameters.PageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)parameters.PageSize)
            };

            return (_mapper.Map<IEnumerable<TournamentDto>>(tournaments), metadata);
        }

        public async Task<TournamentDto> GetTournamentByIdAsync(int id)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetTournamentWithGamesAsync(id);
            if (tournament == null)
                throw new NotFoundException($"Tournament with id: {id} not found");

            return _mapper.Map<TournamentDto>(tournament);
        }

        public async Task<TournamentDto> CreateTournamentAsync(TournamentForCreationDto tournamentDto)
        {
            var tournament = _mapper.Map<Tournaments.Core.Entities.Tournament>(tournamentDto);
            _unitOfWork.TournamentRepository.Create(tournament);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<TournamentDto>(tournament);
        }

        public async Task UpdateTournamentAsync(int id, TournamentForUpdateDto tournamentDto)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetByIdAsync(id);
            if (tournament == null)
                throw new NotFoundException($"Tournament with id: {id} not found");

            _mapper.Map(tournamentDto, tournament);
            _unitOfWork.TournamentRepository.Update(tournament);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTournamentAsync(int id)
        {
            var tournament = await _unitOfWork.TournamentRepository.GetByIdAsync(id);
            if (tournament == null)
                throw new NotFoundException($"Tournament with id: {id} not found");

            _unitOfWork.TournamentRepository.Delete(tournament);
            await _unitOfWork.SaveAsync();
        }
    }
}