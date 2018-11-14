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
    [RoutePrefix("api/questions/{questionID}")]
    public class AnswersController : ApiController
    {
        AnswerBDC answerBDC = new AnswerBDC();

        [Route("answers")]
        [HttpGet]
        public IHttpActionResult AllAnswersOfQuestion(int questionID)
        {
            return Ok();
        }


        [Route("answers")]
        [HttpPost]
        public IHttpActionResult AddAnswer(int questionID, Answer answer)
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

                if (userDTO == null)
                {
                    return Unauthorized();
                }
                else
                {
                    AnswerDTO answerDTO = MapConfig.mapper.Map<Answer, AnswerDTO>(answer);
                    AnswerDTO newAnswerDTO = answerBDC.addAnswerToQuestion(questionID, userDTO.UserID, answerDTO);

                    return Ok(MapConfig.mapper.Map<AnswerDTO, Answer>(newAnswerDTO));
                }
            }
            catch (Exception)
            {
                return Unauthorized();
            }

        }


        [Route("answers/{answerID}")]
        [HttpPost]
        public IHttpActionResult UpdateAnswer(int questionID, int answerID, Answer answer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Format");
            }

            AnswerDTO answerDTO = MapConfig.mapper.Map<Answer, AnswerDTO>(answer);
            AnswerDTO newAnswerDTO = answerBDC.updateAnswer(questionID, answerID, answerDTO);

            return Ok(MapConfig.mapper.Map<AnswerDTO, Answer>(newAnswerDTO));
        }


        [Route("answers/{answerID}")]
        [HttpDelete]
        public IHttpActionResult DeleteAnswer(int questionID, int answerID)
        {
            try
            {
                answerBDC.deleteAnswer(questionID, answerID);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [Route("answers/{answerID}/vote/{key}")]
        [HttpPost]
        public IHttpActionResult VoteAnswer(int questionID, int answerID, int key)
        {
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

                if (userDTO == null)
                {
                    return Unauthorized();
                }
                else
                {
                    try
                    {
                        answerBDC.voteAnswer(questionID, answerID, userDTO.UserID, key);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }

                    return Ok();
                }
            }
            catch (Exception)
            {
                return Unauthorized();
            }
            
        }
    }
}
