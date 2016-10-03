
namespace spsbarcelona.UWP
{
    using Microsoft.WindowsAzure.MobileServices;

    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();            
            LoadApplication(new spsbarcelona.App());
        }      
        
    }
}
