using QuizWebApiProject.Data;
using QuizWebApiProject.Interfaces;
using QuizWebApiProject.Model;

namespace QuizWebApiProject.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateDepartment(Department department)
        {
            _context.Add(department);
            return Save();
        }

        public bool DeleteDepartment(Department department)
        {
            _context.Remove(department);
            return Save();
        }

        public bool DepartmentExists(string department)
        {
            return _context.Departments.Any(c => c.DepartmentName == department);
        }

        public bool DepartmentIdExists(int id)
        {
            return _context.Departments.Any(c => c.Id == id);
        }

        public Department GetDepartment(int id)
        {
            return _context.Departments.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Department> GetDepartments()
        {
            return _context.Departments.OrderBy(r => r.Id).ToList();
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

        public bool UpdateDepartment(Department department)
        {
            _context.Update(department);
            return Save();
        }
    }
}
