using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.DAL.Entities
{
    public class Director
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly? BirthDate { get; set; }
        public string? Country { get; set; }
        public string? Biography { get; set; }
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();


    }
}
