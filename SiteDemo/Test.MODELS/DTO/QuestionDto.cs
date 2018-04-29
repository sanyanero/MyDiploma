using System;
using System.ComponentModel.DataAnnotations;

namespace Test.MODELS.DTO {
    public class QuestionDto {

        public virtual Guid? Id { get; set; }

        public string QuestionText { get; set; }

        public string QuestionPhoto { get; set; }

        public int Language { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }

        public int AnswerNum { get; set; }
    }
}