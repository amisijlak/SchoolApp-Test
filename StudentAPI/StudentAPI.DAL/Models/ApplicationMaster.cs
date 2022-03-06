using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.DAL.Models
{
    public class ApplicationMaster: BaseEntity
    {
        [Column(TypeName = "nvarchar(75)")]
        public string ApplicationNumber { get; set; }
        public int StudentId { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string TeachingMethod { get; set; }

        public decimal GrandTotal { get; set; }

        public virtual List<ApplicationDetail> ApplicationDetails { get; set; }

        [NotMapped]
        public string DeletedMasterDetailsIds { get; set; }
        public Student Student { get; set; }
    }
}
