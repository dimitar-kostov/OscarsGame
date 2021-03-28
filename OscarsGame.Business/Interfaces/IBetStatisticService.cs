using OscarsGame.Domain.Models;
using System.Collections.Generic;

namespace OscarsGame.Business.Interfaces
{
    public interface IBetStatisticService
    {

        List<BetObject> GetData();

        string GetWinner();

        string[] GetCategories();

        List<Winners> GetWinners();

    }
}
