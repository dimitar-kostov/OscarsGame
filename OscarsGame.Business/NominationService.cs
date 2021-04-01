using OscarsGame.Business.Interfaces;
using OscarsGame.Domain;
using OscarsGame.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OscarsGame.Business
{
    public class NominationService : INominationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NominationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Nomination> GetAllNominationsInCategory(int categoryId)
        {
            return _unitOfWork.NominationRepository.GetAllNominationsInCategory(categoryId);
        }

        public void RemoveNomination(int nominationId)
        {
            _unitOfWork.NominationRepository.RemoveNomination(nominationId);
        }

        public bool AreAllWinnersSet()
        {
            List<Nomination> listAllNominations = _unitOfWork.NominationRepository.GetAllNominations();
            int categoriesCount = listAllNominations.Select(x => x.Category.Id).Distinct().Count();
            int winnersCount = listAllNominations.Count(x => x.IsWinner);

            return categoriesCount == winnersCount;
        }
    }
}
