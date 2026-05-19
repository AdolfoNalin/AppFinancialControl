namespace AppFinancialControl.Models
{
    public class UserSession 
    {
        public static Guid Id { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
        public static string Token { get; set; }
    }
}
