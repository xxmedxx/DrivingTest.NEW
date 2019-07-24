using AutoMapper;
using App.DataBaseAccess;
using App.Models;

namespace App.WebApi
{
    public class AutoMApperProfile:Profile
    {
        public AutoMApperProfile()
        {
            CreateMap<AspNetUser, AppUserModel>();
            CreateMap<AppUserModel, AspNetUser > ();
            CreateMap<Question, QuestionModel>();
            CreateMap<Serie, SeriesModel>();
            CreateMap<Test, ExamModel>();
        }
    }
}