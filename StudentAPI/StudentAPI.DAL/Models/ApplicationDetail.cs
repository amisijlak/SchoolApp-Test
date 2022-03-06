using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.DAL.Models
{
    public class ApplicationDetail : BaseEntity
    {
        public int ApplicationMasterId { get; set; }
        public int CourseDetailsId { get; set; }
        public decimal CoursePrice { get; set; }
        public int Frequency { get; set; }

        public virtual CourseUnit CourseDetails { get; set; }
    }
}
