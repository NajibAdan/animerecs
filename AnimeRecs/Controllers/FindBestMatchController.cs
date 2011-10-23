﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnimeRecs.Models;
using System.ComponentModel.DataAnnotations;
using AnimeCompatibility;

namespace AnimeRecs.Controllers
{
    public class FindBestMatchController : Controller
    {
        private IRecommendationFinderFactory m_finderFactory;

        public FindBestMatchController()
        {
            m_finderFactory = new RecommendorCacheFinderFactory(new MockRecommendorCacheFactory());
        }
        
        public FindBestMatchController(IRecommendationFinderFactory finderFactory)
        {
            m_finderFactory = finderFactory;
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Index([Required] MalApiJson animeList)
        //public JsonResult Index(AnimeJson animeJson)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 500;
                return Json(new { error = true });
            }

            List<MyAnimeListEntry> malList = animeList.anime.Select(malApiAnimeJson =>
                new MyAnimeListEntry()
                {
                    Id = malApiAnimeJson.id,
                    Name = malApiAnimeJson.title,
                    Score = malApiAnimeJson.score,
                    Status = ParseCompletionStatus(malApiAnimeJson.watched_status)
                })
                .ToList();
            
            
            IRecommendationFinder recFinder = m_finderFactory.GetRecommendationFinder();
            RecommendationResults results = recFinder.GetRecommendations(malList);

            //return Json(results);

            return Json(animeList);
        }

        internal static CompletionStatus ParseCompletionStatus(string status)
        {
            if (status.Equals("completed", StringComparison.OrdinalIgnoreCase))
            {
                return CompletionStatus.Completed;
            }
            else if (status.Equals("watching", StringComparison.OrdinalIgnoreCase))
            {
                return CompletionStatus.Watching;
            }
            else if (status.Equals("dropped", StringComparison.OrdinalIgnoreCase))
            {
                return CompletionStatus.Dropped;
            }
            else if (status.Equals("plan to watch", StringComparison.OrdinalIgnoreCase))
            {
                return CompletionStatus.PlanToWatch;
            }
            else if (status.Equals("on-hold", StringComparison.OrdinalIgnoreCase))
            {
                return CompletionStatus.OnHold;
            }
            else
            {
                return CompletionStatus.Unknown;
            }
        }
    }
}
