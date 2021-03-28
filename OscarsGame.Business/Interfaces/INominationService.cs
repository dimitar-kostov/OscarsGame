using OscarsGame.Domain.Entities;
using System.Collections.Generic;

namespace OscarsGame.Business.Interfaces
{
    public interface INominationService
    {
        List<Nomination> GetAllNominationsInCategory(int categoryId);
        void RemoveNomination(int nominationId);
        bool AreAllWinnersSet();
    }
}
