using OscarsGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OscarsGame.Business.Interfaces
{
    public interface INominationService
    {
        List<Nomination> GetAllNominationsInCategory(int categoryId);
        void RemoveNomination(int nominationId);
        bool AreAllWinnersSet();
    }
}
