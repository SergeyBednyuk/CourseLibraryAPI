using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseTitleMustBeDifferentFromDescription(ErrorMessage = "The Title equals the description")]
    public class CourseForCerationDto /*: IValidatableObject*/
    {
        [Required(ErrorMessage = "You should fill out the title")]
        [MaxLength(100, ErrorMessage ="The description souldn't have more than 100 characters")]
        public string Title { get; set; }
        [MaxLength(1500)]
        public string Description { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title == Description)
        //    {
        //        yield return new ValidationResult("The Title equals the description", new [] { "CourseForCerationDto" });
        //    }
        //}
    }
}
