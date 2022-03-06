using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.DAL.Models
{
    public class CourseUnit : BaseEntity
    {
        [Column(TypeName ="nvarchar(100)")]
        public string CourseName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string CourseCode { get; set; }

        public decimal Price { get; set; }
    }
}
