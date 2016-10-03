
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(spsbarcelona.UWP.ADALAuthenticator))]
namespace spsbarcelona.UWP
{
    public class ADALAuthenticator : IADALAuthenticator
    {
        public Task<AuthenticationResultCode> Authenticate(string resource, string clientId, string returnUri)
        {
            ADALAuthentication.Instance.platformParameters = new PlatformParameters(PromptBehavior.Auto, false);
            return ADALAuthentication.Instance.Authenticate(resource, clientId, returnUri);
        }

    }
}
