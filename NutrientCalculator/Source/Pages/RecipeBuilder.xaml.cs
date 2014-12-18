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
    public partial class RecipeBuilder : PhoneApplicationPage
    {
        public RecipeBuilder()
        {
            InitializeComponent();
        }

        private void addFoodButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/FoodSearch.xaml", UriKind.Relative));
        }
    }
}