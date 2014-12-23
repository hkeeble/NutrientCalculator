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
    public partial class CustomServingPage : PhoneApplicationPage
    {
        public static readonly string PageID = "CustomServingPage";

        public Serving serving;
        public Serving resultantServing;
        private Serving baseServing;

        bool isAdding = false;

        public CustomServingPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationData data = NavigationData.ParseURI(e.Uri.ToString());
            if (data.Get("isAdding") == "true")
                isAdding = true;
            else
                isAdding = false;

            foodName.Text = "Enter custom serving size for: \n" + data.Get("foodName");

            baseServing = serving.CreateBase();

            unitLabel.Text = baseServing.MetricServingUnit;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (servingBox.Text.Length > 0)
            {
                double amount = Convert.ToDouble(servingBox.Text);
                if (amount > 0)
                {
                    resultantServing = baseServing.CreateMultiple(amount);
                    RecipeBuilder.servingToAdd = resultantServing; 

                    NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, RecipeBuilder.PageID);

                    if (isAdding)
                        data.AddData("isAdding", "true");
                    else
                        data.AddData("isAdding", "false");

                    NavigationService.Navigate(new Uri("/Source/Pages/RecipeBuilder.xaml?" + data.Build(), UriKind.Relative));
                }
                else
                    MessageBox.Show("Please enter a serving size greater than 0.", "Error", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Please enter a serving size.", "Error", MessageBoxButton.OK);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationData data = new NavigationData(PageID, NavigationData.REJECT, ServingView.PageID);
            NavigationService.Navigate(new Uri("/Source/Pages/ServingViewer.xaml?" + data.Build(), UriKind.Relative));
        }
    }
}