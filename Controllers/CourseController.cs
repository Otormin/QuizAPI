using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Model;
using QuizWebApiProject.Repository;

namespace QuizWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = _courseRepository.GetCourses();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(courses);
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("get-course-by-Id/{courseId}")]
        public IActionResult GetCourse(int courseId)
        {
            if (!_courseRepository.CourseIdExists(courseId))
            {
                return NotFound();
            }

            var course = _courseRepository.GetCourse(courseId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(course);
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("get-course-by-CourseCode{courseCode}")]
        public IActionResult GetCourseByCourseCode(string courseCode)
        {
            if (!_courseRepository.CourseCodeExists(courseCode))
            {
                return NotFound();
            }

            var course = _courseRepository.GetCourseByCourseCode(courseCode);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(course);
        }



        [Authorize(Roles = "Teacher")]
        [HttpPost ("create-course")]
        public IActionResult CreateCourse([FromBody] Course courseCreate)
        {
            if (courseCreate == null)
            {
                return BadRequest(ModelState);
            }

            var courseId = _courseRepository.GetCourses()
        .Where(c => c.Id == courseCreate.Id)
        .FirstOrDefault();

            if (courseId != null)
            {
                ModelState.AddModelError("", "course already exists");
                return StatusCode(422, ModelState);
            }


            var courseCode = _courseRepository.GetCourses()
        .Where(c => c.CourseCode.Trim().ToUpper() == courseCreate.CourseCode.TrimEnd().ToUpper())
        .FirstOrDefault();

            if (courseCode != null)
            {
                ModelState.AddModelError("", "course code already exists");
                return StatusCode(422, ModelState);
            }


            var courseTitle = _courseRepository.GetCourses()
          .Where(c => c.CourseTitle.Trim().ToUpper() == courseCreate.CourseTitle.TrimEnd().ToUpper())
          .FirstOrDefault();

            if (courseTitle != null)
            {
                ModelState.AddModelError("", "course already exists");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createCourse = _courseRepository.CreateCourse(courseCreate);

            if (createCourse == false)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }



        [Authorize(Roles = "Teacher")]
        [HttpPut]
        public IActionResult UpdateCourse(int courseId, [FromBody] Course UpdatedCourse)
        {
            if (UpdatedCourse == null)
            {
                return BadRequest(ModelState);
            }

            if (UpdatedCourse.Id != courseId)
            {
                return BadRequest(ModelState);
            }

            if (!_courseRepository.CourseIdExists(courseId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var courseUpdate = _courseRepository.UpdateCourse(UpdatedCourse);

            if (!courseUpdate)
            {
                ModelState.AddModelError("", "Something went wrong while updating Course");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }



        [Authorize(Roles = "Teacher")]
        [HttpDelete("delete-course-by-Id")]
        public IActionResult DeleteCourseById(int courseId)
        {
            if (!_courseRepository.CourseIdExists(courseId))
            {
                return NotFound();
            }

            var CourseToDelete = _courseRepository.GetCourse(courseId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleteCourse = _courseRepository.DeleteCourse(CourseToDelete);

            if (deleteCourse == false)
            {
                ModelState.AddModelError("", "Something went wrong deleting Course");
            }

            return Ok("Successfully Deleted");
        }



        [Authorize(Roles = "Teacher")]
        [HttpDelete("delete-course-by-courseCode")]
        public IActionResult DeleteCourseByCourseCode(string courseCode)
        {
            if (!_courseRepository.CourseCodeExists(courseCode))
            {
                return NotFound();
            }

            var CourseToDelete = _courseRepository.GetCourseByCourseCode(courseCode);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleteCourse = _courseRepository.DeleteCourse(CourseToDelete);

            if (deleteCourse == false)
            {
                ModelState.AddModelError("", "Something went wrong deleting Course");
            }

            return Ok("Successfully Deleted");
        }
    }
}
