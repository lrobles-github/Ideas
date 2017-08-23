using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ideas.Models
{
    public class Idea : BaseEntity
    {

        public int Id { get; set; }
        
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<Like> Likes { get; set; }

        public User User { get; set; }

        public Idea()
        {
            Likes = new List<Like> {};
        }
    }

}