using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCApp_Entities
{
    public class PatientEntities
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Enter valid number")]
        public decimal Dosage { get; set; }

        [Required]
        [RegularExpression(@"^\S+$", ErrorMessage = "White Spaces is not allowed")]
        [MaxLength(50, ErrorMessage = "Only 50 characters allowed")]
        public string Drug { get; set; }

        [Required]
        [Display(Name = "Patient Name")]
        [RegularExpression(@"^\S+$", ErrorMessage = "White Spaces is not allowed")]
        public string PatientName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
