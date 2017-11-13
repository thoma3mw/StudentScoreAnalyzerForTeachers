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
    using System;
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
        /// Returns the About view
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
        /// Returns the Contact view
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
        /// Returns the Home view
        /// </summary>
        /// <returns>
        /// View
        /// </returns>
        public ActionResult Index()
        {
            return this.View(new HomePageModel());
        }

        /// <summary>
        /// Redirects to the ScoreInput view
        /// </summary>
        /// <param name="numberOfStudentsModel">The number of students model.</param>
        /// <returns>
        /// Action
        /// </returns>
        [HttpPost]
        public ActionResult Index(HomePageModel numberOfStudentsModel)
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
        /// Returns the ScoreInput view
        /// </summary>
        /// <param name="numberOfStudents">The number of students.</param>
        /// <returns>
        /// View
        /// </returns>
        public ActionResult ScoreInput(int numberOfStudents)
        {
            if (numberOfStudents == Settings.SecretCode)
            {
                this.InitializeScoreModelsForSecretCode();
            }
            else
            {
                this.InitializeStudentScoreModels(numberOfStudents);
            }

            return this.View(new ScoreInputModel());
        }

        /// <summary>
        /// Updates the score models
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
                var models = (List<StudentScoreModel>)this.Session["StudentScoreModels"];

                foreach (var model in models)
                {
                    if (studentNumbersChanged.Contains(model.StudentNumber))
                    {
                        var changedModel = changed.First(x => x.StudentNumber.Equals(model.StudentNumber));
                        model.StudentName = changedModel.StudentName;
                        model.StudentScore = changedModel.StudentScore;
                    }
                }
            }

            return this.Json(this.Session["StudentScoreModels"], JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Redirects to the Results view
        /// </summary>
        /// <param name="sortingModel">The sorting model.</param>
        /// <returns>
        /// Action
        /// </returns>
        [HttpPost]
        public ActionResult ScoreInput(ScoreInputModel sortingModel)
        {
            return this.RedirectToAction(
                "Results",
                "Home",
                new
                    {
                        scoreGoal = sortingModel.ScoreGoal
                    });
        }

        /// <summary>
        /// Returns the Results view
        /// </summary>
        /// <param name="scoreGoal">The score goal.</param>
        /// <returns>
        /// View
        /// </returns>
        public ActionResult Results(decimal scoreGoal)
        {
            this.GroupBasedOnGoal(scoreGoal);
            this.ViewBag.Goal = scoreGoal;

            return this.View(new ResultsModel());
        }

        /// <summary>
        /// Redirects to the Groups view
        /// </summary>
        /// <param name="resultsModel">The results model.</param>
        /// <returns>
        /// Action
        /// </returns>
        [HttpPost]
        public ActionResult Results(ResultsModel resultsModel)
        {
            return this.RedirectToAction(
                "Groups",
                "Home",
                new
                    {
                        numberOfGroups = resultsModel.NumberOfGroups
                    });
        }

        /// <summary>
        /// Returns the Groups view
        /// </summary>
        /// <param name="numberOfGroups">The number of groups.</param>
        /// <returns>
        /// View
        /// </returns>
        public ActionResult Groups(int numberOfGroups)
        {
            this.CreateGroups(numberOfGroups);
            this.Session["NumberOfGroups"] = numberOfGroups;

            return this.View();
        }

        /// <summary>
        /// Updates the changed score model
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>View</returns>
        public ActionResult Update(StudentScoreModel value)
        {
            var models = (List<StudentScoreModel>)this.Session["StudentScoreModels"];

            var model = models.First(x => x.StudentNumber.Equals(value.StudentNumber));
            model.GroupNumber = value.GroupNumber;

            return this.Json(this.Session["StudentScoreModels"], JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Initializes the score models if the secret code is given.
        /// </summary>
        private void InitializeScoreModelsForSecretCode()
        {
            var numberOfStudents = Settings.SecretNumberOfStudents;
            var numberToSkip = Settings.SecretStudentNumberSkip;
            var studentNames = Settings.SecretStudentNames;

            var data = new List<StudentScoreModel>();
            for (var i = 0; i < numberOfStudents; i++)
            {
                data.Add(
                    new StudentScoreModel
                        {
                            StudentNumber = i + 1,
                            StudentName = studentNames[i],
                            StudentScore = decimal.Zero
                        });
            }

            if (numberToSkip != 0)
            {
                var modelToRemove = data.First(m => m.StudentNumber.Equals(numberToSkip));
                data.Remove(modelToRemove);
            }

            this.Session["StudentScoreModels"] = data;
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
        /// Groups the students based on the goal.
        /// </summary>
        /// <param name="goal">The goal.</param>
        private void GroupBasedOnGoal(decimal goal)
        {
            // -1 mean below goal, 0 means at goal, 1 means above goal
            var data = (List<StudentScoreModel>)this.Session["StudentScoreModels"];
            foreach (var model in data)
            {
                if (model.StudentScore == goal)
                {
                    model.GoalGroup = "At Goal";
                }
                else if (model.StudentScore < goal)
                {
                    model.GoalGroup = "Below Goal";
                }
                else
                {
                    model.GoalGroup = "Above Goal";
                }
            }

            this.Session["StudentScoreModels"] = data;
        }

        /// <summary>
        /// Creates the groups.
        /// </summary>
        /// <param name="numberOfGroups">The number of groups.</param>
        private void CreateGroups(int numberOfGroups)
        {
            var data = (List<StudentScoreModel>)this.Session["StudentScoreModels"];
            var numberOfStudents = data.Count;
            var studentsPerGroup = (decimal)numberOfStudents / numberOfGroups;
            var roundedStudentsPerGroup = (int)Math.Round(studentsPerGroup, 0);
            var studentsLeft = numberOfStudents - (numberOfGroups * roundedStudentsPerGroup);
            var tempData = data.OrderByDescending(x => x.StudentScore)
                .ToList();

            for (var i = 0; i < numberOfGroups; i++)
            {
                var students = roundedStudentsPerGroup;

                if (studentsLeft < 0 && i == numberOfGroups + studentsLeft)
                {
                    students--;
                    studentsLeft++;
                }

                if (studentsLeft > 0 && i <= studentsLeft)
                {
                    students++;
                    studentsLeft--;
                }

                var group = tempData.Take(students)
                    .ToList();
                foreach (var model in group)
                {
                    model.GroupNumber = i + 1;
                }

                tempData.RemoveRange(0, students);
            }
        }
    }
}