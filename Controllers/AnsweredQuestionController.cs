using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Repository;

namespace QuizWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnsweredQuestionController : ControllerBase
    {
        private readonly IAnsweredQuestionRepository _answeredQuestionRepository;

        public AnsweredQuestionController(IAnsweredQuestionRepository answeredQuestionRepository)
        {
            _answeredQuestionRepository = answeredQuestionRepository;
        }



        [Authorize(Roles = "Teacher")]
        [HttpDelete("delete-SelectedQuestions-by-Id")]
        public IActionResult DeleteSelectedQuestionById(int selectedQuestionId)
        {

            if (!_answeredQuestionRepository.SelectedQuestionExistsById(selectedQuestionId))
            {
                return NotFound();
            }

            var SelectedCourseToDelete = _answeredQuestionRepository.GetSelectedQuestionById(selectedQuestionId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deleteSelectedQuestion = _answeredQuestionRepository.DeleteSelectedQuestions(SelectedCourseToDelete);

            if (deleteSelectedQuestion == false)
            {
                ModelState.AddModelError("", "Something went wrong deleting selected question");
            }

            return Ok("Successfully Deleted");
        }

    }
}
