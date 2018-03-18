using System.Collections.Generic;
using System.Threading.Tasks;
using Test.DAL.Abstract;
using Test.MODELS.Entities;
using Test.MODELS.Enums;

namespace Test.API.Helpers
{
    public class DataBaseInitializer : IDataBaseInitializer
    {
        private readonly IUnitOfWork _unitOfWork;
        public DataBaseInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Initialize()
        {
            var questionsCount = await _unitOfWork.QuestionsRepository.CountAsync(filters: null);

            if (questionsCount == 0)
            {
                var questions = new List<Question>
                {
                    new Question {QuestionText = "Which of the following is correct about C#?", Language = LanguageType.DotNet, Option1 = "It is component oriented.", Option2 = "It can be compiled on a variety of computer platforms." ,Option3 = "It is a part of .Net Framework.", Option4 = "All of the above.", AnswerNum = 4}
                };

                foreach (var question in questions)
                {
                    await _unitOfWork.QuestionsRepository.AddAsync(question);
                }

                await _unitOfWork.QuestionsRepository.AddRangeAsync(questions);

                await _unitOfWork.Save();
            }
        }
    }
}
