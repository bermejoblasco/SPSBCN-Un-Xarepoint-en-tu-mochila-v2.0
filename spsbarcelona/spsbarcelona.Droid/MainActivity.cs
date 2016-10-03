
namespace spsbarcelona.Droid
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Gcm.Client;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using System;

    [Activity(Label = "spsbarcelona", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        static MainActivity instance = null;

        // Devuleve la activity actual
        public static MainActivity CurrentActivity
        {
            get
            {
                return instance;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            // Asignamos la acutal instacia de MainAcivity
            instance = this;
            //Registramos las notificaciones
            RegisterNotificationsPush();

            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);            
            LoadApplication(new App());
        }

        private void RegisterNotificationsPush()
        {
            try
            {
                // Validamos que todo este correcto para poder aplicar notificaciones Push
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);
                // Registramos el dispositivo para poder recibir notificaciones Push
                GcmClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
            }
            catch (Java.Net.MalformedURLException)
            {
                CreateAndShowDialog("There was an error creating the client. Verify the URL.", "Error");
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e.Message, "Error");
            }
        }

        private void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }
    }
}

