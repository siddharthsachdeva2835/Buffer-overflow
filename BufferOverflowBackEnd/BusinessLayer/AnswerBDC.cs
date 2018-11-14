using Repo;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class AnswerBDC
    {
        private AnswerRepository answerRepository;

        public AnswerBDC()
        {
            answerRepository = new AnswerRepository();
        }

        public AnswerDTO addAnswerToQuestion(int questionID, int userID, AnswerDTO answerDTO)
        {
            return answerRepository.addAnswerToQuestion(questionID, userID, answerDTO);
        }

        public AnswerDTO updateAnswer(int questionID, int answerID, AnswerDTO answerDTO)
        {
            return answerRepository.updateAnswer(questionID, answerID, answerDTO);
        }

        public void deleteAnswer(int questionID, int answerID)
        {
            answerRepository.deleteAnswer(questionID, answerID);
        }

        public void voteAnswer(int questionID, int answerID, int userID, int key)
        {
            answerRepository.voteAnswer(questionID, answerID, userID, key);
        }
    }
}
