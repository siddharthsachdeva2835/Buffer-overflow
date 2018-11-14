using Repo;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class QuestionBDC
    {
        private QuestionRepository questionRepository;

        public QuestionBDC()
        {
            questionRepository = new QuestionRepository();
        }

        public List<QuestionDTO> getAllQuestions()
        {
            return questionRepository.getAllQuestions();
        }

        public QuestionDTO addQuestion(QuestionDTO questionDTO, int userID)
        {
            return questionRepository.addQuestion(questionDTO, userID);
        }

        public List<QuestionDTO> getQuestionsByAuthor(int authorID)
        {
            return questionRepository.getQuestionsByAuthor(authorID);

        }

        public List<QuestionDTO> getAllQuestionsBySearchString(string searchString)
        {
            return questionRepository.getAllQuestionsBySearchString(searchString);
        }

        public QuestionDTO getQuestionsByQuestionId(int questionID)
        {
            return questionRepository.getQuestionByQuestionId(questionID);
        }

        public void deleteQuestion(int questionID)
        {
            questionRepository.deleteQuestion(questionID);
        }

        public QuestionDTO updateQuestion(int questionID, QuestionDTO questionDTO)
        {
            return questionRepository.updateQuestion(questionID, questionDTO);
        }
    }
}
