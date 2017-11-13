// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="Does Not Apply">
// Copyright (c) Matthew Thomas. All rights reserved.
// </copyright>
// <summary>
//   Settings.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace StudentScoreAnalyzerForTeachers
{
    using System.Configuration;

    /// <summary>
    /// Class Settings
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// The secret code key
        /// </summary>
        public const string SecretCodeKey = "SecretCode";

        /// <summary>
        /// The secret number of students key
        /// </summary>
        public const string SecretNumberOfStudentsKey = "SecretNumberOfStudents";

        /// <summary>
        /// The secret student number skip key
        /// </summary>
        public const string SecretStudentNumberSkipKey = "SecretStudentNumberSkip";

        /// <summary>
        /// The secret student names key
        /// </summary>
        public const string SecretStudentNamesKey = "SecretStudentNames";

        /// <summary>
        /// Gets the secret code.
        /// </summary>
        /// <value>
        /// The secret code.
        /// </value>
        public static int SecretCode
        {
            get
            {
                int secretCode;
                if (!int.TryParse(ConfigurationManager.AppSettings[SecretCodeKey], out secretCode))
                {
                    secretCode = 0;
                }

                return secretCode;
            }
        }

        /// <summary>
        /// Gets the secret number of students.
        /// </summary>
        /// <value>
        /// The secret number of students.
        /// </value>
        public static int SecretNumberOfStudents
        {
            get
            {
                int secretNumberOfStudents;
                if (!int.TryParse(ConfigurationManager.AppSettings[SecretNumberOfStudentsKey], out secretNumberOfStudents))
                {
                    secretNumberOfStudents = 0;
                }

                return secretNumberOfStudents;
            }
        }

        /// <summary>
        /// Gets the secret student number skip.
        /// </summary>
        /// <value>
        /// The secret student number skip.
        /// </value>
        public static int SecretStudentNumberSkip
        {
            get
            {
                int secretStudentNumberSkip;
                if (!int.TryParse(ConfigurationManager.AppSettings[SecretStudentNumberSkipKey], out secretStudentNumberSkip))
                {
                    secretStudentNumberSkip = 0;
                }

                return secretStudentNumberSkip;
            }
        }

        /// <summary>
        /// Gets the secret student names.
        /// </summary>
        /// <value>
        /// The secret student names.
        /// </value>
        public static string[] SecretStudentNames
        {
            get
            {
                var secretStudentNames = ConfigurationManager.AppSettings[SecretStudentNamesKey];
                var studentNameArray = secretStudentNames.Split(';');

                return studentNameArray;
            }
        }
    }
}