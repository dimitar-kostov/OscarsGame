using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OscarsGame.Data
{
    public class NominationRepository : INominationRepository
    {
        public List<Nomination> GetAllNominations()
        {
            using (var ctx = new MovieContext())
            {
                return ctx.Nominations
                    .Include(cat => cat.Category)
                    .Where(cat => cat.Category != null)
                    .Where(cat => cat.Movie != null)
                    .ToList();
            }
        }

        public List<Nomination> GetAllNominationsInCategory(int categoryId)
        {
            using (var ctx = new MovieContext())
            {
                return ctx.Nominations
                    .Include(cat => cat.Movie)
                    .Include(cat => cat.Credits)
                    .Where(cat => cat.Category.Id == categoryId)
                    .ToList();
            }
        }

        public void RemoveNomination(int nominationId)
        {
            using (var ctx = new MovieContext())
            {
                var foundNomination = ctx.Nominations.Single(x => x.Id == nominationId);
                ctx.Nominations.Remove(foundNomination);
                ctx.SaveChanges();
            }
        }
    }
}
