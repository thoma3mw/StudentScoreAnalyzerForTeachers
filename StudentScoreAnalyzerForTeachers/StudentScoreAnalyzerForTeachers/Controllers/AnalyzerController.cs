// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnalyzerController.cs" company="Does Not Apply">
// Copyright (c) Matthew Thomas. All rights reserved.
// </copyright>
// <summary>
//   AnalyzerController.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StudentScoreAnalyzerForTeachers.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Class AnalyzerController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class AnalyzerController : Controller
    {
        /// <summary>
        /// Scores the input.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ScoreInput()
        {
            return this.View();
        }
    }
}