using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.API.Services;
using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Models;
using CourseLibrary.API.Helpers;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authorcollections")]
    public class AuthorCollectionsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorCollectionsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetAuthorsCollection")]
        public ActionResult GetAutorsCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> authorsIds)
        {
            if (authorsIds == null)
            {
                return BadRequest();
            }

            var authorsCollestion = _courseLibraryRepository.GetAuthors(authorsIds);

            if (authorsIds.Count() != authorsCollestion.Count())
            {
                return NotFound();
            }

            var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authorsCollestion);
            return Ok(authorsDto);
        }

        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection(IEnumerable<AuthorForCreationDto> authorsCollection)
        {
            var authors = _mapper.Map<IEnumerable<Author>>(authorsCollection);

            foreach (var author in authors)
            {
                _courseLibraryRepository.AddAuthor(author);
            }

            _courseLibraryRepository.Save();

            var authorsCollectionToReturn = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            var idsAsString = string.Join(",", authorsCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetAuthorsCollection", new { ids = idsAsString }, authorsCollectionToReturn);
        }

        [HttpOptions]
        public ActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
}
