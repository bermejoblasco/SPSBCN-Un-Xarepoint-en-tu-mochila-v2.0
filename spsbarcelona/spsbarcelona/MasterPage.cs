
namespace MasterDetailPageNavigation
{
    using MasterDetailPageNavigation.Models;
    using System.Collections.Generic;
    using Xamarin.Forms;

    public class MasterPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		ListView listView;

		public MasterPage ()
		{
			var masterPageItems = new List<MasterPageItem> ();			
            masterPageItems.Add(new MasterPageItem
            {
                Title = " Log In",
                IconSource = "log.png",
                TargetType = typeof(LogIn)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = " Documents",
                IconSource = "document.png",
                TargetType = typeof(DocumentList)
            });

            listView = new ListView {
				ItemsSource = masterPageItems,
				ItemTemplate = new DataTemplate (() => {
					var imageCell = new ImageCell ();
					imageCell.SetBinding (TextCell.TextProperty, "Title");
					imageCell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");
                    imageCell.TextColor = Color.White;
					return imageCell;
				}),
				VerticalOptions = LayoutOptions.FillAndExpand,
				SeparatorVisibility = SeparatorVisibility.None
			};

			Padding = new Thickness (0, 40, 0, 0);
			Icon = "hamburger.png";
			Title = "Xarepoint 2.0";
            BackgroundColor = Color.FromHex("#ef4527");         
            Content = new StackLayout {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#ef4527"),
                Children = {
					listView
				}	
			};
		}
	}
}
