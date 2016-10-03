using Android.App;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(spsbarcelona.Droid.ADALAuthenticator))]
namespace spsbarcelona.Droid
{
    public class ADALAuthenticator : PageRenderer, IADALAuthenticator
    {
        public Task<AuthenticationResultCode> Authenticate(string resource, string clientId, string returnUri)
        {
            ADALAuthentication.Instance.platformParameters = new PlatformParameters((Activity)Forms.Context);
            return ADALAuthentication.Instance.Authenticate(resource, clientId, returnUri);
        }

    }
}