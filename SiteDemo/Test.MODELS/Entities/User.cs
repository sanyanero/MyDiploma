using Test.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test.MODELS.DTO;

namespace Test.MODELS.Entities
{
    public class User : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Group { get; set; }

        public int Score { get; set; }

        public int TimeSpent { get; set; }

    }
}
