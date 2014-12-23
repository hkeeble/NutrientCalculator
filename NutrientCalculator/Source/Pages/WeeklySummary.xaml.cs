using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Assignment;

namespace NutrientCalculator.Source.Pages
{
    public partial class WeeklySummary : PhoneApplicationPage
    {
        List<DailyTracker> days;

        public WeeklySummary()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            days = Main.CurrentUserProfile.WeeklyTrackerData;

            StackPanel[] dayPanels = new StackPanel[days.Count];

            int totalAmountUnder = 0;
            int totalAmountOver = 0;
            int totalAmountInner = 0;

            for(int i = 0; i < days.Count; i++)
            {
                dayPanels[i] = new StackPanel();
                TextBlock day = new TextBlock();
                day.Text = "Day " + Convert.ToString(i + 1) + " - " + days[i].StartTime.DayOfWeek.ToString() + " " + days[i].StartTime.Date.Day + " " + days[i].StartTime.ToString("MMMM");
                day.FontWeight = FontWeights.Bold;
                day.FontSize = 28;

                // Add Nutrients
                TextBlock calories = new TextBlock();
                TextBlock protein = new TextBlock();
                TextBlock carbohydrate = new TextBlock();
                TextBlock sugars = new TextBlock();
                TextBlock fat = new TextBlock();
                TextBlock saturates = new TextBlock();
                TextBlock fiber = new TextBlock();
                TextBlock salt = new TextBlock();

                calories.Text = "Calories: " + Convert.ToString(days[i].Nutrients.Calories) + "kcal";
                protein.Text = "Protein: " + Convert.ToString(days[i].Nutrients.Protein) + "g";
                carbohydrate.Text = "Carbohydrate: " + Convert.ToString(days[i].Nutrients.Carbohydrate) + "g";
                sugars.Text = "Sugars: " + Convert.ToString(days[i].Nutrients.Sugars) + "g";
                fat.Text = "Fat: " + Convert.ToString(days[i].Nutrients.Fat) + "g";
                saturates.Text = "Saturates: " + Convert.ToString(days[i].Nutrients.Saturates) + "g";
                fiber.Text = "Fiber: " + Convert.ToString(days[i].Nutrients.Fiber) + "g";
                salt.Text = "Salt: " + Convert.ToString(days[i].Nutrients.Salt) + "g";

                int amountOver = 0;
                int amountUnder = 0;
                int amountInner = 0;

                // Change colors based upon comparison with range (far too much boilerplate! Need to find a more convinient way to manage nutrients.......)
                if (days[i].Nutrients.Calories < Main.CurrentUserProfile.DietSpecifications.Calories.Minimum)
                {
                    calories.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                    amountUnder++;
                }
                else if (days[i].Nutrients.Calories > Main.CurrentUserProfile.DietSpecifications.Calories.Maximum)
                {
                    calories.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    amountOver++;
                }
                else
                {
                    calories.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                    amountInner++;
                }

                if (days[i].Nutrients.Protein < Main.CurrentUserProfile.DietSpecifications.Protein.Minimum)
                {
                    protein.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                    amountUnder++;
                }
                else if (days[i].Nutrients.Protein > Main.CurrentUserProfile.DietSpecifications.Protein.Maximum)
                {
                    protein.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    amountOver++;
                }
                else
                {
                    protein.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                    amountInner++;
                }

                if (days[i].Nutrients.Carbohydrate < Main.CurrentUserProfile.DietSpecifications.Carbohydrate.Minimum)
                {
                    carbohydrate.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                    amountUnder++;
                }
                else if (days[i].Nutrients.Carbohydrate > Main.CurrentUserProfile.DietSpecifications.Carbohydrate.Maximum)
                {
                    carbohydrate.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    amountOver++;
                }
                else
                {
                    carbohydrate.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                    amountInner++;
                }

                if (days[i].Nutrients.Sugars < Main.CurrentUserProfile.DietSpecifications.Sugars.Minimum)
                {
                    sugars.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                    amountUnder++;
                }
                else if (days[i].Nutrients.Sugars > Main.CurrentUserProfile.DietSpecifications.Sugars.Maximum)
                {
                    sugars.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    amountOver++;
                }
                else
                {
                    amountInner++;
                    sugars.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                }

                if (days[i].Nutrients.Fat < Main.CurrentUserProfile.DietSpecifications.Fat.Minimum)
                {
                    fat.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                    amountUnder++;
                }
                else if (days[i].Nutrients.Fat > Main.CurrentUserProfile.DietSpecifications.Fat.Maximum)
                {
                    fat.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    amountOver++;
                }
                else
                {
                    fat.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                    amountInner++;
                }

                if (days[i].Nutrients.Saturates < Main.CurrentUserProfile.DietSpecifications.Saturates.Minimum)
                {
                    amountUnder++;
                    saturates.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                }
                else if (days[i].Nutrients.Saturates > Main.CurrentUserProfile.DietSpecifications.Saturates.Maximum)
                {
                    amountOver++;
                    saturates.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                }
                else
                {
                    amountInner++;
                    saturates.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                }

                if (days[i].Nutrients.Fiber < Main.CurrentUserProfile.DietSpecifications.Fibre.Minimum)
                {
                    amountUnder++;
                    fiber.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                }
                else if (days[i].Nutrients.Fiber > Main.CurrentUserProfile.DietSpecifications.Fibre.Maximum)
                {
                    amountOver++;
                    fiber.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                }
                else
                {
                    amountInner++;
                    fiber.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                }

                if (days[i].Nutrients.Salt < Main.CurrentUserProfile.DietSpecifications.Salt.Minimum)
                {
                    amountUnder++;
                    salt.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));

                }
                else if (days[i].Nutrients.Salt > Main.CurrentUserProfile.DietSpecifications.Salt.Maximum)
                {
                    amountOver++;
                    salt.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                }
                else
                {
                    amountInner++;
                    salt.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                }

                // Add to totals
                totalAmountInner += amountInner;
                totalAmountOver += amountOver;
                totalAmountUnder += amountUnder;

                TextBlock summary = new TextBlock();
                if (amountInner > amountUnder && amountInner > amountOver)
                {
                    summary.Text = "You're eating just right!";
                    summary.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
                }
                if (amountUnder > amountInner && amountUnder > amountOver)
                {
                    summary.Text = "You need to eat more!";
                    summary.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                }
                if (amountOver > amountUnder && amountOver > amountInner)
                {
                    summary.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    summary.Text = "You need to eat less!";
                }

                dayPanels[i].Children.Add(day);
                dayPanels[i].Children.Add(summary);
                dayPanels[i].Children.Add(calories);
                dayPanels[i].Children.Add(protein);
                dayPanels[i].Children.Add(carbohydrate);
                dayPanels[i].Children.Add(sugars);
                dayPanels[i].Children.Add(fat);
                dayPanels[i].Children.Add(saturates);
                dayPanels[i].Children.Add(fiber);
                dayPanels[i].Children.Add(salt);

                mainPanel.Children.Add(dayPanels[i]);
            }

            if (totalAmountInner > totalAmountUnder && totalAmountInner > totalAmountOver)
            {
                verdictLabel.Text = "Verdict this week: You're eating just right!";
                verdictLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 255));
            }
            if (totalAmountUnder > totalAmountInner && totalAmountUnder > totalAmountOver)
            {
                verdictLabel.Text = "Verdict this week: You need to eat more!";
                verdictLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
            }
            if (totalAmountOver > totalAmountUnder && totalAmountOver > totalAmountInner)
            {
                verdictLabel.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                verdictLabel.Text = "Verdict this week: You need to eat less!";
            }

        }
    }
}