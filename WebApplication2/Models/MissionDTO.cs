using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class MissionDTO
    {
        [Required]
        [DisplayName("ID")]
        public int Id { get; set; }
        
        [Required]
        [DisplayName("Title")]        
        public string Title { get; set; }
        
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }        

        [DisplayName("Finished")]
        public bool isDone { get; set; }

        [Required]
        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        [Required]
        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }
    }
}
