using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.DAL.Models
{
    public class Student : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName ="nvarchar(100)")]
        public string StudentName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
