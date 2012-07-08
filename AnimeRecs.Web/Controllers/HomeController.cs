﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnimeRecs.Web.Models.ViewModels;
using AnimeRecs.RecService.ClientLib;

namespace AnimeRecs.Web.Controllers
{
    public class HomeController : Controller
    {
        private IAnimeRecsClientFactory m_recClientFactory;

        public HomeController()
            : this(AppGlobals.RecClientFactory)
        {
            ;
        }

        public HomeController(IAnimeRecsClientFactory recClientFactory)
        {
            m_recClientFactory = recClientFactory;
        }
        
        public ViewResult Index(string algorithm)
        {
            algorithm = algorithm ?? AppGlobals.Config.DefaultRecSource;

            string recSourceType = null;

            using (AnimeRecsClient client = m_recClientFactory.GetClient())
            {
                try
                {
                    recSourceType = client.GetRecSourceType(algorithm);
                }
                catch
                {
                    ;
                }
            }

            bool algorithmAvailable = recSourceType != null;

            bool targetScoreNeeded = false;
            if (AnimeRecs.RecService.DTO.RecSourceTypes.AnimeRecs.Equals(recSourceType, StringComparison.OrdinalIgnoreCase))
            {
                targetScoreNeeded = true;
            }

            ViewData.Model = new HomeViewModel(algorithm: algorithm, algorithmAvailable: algorithmAvailable, targetScoreNeeded: targetScoreNeeded);
            return View();
        }
    }
}

// Copyright (C) 2012 Greg Najda
//
// This file is part of AnimeRecs.Web.
//
// AnimeRecs.Web is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// AnimeRecs.Web is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with AnimeRecs.Web.  If not, see <http://www.gnu.org/licenses/>.