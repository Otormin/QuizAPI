using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizWebApiProject.Data;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Repository;

namespace QuizWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentScoreController : ControllerBase
    {
        private readonly IQuestionAndAnswerRepository _studentQuestionAndAnswer;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentScoreRepository _studentScoreRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public StudentScoreController(IQuestionAndAnswerRepository questionAndAnswer, ICourseRepository courseRepository, IStudentScoreRepository studentScoreRepository, IMapper mapper, DataContext context)
        {
            _studentQuestionAndAnswer = questionAndAnswer;
            _courseRepository = courseRepository;
            _studentScoreRepository = studentScoreRepository;
            _mapper = mapper;
            _context = context;
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public IActionResult GetStudentScores()
        {
            var scores = _studentScoreRepository.GetStudentScores();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(scores);
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("get-Scores-by-MatricNumber/{MatricNumber}")]
        public IActionResult GetScoreByMatricNumber(string MatricNumber)
        {
            if (!_studentScoreRepository.MatricNumberExists(MatricNumber))
            {
                return NotFound();
            }

            var score = _studentScoreRepository.GetScoreByMatricNumber(MatricNumber);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(score);
        }


        [Authorize(Roles = "Teacher")]
        [HttpDelete("delete-Score-by-MatricNumber")]
        public IActionResult DeleteScoreByMatricNumber(string MatricNumber)
        {
            if (!_studentScoreRepository.MatricNumberExists(MatricNumber))
            {
                return NotFound();
            }

            var ScoreToDelete = _studentScoreRepository.GetScoreByMatricNumber(MatricNumber);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleteScore = _studentScoreRepository.DeleteScore(ScoreToDelete);

            if (deleteScore == false)
            {
                ModelState.AddModelError("", "Something went wrong deleting Score");
            }

            return Ok("Successfully Deleted");
        }


        [Authorize(Roles = "Teacher")]
        [HttpDelete("delete--Score-by-Id")]
        public IActionResult DeleteScoreById(int Id)
        {

            if (!_studentScoreRepository.StudentScoreExistsById(Id))
            {
                return NotFound();
            }

            var StudentScoreToDelete = _studentScoreRepository.GetStudentScoreById(Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleteStudentScore = _studentScoreRepository.DeleteScore(StudentScoreToDelete);

            if (deleteStudentScore == false)
            {
                ModelState.AddModelError("", "Something went wrong deleting student score");
            }

            return Ok("Successfully Deleted");
        }
    }
}
