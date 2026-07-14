namespace LibrarySeatReservation.Web.Services;

public class AdminAuthService : IAdminAuthService
{
    public bool ValidateCredentials(string username, string password)
    {
        return username == "admin" && password == "123456";
    }
}
