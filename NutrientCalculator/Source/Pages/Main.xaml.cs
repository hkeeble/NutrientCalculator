using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Assignment;

namespace NutrientCalculator.Source.Pages
{
    /// <summary>
    /// This main page acts a a hub for the entire application. It also holds the current user profile.
    /// </summary>
    public partial class Main : PhoneApplicationPage
    {
        public static readonly string PageID = "Main";

        public static UserProfile CurrentUserProfile = new UserProfile();

        public Main()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Update user profile based upon time.
            CurrentUserProfile.UpdateTime(DateTime.Now.AddDays(1));

            // Update summary based on new user profile
            UpdateTotals();
            UpdateComparisons();
            UpdateMeals();
        }

        private void UpdateTotals()
        {
            CaloriesTotal.Text =    "Calories: "    + Convert.ToString(CurrentUserProfile.CurrentDayTracker.Nutrients.Calories) + "kcal";
            ProteinTotal.Text =     "Protein: "     + Convert.ToString(CurrentUserProfile.CurrentDayTracker.Nutrients.Protein) + "g";
            SugarsTotal.Text =      "Sugars: "      + Convert.ToString(CurrentUserProfile.CurrentDayTracker.Nutrients.Sugars) + "g";
            FatTotal.Text =         "Fat: "         + Convert.ToString(CurrentUserProfile.CurrentDayTracker.Nutrients.Fat) + "g";
            SaturatesTotal.Text =   "Saturates: "   + Convert.ToString(CurrentUserProfile.CurrentDayTracker.Nutrients.Saturates) + "g";
            FiberTotal.Text =       "Fiber: "       + Convert.ToString(CurrentUserProfile.CurrentDayTracker.Nutrients.Fiber) + "g";
            SaltTotal.Text =        "Salt: "        + Convert.ToString(CurrentUserProfile.CurrentDayTracker.Nutrients.Salt) + "g";
            CarbohydrateTotal.Text = "Carbohydrate: " + Convert.ToString(CurrentUserProfile.CurrentDayTracker.Nutrients.Carbohydrate) + "g";
        }

        private void UpdateComparisons()
        {
            comparisonPanel.Children.Clear();

            // Obtain differences
            NutrientData maximum = NutrientData.CreateFromSpecification(CurrentUserProfile.DietSpecifications, true);
            NutrientData minimum = NutrientData.CreateFromSpecification(CurrentUserProfile.DietSpecifications, false);
            NutrientData maxDiff = (CurrentUserProfile.CurrentDayTracker.Nutrients - maximum).Absolute();
            NutrientData minDiff = (CurrentUserProfile.CurrentDayTracker.Nutrients - minimum).Absolute();

            List<TextBlock> blocks = new List<TextBlock>();

            TextBlock calories = new TextBlock();
            TextBlock protein = new TextBlock();
            TextBlock carbohydrate = new TextBlock();
            TextBlock sugars = new TextBlock();
            TextBlock fat = new TextBlock();
            TextBlock saturates = new TextBlock();
            TextBlock fiber = new TextBlock();
            TextBlock salt = new TextBlock();

            // Calories
            if (CurrentUserProfile.CurrentDayTracker.Nutrients.Calories < minimum.Calories)
            {
                calories.Text = "Eat More Calories! (" + Convert.ToString(minDiff.Calories) + "kcal - " + Convert.ToString(maximum.Calories - CurrentUserProfile.CurrentDayTracker.Nutrients.Calories) + "kcal)";
                calories.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                blocks.Add(calories);
            }
            else
            {
                calories.Text = "Too Many Calories! (Exceeded by " + Convert.ToString(maxDiff.Calories) + "kcal)";
                calories.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                blocks.Add(calories);
            }

            // Protein
            if (CurrentUserProfile.CurrentDayTracker.Nutrients.Protein < minimum.Protein)
            {
                protein.Text = "Eat More Protein! (" + Convert.ToString(minDiff.Calories) + "g - " + Convert.ToString(maximum.Protein - CurrentUserProfile.CurrentDayTracker.Nutrients.Protein) + "g)";
                protein.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                blocks.Add(protein);
            }
            else
            {
                protein.Text = "Too Much Protein! (Exceeded by " + Convert.ToString(maxDiff.Protein) + "g)";
                protein.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                blocks.Add(protein);
            }

            // Carbohydrate
            if (CurrentUserProfile.CurrentDayTracker.Nutrients.Carbohydrate < minimum.Carbohydrate)
            {
                carbohydrate.Text = "Eat More Carbohydrate! (" + Convert.ToString(minDiff.Carbohydrate) + "g - " + Convert.ToString(maximum.Carbohydrate - CurrentUserProfile.CurrentDayTracker.Nutrients.Carbohydrate) + "g)";
                carbohydrate.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                blocks.Add(carbohydrate);
            }
            else
            {
                carbohydrate.Text = "Too Much Carbohydrate! (Exceeded by " + Convert.ToString(maxDiff.Carbohydrate) + "g)";
                carbohydrate.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                blocks.Add(carbohydrate);
            }

            // Sugars
            if (CurrentUserProfile.CurrentDayTracker.Nutrients.Sugars < minimum.Sugars)
            {
                sugars.Text = "Eat More Sugar! (" + Convert.ToString(minDiff.Sugars) + "g - " + Convert.ToString(maximum.Sugars - CurrentUserProfile.CurrentDayTracker.Nutrients.Sugars) + "g)";
                sugars.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                blocks.Add(sugars);
            }
            else
            {
                sugars.Text = "Too Much Sugar! (Exceeded by " + Convert.ToString(maxDiff.Calories) + "g)";
                sugars.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                blocks.Add(sugars);
            }

            // Fat
            if (CurrentUserProfile.CurrentDayTracker.Nutrients.Fat < minimum.Fat)
            {
                fat.Text = "Eat More Fat! (" + Convert.ToString(minDiff.Fat) + "g - " + Convert.ToString(maximum.Fat - CurrentUserProfile.CurrentDayTracker.Nutrients.Fat) + "g)";
                fat.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                blocks.Add(fat);
            }
            else
            {
                fat.Text = "Too Much Fat! (Exceeded by " + Convert.ToString(maxDiff.Fat) + "g)";
                fat.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                blocks.Add(fat);
            }

            // Saturates
            if (CurrentUserProfile.CurrentDayTracker.Nutrients.Saturates < minimum.Saturates)
            {
                saturates.Text = "Eat More Saturates! (" + Convert.ToString(minDiff.Saturates) + "g - " + Convert.ToString(maximum.Saturates - CurrentUserProfile.CurrentDayTracker.Nutrients.Saturates) + "g)";
                saturates.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                blocks.Add(saturates);
            }
            else
            {
                saturates.Text = "Too Many Saturates! (Exceeded by " + Convert.ToString(maxDiff.Saturates) + "g)";
                saturates.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                blocks.Add(saturates);
            }

            // Fiber
            if (CurrentUserProfile.CurrentDayTracker.Nutrients.Fiber < minimum.Fiber)
            {
                fiber.Text = "Eat More Fiber! (" + Convert.ToString(minDiff.Fiber) + "g - " + Convert.ToString(maximum.Fiber - CurrentUserProfile.CurrentDayTracker.Nutrients.Fiber) + "g)";
                fiber.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                blocks.Add(fiber);
            }
            else
            {
                fiber.Text = "Too Much Fiber! (Exceeded by " + Convert.ToString(maxDiff.Fiber) + "g)";
                fiber.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                blocks.Add(fiber);
            }

            // Salt
            if (CurrentUserProfile.CurrentDayTracker.Nutrients.Salt < minimum.Salt)
            {
                salt.Text = "Eat More Salt! (" + Convert.ToString(minDiff.Salt) + "g - " + Convert.ToString(maximum.Salt - CurrentUserProfile.CurrentDayTracker.Nutrients.Salt) + "g)";
                salt.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                blocks.Add(salt);
            }
            else
            {
                salt.Text = "Too Much Salt! (Exceeded by " + Convert.ToString(maxDiff.Salt) + "g)";
                salt.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                blocks.Add(salt);
            }

            foreach(TextBlock block in blocks)
                comparisonPanel.Children.Add(block);
            
            if(blocks.Count == 0)
            {
                TextBlock block = new TextBlock();
                block.Text = "All within guidelines! Well done!";
                block.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
                comparisonPanel.Children.Add(block);
            }
        }

        private void UpdateMeals()
        {
            mealPanel.Children.Clear();

            if(CurrentUserProfile.CurrentDayTracker.Meals.Count == 0)
            {
                TextBlock block = new TextBlock();
                block.Text = "No Meals Eaten Today.";
                block.Foreground = new SolidColorBrush(Color.FromArgb(255, 211, 211, 211));
                mealPanel.Children.Add(block);
            }
            else
            {
                foreach(CustomRecipe meal in CurrentUserProfile.CurrentDayTracker.Meals)
                {
                    TextBlock block = new TextBlock();
                    block.Text = meal.Name;
                    mealPanel.Children.Add(block);
                }
            }
        }

        private void buildRecipeTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/RecipeBuilder.xaml", UriKind.Relative));
        }

        private void weeklySummaryTile_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/WeeklySummary.xaml", UriKind.Relative));
        }

        private void addAMeal_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/MealPicker.xaml", UriKind.Relative));
        }

        private void fatSecretRecipeSearch_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void browseRecipes_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/CustomRecipeViewer.xaml", UriKind.Relative));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            CurrentUserProfile.Save();
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void browseUserRecipes_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/UserRecipeBrowser.xaml", UriKind.Relative));
        }

        private void searchUserRecipes_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Source/Pages/SearchUserRecipes.xaml", UriKind.Relative));
        }
    }
}