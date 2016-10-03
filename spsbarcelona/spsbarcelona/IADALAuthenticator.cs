namespace spsbarcelona
{
    using System.Threading.Tasks;

    public interface IADALAuthenticator
    {
        Task<AuthenticationResultCode> Authenticate(string resource, string clientId, string returnUri);

    }
}
