using Test.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test.MODELS.DTO;

namespace Test.MODELS.Entities
{
    public class Product : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public DateTimeOffset Date { get; set; }

    }
}
