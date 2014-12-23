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
    public partial class FoodSearch : PhoneApplicationPage
    {
        public static readonly string PageID = "foodSearch";

        FatSecretService svc;

        FoodSearchResults currentResults;
        FoodSearchResult currentSelection;

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
            LoadingBar.Show("Searching...");
            svc.SearchFood(searchTerms.Text, RESULTS_PER_PAGE, pageNumber, FoodDownloadFinishedHandler);
        }

        private void FoodDownloadFinishedHandler(object sender, DownloadStringCompletedEventArgs args)
        {
            LoadingBar.Hide();

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
            currentSelection = (sender as ListBox).SelectedItem as FoodSearchResult;

            if (currentSelection != null)
            {
                NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, ServingView.PageID);
                NavigationService.Navigate(new Uri("/Source/Pages/ServingViewer.xaml?" + data.Build(), UriKind.Relative));
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Page page = e.Content as Page;
            if (page is ServingView)
                ServingView.CurrentFoodID = currentSelection.ID;
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

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            NavigationData data = new NavigationData(PageID, NavigationData.REJECT, RecipeBuilder.PageID);
            NavigationService.Navigate(new Uri("/Source/Pages/RecipeBuilder.xaml?" + data.Build(), UriKind.Relative));
        }
    }
}