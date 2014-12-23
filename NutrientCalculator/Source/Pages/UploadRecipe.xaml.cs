using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

using Assignment;

namespace NutrientCalculator.Source.Pages
{
    public partial class UploadRecipe : PhoneApplicationPage
    {
        public static readonly string PageID = "uploadRecipe";

        public static CustomRecipe CurrentRecipe;

        public UploadRecipe()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            recipeLabel.Text = "Uploading Recipe:\n" + CurrentRecipe.Name;
        }

        private void captureButton_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureTask task = new CameraCaptureTask();
            task.Completed += new EventHandler<PhotoResult>(cameraCaptureTask_Completed);
            task.Show();
        }

        void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {

        }

        private void chooseButtonClick(object sender, RoutedEventArgs e)
        {
            openChooser();
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void recipeImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            openChooser();
        }

        private void openChooser()
        {

        }
    }
}