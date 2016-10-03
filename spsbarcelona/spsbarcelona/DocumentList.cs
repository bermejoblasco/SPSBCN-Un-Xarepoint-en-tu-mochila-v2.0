
namespace MasterDetailPageNavigation
{
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using spsbarcelona;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Xamarin.Forms;
    using System.Linq;
    using Models;
    using System.Collections.Generic;

    public class DocumentList : ContentPage
    {
        public ListView ListView { get { return listView; } }

        ListView listView;

        public DocumentList()
        {
            var adalInstance = ADALAuthentication.Instance;
            Image img = new Image
            {
                Source = "sogeti.png"
            };

            listView = new ListView
            {
                ItemsSource = adalInstance.Documents,
                ItemTemplate = new DataTemplate(() =>
                {
                    var imageCell = new ImageCell();
                    imageCell.SetBinding(TextCell.TextProperty, "Title");
                    imageCell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
                    return imageCell;
                }),
                RowHeight = 50,
                VerticalOptions = LayoutOptions.Center,
                SeparatorVisibility = SeparatorVisibility.Default,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };            

            Title = "    Sharepoint Documents";
            Content = new StackLayout
            {            
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                Children = {
                    img,
                    listView
                }
            };
        }        
    }
}