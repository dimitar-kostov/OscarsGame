namespace OscarsGame.Entities
{
    public class Bet
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Nomination Nomination { get; set; }
    }
}
