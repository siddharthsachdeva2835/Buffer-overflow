using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Entity.Models;
using Repo.Config;

using Shared;

namespace Repo
{
    public class QuestionRepository
    {
        public List<QuestionDTO> getAllQuestions()
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    List<Question> questions = db.Questions.Include("Author").Include(x => x.Answers.Select(y => y.Author)).OrderByDescending(z => z.UpdatedAt).ToList();

                    List<QuestionDTO> questionDTOs = MapConfig.mapper.Map<List<Question>, List<QuestionDTO>>(questions);

                    for (int i = 0; i < questions.Count; i++)
                    {
                        questionDTOs[i].Tags = new List<string>();
                        for (int j = 0; j < questions[i].QuestionTags.Count; j++)
                        {

                            questionDTOs[i].Tags.Add(questions[i].QuestionTags.ElementAt(j).Tag.TagName);
                        }
                    }

                    return questionDTOs;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public QuestionDTO addQuestion(QuestionDTO questionDTO, int userID)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Question question = MapConfig.mapper.Map<QuestionDTO, Question>(questionDTO);
                    question.AnswerCount = 0;
                    question.CreatedAt = DateTime.Now;
                    question.UpdatedAt = DateTime.Now;
                    question.Author = db.Users.SingleOrDefault(x => x.UserID == userID);
                    question.Answers = new List<Answer>();
                    question.QuestionTags = new List<QuestionTag>();

                    for ( int i = 0 ; i < questionDTO.Tags.Count; i++ )
                    {
                        QuestionTag questionTag = new QuestionTag();

                        questionTag.QuestionID = question.QuestionID;
                        questionTag.Tag = new Tag();
                        questionTag.Tag.TagName = questionDTO.Tags[i];
                        question.QuestionTags.Add(questionTag);
                    }
                    

                    db.Questions.Add(question);
                    db.SaveChanges();

                    return MapConfig.mapper.Map<Question,QuestionDTO>(question);
                }
            }
            catch (Exception ex)
            {
                    throw new Exception(ex.Message);
            }
        }

        public List<QuestionDTO> getAllQuestionsBySearchString(string searchString)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    bool check = db.QuestionTags.Any(y => y.Tag.TagName.ToLower().Replace(" ", String.Empty)
                         .Contains(searchString.ToLower().Replace(" ", String.Empty)));
                    List<Question> questions = db.Questions.Include("Author").Include(x => x.Answers.Select(y => y.Author))
                        .Where(x => x.Description.ToLower().Replace(" ", String.Empty)
                        .Contains(searchString.ToLower().Replace(" ", String.Empty)) || x.Title.ToLower().Replace(" ", String.Empty)
                        .Contains(searchString.ToLower().Replace(" ", String.Empty)) ||
                         x.QuestionTags.Any(y => y.Tag.TagName.ToLower().Replace(" ", String.Empty)
                         .Contains(searchString.ToLower().Replace(" ", String.Empty))))
                        .OrderByDescending(z => z.UpdatedAt).ToList();
                    return MapConfig.mapper.Map<List<Question>, List<QuestionDTO>>(questions);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public QuestionDTO updateQuestion(int questionID, QuestionDTO questionDTO)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Question question = db.Questions.SingleOrDefault(x => x.QuestionID == questionID);

                    question.Title = questionDTO.Title;
                    question.Description = questionDTO.Description;
                    question.UpdatedAt = DateTime.Now;

                    db.SaveChanges();

                    return MapConfig.mapper.Map<Question, QuestionDTO>(question);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void deleteQuestion(int questionID)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    db.Questions.Remove(db.Questions.SingleOrDefault(x => x.QuestionID == questionID));
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public QuestionDTO getQuestionByQuestionId(int questionID)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    Question question = db.Questions.Include("Author").Include(x => x.Answers.Select(y => y.Author))
                                    .Include("Author").Include(x => x.Answers.Select(y => y.Votings)).SingleOrDefault(x => x.QuestionID == questionID);
                    QuestionDTO questionDTO = MapConfig.mapper.Map<Question, QuestionDTO>(question);
                    questionDTO.Tags = new List<string>();
                    for (int j = 0; j < question.QuestionTags.Count; j++)
                    {

                        questionDTO.Tags.Add(question.QuestionTags.ElementAt(j).Tag.TagName);
                    }
                    return questionDTO;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<QuestionDTO> getQuestionsByAuthor(int authorID)
        {
            try
            {
                using (var db = new DataBaseContext())
                {
                    List<Question> questions = db.Questions.Include("Author").Include(x => x.Answers.Select(y => y.Author)).Where(x => x.Author.UserID == authorID).ToList();
                    return MapConfig.mapper.Map<List<Question>, List<QuestionDTO>>(questions);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
