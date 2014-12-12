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
using System.Windows.Media.Imaging;

using Assignment;

namespace NutrientCalculator.Source.Pages
{
    public partial class NewProfile : PhoneApplicationPage
    {
        PhotoChooserTask chooser;

        BitmapImage profileBitmap;

        public NewProfile()
        {
            InitializeComponent();

            profileBitmap = new BitmapImage();

            chooser = new PhotoChooserTask();
            chooser.Completed += new EventHandler<PhotoResult>(profileChooserTask_Completed);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("/Images/defaultProfile.png", UriKind.Relative);
            profileBitmap.UriSource = uri;
            profileImage.Source = profileBitmap;
        }

        private void chooseButton_Click(object sender, RoutedEventArgs e)
        {
            chooser.Show();
        }

        public void profileChooserTask_Completed(object sender, PhotoResult args)
        {
            if (args.ChosenPhoto != null)
            {
                profileBitmap.SetSource(args.ChosenPhoto);
                profileImage.Source = profileBitmap;
            }
        }

        private void profileImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            chooser.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text.Length < 16 && nameTextBox.Text.Length > 0)
            {
                NavigationData data = new NavigationData();
                data.AddData(NavigationData.PROFILE_NAME, nameTextBox.Text);
                data.AddData(NavigationData.PROFILE_IMAGE, profileBitmap.UriSource.ToString());

                NavigationService.Navigate(new Uri("/MainPage.xaml?" + data.Build(), UriKind.Relative));
            }
            else
                MessageBox.Show("Please enter a name between 1 and 16 characters long.", "Error", MessageBoxButton.OK);
        }
    }
}