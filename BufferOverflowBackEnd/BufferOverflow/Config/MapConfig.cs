using AutoMapper;
using BufferOverflow.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BufferOverflow.Config
{
    public class MapConfig
    {
        public static IMapper mapper = new MapperConfiguration(cfg =>
        {

            cfg.CreateMap<UserDTO, User>();
            cfg.CreateMap<LoginUser, UserDTO>();
            cfg.CreateMap<RegisterUser, UserDTO>();
            cfg.CreateMap<QuestionDTO, Question>();
            cfg.CreateMap<Question, QuestionDTO>();
            cfg.CreateMap<Answer, AnswerDTO>();
            cfg.CreateMap<AnswerDTO, Answer>();

        }).CreateMapper();
    }
}