using OscarsGame.Business.Interfaces;
using OscarsGame.Data.Interfaces;
using OscarsGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
