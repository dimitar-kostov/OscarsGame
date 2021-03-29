using OscarsGame.Domain.Entities;
using System.Collections.Generic;

namespace OscarsGame.Domain.Repositories
{
    public interface INominationRepository : IRepository<Nomination>
    {
        List<Nomination> GetAllNominations();
        List<Nomination> GetAllNominationsInCategory(int categoryId);
        void RemoveNomination(int nominationId);
    }
}
