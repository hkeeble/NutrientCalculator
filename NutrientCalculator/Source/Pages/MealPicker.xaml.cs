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
    public partial class MealPicker : PhoneApplicationPage
    {
        public static readonly string PageID = "MealPicker";

        public MealPicker()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            recipeList.ItemsSource = Main.CurrentUserProfile.Recipes;
        }

        private void recipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CustomRecipe recipe = (sender as ListBox).SelectedItem as CustomRecipe;

            if(MessageBox.Show("Adding meal " + recipe.Name + " to todays meal list. Is that ok?", "Information", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Main.CurrentUserProfile.CurrentDayTracker.AddMeal(recipe);
                NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, Main.PageID);
                NavigationService.Navigate(new Uri("/Source/Pages/Main.xaml?" + data.Build(), UriKind.Relative));
            }
        }
    }
}