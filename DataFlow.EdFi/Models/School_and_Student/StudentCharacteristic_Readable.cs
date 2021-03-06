using System;

namespace DataFlow.EdFi.Models.School_and_Student 
{
    public class StudentCharacteristic_Readable 
    {
        /// <summary>
        /// The characteristic designated for the student.
        /// </summary>
        public string descriptor { get; set; }

        /// <summary>
        /// The date the characteristic was designated.
        /// </summary>
        public DateTime? beginDate { get; set; }

        /// <summary>
        /// The date the characteristic was removed.
        /// </summary>
        public DateTime? endDate { get; set; }

        /// <summary>
        /// The person, organization, or department that made a student designation.
        /// </summary>
        public string designatedBy { get; set; }

        }
}

