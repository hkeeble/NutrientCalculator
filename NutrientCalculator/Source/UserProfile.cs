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

using FatSecretAPI;
using Assignment;
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
            if (gender == Gender.MALE)
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
            spec.Calories = new Range(gda.Calories, tolerance, tolerance);
            spec.Protein = new Range(gda.Protein, tolerance, tolerance);
            spec.Carbohydrate = new Range(gda.Carbohydrate, tolerance, tolerance);
            spec.Sugars = new Range(gda.Sugars, tolerance, tolerance);
            spec.Fat = new Range(gda.Fat, tolerance, tolerance);
            spec.Saturates = new Range(gda.Saturates, tolerance, tolerance);
            spec.Fibre = new Range(gda.Fibre, tolerance, tolerance);
            spec.Salt = new Range(gda.Salt, tolerance, tolerance);

            return spec;
        }
    }

    /// <summary>
    /// Represents a user profile.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class UserProfile
    {
        [JsonProperty]
        private string name;

        [JsonProperty]
        private string profileDirectory;

        [JsonProperty]
        private string imageLocation;

        [JsonProperty]
        private DietSpecification dietSpecifications;

        [JsonProperty]
        private Gender gender;

        [JsonProperty, JsonConverter(typeof(TypeConverter<double>))]
        private double imageRotation;

        [JsonProperty]
        private DailyTracker currentDayTracker;
        
        private List<DailyTracker> weeklyData;
        private List<CustomRecipe> recipes;

        // Public accessors
        public string Name                          { get { return name; } }
        public string ProfileDirectory              { get { return profileDirectory; } }
        public string ImageLocation                 { get { return imageLocation; } }
        public DietSpecification DietSpecifications { get { return dietSpecifications; } }
        public Gender Gender                        { get { return gender; } }
        public double ImageRotation                 { get { return imageRotation; } }
        public DailyTracker CurrentDayTracker       { get { return currentDayTracker; } }
        public List<CustomRecipe> Recipes           { get { return recipes; } }
        public List<DailyTracker> WeeklyTrackerData { get { return weeklyData; } }

        private BitmapImage image;

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
            this.name = name;
            this.gender = gender;
            this.imageLocation = imageURI;
            this.dietSpecifications = DietSpecification.createGDA(gender, 5f);

            weeklyData = new List<DailyTracker>();
            recipes = new List<CustomRecipe>();
            currentDayTracker = new DailyTracker();
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
            LoadingBar.Show("Creating new profile...");

            // Create folder structure
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string fileName = name + ".json";

                if (!storage.DirectoryExists(@"profiles\" + name))
                    storage.CreateDirectory(@"profiles\" + name);

                // Handle profile picture...

                // Retrieve temporary image
                BitmapImage image = new BitmapImage();
                using (IsolatedStorageFileStream stream = storage.OpenFile(tempImageLocation, FileMode.Open, FileAccess.Read))
                {
                    image.SetSource(stream);
                    stream.Close();
                }

                // Save to a permanent location
                using (IsolatedStorageFileStream stream = storage.CreateFile(@"profiles\" + name + @"\" + name + "_picture.jpg"))
                {
                    WriteableBitmap writable = new WriteableBitmap(image);
                    writable.SaveJpeg(stream, writable.PixelWidth, writable.PixelHeight, 0, 90);
                    stream.Close();
                }


                string[] f = storage.GetFileNames(@"profiles\" + name + @"\*");
                int i = 0;
            }


            UserProfile profile = new UserProfile(name, gender, @"profiles\" + name + @"\" + name + "_picture.jpg");
            profile.profileDirectory = @"profiles\" + name + @"\";
            profile.Save();
            profile.LoadImage();

            LoadingBar.Hide();

            return profile;
        }

        /// <summary>
        /// Loads all data not stored in the profile's JSON file (recipes and tracker files).
        /// </summary>
        public void LoadData()
        {
            LoadingBar.Show("Loading Profile Data...");

            recipes = new List<CustomRecipe>();
            weeklyData = new List<DailyTracker>();

            // Load existing recipes
            using(IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!storage.DirectoryExists(ProfileDirectory + @"\recipes\"))
                    storage.CreateDirectory(ProfileDirectory + @"\recipes\");

                string[] recipeNames = storage.GetFileNames(ProfileDirectory + @"recipes\*");
                foreach (string recipeName in recipeNames)
                {
                    using (StreamReader reader = new StreamReader(storage.OpenFile(ProfileDirectory + @"recipes\" + recipeName, FileMode.Open)))
                    {
                        string data = reader.ReadToEnd();
                        CustomRecipe loadedRecipe = JsonConvert.DeserializeObject<CustomRecipe>(data);
                        recipes.Add(loadedRecipe);
                        reader.Close();
                    }
                }
            }

            // Load weekly data

            LoadingBar.Hide();
        }

        /// <summary>
        /// Update all data according to the current time.
        /// </summary>
        /// <param name="currentTime">The current time.</param>
        public void UpdateTime(DateTime currentTime)
        {
            if(currentDayTracker.EndTime <= currentTime)
            {
                if(weeklyData.Count > 6)
                    weeklyData.RemoveAt(0);
                weeklyData.Add(currentDayTracker);
                currentDayTracker = new DailyTracker();
            }
        }

        public void Save()
        {
            LoadingBar.Show("Saving Data...");

            JsonSerializerSettings settings = new JsonSerializerSettings() { ContractResolver = new PrivateMembersContractResolver() };
            string output = JsonConvert.SerializeObject(this, settings);

            // Save out data
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string fileName = Name + ".json";

                try
                {
                    // Main data
                    StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream(ProfileDirectory + fileName, FileMode.Create, storage));
                    writer.Write(output);
                    writer.Close();

                    // Tracking data
                    writer = new StreamWriter(storage.OpenFile(ProfileDirectory + "todayData.json", FileMode.Create));
                    string data = JsonConvert.SerializeObject(currentDayTracker);
                    writer.Write(data);
                    writer.Close();

                    // Delete existing tracking data
                    if(storage.DirectoryExists(ProfileDirectory + "weeklyData"))
                    {
                        string[] files = storage.GetFileNames(ProfileDirectory + "weeklyData\\*");
                        for(int i = 0; i < files.Length; i++)
                            storage.DeleteFile(ProfileDirectory + "weeklyData\\" + files[i]);
                    }
                    else
                        storage.CreateDirectory(ProfileDirectory + "weeklyData");

                    for(int i = 0; i < weeklyData.Count; i++)
                    {
                        writer = new StreamWriter(storage.OpenFile(ProfileDirectory + "weeklyData\\" + weeklyData[i].StartTime.DayOfWeek.ToString() + ".json", FileMode.Create));
                        writer.Write(JsonConvert.SerializeObject(weeklyData[i]));
                        writer.Close();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error saving profile data: " + e.Message, "I/O Error", MessageBoxButton.OK);
                }
            }

            LoadingBar.Hide();
        }

        public void LoadImage()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    using (IsolatedStorageFileStream stream = storage.OpenFile(ImageLocation, FileMode.Open, FileAccess.Read))
                    {
                        image = new BitmapImage();
                        image.SetSource(stream);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error, failed to load profile image for profile " + Name + ":" + e.Message, "I/O Error", MessageBoxButton.OK);
                }
            }
        }

        public bool RecipeExists(string name)
        {
            bool exists = false;
            foreach (CustomRecipe existingRecipe in recipes)
            {
                if (existingRecipe.Name == name)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        /// <summary>
        /// Save the given recipe to this user's user directory.
        /// </summary>
        /// <param name="recipe">The recipe to save.</param>
        public void SaveRecipe(CustomRecipe recipe)
        {
            LoadingBar.Show("Saving Recipe...");

            // Check for existing...
            int index = 0;
            bool exists = false;
            for(int i = 0; i < recipes.Count; i++)
            {
                if (recipes[i].Name == recipe.Name)
                {
                    index = i;
                    exists = true;
                }
            }

            // If the recipe exists, modify it, otherwise add a new recipe
            if (exists)
                recipes[index] = recipe;
            else
                recipes.Add(recipe);

            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                JsonSerializerSettings settings = new JsonSerializerSettings() { ContractResolver = new PrivateMembersContractResolver() };
                string data = JsonConvert.SerializeObject(recipe, settings);

                try
                {
                    if (!storage.DirectoryExists(ProfileDirectory + @"\recipes\"))
                        storage.CreateDirectory(ProfileDirectory + @"\recipes\");

                    using (StreamWriter stream = new StreamWriter(storage.OpenFile(ProfileDirectory + @"\recipes\" + recipe.Name + ".json", FileMode.Create)))
                    {
                        stream.Write(data);
                        stream.Close();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error, failed to save recipe " + recipe.Name + ":" + e.Message, "I/O Error", MessageBoxButton.OK);
                }
            }

            LoadingBar.Hide();
        }

        public bool DeleteRecipe(string recipeName)
        {
            bool success = false;
            for (int i = 0; i < recipes.Count; i++)
            {
                if (recipes[i].Name == recipeName)
                {
                    success = true;
                    
                    // Delete the file in storage
                    using(IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        try
                        {
                            storage.DeleteFile(ProfileDirectory + @"recipes\" + recipeName + ".json");
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show("Error, failed to delete recipe " + recipeName + ": " + e.Message, "I/O Error", MessageBoxButton.OK);
                        }
                    }
                    
                    recipes.RemoveAt(i);
                    break;
                }
            }

            return success;
        }
    }
}
