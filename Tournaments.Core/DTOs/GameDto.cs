﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournaments.Core.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public DateTime GameDate { get; set; }
        public int TournamentId { get; set; }
    }

    public class GameForCreationDto
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public DateTime GameDate { get; set; }
    }

    public class GameForUpdateDto : GameForCreationDto
    {
    }
}