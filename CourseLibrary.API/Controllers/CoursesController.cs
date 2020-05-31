using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Services;
using AutoMapper;
using CourseLibrary.API.Models;
using CourseLibrary.API.Entities;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesFromAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var coursesFromRepo = _courseLibraryRepository.GetCourses(authorId);

            return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesFromRepo));
        }

        [HttpGet("{courseId}", Name = "GetCourseFromAuthor")]
        public ActionResult<IEnumerable<CourseDto>> GetCourseFromAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

            if (courseFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CourseDto>(courseFromRepo));
        }

        [HttpPost]
        public ActionResult<CourseDto> CreateCourceForAuthors(Guid authorId, CourseForCerationDto course) 
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courceEntity = _mapper.Map<Course>(course);
            _courseLibraryRepository.AddCourse(authorId, courceEntity);
            _courseLibraryRepository.Save();

            var courseDto = _mapper.Map<CourseDto>(courceEntity);
            return CreatedAtRoute("GetCourseFromAuthor", new { AuthorId = authorId, CourseId = courseDto.Id }, courseDto);
        }

    }
}
