using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Assignment;

namespace NutrientCalculator.Source.Pages
{
    public partial class Main : PhoneApplicationPage
    {
        public UserProfile currentUserProfile;

        public Main()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        private void buildRecipeTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/RecipeBuilder.xaml", UriKind.Relative));
        }

        private void searchRecipesTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/RecipeSearch.xaml", UriKind.Relative));
        }

        private void weeklySummaryTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/WeeklySummary.xaml", UriKind.Relative));
        }

        private void modifyDietTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/DietModifier.xaml", UriKind.Relative));
        }
    }
}