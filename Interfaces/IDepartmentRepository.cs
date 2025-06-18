using QuizWebApiProject.Model;

namespace QuizWebApiProject.Interfaces
{
    public interface IDepartmentRepository
    {
        ICollection<Department> GetDepartments();
        Department GetDepartment(int id);
        bool DepartmentExists(string department);
        bool DepartmentIdExists(int id);


        bool CreateDepartment(Department department);


        bool DeleteDepartment(Department department);


        bool UpdateDepartment(Department department);

        bool Save();
    }
}
