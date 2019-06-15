﻿using AutoMapper;
using App.DataBaseAccess;
using App.Models;

namespace App.WebApi
{
    public class AutoMApperProfile:Profile
    {
        public AutoMApperProfile()
        {
            CreateMap<AspNetUser, AppUserModel>();
            CreateMap<Question, QuestionModel>();
            CreateMap<Serie, SeriesModel>();
            CreateMap<Test, ExamModel>();
        }
    }
}