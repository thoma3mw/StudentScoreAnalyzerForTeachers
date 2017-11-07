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
    using System.Collections.Generic;
    using System.Linq;
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
        /// <returns>
        /// View
        /// </returns>
        public ActionResult Index()
        {
            return this.View(new NumberOfStudentsModel());
        }

        /// <summary>
        /// Indexes the specified number of students model.
        /// </summary>
        /// <param name="numberOfStudentsModel">The number of students model.</param>
        /// <returns>
        /// Action
        /// </returns>
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
        /// <returns>
        /// View
        /// </returns>
        public ActionResult About()
        {
            this.ViewBag.Message = string.Empty;

            return this.View();
        }

        /// <summary>
        /// Contacts this instance.
        /// </summary>
        /// <returns>
        /// View
        /// </returns>
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
            this.InitializeStudentScoreModels(numberOfStudents);

            return this.View();
        }

        /// <summary>
        /// Batches the update.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="added">The added.</param>
        /// <param name="changed">The changed.</param>
        /// <param name="deleted">The deleted.</param>
        /// <param name="key">The key.</param>
        /// <returns>Results View</returns>
        public ActionResult BatchUpdate(string action, List<StudentScoreModel> added, List<StudentScoreModel> changed, List<StudentScoreModel> deleted, int? key)
        {
            var studentNumbersChanged = changed.Select(c => c.StudentNumber).ToList();
            var studentsChanged = changed.ToDictionary(x => x.StudentNumber, x => x.StudentScore);
            var models = (IEnumerable<StudentScoreModel>)this.Session["StudentScoreModels"];

            foreach (var model in models)
            {
                if (studentNumbersChanged.Contains(model.StudentNumber))
                {
                    model.StudentScore = studentsChanged[model.StudentNumber];
                }
            }

            return this.Json(this.Session["StudentScoreModels"], JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Scores the input.
        /// </summary>
        /// <returns>Action</returns>
        [HttpPost]
        public ActionResult ScoreInput()
        {
            return this.RedirectToAction("Results");
        }

        /// <summary>
        /// Results the specified scores models.
        /// </summary>
        /// <returns>
        /// View
        /// </returns>
        public ActionResult Results()
        {
            return this.View();
        }

        /// <summary>
        /// Initializes the student score models.
        /// </summary>
        /// <param name="numberOfStudents">The number of students.</param>
        private void InitializeStudentScoreModels(int numberOfStudents)
        {
            var data = new List<StudentScoreModel>();
            for (var i = 0; i < numberOfStudents; i++)
            {
                data.Add(
                    new StudentScoreModel
                        {
                            StudentNumber = i + 1,
                            StudentScore = decimal.Zero
                        });
            }

            this.Session["StudentScoreModels"] = data;
        }
    }
}