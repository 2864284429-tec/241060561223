namespace LibrarySeatReservation.Web.Services;

public interface IAdminAuthService
{
    bool ValidateCredentials(string username, string password);
}
