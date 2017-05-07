﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AnimeRecs.UpdateStreams.Crunchyroll
{
    /// <summary>
    /// Reads a Crunchyroll html file from disk instead of accessing the Crunchyroll website.
    /// </summary>
    internal class CrunchyrollLocalHtmlStreamInfoSource : IAnimeStreamInfoSource
    {
        private string _crunchyrollLocalHtmlPath;

        public CrunchyrollLocalHtmlStreamInfoSource(string crunchyrollLocalHtmlPath)
        {
            this._crunchyrollLocalHtmlPath = crunchyrollLocalHtmlPath;
        }

        public ICollection<AnimeStreamInfo> GetAnimeStreamInfo()
        {
            Console.WriteLine("Getting HTML for {0}", _crunchyrollLocalHtmlPath);
            string html = File.ReadAllText(_crunchyrollLocalHtmlPath);
            var source = new CrunchyrollHtmlStreamInfoSource(html);
            return source.GetAnimeStreamInfo();
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
//
//  If you modify AnimeRecs.UpdateStreams, or any covered work, by linking 
//  or combining it with HTML Agility Pack (or a modified version of that 
//  library), containing parts covered by the terms of the Microsoft Public 
//  License, the licensors of AnimeRecs.UpdateStreams grant you additional 
//  permission to convey the resulting work. Corresponding Source for a non-
//  source form of such a combination shall include the source code for the parts 
//  of HTML Agility Pack used as well as that of the covered work.