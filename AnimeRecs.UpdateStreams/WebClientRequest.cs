﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecs.UpdateStreams
{
    class WebClientRequest
    {
        public Uri URL { get; set; }
        public string UserAgent { get; set; }
        public string Accept { get; set; }
        public List<KeyValuePair<string, string>> Headers { get; set; }
        public List<KeyValuePair<string, string>> PostParameters { get; set; }

        public WebClientRequest(string url)
        {
            URL = new Uri(url);
            Headers = new List<KeyValuePair<string, string>>();
            PostParameters = new List<KeyValuePair<string, string>>();
        }
    }
}

// Copyright (C) 2017 Greg Najda
//
// This file is part of AnimeRecs.UpdateStreams
//
// AnimeRecs.UpdateStreams is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// AnimeRecs.UpdateStreams is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with AnimeRecs.UpdateStreams.  If not, see <http://www.gnu.org/licenses/>.