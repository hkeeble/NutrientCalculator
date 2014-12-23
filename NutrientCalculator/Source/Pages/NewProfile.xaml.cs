using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

using Assignment;

namespace NutrientCalculator.Source.Pages
{
    public partial class NewProfile : PhoneApplicationPage
    {
        public static readonly string PageID = "NewProfile";

        PhotoChooserTask chooser;

        BitmapImage displayImage;
        WriteableBitmap profileBitmap;

        private double imageAngle;

        public NewProfile()
        {
            InitializeComponent();

            chooser = new PhotoChooserTask();
            chooser.Completed += new EventHandler<PhotoResult>(profileChooserTask_Completed);

            imageAngle = 0;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs args)
        {
            Uri uri = new Uri("/Images/defaultProfile.png", UriKind.Relative);
            displayImage = new BitmapImage(uri);
            displayImage.CreateOptions = BitmapCreateOptions.None;
            displayImage.ImageOpened += (s, e) =>
                {
                    profileBitmap = new WriteableBitmap((BitmapImage)s);
                    profileImage.Source = profileBitmap;
                };
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
                NavigationData data = new NavigationData(PageID, NavigationData.ACCEPT, MainPage.PageID);
                data.AddData(NavigationData.PROFILE_NAME, nameTextBox.Text);

                // Save temporary profile pic to isolated storage for later use
                WriteableBitmap proflePic = new WriteableBitmap(profileBitmap);
                string profilePicLocation = @"temp\" + nameTextBox.Text + "_picture.jpg";
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = storage.CreateFile(profilePicLocation))
                    {
                        proflePic.SaveJpeg(stream, 200, 100, 0, 95);
                        stream.Close();
                    }
                }

                string gender = "";
                if (maleRadioButton.IsChecked == true)
                    gender = Gender.MALE.ToString();
                else
                    gender = Gender.FEMALE.ToString();

                data.AddData(NavigationData.PROFILE_IMAGE, profilePicLocation);
                data.AddData(NavigationData.PROFILE_GENDER, gender);

                NavigationService.Navigate(new Uri("/MainPage.xaml?" + data.Build(), UriKind.Relative));
            }
            else
                MessageBox.Show("Please enter a name between 1 and 16 characters long.", "Error", MessageBoxButton.OK);
        }

        private void rotateButtonClick(object sender, RoutedEventArgs e)
        {
            profileBitmap = profileBitmap.Rotate(1);
            profileImage.Source = profileBitmap;
        }
    }
}