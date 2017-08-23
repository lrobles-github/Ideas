using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ideas.Models
{
    public class Like : BaseEntity
    {

        public int Id { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int IdeaId { get; set; }
        public Idea Idea { get; set; }

    }

}