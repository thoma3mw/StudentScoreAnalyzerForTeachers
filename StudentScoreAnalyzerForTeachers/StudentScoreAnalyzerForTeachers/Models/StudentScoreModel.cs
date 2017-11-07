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
        /// Gets or sets the student score.
        /// </summary>
        /// <value>
        /// The student score.
        /// </value>
        public decimal StudentScore { get; set; }
    }
}