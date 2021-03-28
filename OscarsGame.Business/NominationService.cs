using OscarsGame.Business.Interfaces;
using OscarsGame.Domain.Entities;
using OscarsGame.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace OscarsGame.Business
{
    public class NominationService : INominationService
    {
        private readonly INominationRepository _nominationRepository;

        public NominationService(INominationRepository nominationRepository)
        {
            _nominationRepository = nominationRepository;
        }

        public List<Nomination> GetAllNominationsInCategory(int categoryId)
        {
            return _nominationRepository.GetAllNominationsInCategory(categoryId);
        }

        public void RemoveNomination(int nominationId)
        {
            _nominationRepository.RemoveNomination(nominationId);
        }

        public bool AreAllWinnersSet()
        {
            List<Nomination> listAllNominations = _nominationRepository.GetAllNominations();
            int categoriesCount = listAllNominations.Select(x => x.Category.Id).Distinct().Count();
            int winnersCount = listAllNominations.Count(x => x.IsWinner);

            return categoriesCount == winnersCount;
        }
    }
}
