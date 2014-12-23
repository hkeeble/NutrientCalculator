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
    public partial class CustomRecipeViewer : PhoneApplicationPage
    {
        public static readonly string PageID = "CustomRecipeViewer";

        CustomRecipe currentSelection;

        public CustomRecipeViewer()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            recipeList.ItemsSource = Main.CurrentUserProfile.Recipes;
        }

        private void editRecipeClick(object sender, RoutedEventArgs e)
        {
            CustomRecipe recipe = (sender as Button).DataContext as CustomRecipe;
            RecipeBuilder.CurrentRecipe = recipe;
            NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, RecipeBuilder.PageID);
            NavigationService.Navigate(new Uri("/Source/Pages/RecipeBuilder.xaml?" + data.Build(), UriKind.Relative));
        }

        private void recipeList_selectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void deleteRecipeClick(object sender, RoutedEventArgs e)
        {
            CustomRecipe recipe = (sender as Button).DataContext as CustomRecipe;

            if (MessageBox.Show("Deleting recipe " + recipe.Name + " from your recipe collection. Is this ok?", "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Main.CurrentUserProfile.DeleteRecipe(recipe.Name);
                recipeList.ItemsSource = null;
                recipeList.ItemsSource = Main.CurrentUserProfile.Recipes;
            }
        }

        private void uploadRecipeClick(object sender, RoutedEventArgs e)
        {
            NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, UploadRecipe.PageID);
            UploadRecipe.CurrentRecipe = (sender as Button).DataContext as CustomRecipe;
            NavigationService.Navigate(new Uri("/Source/Pages/UploadRecipe.xaml?" + data.Build(), UriKind.Relative));
        }
    }
}