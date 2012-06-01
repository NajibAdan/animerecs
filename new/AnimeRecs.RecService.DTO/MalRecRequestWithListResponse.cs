﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimeRecs.RecService.DTO
{    
    public class MalRecRequestWithListResponse<TRecommendation>
        where TRecommendation : BasicRecommendation
    {
        public string RecommendationType { get; set; }
        public IList<TRecommendation> Recommendations { get; set; }

        public MalRecRequestWithListResponse()
        {
            ;
        }

        public MalRecRequestWithListResponse(string recommendationType, IList<TRecommendation> recommendations)
        {
            RecommendationType = recommendationType;
            Recommendations = recommendations;
        }
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