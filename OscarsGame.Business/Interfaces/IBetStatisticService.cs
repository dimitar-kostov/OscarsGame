using OscarsGame.Entities.StatisticsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
