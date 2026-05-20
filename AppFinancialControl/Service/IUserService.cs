using AppFinancialControl.Models;

namespace AppFinancialControl.Service
{
    public interface IUserService
    {
        public User Login(UserLogin user);
        public void Insert(User user);
        public void Delete(User user);
        public void Update(User user);
    }
}
