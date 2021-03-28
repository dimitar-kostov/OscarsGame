namespace OscarsGame.Domain.Models
{
    public class UserScore
    {
        public int Rank { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public int WatchedMovies { get; set; }
        public int WatchedNominations { get; set; }
        public int Bets { get; set; }
    }
}
