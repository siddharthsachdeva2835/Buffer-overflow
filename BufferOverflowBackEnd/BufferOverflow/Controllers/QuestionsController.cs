using BufferOverflow.Config;
using BufferOverflow.Models;
using BusinessLayer;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BufferOverflow.Controllers
{
    public class QuestionsController : ApiController
    {
        QuestionBDC questionBDC = new QuestionBDC();



        public IHttpActionResult GetAllQuestions()
        {
            List<QuestionDTO> questionDTOs = questionBDC.getAllQuestions();
            
            return Ok(MapConfig.mapper.Map<List<QuestionDTO>, List<Question>>(questionDTOs));
        }

        public IHttpActionResult GetAllQuestionsBySearchString(string searchString)
        {
            List<QuestionDTO> questionDTOs;
            if (searchString == null || searchString == "null")
            {
                questionDTOs = questionBDC.getAllQuestions();
            }
            else
            {
                questionDTOs = questionBDC.getAllQuestionsBySearchString(searchString);
            }

            return Ok(MapConfig.mapper.Map<List<QuestionDTO>, List<Question>>(questionDTOs));
        }



        public IHttpActionResult GetQuestionsByAuthorID(int AuthorID)
        {
            List<QuestionDTO> questionDTOs = questionBDC.getQuestionsByAuthor(AuthorID);

            return Ok(MapConfig.mapper.Map<List<QuestionDTO>, List<Question>>(questionDTOs));
        }



        public IHttpActionResult GetQuestionByID(int id)
       {
            QuestionDTO questionDTO = questionBDC.getQuestionsByQuestionId(id);
            
            if(questionDTO == null)
            {
                return NotFound();
            }

            return Ok(MapConfig.mapper.Map<QuestionDTO, Question>(questionDTO));
        }



        public IHttpActionResult PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Format");
            }

            IEnumerable<string> headerValues;
            var nameFilter = string.Empty;
            if (Request.Headers.TryGetValues("token", out headerValues))
            {
                nameFilter = headerValues.FirstOrDefault();
            }

            try
            {
                UserBDC userBDC = new UserBDC();
                UserDTO userDTO = userBDC.getUserByToken(nameFilter);

                if(userDTO == null)
                {
                    //return Ok(MapConfig.mapper.Map<UserDTO, User>(userDTO));
                    return Unauthorized();
                }
                else
                {
                    QuestionDTO questionDTO = MapConfig.mapper.Map<Question, QuestionDTO>(question);
                    QuestionDTO newQuestionDTO = questionBDC.addQuestion(questionDTO, userDTO.UserID);

                    return Ok(MapConfig.mapper.Map<QuestionDTO, Question>(newQuestionDTO));
                }
            }
            catch (Exception)
            {
                return Unauthorized();
            } 
        }



        public IHttpActionResult PutUpdateQuestion(int id, Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Format");
            }

            QuestionDTO questionDTO = MapConfig.mapper.Map<Question, QuestionDTO>(question);
            QuestionDTO newQuestionDTO = questionBDC.updateQuestion(id, questionDTO);

            return Ok(MapConfig.mapper.Map<QuestionDTO, Question>(newQuestionDTO));
        }



        public IHttpActionResult DeleteQuestion(int id)
        {
            try
            {
                questionBDC.deleteQuestion(id);
            }
            catch
            {
                return BadRequest("No such question exists");
            }

            return Ok();
        }
    }
}
