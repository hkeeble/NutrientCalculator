using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

using Newtonsoft.Json;

using Assignment;

namespace NutrientCalculator.Source.Pages
{
    public partial class UploadRecipe : PhoneApplicationPage
    {
        // GUID used by the cloud service
        public  static readonly Guid GUID = new Guid("a7553def-088f-4c14-9218-13cf2b09c871");

        public static readonly string PageID = "uploadRecipe";

        public static CustomRecipe CurrentRecipe;

        BitmapImage currentImage;

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

        private void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            currentImage = new BitmapImage();
            currentImage.SetSource(e.ChosenPhoto);
            recipeImage.Source = currentImage;
        }

        private void chooserTask_Completed(object sender, PhotoResult e)
        {
            currentImage = new BitmapImage();
            currentImage.SetSource(e.ChosenPhoto);
            recipeImage.Source = currentImage;
        }

        private void chooseButtonClick(object sender, RoutedEventArgs e)
        {
            openChooser();
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            if (descriptionBox.Text.Length == 0 || categoryBox.Text.Length == 0)
            {
                MessageBox.Show("Please enter a description and category for this recipe.", "Error", MessageBoxButton.OK);
            }
            else
            {
                WriteableBitmap bmp = new WriteableBitmap((BitmapSource)recipeImage.Source);

                byte[] bytes;

                using (MemoryStream stream = new MemoryStream())
                {
                    bmp.SaveJpeg(stream, bmp.PixelWidth, bmp.PixelHeight, 0, 50);
                    bytes = stream.ToArray();
                }

                SADService.Service1Client client = new SADService.Service1Client();
                client.AddImageCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(uploadImageCompleted);
                client.AddImageAsync(GUID, bytes, "0", "0", descriptionBox.Text, categoryBox.Text);
                LoadingBar.Show("Uploading Recipe...");
                chooseButton.Focus();
                uploadButton.IsEnabled = false;
            }
        }

        void uploadImageCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            LoadingBar.Hide();
            chooseButton.Focus();
            uploadButton.IsEnabled = true;

            if (e.Error != null)
            {
                MessageBox.Show("Error, could not upload recipe: " + e.Error.Message, "Error", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Recipe uploaded successfully.", "Information", MessageBoxButton.OK);

            NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, Main.PageID);
            NavigationService.Navigate(new Uri("/Source/Pages/Main.xaml?" + data.Build(), UriKind.Relative));
        }

        private void recipeImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            openChooser();
        }

        private void openChooser()
        {
            PhotoChooserTask task = new PhotoChooserTask();
            task.Completed += new EventHandler<PhotoResult>(chooserTask_Completed);
            task.Show();
        }
    }
}