using AppFinancialControl.Models;

namespace AppFinancialControl.Service
{
    public interface IUserService
    {
        public Task<User> Login(UserLogin user);
        public User GetUser(Guid id);
        public Task<string> Insert(User user);
        public Task<string> Delete(User user);
        public Task<string> Update(User user);
    }
}
