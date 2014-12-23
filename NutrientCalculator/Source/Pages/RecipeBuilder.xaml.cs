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
using FatSecretAPI;

namespace NutrientCalculator.Source.Pages
{
    /// <summary>
    /// Page used to build custom user recipes from ingredients (makes use of food search to get ingredients)
    /// </summary>
    public partial class RecipeBuilder : PhoneApplicationPage
    {
        public static readonly string PageID = "recipeBuilder";

        public static CustomRecipe CurrentRecipe = new CustomRecipe();

        public static Food foodToAdd;
        public static Serving servingToAdd;

        private static CustomIngredient currentSelection;
        private static CustomIngredient currentButtonContext;

        private static string recipeNameStatic;

        public RecipeBuilder()
        {
            InitializeComponent();
            ingredientList.ItemsSource = CurrentRecipe.Ingredients;
        }

        private void addFoodButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/FoodSearch.xaml", UriKind.Relative));
        }

        private void deleteButtonClick(object sender, RoutedEventArgs e)
        {
            CustomIngredient ingredient = (sender as Button).DataContext as CustomIngredient;

            if (MessageBox.Show("Deleting ingredient " + ingredient.FoodItem.Name + " from the recipe. Is this ok?", "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                CurrentRecipe.RemoveIngredient(ingredient);
                UpdateIngredientList();
            }
        }

        private void changeServingButtonClick(object sender, RoutedEventArgs e)
        {
            currentButtonContext = (sender as Button).DataContext as CustomIngredient;
            NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, ServingView.PageID);
            data.AddData("foodName", currentButtonContext.FoodItem.Name);
            NavigationService.Navigate(new Uri("/Source/Pages/ServingViewer.xaml?" + data.Build(), UriKind.Relative));
        }

        private void ingredientList_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSelection = (sender as ListBox).SelectedItem as CustomIngredient;
        }

        private void cancelButtonClick(object sender, RoutedEventArgs e)
        {
            quit();
        }

        private void okButtonClick(object sender, RoutedEventArgs e)
        {
            if (recipeName.Text.Length > 0)
            {
                CurrentRecipe.Name = recipeName.Text;

                bool save = true;
                if(Main.CurrentUserProfile.RecipeExists(CurrentRecipe.Name))
                {
                    if(MessageBox.Show("A recipe with that name already exists. Saving will overwrite it. is this ok?", "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                        save = false;
                }

                if (save)
                {
                    Main.CurrentUserProfile.SaveRecipe(CurrentRecipe);
                    CurrentRecipe = new CustomRecipe();
                    MessageBox.Show("Recipe " + recipeName.Text + " was saved.", "Information", MessageBoxButton.OK);
                    recipeNameStatic = null;
                    NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, Main.PageID);
                    NavigationService.Navigate(new Uri("/Source/Pages/Main.xaml?" + data.Build(), UriKind.Relative));
                }
            }
            else
                MessageBox.Show("Please enter a recipe name.", "Error", MessageBoxButton.OK);
        }

        private void quit()
        {
            if (MessageBox.Show("Leaving will lose any unsaved changes, are you sure?", "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                CurrentRecipe.clear();
                NavigationData data = new NavigationData(PageID, NavigationData.REJECT, Main.PageID);
                NavigationService.Navigate(new Uri("/Source/Pages/Main.xaml?" + data.Build(), UriKind.Relative));
            }
        }

        private void UpdateIngredientList()
        {
            ingredientList.ItemsSource = null;
            ingredientList.ItemsSource = CurrentRecipe.Ingredients;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationData data = NavigationData.ParseURI(e.Uri.ToString());
            
            // If the navigation has come from serving viewer or custom serving form, add new ingredient 
            if ((data.GetPageID() == ServingView.PageID || data.GetPageID() == CustomServingPage.PageID) && data.Accepted())
            {
                string isAdding = data.Get("isAdding");

                if (isAdding == "true")
                    CurrentRecipe.AddIngredient(new CustomIngredient(foodToAdd, servingToAdd));
                else
                {
                    foreach(CustomIngredient i in CurrentRecipe.Ingredients)
                    {
                        if (i == currentButtonContext)
                        {
                            i.Serving = servingToAdd;
                            break;
                        }
                    }
                }
            }
            if(data.GetPageID() == CustomRecipeViewer.PageID && data.Accepted())
                recipeNameStatic = CurrentRecipe.Name;

            if (recipeNameStatic != null)
                recipeName.Text = recipeNameStatic;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationData data = NavigationData.ParseURI(e.Uri.ToString());

            // When navigating to serving view, set the food ID to the current button context
            if(data.GetTargetPageID() == ServingView.PageID)
            {
                Page page = (e.Content as Page);
                ServingView.CurrentFoodID = currentButtonContext.FoodItem.ID;
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            quit();
        }
    }
}