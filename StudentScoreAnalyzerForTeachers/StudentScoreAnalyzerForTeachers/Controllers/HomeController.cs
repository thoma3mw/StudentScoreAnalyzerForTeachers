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

            return this.View(new SortingModel());
        }

        /// <summary>
        /// Batches the update.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="added">The added.</param>
        /// <param name="changed">The changed.</param>
        /// <param name="deleted">The deleted.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        /// Results View
        /// </returns>
        public ActionResult BatchUpdate(string action, List<StudentScoreModel> added, List<StudentScoreModel> changed, List<StudentScoreModel> deleted, int? key)
        {
            if (changed != null)
            {
                var studentNumbersChanged = changed.Select(c => c.StudentNumber)
                    .ToList();
                var studentsChanged = changed.ToDictionary(x => x.StudentNumber, x => x.StudentScore);
                var models = (List<StudentScoreModel>)this.Session["StudentScoreModels"];

                foreach (var model in models)
                {
                    if (studentNumbersChanged.Contains(model.StudentNumber))
                    {
                        model.StudentScore = studentsChanged[model.StudentNumber];
                    }
                }
            }

            return this.Json(this.Session["StudentScoreModels"], JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Scores the input.
        /// </summary>
        /// <param name="sortingModel">The sorting model.</param>
        /// <returns>
        /// Action
        /// </returns>
        [HttpPost]
        public ActionResult ScoreInput(SortingModel sortingModel)
        {
            return this.RedirectToAction(
                "Results",
                "Home",
                new
                    {
                        sortDirection = sortingModel.SortingDirection
                    });
        }

        /// <summary>
        /// Results the specified scores models.
        /// </summary>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns>
        /// View
        /// </returns>
        public ActionResult Results(string sortDirection)
        {
            this.SortScores(sortDirection);

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

        /// <summary>
        /// Sorts the scores.
        /// </summary>
        /// <param name="sortDirection">The sort direction.</param>
        private void SortScores(string sortDirection)
        {
            var data = (List<StudentScoreModel>)this.Session["StudentScoreModels"];
            IEnumerable<StudentScoreModel> sortedData = sortDirection.Equals("ascending") ? data.OrderBy(x => x.StudentScore) : data.OrderByDescending(x => x.StudentScore);

            this.Session["StudentScoreModels"] = sortedData.ToList();
        }
    }
}