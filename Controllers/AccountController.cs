using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using QuizWebApiProject.Dto;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Model;
using QuizWebApiProject.Services;
using QuizWebApiProject.ViewModel;

namespace QuizWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMailService _mailService;

        public AccountController(UserManager<User> userManager, TokenService tokenService, ICourseRepository courseRepository, IDepartmentRepository departmentRepository, IMailService mailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
            _mailService = mailService;
        }


        [HttpPost("Login-Teacher")]
        public async Task<ActionResult<UserDto>> LoginTeacher(LoginTeacherViewModel login)
        {
            //checking if a user exists in the database
            var user = await _userManager.FindByNameAsync(login.StaffId);
            //checking if the person exists and checking if the password is the same as the one in the database
            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return Unauthorized();
            }
            //if user exists
            else
            {
                return new UserDto
                {
                    Email = user.Email,
                    Token = await _tokenService.GenerateToken(user)
                };
            }
        }


        [HttpPost("Login-Student")]
        public async Task<ActionResult<UserDto>> LoginStudent(LoginStudentViewModel login)
        {
            //checking if a user exists in the database
            var user = await _userManager.FindByNameAsync(login.MatricNumber);
            //checking if the person exists and checking if the password is the same as the one in the database
            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return Unauthorized();
            }
            //if user exists
            else 
            {
                //await _mailService.SendEmailAsync(login.Email, "Welcome to Jesse Classroom", "<h1> Hey! " + login.MatricNumber + "</h1><p>You logged in to your Jesse classroom account by " + DateTime.Now +"</p>");

                return new UserDto
                {
                    Email = user.Email,
                    Token = await _tokenService.GenerateToken(user)
                };
            }
        }

        [HttpPost("Register-Student")]
        //since we want to ask the user to login immediately after being registered we will just return Task<ActionResult> 
        public async Task<ActionResult> RegsiterStudent(RegisterStudentViewModel register)
        {
            //creating matric number 
            Random random = new Random();
            int lowestNumber = 10000;
            int highestNumber = 100000;
            string randomNumber = random.Next(lowestNumber, highestNumber).ToString();

            string matricNumber = DateTime.Now.Year.ToString() + "/" + randomNumber.ToString();
            //checking if a user exists in the database
            var checkingUser = await _userManager.FindByNameAsync(matricNumber);

            while(checkingUser != null)
            {
                randomNumber = random.Next(lowestNumber, highestNumber).ToString();
                matricNumber = DateTime.Now.Year.ToString() + "/" + randomNumber.ToString();
            }

            //creating user
            var user = new User 
            { 
                UserName = matricNumber, 
                Email = register.Email, 
                MatricNumber = matricNumber,
                Department = register.Department,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Gender = register.Gender,
            };

            var studentDepartment = _departmentRepository.GetDepartments()
        .Where(c => c.DepartmentName.Trim().ToUpper() == register.Department.TrimEnd().ToUpper())
        .FirstOrDefault();

            if (studentDepartment == null)
            {
                ModelState.AddModelError("", "Department does not exist");
                return StatusCode(422, ModelState);
            }

            var result = await _userManager.CreateAsync(user, register.Password);
            
            //if the user did not create successfully
            if (!result.Succeeded)
            {
                //print out these list of errors depending on what the user failed to do
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);           
                }

                return ValidationProblem();
            }
            //if it succedded
            else
            {
                await _mailService.SendEmailAsync(register.Email, "Welcome to Jesse Classroom", "<h1> Hey! " + register.FirstName +" "+ register.LastName + ", You have been registered as a student successfully</h1><p>Your matriculation number is " + matricNumber + "</p>");
                await _userManager.AddToRoleAsync(user, "Student");
            }

            return Ok("You have been registered successfully, your Matriculation Number is " + matricNumber);
        }

        [HttpPost("Register-Teacher")]
        //since we want to ask the user to login immediately after being registered we will just return Task<ActionResult> 
        public async Task<ActionResult> RegsiterTeacher(RegisterTeacherViewModel register)
        {
            //creating matric number 
            Random random = new Random();
            int lowestNumber = 1000;
            int highestNumber = 10000;
            string staffId = random.Next(lowestNumber, highestNumber).ToString();

            var checkingUser = await _userManager.FindByNameAsync(staffId);

            while (checkingUser != null)
            {
                staffId = random.Next(lowestNumber, highestNumber).ToString();
            }

            //creating user
            var user = new User 
            { 
                UserName = staffId, 
                Email = register.Email, 
                StaffId = staffId,
                CourseCode = register.CourseCode,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Gender = register.Gender,
            };

            var teacherCourseCode = _courseRepository.GetCourses()
        .Where(c => c.CourseCode.Trim().ToUpper() == register.CourseCode.TrimEnd().ToUpper())
        .FirstOrDefault();

            if (teacherCourseCode == null)
            {
                ModelState.AddModelError("", "Course does not exist");
                return StatusCode(422, ModelState);
            }

            var result = await _userManager.CreateAsync(user, register.Password);

            //if the user did not create successfully
            if (!result.Succeeded)
            {
                //print out these list of errors depending on what the user failed to do
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }
            //if it succedded
            else
            {
                await _mailService.SendEmailAsync(register.Email, "Welcome to Jesse Classroom", "<h1> Hey! " + register.FirstName +" "+ register.LastName + ", You have been registered as a teacher successfully</h1><p>Your Staff ID is " + staffId + "</p>");
                await _userManager.AddToRoleAsync(user, "Teacher");
            }

            return Ok("You have been registered successfully, your staff Id is " + staffId);
        }

        //[Authorize] protects endpoints from people that have not logged in
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user)
            };
        }
    }
}



/*
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizWebApiProject.Dto;
using QuizWebApiProject.Model;
using QuizWebApiProject.Services;
using QuizWebApiProject.ViewModel;

namespace QuizWebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<User> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginViewModel login)
        {
            //checking if a user exists in the database
            var user = await _userManager.FindByNameAsync(login.Username);
            //checking if the person exists and checking if the password is the same as the one in the database
            if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
            {
                return Unauthorized();
            }
            //if user exists
            else 
            {
                return new UserDto
                {
                    Email = user.Email,
                    Token = await _tokenService.GenerateToken(user)
                };
            }
        }

        [HttpPost("Register")]
        //since we want to ask the user to login immediately after being registered we will just return Task<ActionResult> 
        public async Task<ActionResult> Regsiter(RegisterViewModel register)
        {
            //creating user
            var user = new User { UserName = register.Username, Email = register.Email};

            var result = await _userManager.CreateAsync(user, register.Password);
            
            //if the user did not create successfully
            if (!result.Succeeded)
            {
                //print out these list of errors depending on what the user failed to do
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);           
                }

                return ValidationProblem();
            }
            //if it succedded
            await _userManager.AddToRoleAsync(user, "Student");

            return StatusCode(201);
        }

        //[Authorize] protects endpoints from people that have not logged in
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.GenerateToken(user)
            };
        }
    }
}
*/