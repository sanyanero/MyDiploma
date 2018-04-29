using Test.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test.MODELS.DTO;
using Test.MODELS.Enums;

namespace Test.MODELS.Entities
{
    public class Question : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

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
