using AutoMapper;
using System;
using Test.MODELS.DTO;
using Test.MODELS.Entities;

namespace Test.MODELS.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>().MaxDepth(1);
            CreateMap<QuestionDto, Question>().MaxDepth(1);
        }
    }
}