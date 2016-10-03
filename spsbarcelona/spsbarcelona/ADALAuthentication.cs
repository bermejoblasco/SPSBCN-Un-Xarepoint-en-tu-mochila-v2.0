namespace spsbarcelona
{
    using MasterDetailPageNavigation.Models;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class ADALAuthentication
    {

        private string error = "";

        private AuthenticationResultCode resultCode = AuthenticationResultCode.None;

        private AuthenticationResult authResult = null;

        private List<DocumentsItem> documents = null;

        public IPlatformParameters platformParameters
        {
            get;
            set;
        }

        public AuthenticationResultCode ResultCode
        {
            get
            {
                return resultCode;
            }
        }

        public AuthenticationResult AuthResult
        {
            get
            {
                return authResult;
            }
        }

        public string Error
        {
            get
            {
                return error;
            }

        }

        public List<DocumentsItem> Documents
        {
            get
            {
                return documents;
            }
        }

        private ADALAuthentication()
        {

        }

        private static ADALAuthentication instance = null;

        public static ADALAuthentication Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ADALAuthentication();
                }

                return instance;
            }
        }

        public async Task<AuthenticationResultCode> Authenticate(string resource, string clientId, string returnUrl)
        {
            try
            {
                AuthenticationContext ac2 = new AuthenticationContext("https://login.microsoftonline.com/yourtenantId");
                authResult = await ac2.AcquireTokenAsync(resource, clientId, new Uri(returnUrl), platformParameters);
                var result =  await FetchListItems(authResult.AccessToken);
            }

            catch (AdalException adalEx)
            {
                switch (adalEx.ErrorCode)
                {
                    case "authentication_canceled":
                        resultCode = AuthenticationResultCode.Canceled;
                        break;

                    case "access_denied":
                        resultCode = AuthenticationResultCode.Denied;
                        break;

                    default:
                        resultCode = AuthenticationResultCode.Unknown;
                        break;
                }

            }
            catch (Exception ex)
            {
                resultCode = AuthenticationResultCode.Unknown;
                error = ex.Message + " " + ex.StackTrace;
            }

            return resultCode;
        }

        protected async Task<bool> FetchListItems(string token)
        {            
            documents = new List<DocumentsItem>();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var mediaType = new MediaTypeWithQualityHeaderValue("application/xml");
            mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));
            client.DefaultRequestHeaders.Accept.Add(mediaType);

            try
            {
                var result = await client.GetStringAsync("https://sogetispainlab.sharepoint.com/gt/_api/web/lists/GetByTitle('Documentos')/items?$expand=File");

                XElement xelement = XElement.Parse(result);
                var documentsServices = xelement.Descendants().Where(x => x.Name.LocalName.Contains("Name") && x.Name.LocalName != "" && x.Value.Contains(".pdf"));
                foreach (var document in documentsServices)
                {
                    documents.Add(new DocumentsItem
                    {
                        Title = document.Value,
                        IconSource = "pdf.png"
                    });
                }
            }
            catch (Exception ex)
            {
                var msg = "Unable to fetch list items. " + ex.Message;
            }

            return true;
        }

    }
}
