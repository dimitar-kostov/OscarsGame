using OscarsGame.Business.Interfaces;
using OscarsGame.Domain;
using OscarsGame.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OscarsGame.Business
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork UnitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAll()
        {
            return UnitOfWork.UserRepository.GetAll();
        }

        public User GetUser(Guid userId)
        {
            return UnitOfWork.UserRepository.FindById(userId);
        }

        public void UpdateUser(User user)
        {
            UnitOfWork.UserRepository.Update(user);
            UnitOfWork.SaveChanges();
        }

        public void CreateUser(User user)
        {
            UnitOfWork.UserRepository.Add(user);
            UnitOfWork.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            UnitOfWork.UserRepository.Remove(user);
            UnitOfWork.SaveChanges();
        }
    }
}
