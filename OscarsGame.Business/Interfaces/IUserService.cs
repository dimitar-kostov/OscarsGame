using OscarsGame.Domain.Entities;
using System;
using System.Collections.Generic;

namespace OscarsGame.Business.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User GetUser(Guid userId);

        void UpdateUser(User user);

        void CreateUser(User user);

        void DeleteUser(User user);
    }
}
