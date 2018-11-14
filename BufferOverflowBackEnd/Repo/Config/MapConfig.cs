using AutoMapper;
using Entity.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Config
{
    class MapConfig
    {
        public static IMapper mapper = new MapperConfiguration(cfg =>
        {

            cfg.CreateMap<User, UserDTO>();
            cfg.CreateMap<UserDTO, User>();
            cfg.CreateMap<User, User>();
            cfg.CreateMap<QuestionDTO, Question>();
            cfg.CreateMap<Question, QuestionDTO>();
            cfg.CreateMap<Answer, AnswerDTO>();
            cfg.CreateMap<AnswerDTO, Answer>();

        }).CreateMapper();
    }
}
