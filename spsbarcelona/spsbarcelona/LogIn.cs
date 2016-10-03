using Microsoft.IdentityModel.Clients.ActiveDirectory;
using spsbarcelona;
using System;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace MasterDetailPageNavigation
{
    public class LogIn : ContentPage
    {
        public IPlatformParameters PlatformParameters { get; set; }
        private ProgressBar progress { get; set; }

        public LogIn()
        {
            var button = new Button
            {
                Text = "Login"
            };
            button.Clicked += LoginButton_Clicked;
            progress = new ProgressBar
            {
                IsVisible = false,
                BackgroundColor = Color.Black,
                Progress = 1
            };
            Image img = new Image
            {
                Source = "sogeti.png"
            };

            Title = "   LogIn";
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children = {
                  img,
                  progress,
                  button
                },
                BackgroundColor = Color.White
            };
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            View progressLayout = new ProgressBar();
            try
            {
                var button = (Button)sender;
                button.IsEnabled = false;
                var layout = (StackLayout)button.Parent;
                progressLayout = (ProgressBar)layout.Children.Where(x => x.GetType() == typeof(ProgressBar)).First();
                progressLayout.IsVisible = true;


                var auth = DependencyService.Get<IADALAuthenticator>();

                AuthenticationResultCode code = await auth.Authenticate("https://sogetispainlab.sharepoint.com/", "yourappcode", "https://yourmobileapp.azurewebsites.net");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}