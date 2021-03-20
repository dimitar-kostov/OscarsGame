namespace OscarsGame.Entities.Interfaces
{
    interface IBet
    {
        int Id { get; set; }
        string UserId { get; set; }
        Movie Movie { get; set; }
        Category Category { get; set; }
    }
}
