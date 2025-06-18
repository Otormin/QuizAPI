using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWebApiProject.Dto;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Model;
using QuizWebApiProject.Repository;
using System.Collections.Generic;
using System.Data;

namespace QuizWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IQuestionAndAnswerRepository _questionAndAnswerRepository;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepository, ICourseRepository courseRepository, IQuestionAndAnswerRepository questionAndAnswerRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _courseRepository = courseRepository;
            _questionAndAnswerRepository = questionAndAnswerRepository;
            _mapper = mapper;
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public IActionResult GetTeachers()
        {
            var teachers = _mapper.Map<List<TeacherDto>>(_teacherRepository.GetTeachers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(teachers);
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("Get-teacher-by-staffId/{teacherStaffId}")]
        public IActionResult GetTeacherByStaffId(string teacherStaffId)
        {
            if (!_teacherRepository.TeacherStaffIdExists(teacherStaffId))
            {
                return NotFound();
            }

            var teacher = _mapper.Map<TeacherDto>(_teacherRepository.GetTeacherByStaffId(teacherStaffId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(teacher);
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("Get-teachers-by-courseCode/{teacherCourseCode}")]
        public IActionResult GetTeachersByCourseCode(string teacherCourseCode)
        {
            if (!_teacherRepository.TeacherCourseExists(teacherCourseCode))
            {
                return NotFound();
            }

            var teacher = _mapper.Map<List<TeacherDto>>(_teacherRepository.GetTeachersByCourseCode(teacherCourseCode));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(teacher);
        }


        [Authorize(Roles = "Teacher")]
        [HttpDelete("delete-teacher-by-staffId")]
        public IActionResult DeleteTeacherByStaffId(string teacherStaffId)
        {
            if (!_teacherRepository.TeacherStaffIdExists(teacherStaffId))
            {
                return NotFound();
            }

            var TeacherToDelete = _teacherRepository.GetTeacherByStaffId(teacherStaffId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleteteacher = _teacherRepository.DeleteTeacher(TeacherToDelete);

            if (deleteteacher == false)
            {
                ModelState.AddModelError("", "Something went wrong deleting Teacher");
            }

            return Ok("Successfully Deleted");
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("Get-students")]
        public IActionResult GetStudentss()
        {
            var teachers = _mapper.Map<List<StudentDto>>(_teacherRepository.GetStudents());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(teachers);
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("Get-student-by-matricNumber/{studentMatricNumber}")]
        public IActionResult GetStudentByMatricNumber(string studentMatricNumber)
        {
            if (!_teacherRepository.StudentMatricNumberExists(studentMatricNumber))
            {
                return NotFound();
            }

            var teacher = _mapper.Map<StudentDto>(_teacherRepository.GetStudentByMatricNumber(studentMatricNumber));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(teacher);
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("Get-students-by-department/{studentDepartment}")]
        public IActionResult GetStudentsByDepartment(string studentDepartment)
        {
            if (!_teacherRepository.TeacherCourseExists(studentDepartment))
            {
                return NotFound();
            }

            var teacher = _mapper.Map<List<StudentDto>>(_teacherRepository.GetStudentsByDepartment(studentDepartment));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(teacher);
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("Get-QuestionsAndAnswers")]
        public IActionResult GetQuestionsAndAnswers()
        {
            var teachers = _questionAndAnswerRepository.GetQuestionsAndAnswers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(teachers);
        }


        [Authorize(Roles = "Teacher")]
        [HttpPost("Questions")]
        public IActionResult CreateQuestionAndAnswer([FromBody] QuestionAndAnswer questionsCreate)
        {
            if (questionsCreate == null)
            {
                return BadRequest(ModelState);
            }


            var courseCode = _courseRepository.GetCourses()
        .Where(c => c.CourseCode.Trim().ToUpper() == questionsCreate.CourseCode.TrimEnd().ToUpper())
        .FirstOrDefault();
          
            if (courseCode == null)
            {
                ModelState.AddModelError("", "Course does not exist");
                return StatusCode(422, ModelState);
            }

            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            else
            {
                var createTeacher = _questionAndAnswerRepository.CreateQuestionsAndAnswers(questionsCreate);
                return Ok("Successfully Created");
            }
        }


        [Authorize(Roles = "Teacher")]
        [HttpPut("Update-Question")]
        public IActionResult UpdateQuestion(int questionId, [FromBody] QuestionAndAnswer UpdatedQuestion)
        {
            var teacherCourseCode = _courseRepository.GetCourses()
      .Where(c => c.CourseCode.Trim().ToUpper() == UpdatedQuestion.CourseCode.TrimEnd().ToUpper())
      .FirstOrDefault();

            if (UpdatedQuestion == null)
            {
                return BadRequest(ModelState);
            }

            if (UpdatedQuestion.Id != questionId)
            {
                return BadRequest(ModelState);
            }

            if (!_questionAndAnswerRepository.QuestionAndAnswerExists(questionId))
            {
                return NotFound();
            }

            if (teacherCourseCode == null)
            {
                ModelState.AddModelError("", "Course does not exist");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var questionAndAnswerUpdate = _questionAndAnswerRepository.UpdateQuestionsAndAnswers(UpdatedQuestion);

            if (!questionAndAnswerUpdate)
            {
                ModelState.AddModelError("", "Something went wrong while updating category");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }


        [Authorize(Roles = "Teacher")]
        [HttpDelete("delete-QuestionsAndAnswers-by-Id")]
        public IActionResult DeleteQuestionAndAnswerById(int questionAndAnswerID)
        {
            if (!_questionAndAnswerRepository.QuestionAndAnswerExists(questionAndAnswerID))
            {
                return NotFound();
            }

            var questionAndAnswerToDelete = _questionAndAnswerRepository.GetQuestionAndAnswer(questionAndAnswerID);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleteQuestionAndAnswer = _questionAndAnswerRepository.DeleteQuestionAndAnswers(questionAndAnswerToDelete);

            if (deleteQuestionAndAnswer == false)
            {
                ModelState.AddModelError("", "Something went wrong deleting Teacher");
            }

            return Ok("Successfully Deleted");
        }
    }
}
