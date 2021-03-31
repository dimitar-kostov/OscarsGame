using System;

namespace OscarsGame.Domain.Models
{
    public class WatchedMovies
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public Guid? UserId { get; set; }
        public string UserDisplayName { get; set; }
    }
}
