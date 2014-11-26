using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NutrientCalculator.Resources;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Assignment
{
    delegate void StringDownloadEventHandler(object sender, DownloadStringCompletedEventArgs args);
    delegate void StringDownloadProgessEventHandler(object sender, DownloadProgressChangedEventArgs args);

    public partial class MainPage : PhoneApplicationPage
    {

        FatSecretService fatSecret;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            fatSecret = new FatSecretService();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            SystemTray.ProgressIndicator = new ProgressIndicator();
        }

        private void GetItemButton_Click(object sender, RoutedEventArgs e)
        {
            //fatSecret.SearchFood("Cheese", 20, 0, new StringDownloadEventHandler(GetFoodEventHandler),
            //new StringDownloadProgessEventHandler(GetFoodProgressEventHandler));


            fatSecret.GetFood(33691, new StringDownloadEventHandler(GetFoodEventHandler),
                               new StringDownloadProgessEventHandler(GetFoodProgressEventHandler));
            SetProgressIndicator(true);
            SystemTray.ProgressIndicator.Text = "Downloading";
        }

        private void GetFoodEventHandler(object sender, DownloadStringCompletedEventArgs args)
        {
            SetProgressIndicator(false);
            //JObject r = JObject.Parse(args.Result);
            //JToken food = r["foods"];
            //string s = r.ToString();

            //FoodSearchResults results = JsonConvert.DeserializeObject<FoodSearchResults>(food.ToString());

            Food food = fatSecret.DeserializeFood(args.Result);
        }

        private void GetFoodProgressEventHandler(object sender, DownloadProgressChangedEventArgs args)
        {

        }

        private void SetProgressIndicator(bool isVisible)
        {
            SystemTray.ProgressIndicator.IsIndeterminate = isVisible;
            SystemTray.ProgressIndicator.IsVisible = isVisible;
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}