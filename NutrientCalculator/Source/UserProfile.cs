using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    enum Gender
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

    struct Range
    {
        private double min, max;

        double Minimum { get { return min; } }
        double Maximum { get { return max; } }

        public Range(double minimum, double maximum)
        {
            this.min = minimum;
            this.max = maximum;
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
    class DietSpecification
    {
        public Range Calories       { get; set; }
        public Range Protein        { get; set; }
        public Range Carbohydrate   { get; set; }
        public Range Sugars         { get; set; }
        public Range Fat            { get; set; }
        public Range Saturates      { get; set; }
        public Range Fibre          { get; set; }
        public Range Salt           { get; set; }

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
    class UserProfile
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public DietSpecification DietSpecifications { get; set; }
        public Gender Gender { get; set; }

        /// <summary>
        /// Creates a new user profile, and initializes dietary specifications with GDA for the given gender.
        /// </summary>
        /// <param name="name">Name of the profile.</param>
        /// <param name="gender">Gender of the profile.</param>
        public UserProfile(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
            DietSpecifications = DietSpecification.createGDA(gender, 5f);
        }
    }
}
