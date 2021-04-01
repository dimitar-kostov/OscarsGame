namespace OscarsGame.Domain.Models
{
    public class BetsStatistic
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string CategoryTitle { get; set; }
        public string MovieTitle { get; set; }
        public bool IsWinner { get; set; }
    }
}
