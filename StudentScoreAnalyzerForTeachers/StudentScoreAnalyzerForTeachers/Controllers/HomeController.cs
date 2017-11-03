// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Does Not Apply">
// Copyright (c) Matthew Thomas. All rights reserved.
// </copyright>
// <summary>
//   HomeController.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StudentScoreAnalyzerForTeachers.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Class HomeController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Abouts this instance.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult About()
        {
            this.ViewBag.Message = string.Empty;

            return this.View();
        }

        /// <summary>
        /// Contacts this instance.
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Contact()
        {
            this.ViewBag.Message = string.Empty;

            return this.View();
        }
    }
}