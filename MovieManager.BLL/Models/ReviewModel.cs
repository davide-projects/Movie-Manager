using MovieManager.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.BLL.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public string ReviewerName { get; set; } = string.Empty;
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
