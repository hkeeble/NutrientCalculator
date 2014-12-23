using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NutrientCalculator.Resources;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

using FatSecretAPI;

using NutrientCalculator.Source.Pages;

namespace Assignment
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static string PageID = "mainPage";

        FatSecretService fatSecret;
        UserProfile selectedProfile;
        UserProfileList userProfileList;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            fatSecret = new FatSecretService();
            userProfileList = new UserProfileList();
            UpdateProfileList();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            SystemTray.ProgressIndicator = new ProgressIndicator();
        }

        private void SetProgressIndicator(bool isVisible)
        {
            SystemTray.ProgressIndicator.IsIndeterminate = isVisible;
            SystemTray.ProgressIndicator.IsVisible = isVisible;
        }

        private void newProfileButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/NewProfile.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Check for navigation
            NavigationData data = NavigationData.ParseURI(e.Uri.ToString());
            if(data.GetPageID() == NewProfile.PageID && data.Accepted()) // Navigation came from new profile page, we are creating a new profile
            {
                Gender gender;
                string genderVal = data.Get(NavigationData.PROFILE_GENDER);
                if(genderVal == Gender.MALE.ToString())
                    gender = Gender.MALE;
                else
                    gender = Gender.FEMALE;

                UserProfile profile = UserProfile.Create(data.Get(NavigationData.PROFILE_NAME), gender, data.Get(NavigationData.PROFILE_IMAGE));
                UpdateProfileList();
            }
        }

        private void UpdateProfileList()
        {
            userProfileList.ReadProfiles();

            // Fill in the list
            profileList.ItemsSource = userProfileList.Profiles;
        }

        private void profileList_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void profileList_selectionChaged(object sender, SelectionChangedEventArgs e)
        {
            selectedProfile = (sender as ListBox).SelectedItem as UserProfile;

            if(selectedProfile != null)
            {
                selectedProfile.LoadData(); // Load all profile data
                NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, Main.PageID);
                NavigationService.Navigate(new Uri("/Source/Pages/Main.xaml?" + data.Build(), UriKind.Relative));
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Page destination = e.Content as Page;
            if(destination is Main)
                Main.CurrentUserProfile = selectedProfile;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Terminate();
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