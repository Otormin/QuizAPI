using QuizWebApiProject.Data;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Model;

namespace QuizWebApiProject.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext context)
        {
            _context = context;
        }

        /*
        //you can use this method to get all the teachers if they have the role teacher
         public ICollection<User> GetTeacherRegister()
        {
            //var role = _context.Roles.FirstOrDefault(x => x.Name == "Teacher");
            //var userRoles = _context.UserRoles.Where(x => x.RoleId == role.Id);
            //var users = from user in _context.Users join
            //             userRole in _context.UserRoles.Where(x => x.RoleId == role.Id) on user.Id equals userRole.UserId
            var usersWithRole = from userRole in _context.UserRoles
                                join user in _context.Users on userRole.UserId equals user.Id
                                join role in _context.Roles on userRole.RoleId equals role.Id
                                where role.Name == "Teacher"
                                select user;

            return usersWithRole.ToList();
        }*/

        /*
        //you can use this method to get all the teachers if they have the role teacher but its slow 
        public ICollection<User> GetTeacherRegister()
        {
            var role = _context.Roles.FirstOrDefault(x => x.Name == "Teacher");
            var userRoles = _context.UserRoles.Where(x => x.RoleId == role.Id);
            var users = new List<User>();
            foreach (var user in userRoles)
            {
                var us = _context.Users.FirstOrDefault(x => x.Id == user.UserId);
                if(us != null)
                {
                    users.Add(us);
                }
         }


            return users;
        }*/

        //you can use this method to get all the teachers if they have a staffId
        public ICollection<User> GetTeachers()
        {
            return _context.Users
                            .Where(u => u.StaffId != null)
                            .OrderBy(t => t.Id)
                            .ToList();
        }


        public ICollection<User> GetTeachersByCourseCode(string course)
        {
            return _context.Users.Where(t => t.CourseCode == course).ToList();
        }


        public User GetTeacherByStaffId(string staffId)
        {
            return _context.Users.Where(t => t.StaffId == staffId).FirstOrDefault();
        }


        public bool TeacherCourseExists(string course)
        {
            return _context.Users.Any(t => t.CourseCode == course);
        }


        public bool TeacherStaffIdExists(string staffId)
        {
            return _context.Users.Any(t => t.StaffId == staffId);
        }


        public bool DeleteTeacher(User teacher)
        {
            _context.Remove(teacher);
            return Save();
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            if (saved > 0)
            {
                return true;
            }
            return false;
        }








        public ICollection<User> GetStudents()
        {
            return _context.Users
                            .Where(u => u.MatricNumber != null)
                            .OrderBy(t => t.Id)
                            .ToList();
        }
        public User GetStudentByMatricNumber(string matricNumber)
        {
            return _context.Users.Where(t => t.MatricNumber == matricNumber).FirstOrDefault();
        }

        public ICollection<User> GetStudentsByDepartment(string department)
        {
            return _context.Users.Where(t => t.Department == department).ToList();
        }

        public bool StudentMatricNumberExists(string matricNumber)
        {
            return _context.Users.Any(t => t.MatricNumber == matricNumber);
        }
    }
}
