using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBot.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        public Int64 UserId { get; set; }

        public Int64 ChatId { get; set; }

        public string Name { get; set; }

        public Int64 Score { get; set; }
    }
}
