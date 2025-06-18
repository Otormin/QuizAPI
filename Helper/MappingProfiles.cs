using AutoMapper;
using QuizWebApiProject.Dto;
using QuizWebApiProject.Model;

namespace QuizWebApiProject.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<QuestionAndAnswer, QuestionsDto>();
            CreateMap<QuestionsDto, QuestionAndAnswer>();
            CreateMap<StudentScore, AnswersDto>();
            CreateMap<AnswersDto, StudentScore>();
            CreateMap<User, TeacherDto>();
            CreateMap<User, StudentDto>();
        }
    }
}
