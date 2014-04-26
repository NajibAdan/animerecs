﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnimeRecs.Web.Models.Json
{
    /// <summary>
    /// Returned to the client on ajax calls that fail along with an error HTTP status code.
    /// This is only returned for certain expected errors.
    /// If an error HTTP status code is returned, check if the Content-Type contains application/json.
    /// You cannot check simple equality because it will actually be something like application/json; charset=utf-8.
    /// If so, the content is an AjaxError.
    /// If not, something went wrong, who knows what. Displaying a generic error message may be appropriate.
    /// </summary>
    public class AjaxError
    {
        public string Message { get; set; }

        public AjaxError()
        {
            ;
        }

        public AjaxError(string message)
        {
            Message = message;
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