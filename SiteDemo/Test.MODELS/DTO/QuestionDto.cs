using System;
using System.ComponentModel.DataAnnotations;

namespace Test.MODELS.DTO {
    public class QuestionDto {

        public virtual Guid? Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Type { get; set; }
    }
}