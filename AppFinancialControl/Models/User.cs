namespace AppFinancialControl.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
