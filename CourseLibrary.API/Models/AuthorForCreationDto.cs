using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class AuthorForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirthDay { get; set; }
        public string MainCategory { get; set; }
        public ICollection<CourseForCerationDto> Courses { get; set; }
            = new List<CourseForCerationDto>();
    }
}
