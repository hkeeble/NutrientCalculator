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
    public partial class ServingView : PhoneApplicationPage
    {
        public static readonly string PageID = "servingView";

        FatSecretService svc;

        public static string CurrentFoodID;
        private Food food;
        private List<Serving> servings;
        private Serving currentSelection;

        bool isAdding;

        public ServingView()
        {
            InitializeComponent();

            svc = new FatSecretService();
            isAdding = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationData data = NavigationData.ParseURI(e.Uri.ToString());

            if (data.GetPageID() == FoodSearch.PageID)
                isAdding = true;
            else
                isAdding = false;

            if(data.GetPageID() == FoodSearch.PageID || data.GetPageID() == RecipeBuilder.PageID || data.GetPageID() == CustomServingPage.PageID)
            {
                LoadingBar.Show("Retrieving Serving List...");
                svc.GetFood(CurrentFoodID, FoodDownloadFinishedHandler);
            }

            base.OnNavigatedTo(e);
        }

        private void FoodDownloadFinishedHandler(object sender, DownloadStringCompletedEventArgs args)
        {
            LoadingBar.Hide();

            food = svc.DeserializeFood(args.Result);
            servings = new List<Serving>();

            foreach (Serving serving in food.Servings.Values)
                servings.Add(serving);

            servingList.ItemsSource = servings;

            foodName.Text = "Select serving size for:\n" + food.Name;
        }

        private void customServingButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, CustomServingPage.PageID);

            if (isAdding)
                data.AddData("isAdding", "true");
            else
                data.AddData("isAdding", "false");

            data.AddData("foodName", food.Name);

            NavigationService.Navigate(new Uri("/Source/Pages/CustomServingPage.xaml?" + data.Build(), UriKind.Relative));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationData data = NavigationData.ParseURI(e.Uri.ToString());

            if (data.Accepted())
            {
                if (data.GetTargetPageID() == CustomServingPage.PageID)
                {
                    Page page = e.Content as Page;
                    if (page is CustomServingPage)
                        (page as CustomServingPage).serving = servings[0];
                    RecipeBuilder.foodToAdd = food;
                }
                else if (data.GetTargetPageID() == RecipeBuilder.PageID)
                {
                    if (data.Accepted())
                    {
                        RecipeBuilder.servingToAdd = currentSelection;
                        RecipeBuilder.foodToAdd = food;
                    }
                }
            }
        }

        private void servingSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSelection = (sender as ListBox).SelectedItem as Serving;
            NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, RecipeBuilder.PageID);
            
            if (isAdding)
                data.AddData("isAdding", "true");
            else
                data.AddData("isAdding", "false");

            NavigationService.Navigate(new Uri("/Source/Pages/RecipeBuilder.xaml?" + data.Build(), UriKind.Relative));
        }

        // Handle back key
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            NavigationData data = new NavigationData(PageID, NavigationData.REJECT, FoodSearch.PageID);
            NavigationService.Navigate(new Uri("/Source/Pages/FoodSearch.xaml?" + data.Build(), UriKind.Relative));
        }
    }
}