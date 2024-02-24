using BuberDinner.Application.Common.Interface.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly static List<User> _users = new();
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }
    }
}
