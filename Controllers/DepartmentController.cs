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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public IActionResult GetDepartments()
        {
            var courses = _departmentRepository.GetDepartments();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(courses);
        }


        [Authorize(Roles = "Teacher")]
        [HttpGet("get-department-by-Id/{departmentId}")]
        public IActionResult GetDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentIdExists(departmentId))
            {
                return NotFound();
            }

            var department = _departmentRepository.GetDepartment(departmentId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(department);
        }


        [Authorize(Roles = "Teacher")]
        [HttpPost("create-department")]
        public IActionResult CreateDepartment([FromBody] Department departmentCreate)
        {
            if (departmentCreate == null)
            {
                return BadRequest(ModelState);
            }

            var departmentId = _departmentRepository.GetDepartments()
        .Where(c => c.Id == departmentCreate.Id)
        .FirstOrDefault();

            if (departmentId != null)
            {
                ModelState.AddModelError("", "department already exists");
                return StatusCode(422, ModelState);
            }


            var nameofDepartment = _departmentRepository.GetDepartments()
          .Where(c => c.DepartmentName.Trim().ToUpper() == departmentCreate.DepartmentName.TrimEnd().ToUpper())
          .FirstOrDefault();

            if (nameofDepartment != null)
            {
                ModelState.AddModelError("", "Department already exists");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createDepartment = _departmentRepository.CreateDepartment(departmentCreate);

            if (createDepartment == false)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created");
        }



        [Authorize(Roles = "Teacher")]
        [HttpPut]
        public IActionResult UpdateDepartment(int departmentId, [FromBody] Department UpdatedDepartment)
        {
            if (UpdatedDepartment == null)
            {
                return BadRequest(ModelState);
            }

            if (UpdatedDepartment.Id != departmentId)
            {
                return BadRequest(ModelState);
            }

            if (!_departmentRepository.DepartmentIdExists(departmentId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                BadRequest();
            }

            var departmentUpdate = _departmentRepository.UpdateDepartment(UpdatedDepartment);

            if (!departmentUpdate)
            {
                ModelState.AddModelError("", "Something went wrong while updating department");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated");
        }



        [Authorize(Roles = "Teacher")]
        [HttpDelete("delete-department-by-Id")]
        public IActionResult DeleteDepartmentById(int departmentId)
        {
            if (!_departmentRepository.DepartmentIdExists(departmentId))
            {
                return NotFound();
            }

            var DepartmentToDelete = _departmentRepository.GetDepartment(departmentId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deletedDepartment = _departmentRepository.DeleteDepartment(DepartmentToDelete);

            if (deletedDepartment == false)
            {
                ModelState.AddModelError("", "Something went wrong deleting Department");
            }

            return Ok("Successfully Deleted");
        }
    }
}
