using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Entity.Models;
using Repo.Config;
using Shared;

namespace Repo
{
    public class AnswerRepository
    {
        public AnswerDTO addAnswerToQuestion(int questionID, int userID, AnswerDTO answerDTO)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Question question = db.Questions.SingleOrDefault(x => x.QuestionID == questionID);
                    question.AnswerCount++;
                    Answer answer = MapConfig.mapper.Map<AnswerDTO, Answer>(answerDTO);
                    answer.CreatedAt = DateTime.Now;
                    answer.UpdatedAt = DateTime.Now;
                    answer.Author = db.Users.SingleOrDefault(x => x.UserID == userID);
                    answer.Question = question;
                    answer.QuestionID = questionID;
                    answer.Votings = new List<Voting>();

                    question.Answers.Add(answer);
                    db.SaveChanges();

                    return MapConfig.mapper.Map<Answer, AnswerDTO>(answer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void voteAnswer(int questionID, int answerID, int userID, int key)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Voting vote = db.Votings.SingleOrDefault(x => x.AnswerID == answerID && x.UserID == userID);

                    bool Key = (key == 0) ? false : true;
                    if( vote != null )
                    {
                        if (vote.Status == Key)
                        {
                            db.Votings.Remove(vote);
                        }
                        else
                        {
                            vote.Status = Key;
                        }
                    }
                    else
                    {
                        Voting newVote = new Voting();
                        newVote.UserID = userID;
                        newVote.AnswerID = answerID;
                        newVote.Status = Key;
                        db.Votings.Add(newVote);
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void deleteAnswer(int questionID, int answerID)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Answer answer = db.Answers.SingleOrDefault(x => x.AnswerID == answerID);
                    answer.Question.AnswerCount--;
                    db.Answers.Remove(answer);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AnswerDTO updateAnswer(int questionID, int answerID, AnswerDTO answerDTO)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Answer answer = db.Answers.Include("Author").SingleOrDefault(x => x.AnswerID == answerID);

                    answer.UpdatedAt = DateTime.Now;
                    answer.Body = answerDTO.Body;
      
                    db.SaveChanges();

                    return MapConfig.mapper.Map<Answer, AnswerDTO>(answer);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
