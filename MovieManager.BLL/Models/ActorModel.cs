using MovieManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.BLL.Model
{
    public class ActorModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public string? Country { get; set; }
        public string? Biography { get; set; }
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
