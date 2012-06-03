﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimeRecs.RecService.DTO
{
    public class RecSourceParams
    {

    }

    public class AverageScoreRecSourceParams : RecSourceParams
    {
        public int MinEpisodesToCountIncomplete { get; set; }
        public int MinUsersToCountAnime { get; set; }
        public bool UseDropped { get; set; }
    }

    public class MostPopularRecSourceParams : RecSourceParams
    {
        public int MinEpisodesToCountIncomplete { get; set; }
        public bool UseDropped { get; set; }
    }

    public class AnimeRecsRecSourceParams : RecSourceParams
    {
        public int NumRecommendersToUse { get; set; }
        public double FractionConsideredRecommended { get; set; }
        public int MinEpisodesToClassifyIncomplete { get; set; }
    }
}

// Copyright (C) 2012 Greg Najda
//
// This file is part of AnimeRecs.RecService.DTO.
//
// AnimeRecs.RecService.DTO is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// AnimeRecs.RecService.DTO is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with AnimeRecs.RecService.DTO.  If not, see <http://www.gnu.org/licenses/>.