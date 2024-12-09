using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.DTOs;
using Tournaments.Core.Entities;

namespace Tournament.Data.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Tournament mappings
            CreateMap<Tournaments.Core.Entities.Tournament, TournamentDto>();
            CreateMap<TournamentForCreationDto, Tournaments.Core.Entities.Tournament>();
            CreateMap<TournamentForUpdateDto, Tournaments.Core.Entities.Tournament>();

            // Game mappings
            CreateMap<Game, GameDto>();
            CreateMap<GameForCreationDto, Game>();
            CreateMap<GameForUpdateDto, Game>();
        }
    }
}
