using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;
using System.Net;
using System.Windows;
using System.Windows.Controls;

using Newtonsoft.Json;

namespace Assignment
{
    public enum Gender
    {
        MALE,
        FEMALE
    }

    struct UKGovGDA
    {
        public double Calories, Protein, Carbohydrate, Sugars, Fat, Saturates, Fibre, Salt;

        public UKGovGDA(Gender gender)
        {
            Fibre = 24;
            Salt = 6;
            if(gender == Gender.MALE)
            {
                    Calories = 2500;
                    Protein = 55;
                    Carbohydrate = 300;
                    Sugars = 120;
                    Fat = 95;
                    Saturates = 30;
            }
            else
            {
                    Calories = 2000;
                    Protein = 45;
                    Carbohydrate = 230;
                    Sugars = 90;
                    Fat = 70;
                    Saturates = 20;
            }
        }
    }

    public struct Range
    {
        private double min;
        private double max;

        [JsonIgnore]
        public double Minimum { get { return min; } }

        [JsonIgnore]
        public double Maximum { get { return max; } }

        public Range(double minimum, double maximum)
        {
            min = minimum;
            max = maximum;
        }

        public Range(double median, double toleranceUp, double toleranceDown)
        {
            double tolUpDecimal = toleranceUp / 100;
            double tolDownDecimal = toleranceDown / 100;
            min = median - (tolDownDecimal * median);
            max = median + (tolUpDecimal * median);
        }
    }

    /// <summary>
    /// Represents a collection of dietary requirements.
    /// </summary>
    public class DietSpecification
    {
        public Range Calories;
        public Range Protein;
        public Range Carbohydrate;
        public Range Sugars;
        public Range Fat;
        public Range Saturates;
        public Range Fibre;
        public Range Salt;

        public DietSpecification()
        {

        }

        /// <summary>
        /// Creates a new collection of dietary needs based on UK GDA (can be found at: http://www.foodlabel.org.uk/label/gda_values.aspx). Tolerance
        /// is the percentage either side that is deemed acceptable by the set of dietary specifications.
        /// </summary>
        /// <param name="variance"></param>
        /// <returns></returns>
        public static DietSpecification createGDA(Gender gender, double tolerance)
        {
            DietSpecification spec = new DietSpecification();
            UKGovGDA gda = new UKGovGDA(gender);

            double tolDecimal = tolerance / 100;

            // Initialize all ranges, using tolerance
            spec.Calories       = new Range(gda.Calories, tolerance);
            spec.Protein        = new Range(gda.Protein, tolerance);
            spec.Carbohydrate   = new Range(gda.Carbohydrate, tolerance);
            spec.Sugars         = new Range(gda.Sugars, tolerance);
            spec.Fat            = new Range(gda.Fat, tolerance);
            spec.Saturates      = new Range(gda.Saturates, tolerance);
            spec.Fibre          = new Range(gda.Fibre, tolerance);
            spec.Salt           = new Range(gda.Salt, tolerance);

            return spec;
        }
    }

    /// <summary>
    /// Represents a user profile.
    /// </summary>
    public class UserProfile
    {
        // Serializable/Deserializable values
        public string Name { get; set; }
        public string ImageLocation { get; set; }
        public DietSpecification DietSpecifications { get; set; }
        public Gender Gender { get; set; }
        public double ImageRotation { get; set; }

        [JsonIgnore]
        private BitmapImage image;

        [JsonIgnore]
        public BitmapImage Image { get { return image; } }
        
        public UserProfile()
        {

        }

        /// <summary>
        /// Creates a new user profile, and initializes dietary specifications with GDA for the given gender.
        /// </summary>
        /// <param name="name">Name of the profile.</param>
        /// <param name="gender">Gender of the profile.</param>
        private UserProfile(string name, Gender gender, string imageURI)
        {
            Name = name;
            Gender = gender;
            ImageLocation = imageURI;
            DietSpecifications = DietSpecification.createGDA(gender, 5f);
        }

        /// <summary>
        /// Create a new profile from data received from new profile page.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="tempImageLocation"></param>
        /// <returns></returns>
        public static UserProfile Create(string name, Gender gender, string tempImageLocation)
        {
            // Create folder structure
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string fileName = name + ".json";

                if (!storage.DirectoryExists(@"profiles\" + name))
                    storage.CreateDirectory(@"profiles\" + name);

                // Handle profile picture...

                // Retrieve temporary image
                BitmapImage image = new BitmapImage();
                using(IsolatedStorageFileStream stream = storage.OpenFile(tempImageLocation, FileMode.Open, FileAccess.Read))
                {
                    image.SetSource(stream);
                    stream.Close();
                }

                // Save to a permanent location
                using(IsolatedStorageFileStream stream = storage.CreateFile(@"profiles\" + name + @"\" + name + "_picture.jpg"))
                {
                    WriteableBitmap writable = new WriteableBitmap(image);
                    writable.SaveJpeg(stream, writable.PixelWidth, writable.PixelHeight, 0, 90);
                    stream.Close();
                }


                string[] f = storage.GetFileNames(@"profiles\" + name + @"\*");
                int i = 0;
            }


            UserProfile profile = new UserProfile(name, gender, @"profiles\" + name + @"\" + name + "_picture.jpg");
            profile.Save();
            profile.LoadImage();
            return profile;
        }

        public void Save()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings() { ContractResolver = new PrivateMembersContractResolver() };
            string output = JsonConvert.SerializeObject(this, settings);

            // Save out data
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string fileName = Name + ".json";

                try
                {
                    StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream(@"profiles\" + Name + @"\" + fileName, FileMode.CreateNew, storage));
                    writer.Write(output);
                    writer.Close();
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error saving profile data: " + e.Message, "I/O Error", MessageBoxButton.OK);
                }
            }
        }

        public void LoadImage()
        {
            using(IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    using(IsolatedStorageFileStream stream = storage.OpenFile(ImageLocation, FileMode.Open, FileAccess.Read))
                    {
                        image = new BitmapImage();
                        image.SetSource(stream);
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("Error, failed to load profile image for profile " + Name + ":" + e.Message, "I/O Error", MessageBoxButton.OK);
                }
            }
        }
    }
}
