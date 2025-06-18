using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizWebApiProject.Data;
using QuizWebApiProject.Dto;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Model;
using QuizWebApiProject.Repository;
using System.Diagnostics.Metrics;
using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using QuizWebApiProject.Migrations;
using QuizWebApiProject.Services;
using Microsoft.Win32;
using System.Security.Claims;


namespace QuizWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionAndAnswerController : ControllerBase
    {
        private readonly IQuestionAndAnswerRepository _studentQuestionAndAnswer;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentScoreRepository _studentScoreRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IAnsweredQuestionRepository _answeredQuestionRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuestionAndAnswerController(IQuestionAndAnswerRepository questionAndAnswer, ICourseRepository courseRepository, IStudentScoreRepository studentScoreRepository, IMapper mapper, DataContext context, IAnsweredQuestionRepository answeredQuestionRepository, ITeacherRepository teacherRepository, IMailService mailService, IHttpContextAccessor httpContextAccessor)
        {
            _studentQuestionAndAnswer = questionAndAnswer;
            _courseRepository = courseRepository;
            _studentScoreRepository = studentScoreRepository;
            _mapper = mapper;
            _context = context;
            _answeredQuestionRepository = answeredQuestionRepository;
            _teacherRepository = teacherRepository;
            _mailService = mailService;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet("Get-QuestionAndAnswer-by-courseCode/{QuestionAndAnswerCourseCode}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<QuestionAndAnswer>))]
        [ProducesResponseType(400)]
        public IActionResult GetQuestionsByCourseCode(string QuestionAndAnswerCourseCode)
        {
            /* Mapping manually

            var coursessscode = new QuestionAndAnswer()
            {
                Id = QuestionAndAnswerCourseCode.Id,
                CourseCode = QuestionAndAnswerCourseCode.CourseCode,
                Question = QuestionAndAnswerCourseCode.Question,
                OptionA = QuestionAndAnswerCourseCode.OptionA,
                OptionB = QuestionAndAnswerCourseCode.OptionB,
                OptionC = QuestionAndAnswerCourseCode.OptionC,
                OptionD = QuestionAndAnswerCourseCode.OptionD,
            }
             */

            if (!_studentQuestionAndAnswer.QuestionAndAnswerCourseExists(QuestionAndAnswerCourseCode))
            {
                return NotFound();
            }

            var question = _mapper.Map<List<QuestionsDto>>(_studentQuestionAndAnswer.GetQuestionAndAnswerByCourseCode(QuestionAndAnswerCourseCode));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(question);
        }


        [HttpPost("Input-Answers")]
        public IActionResult CreateStudentAnswer([FromBody] AnswersDto studentAnswer)
        {
            int score = 0;
            var amountOfQuestions = _studentQuestionAndAnswer.GetQuestionAndAnswerByCourseCode(studentAnswer.CourseCode).Count();
            var userName = _httpContextAccessor.HttpContext.User.ToString();


            var currentUserr = _httpContextAccessor.HttpContext.User;
            var userNameClaim = currentUserr.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");

            if (userNameClaim == null)
            {
                return StatusCode(500, ModelState);
                // Handle the case where username claim is not found
            }

            var currentUser = userNameClaim.Value.ToString();
            // Now you have the username of the current user


            //this is to get the current users email
            var currentUsername = _httpContextAccessor.HttpContext.User;
            var userEmail = currentUsername.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var studentAnswerCourseCode = _courseRepository.GetCourses()
           .Where(c => c.CourseCode.Trim().ToUpper() == studentAnswer.CourseCode.TrimEnd().ToUpper())
           .FirstOrDefault();

            if (studentAnswerCourseCode == null)
            {
                ModelState.AddModelError("", "Course does not exist");
                return StatusCode(422, ModelState);
            }

            if (!_studentQuestionAndAnswer.QuestionAndAnswerExists(studentAnswer.QuestionNumber))
            {
                return NotFound();
            }

            // Create new matric number, course code and score when a new matric number is entered 
            if (!_studentScoreRepository.MatricNumberExists(currentUser))
            {
                var selectedQuestionExists = _answeredQuestionRepository.SelectedQuestionExists(currentUser, studentAnswer.QuestionNumber, studentAnswer.CourseCode);

                if (selectedQuestionExists == true)
                {
                    return BadRequest("You have already answered this question.");
                }
                else
                {
                    var questionAnswered = _answeredQuestionRepository.CreateSelectedQuestion(currentUser, studentAnswer.QuestionNumber, studentAnswer.CourseCode);
                }
                //create
                var newStudentScore = new StudentScore
                {
                    CourseCode = studentAnswer.CourseCode,
                    MatricNumber = currentUser
                };

                if (!_studentScoreRepository.CreateStudentScore(newStudentScore))
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }

                var teacherAnswer = _studentQuestionAndAnswer.GetAnswersByQuestionId(studentAnswer.QuestionNumber).ToString().ToUpper();

                if (studentAnswer.Answer == teacherAnswer)
                {
                    var studentScore = _studentScoreRepository.CreateScore(currentUser, ++score);
                    if (!studentScore)
                    {
                        ModelState.AddModelError("", "Something went wrong while saving your score");
                        return StatusCode(500, ModelState);
                    }
                }

                else
                {
                    var studentScore = _studentScoreRepository.CreateScore(currentUser, score);
                    if (!studentScore)
                    {
                        ModelState.AddModelError("", "Something went wrong while saving your score");
                        return StatusCode(500, ModelState);
                    }
                }
            }

            else
            {
                // preventing the same question from being answered twice
                var selectedQuestionExists = _answeredQuestionRepository.SelectedQuestionExists(currentUser, studentAnswer.QuestionNumber, studentAnswer.CourseCode);

                if (selectedQuestionExists == true)
                {
                    return BadRequest("You have already answered this question.");
                }
                else
                {
                    var questionAnswered = _answeredQuestionRepository.CreateSelectedQuestion(currentUser, studentAnswer.QuestionNumber, studentAnswer.CourseCode);
                }

                //update
                var teacherAnswer = _studentQuestionAndAnswer.GetAnswersByQuestionId(studentAnswer.QuestionNumber).ToString().ToUpper();
                var studentScoreByMatricNumber = _studentScoreRepository.GetScoreByMatricNumber(currentUser);

                if (studentAnswer.Answer == teacherAnswer)
                {
                    var studentscore = _studentScoreRepository.UpdateScore(currentUser, studentScoreByMatricNumber.Score + 1);
                    if (!studentscore)
                    {
                        ModelState.AddModelError("", "Something went wrong while saving your score");
                        return StatusCode(500, ModelState);
                    }
                }

                else
                {
                    var studentscore = _studentScoreRepository.UpdateScore(currentUser, studentScoreByMatricNumber.Score);
                    if (!studentscore)
                    {
                        ModelState.AddModelError("", "Something went wrong while saving your score");
                        return StatusCode(500, ModelState);
                    }
                }
            }

            var questions = _studentQuestionAndAnswer.GetQuestionAndAnswer(studentAnswer.QuestionNumber);

            if (questions.OptionA != null || questions.OptionB != null || questions.OptionC != null || questions.OptionD != null) 
            { 
                if (studentAnswer.Answer != "A" && studentAnswer.Answer != "B" && studentAnswer.Answer != "C" && studentAnswer.Answer != "D")
                {
                    return BadRequest("Input either option A, B, C, D");
                }
            }

            var totalAmountOfQuestionsAnswered = _answeredQuestionRepository.TotalAmountOfQuestionsAnswered(currentUser, studentAnswer.CourseCode);
            var getStudentFinalScore = _studentScoreRepository.GetScoreByMatricNumber(currentUser);
            var studentFinalScore = getStudentFinalScore.Score;


            if (totalAmountOfQuestionsAnswered == amountOfQuestions)
            {
               _mailService.SendEmailAsync(userEmail, studentAnswer.CourseCode, "<h1> Hey! " + currentUser + ", You have successfully taken the " + studentAnswer.CourseCode + " test" +"</h1><p>Your score is " + studentFinalScore + "/" + amountOfQuestions +"</p>");
            }

            return Ok("Your score has been updated");
        }
    }
}