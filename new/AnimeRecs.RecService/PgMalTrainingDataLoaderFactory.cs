﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimeRecs.DAL;

namespace AnimeRecs.RecService
{
    internal class PgMalTrainingDataLoaderFactory : IMalTrainingDataLoaderFactory
    {
        private string m_connectionString;

        public PgMalTrainingDataLoaderFactory(string connectionString)
        {
            m_connectionString = connectionString;
        }
        
        public IMalTrainingDataLoader GetTrainingDataLoader()
        {
            return new PgMalDataLoader(m_connectionString);
        }
    }
}

// Copyright (C) 2012 Greg Najda
//
// This file is part of AnimeRecs.RecService.
//
// AnimeRecs.RecService is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// AnimeRecs.RecService is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with AnimeRecs.RecService.  If not, see <http://www.gnu.org/licenses/>.