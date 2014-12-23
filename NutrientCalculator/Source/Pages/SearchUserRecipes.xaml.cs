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
    public partial class SearchUserRecipes : PhoneApplicationPage
    {
        SADService.Service1Client client;

        public SearchUserRecipes()
        {
            InitializeComponent();

            client = new SADService.Service1Client();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if(searchTerms.Text.Length == 0)
            {
                MessageBox.Show("Please enter some search terms.", "Error", MessageBoxButton.OK);
            }
            else
            {
                client.SearchImagesCompleted += new EventHandler<SADService.SearchImagesCompletedEventArgs>(imageSearchCompleted);
                LoadingBar.Show("Searching...");
                client.SearchImagesAsync(UploadRecipe.GUID, searchTerms.Text);
            }
        }

        private void imageSearchCompleted(object sender, SADService.SearchImagesCompletedEventArgs e)
        {
            LoadingBar.Hide();
            recipeList.ItemsSource = null;

            if (e.Result.Count == 0)
                MessageBox.Show("No recipes found. Try a different search tag.", "Information", MessageBoxButton.OK);

            recipeList.ItemsSource = e.Result;
        }
    }
}