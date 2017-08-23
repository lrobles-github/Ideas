using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ideas.Models
{
    public class User : BaseEntity
    {

        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Alias { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } 

        public List<Idea> Ideas { get; set; }

        public List<Like> Likes { get; set; }

        public User()
        {
            Ideas = new List<Idea> {};
            Likes = new List<Like> {};
        }

    }

}