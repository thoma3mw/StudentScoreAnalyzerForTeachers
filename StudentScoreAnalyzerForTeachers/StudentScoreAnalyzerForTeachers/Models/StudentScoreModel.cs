// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StudentScoreModel.cs" company="Does Not Apply">
// Copyright (c) Matthew Thomas. All rights reserved.
// </copyright>
// <summary>
//   StudentScoreModel.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StudentScoreAnalyzerForTeachers.Models
{
    /// <summary>
    /// Class StudentScoreModel
    /// </summary>
    public class StudentScoreModel
    {
        /// <summary>
        /// Gets or sets the student number.
        /// </summary>
        /// <value>
        /// The student number.
        /// </value>
        public int StudentNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the student.
        /// </summary>
        /// <value>
        /// The name of the student.
        /// </value>
        public string StudentName { get; set; }

        /// <summary>
        /// Gets or sets the student score.
        /// </summary>
        /// <value>
        /// The student score.
        /// </value>
        public decimal StudentScore { get; set; }

        /// <summary>
        /// Gets or sets the goal group.
        /// </summary>
        /// <value>
        /// The goal group.
        /// </value>
        public string GoalGroup { get; set; }

        /// <summary>
        /// Gets or sets the group number.
        /// </summary>
        /// <value>
        /// The group number.
        /// </value>
        public int GroupNumber { get; set; }
    }
}