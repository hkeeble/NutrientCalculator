using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using FatSecretAPI;

namespace NutrientCalculator.Source.Pages
{
    public partial class FoodSearch : PhoneApplicationPage
    {
        FatSecretService svc;

        FoodSearchResults currentResults;

        int currentPageNumber;
        const int RESULTS_PER_PAGE = 10;

        string currentSearchTerms;

        public FoodSearch()
        {
            InitializeComponent();

            svc = new FatSecretService();
            currentPageNumber = 0;
        }

        private void searchButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            currentPageNumber = 0;
            previousPageButton.IsEnabled = false;
            nextPageButton.IsEnabled = false;

            if (searchTerms.Text.Length > 0)
            {
                currentSearchTerms = searchTerms.Text;
                pageLabel.Text = "Page: 0";
                SearchFood(currentSearchTerms, 0);
            }
            else
                MessageBox.Show("Please enter search terms.", "Error", MessageBoxButton.OK);
        }

        private void SearchFood(string terms, int pageNumber)
        {
            foodList.Focus();
            searchButton.IsEnabled = false;
            svc.SearchFood(searchTerms.Text, RESULTS_PER_PAGE, pageNumber, FoodDownloadFinishedHandler);
        }

        private void FoodDownloadFinishedHandler(object sender, DownloadStringCompletedEventArgs args)
        {
            FoodSearchResults results = svc.DeserializeFoodSearch(args.Result);

            foodList.Focus();

            if (results != null && results.Results != null)
            {
                pageLabel.Text = "Page: " + Convert.ToString(currentPageNumber);

                foodList.ItemsSource = results.Results;
                nextPageButton.IsEnabled = true;
                if (currentPageNumber != 0)
                    previousPageButton.IsEnabled = true;
                else
                    previousPageButton.IsEnabled = false;
            }

            searchButton.IsEnabled = true;
        }

        private void foodList_selectionChaged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void nextPageButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            currentPageNumber++;
            SearchFood(currentSearchTerms, currentPageNumber);
        }

        private void previousPageButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            currentPageNumber--;
            SearchFood(currentSearchTerms, currentPageNumber);
        }
    }
}