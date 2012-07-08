﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimeRecs.RecService.DTO
{
    public class LoadRecSourceRequest<TRecSourceParams>
        where TRecSourceParams : RecSourceParams
    {
        /// <summary>
        /// Name to give the loaded rec source. Other operations will use this name to refer to it. The name is not case-sensitive.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// If false and there is already a rec source with the given name loaded, it will be an error.
        /// </summary>
        public bool ReplaceExisting { get; set; }

        /// <summary>
        /// Type of rec source.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Parameters specific to the type of rec source.
        /// </summary>
        public TRecSourceParams Params { get; set; }

        public LoadRecSourceRequest()
        {
            ;
        }

        public LoadRecSourceRequest(string name, string type, bool replaceExisting, TRecSourceParams parameters)
        {
            Name = name;
            Type = type;
            ReplaceExisting = replaceExisting;
            Params = parameters;
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