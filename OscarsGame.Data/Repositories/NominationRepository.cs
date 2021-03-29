using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OscarsGame.Data
{
    internal class NominationRepository : Repository<Nomination>, INominationRepository
    {
        internal NominationRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public List<Nomination> GetAllNominations()
        {
            return Context.Nominations
                    .Include(cat => cat.Category)
                    .Where(cat => cat.Category != null)
                    .Where(cat => cat.Movie != null)
                    .ToList();
        }

        public List<Nomination> GetAllNominationsInCategory(int categoryId)
        {
            return Context.Nominations
                    .Include(cat => cat.Movie)
                    .Include(cat => cat.Credits)
                    .Where(cat => cat.Category.Id == categoryId)
                    .ToList();
        }

        public void RemoveNomination(int nominationId)
        {
            var foundNomination = Context.Nominations.Single(x => x.Id == nominationId);
            Context.Nominations.Remove(foundNomination);
            Context.SaveChanges();
        }
    }
}
