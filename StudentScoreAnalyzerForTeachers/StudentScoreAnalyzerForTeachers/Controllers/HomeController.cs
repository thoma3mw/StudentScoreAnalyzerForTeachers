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

    using StudentScoreAnalyzerForTeachers.Models;

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
            return this.View(new NumberOfStudentsModel());
        }

        /// <summary>
        /// Indexes the specified number of students model.
        /// </summary>
        /// <param name="numberOfStudentsModel">The number of students model.</param>
        /// <returns>Action</returns>
        [HttpPost]
        public ActionResult Index(NumberOfStudentsModel numberOfStudentsModel)
        {
            return this.RedirectToAction(
                "ScoreInput",
                "Home",
                new
                    {
                        numberOfStudents = numberOfStudentsModel.NumberOfStudents
                    });
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

        /// <summary>
        /// Scores the input.
        /// </summary>
        /// <param name="numberOfStudents">The number of students.</param>
        /// <returns>
        /// View
        /// </returns>
        public ActionResult ScoreInput(int numberOfStudents)
        {
            this.ViewBag.NumberOfStudents = numberOfStudents;
            return this.View();
        }
    }
}