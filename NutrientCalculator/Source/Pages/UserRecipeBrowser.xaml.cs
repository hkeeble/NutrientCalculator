using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Assignment;

namespace NutrientCalculator.Source.Pages
{
    public partial class UserRecipeBrowser : PhoneApplicationPage
    {
        SADService.Service1Client client;

        public UserRecipeBrowser()
        {
            InitializeComponent();

            client = new SADService.Service1Client();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            client.ViewImagesCompleted += new EventHandler<SADService.ViewImagesCompletedEventArgs>(imageDownloadComplete);
            client.ViewImagesAsync(UploadRecipe.GUID);
            LoadingBar.Show("Loading images...");
        }

        private void imageDownloadComplete(object sender, SADService.ViewImagesCompletedEventArgs e)
        {
            LoadingBar.Hide();
            recipeList.ItemsSource = e.Result;
        }
    }
}